using PrescriptionManagement.cls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;

namespace PrescriptionManagement.view
{
    public partial class uc_MatchingPrescription : UserControl
    {

        clsService clsService = new clsService();
        clsconvertdate clsconvert = new clsconvertdate();
        public uc_MatchingPrescription()
        {
            InitializeComponent();
        }

        private void pic_clear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void uc_MatchingPrescription_Load(object sender, EventArgs e)
        {
            txt_BarcodePrescr.Focus();
        }
        public void clear()
        {
            txt_BarcodePrescr.Text = "";
            //dg_FinishMatching.DataSource = new DataTable();
            //dg_waitMatching.DataSource = new DataTable();
            //lbstatus.Text = "-";
            lbAN.Text = "";
            lbname.Text = "";
            lbPres.Text = "";
            dg_FinishMatching.Rows.Clear();
            txt_BarcodePrescr.Focus();
            txt_BarcodePrescr.Enabled = true;
            lbbasketno.Text = "-";
            lbhn.Text = "-";
           
        }

        private void btn_UnselectAll_print_Click(object sender, EventArgs e)
        {
            //Check For Print
            int totalDtgM = dg_FinishMatching.Rows.Count;
            for (int i = 0; i < totalDtgM; i++)
            {
                dg_FinishMatching.Rows[i].Cells["chkFin"].Value = false;
            }
        }

        private void btn_selectAll_print_Click(object sender, EventArgs e)
        {
            //Check For Print
            int totalDtgM = dg_FinishMatching.Rows.Count;
            for (int i = 0; i < totalDtgM; i++)
            {
                dg_FinishMatching.Rows[i].Cells["chkFin"].Value = true;
            }
        }
        public Bitmap GetBitmapFromUrl(string imageUrl)
        {
            WebRequest request = WebRequest.Create(imageUrl);
            using (WebResponse response = request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    return new Bitmap(stream);
                }
            }
        }

        private async void txt_BarcodePrescr_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                string labs = "";                
                DateTime _dtstr = DateTime.Now;
                DateTime _dtend = DateTime.Now;
                _dtstr = _dtstr.AddDays(-1);

                string dtstr = clsconvert.convert_en(_dtstr.ToString());
                string dtend = clsconvert.convert_en(_dtend.ToString());

                //dtstr = "2025-05-07";
                //dtend = "2025-05-08";

                await clsService.RequestdetailAll(dtstr, dtend, txt_BarcodePrescr.Text.ToString());

                if (clsPackagemaster.db_drug.Rows.Count > 0 && clsPackagemaster.db_data.Rows.Count > 0 && clsPackagemaster.db_packagemaster.Rows.Count > 0)
                {
                    txtbarcode.Focus();
                    DataTable db_packagemaster = new DataTable();
                    DataTable db_data = new DataTable();
                    DataTable db_drug = new DataTable();
                    db_packagemaster = clsPackagemaster.db_packagemaster.Select().CopyToDataTable(); // Copy to a new DataTable
                    db_data = clsPackagemaster.db_data.Select().CopyToDataTable(); // Copy to a new DataTable
                    db_drug = clsPackagemaster.db_drug.Select().CopyToDataTable(); // Copy to a new DataTable

                    if (db_packagemaster.Rows.Count > 0 && db_data.Rows.Count > 0 && db_drug.Rows.Count > 0)
                    {
                        if(db_packagemaster.Rows[0]["matchinguserid"].ToString() == "" && db_packagemaster.Rows[0]["matchingdatetime"].ToString() == "")
                        {
                            if (db_packagemaster.Columns.Contains("basketno"))
                            {
                                lbbasketno.Text = db_packagemaster.Rows[0]["basketno"].ToString();
                            }
                            else
                            {
                                lbbasketno.Text = "-";
                            }

                            lbPres.Text = db_data.Rows[0]["prescriptionno"].ToString();
                            lbAN.Text = db_data.Rows[0]["an"].ToString();
                            lbhn.Text = db_data.Rows[0]["hn"].ToString();
                            lbname.Text = db_data.Rows[0]["patientname"].ToString();
                            txt_BarcodePrescr.Text.ToUpper();
                            #region dgv

                            // เพิ่มข้อมูลตัวอย่าง
                            foreach (DataRow i in db_drug.Rows)
                            {
                                string drugcode = "";
                                drugcode = i["orderitemcode"].ToString();
                                
                                Bitmap bitmap = GetBitmapFromUrl(PrescriptionManagement.Properties.Settings.Default.drugpicUrl + drugcode + "/1");

                                //dg_waitMatching.Rows.Add(i["orderitemcode"].ToString(), i["orderitemname"].ToString(), i["orderqty"].ToString(), bitmap);
                                //dg_waitMatching.Rows[i].Cells("imgColumn").Value = bitmap;

                            }

                            #endregion

                            dg_FinishMatching.Rows.Clear();
                            string columnName = "jobusername";
                            string jobusername = "";
                            string zone = "";
                            bool disfin = true;
                            // เพิ่มข้อมูลตัวอย่าง
                            foreach (DataRow i in db_packagemaster.Rows)
                            {

                                if (db_packagemaster.Columns.Contains(columnName))
                                {
                                    Console.WriteLine($"Column '{columnName}' exists.");
                                    jobusername = i["jobusername"].ToString();
                                }
                                else
                                {
                                    Console.WriteLine($"Column '{columnName}' does not exist.");
                                    jobusername = "";
                                    zone += " " + i["shelfzone"].ToString() + " ";
                                    disfin = false;
                                }
                                

                                if (i["ordertype"].ToString() != "H")
                                {
                                    if (i["shelfzone"].ToString() != "FLOORSTOCK")
                                    {
                                        dg_FinishMatching.Rows.Add(false, i["orderitembarcode"].ToString(), i["matchingdatetime"].ToString(), i["orderitemcode"].ToString(),
                                           i["orderitemname"].ToString(),
                                           i["dosage"].ToString(),
                                           i["orderunitdesc"].ToString(),
                                           i["shelfzone"].ToString() + " " + i["shelfname"].ToString(),
                                           jobusername);
                                    }
                                   
                                }
                                else
                                {
                                    if (i["shelfzone"].ToString() != "FLOORSTOCK")
                                    {
                                        dg_FinishMatching.Rows.Add(false, i["orderitembarcode"].ToString(), i["matchingdatetime"].ToString(), i["orderitemcode"].ToString(),
                                           i["orderitemname"].ToString(),
                                           i["orderqty"].ToString(),
                                           i["orderunitdesc"].ToString(),
                                           i["shelfzone"].ToString() + " " + i["shelfname"].ToString(),
                                           jobusername);
                                    }
                                    
                                }

                               

                                // รีเฟรช DataGridView
                            }


                        }
                        else
                        {
                            frmAlarm alr = new frmAlarm(" Matching แล้ว ");
                            alr.ShowDialog();
                        }
                        //dg_waitMatching.DataSource = db_packagemaster_first;

                    }
                    else
                    {
                        frmAlarm alr = new frmAlarm(" ไม่พบข้อมูลใบสั่งยา ");
                        alr.ShowDialog();
                    }

                }
                else
                {
                    frmAlarm alr = new frmAlarm(" ไม่พบข้อมูลใบสั่งยา ");
                    alr.ShowDialog();
                }

            }
        }
        public static string date_utc(string date_value)
        {
            string str_datenow = "";
            if (date_value != "")
            {
                DateTime utcNow = Convert.ToDateTime(date_value);
                str_datenow = utcNow.ToString("o");
                //DateTime utcDateTime = utcNow.ToUniversalTime();
                str_datenow = utcNow.ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
            }
            else
            {
                str_datenow = "";
            }

            return str_datenow;
        }

        private void txtbarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (dg_FinishMatching.Rows.Count > 0)
                {
                    string search = "";
                    search = txtbarcode.Text.Trim();
                    bool checkall = false;
                    bool found = false;
                    bool dispence = true;
                    int countitemSE = 0;
                    string drugitem_sub = "";
                    string name = "";
                    DataTable db_pic = new DataTable();
                    db_pic.Columns.Add("drugitem_sub", typeof(string));
                    db_pic.Columns.Add("name", typeof(string));

                    dg_FinishMatching.EnableHeadersVisualStyles = false; // ต้องปิดก่อน
                    db_pic.Clear();

                    foreach (DataGridViewRow row in dg_FinishMatching.Rows)
                    {
                        var cellValue = row.Cells["fin_barcode"].Value?.ToString().Trim();
                        if (cellValue == search)
                        {

                            if (row.Cells["Colusername"].Value?.ToString() != "")
                            {

                                dispence = true;
                                DateTime utcNow = DateTime.Now;
                                string str_datenow = date_utc(utcNow.ToString());

                                row.Cells["fin_matching"].Value = str_datenow;

                                found = true;
                                row.Selected = false;
                                row.DefaultCellStyle.BackColor = Color.FromArgb(130, 224, 170);
                                row.Cells["chk_select"].Value = true; // กรณีเจอ barcode ให้ติ๊ก


                                string s1 = row.Cells["fin_orderitemcode"].Value?.ToString();
                                string s2 = s1.IndexOf("^").ToString();
                                if (int.Parse(s2) > 0)
                                {
                                    drugitem_sub = s1.Substring(0, int.Parse(s2));
                                }
                                else
                                {
                                    drugitem_sub = s1;
                                }

                                name = row.Cells["fin_orderitemname"].Value.ToString();
                                if (db_pic.Rows.Count > 0)
                                {
                                    bool chpic = false;
                                    foreach (DataRow rwp in db_pic.Rows)
                                    {
                                        if (rwp["drugitem_sub"].ToString() != drugitem_sub)
                                        {
                                            chpic = true;
                                        }
                                    }
                                    if (chpic)
                                    {
                                        db_pic.Rows.Add(drugitem_sub, name);
                                    }

                                }
                                else
                                {
                                    db_pic.Rows.Add(drugitem_sub, name);
                                }
                            }
                            else
                            {
                                dispence = false;
                                //MessageBox.Show("ไม่พบข้อมูลที่ตรงกัน");
                                txtbarcode.Clear();
                                txtbarcode.Focus();
                                //MessageBox.Show(" ยังจัดยาไม่ครบ ");
                                DialogResult result = MessageBox.Show(
                                    "ไม่มีเจ้าหน้าที่จัดยา คุณต้องการดำเนินการต่อหรือไม่?",      // ข้อความในกล่อง
                                    "ยืนยันการดำเนินการ",                    // หัวข้อ
                                    MessageBoxButtons.YesNo,                   // ปุ่ม Yes และ No
                                    MessageBoxIcon.Question                    // ไอคอนเครื่องหมายคำถาม
                                );

                                if (result == DialogResult.Yes)
                                {
                                    // ผู้ใช้กด Yes
                                    foreach (DataGridViewRow roww in dg_FinishMatching.Rows)
                                    {
                                        cellValue = roww.Cells["fin_barcode"].Value?.ToString().Trim();
                                        if (cellValue == search)
                                        {
                                            DateTime utcNow = DateTime.Now;
                                            string str_datenow = date_utc(utcNow.ToString());

                                            row.Cells["fin_matching"].Value = str_datenow;

                                            found = true;
                                            row.Selected = false;
                                            row.DefaultCellStyle.BackColor = Color.FromArgb(130, 224, 170);
                                            row.Cells["chk_select"].Value = true; // กรณีเจอ barcode ให้ติ๊ก

                                            string s1 = row.Cells["fin_orderitemcode"].Value?.ToString();
                                            string s2 = s1.IndexOf("^").ToString();
                                            if (int.Parse(s2) > 0)
                                            {
                                                drugitem_sub = s1.Substring(0, int.Parse(s2));
                                            }
                                            else
                                            {
                                                drugitem_sub = s1;
                                            }

                                            name = row.Cells["fin_orderitemname"].Value.ToString();
                                            bool chpic = false;
                                            if (db_pic.Rows.Count > 0)
                                            {

                                                foreach (DataRow rwp in db_pic.Rows)
                                                {
                                                    if (rwp["drugitem_sub"].ToString() != drugitem_sub)
                                                    {
                                                        chpic = true;
                                                    }
                                                }
                                                if (chpic)
                                                {
                                                    db_pic.Rows.Add(drugitem_sub, name);
                                                }

                                            }
                                            else
                                            {
                                                db_pic.Rows.Add(drugitem_sub, name);
                                            }


                                        }
                                        //dg_FinishMatching_Click(dg_FinishMatching, EventArgs.Empty);
                                    }

                                }
                                else
                                {
                                    // ผู้ใช้กด No

                                }
                            }



                        }

                        //dg_FinishMatching_Click(dg_FinishMatching, EventArgs.Empty);
                    }

                    if (found)
                    {
                        txtbarcode.Clear();
                        txtbarcode.Focus();

                        if (db_pic.Rows.Count > 0)
                        {
                            db_pic = db_pic.AsEnumerable().GroupBy(r => r.Field<string>("drugitem_sub")).Select(g => g.First()).CopyToDataTable();

                            foreach (DataRow rwp in db_pic.Rows)
                            {
                                frmShowpic frmpic = new frmShowpic(rwp["drugitem_sub"].ToString(), rwp["name"].ToString());
                                frmpic.ShowDialog();
                                if (frmpic.result)
                                {
                                    frmpic.Close();
                                }
                            }
                        }

                    }
                    else
                    {
                        txtbarcode.Clear();
                        txtbarcode.Focus();
                        //MessageBox.Show("ไม่พบข้อมูลที่ตรงกัน");
                    }





                    foreach (DataGridViewRow row in dg_FinishMatching.Rows)
                    {
                        if ((bool)row.Cells["chk_select"].Value)
                        {
                            checkall = true;
                        }
                        else
                        {
                            checkall = false;
                            break;
                        }
                    }
                    if (checkall)
                    {
                        //await clsService.RequestdetailAll(dtstr, dtend, txt_BarcodePrescr.Text.ToString());

                        update_matching();
                        dg_FinishMatching.Refresh();
                    }
                }
                else if (dg_FinishMatching.Rows.Count > 0)
                {

                }


            }

        }
        public async void update_matching()
        {
            DataTable dtprint = new DataTable();
            DataTable dbUser = new DataTable();
            bool result;
            bool checkall = false;
           

            foreach (DataGridViewRow row in dg_FinishMatching.Rows)
            {
                if ((bool)row.Cells["chk_select"].Value)
                {
                    checkall = true;
                }
                else
                {
                    checkall = false;
                    break;
                }
            }
            if (checkall)
            {
                //lbusername.Text = dbUser.Rows[0]["firstname"].ToString() + "  " + dbUser.Rows[0]["lastname"].ToString();

                if (txt_BarcodePrescr.Text != "")
                {
                    DateTime _dtstr = DateTime.Now;
                    DateTime _dtend = DateTime.Now;
                    _dtstr = _dtstr.AddDays(-1);
                    string dtstr = clsconvert.convert_en(_dtstr.ToString());
                    string dtend = clsconvert.convert_en(_dtend.ToString());
                    //dtstr = "2025-05-07";
                    //dtend = "2025-05-08";

                    await clsService.RequestdetailAll(dtstr, dtend, txt_BarcodePrescr.Text.ToString());

                    List<object> ListJsonMatching = new List<object>();

                    ListJsonMatching = GenJson_matching();
                    if (ListJsonMatching.Count > 0)
                    {
                        result = await clsService.update_matching(ListJsonMatching);
                        if (result)
                        {
                            List<object> ListJsonMatching_rx = new List<object>();

                            ListJsonMatching_rx = GenJson_matching_rx();
                            result = await clsService.update_rx(ListJsonMatching_rx);
                            if (result)
                            {
                                lbstatus.Text = " Matching complete ";
                                lbstatus.ForeColor = Color.ForestGreen;
                                clsPackagemaster.db_data = new DataTable();
                                clsPackagemaster.db_drug = new DataTable();
                                clsPackagemaster.db_packagemaster = new DataTable();
                                lbbasketno.Text = "-";
                                clear();
                            }

                        }
                        else
                        {
                            lbstatus.Text = " Matching Not complete !!! ";
                            lbstatus.ForeColor = Color.Red;
                            clsPackagemaster.db_data = new DataTable();
                            clsPackagemaster.db_drug = new DataTable();
                            clsPackagemaster.db_packagemaster = new DataTable();
                        }
                    }


                }
            }
            else
            {
                MessageBox.Show(" ตรวจสอบรายการยาไม่ครบถ้วน กรุณาตรวจสอบอีกครั้ง");
                txtbarcode.Focus();
            }

        }

        public List<object> GenJson_matching()
        {
            List<object> listpackagemaster = new List<object>();
            List<object> listJson = new List<object>();

            try
            {
                if (clsPackagemaster.db_packagemaster.Rows.Count > 0 && clsPackagemaster.db_data.Rows.Count > 0 && clsPackagemaster.db_drug.Rows.Count > 0)
                {
                    DateTime utcNow = DateTime.Now;
                    DateTime utcNow_checkout = DateTime.Now;
                    List<object> listpack = new List<object>();
                    string str_datenow = "";
                    foreach (DataRow rw in clsPackagemaster.db_packagemaster.Rows)
                    {
                        foreach (DataGridViewRow dg_rw in dg_FinishMatching.Rows)
                        {
                            if (dg_rw.Cells["fin_orderitemcode"].Value?.ToString() == rw["orderitemcode"].ToString())
                            {
                                if (!string.IsNullOrEmpty(dg_rw.Cells["fin_orderitemcode"].Value?.ToString()))
                                {
                                    str_datenow = dg_rw.Cells["fin_matching"].Value.ToString();
                                }
                                else
                                {
                                    str_datenow = utcNow.ToString();
                                }

                                break;
                            }
                        }


                        string str_datenow_checkout = date_utc(utcNow_checkout.ToString());
                        listpackagemaster.Add(new
                        {
                            _id = rw["_id"].ToString(),
                            //// ข้อมูลที่อัพเดท
                            //checkoutdatetime = (str_datenow_checkout.ToString().Length > 0) ? Convert.ToDateTime(str_datenow_checkout.ToString()).AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture).ToString() : "",
                            //checkoutuserid = cls.clsuser.userid.Trim(),
                            //checkoutusername = cls.clsuser.name.Trim(),
                            // เวลา matching 
                            matchingdatetime = (str_datenow.ToString().Length > 0) ? Convert.ToDateTime(str_datenow.ToString()).AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture).ToString() : "",
                            matchinguserid = cls.clsuser.userid.Trim()
                        });

                    }
                }
                return listpackagemaster;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return listpackagemaster;
            }
            finally
            {

            }
        }
        public List<object> GenJson_matching_rx()
        {
            List<object> listpackagemaster = new List<object>();
            List<object> listJson = new List<object>();

            try
            {
                if (clsPackagemaster.db_packagemaster.Rows.Count > 0 && clsPackagemaster.db_data.Rows.Count > 0 && clsPackagemaster.db_drug.Rows.Count > 0)
                {
                    DateTime utcNow = DateTime.Now;
                    DateTime utcNow_checkout = DateTime.Now;
                    List<object> listpack = new List<object>();
                    string str_datenow = "";
                    foreach (DataRow rw in clsPackagemaster.db_data.Rows)
                    {
                        string str_datenow_checkout = date_utc(utcNow_checkout.ToString());
                        listpackagemaster.Add(new
                        {
                            _id = rw["_id"].ToString(),
                            //// ข้อมูลที่อัพเดท
                            matchingdatetime = (str_datenow_checkout.ToString().Length > 0) ? Convert.ToDateTime(str_datenow_checkout.ToString()).AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture).ToString() : "",

                        });

                    }
                }
                return listpackagemaster;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return listpackagemaster;
            }
            finally
            {

            }
        }

    }
}
