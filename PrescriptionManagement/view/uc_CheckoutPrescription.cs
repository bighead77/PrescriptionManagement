using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    
    public partial class uc_CheckoutPrescription : UserControl
    {
        clsService clsService = new clsService();
        clsconvertdate clsconvert = new clsconvertdate();
        string drugallergies_his = "";
        string drugallergies_ministry = "";
        private Dictionary<string, Image> _drugImageCache = new Dictionary<string, Image>();

        public uc_CheckoutPrescription()
        {
            InitializeComponent();
            
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F5)
            {
                clear();
                lbstatus.Text = "";
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void uc_CheckoutPrescription_Load(object sender, EventArgs e)
        {
            txt_BarcodePrescr.Focus();
          
        }
        private void EnableDoubleBuffer(DataGridView dgv)
        {
            typeof(DataGridView)
                .GetProperty("DoubleBuffered",
                    System.Reflection.BindingFlags.NonPublic |
                    System.Reflection.BindingFlags.Instance)
                ?.SetValue(dgv, true, null);
        }
        private void HighlightRows()
        {
            foreach (DataGridViewRow row in dg_waitMatching.Rows)
            {
                if (row.Cells["voiddatetime"]?.Value?.ToString() == "")
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(0, 0, 0);
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(200, 0, 0);
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
            }

            foreach (DataGridViewRow row in dg_FinishMatching.Rows)
            {
                if (row.Cells["voiddate"]?.Value?.ToString() == "")
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(0, 0, 0);
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(200, 0, 0);
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }

        private void SetupWaitMatchingGrid()
        {
            // ===== dg_waitMatching =====
            dg_waitMatching.AllowUserToAddRows = false;
            dg_waitMatching.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dg_waitMatching.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dg_waitMatching.RowTemplate.Height = 70;
            dg_waitMatching.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //dg_waitMatching.BackgroundColor = Color.FromArgb(0, 0, 0);

            dg_waitMatching.Columns["instuction"].DefaultCellStyle.WrapMode =
                DataGridViewTriState.True;
            dg_waitMatching.Columns["orderitemname"].DefaultCellStyle.WrapMode =
                DataGridViewTriState.True;

            //var imgCol = (DataGridViewImageColumn)dg_waitMatching.Columns["Colpic"];
            //imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
            //imgCol.Width = 100;

            EnableDoubleBuffer(dg_waitMatching);

            // ===== dg_FinishMatching =====
            dg_FinishMatching.AllowUserToAddRows = false;
            dg_FinishMatching.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dg_FinishMatching.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dg_FinishMatching.RowTemplate.Height = 70;
            dg_FinishMatching.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dg_FinishMatching.Columns["fin_orderitemname"].DefaultCellStyle.WrapMode =
                DataGridViewTriState.True;
            dg_FinishMatching.Columns["Colusername"].DefaultCellStyle.WrapMode =
                DataGridViewTriState.True;
            EnableDoubleBuffer(dg_FinishMatching);
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void pic_clear_Click(object sender, EventArgs e)
        {
            clear();
        }
        private Image LoadImageFromUrl(string url)
        {
            using (WebClient wc = new WebClient())
            {
                byte[] bytes = wc.DownloadData(url);
                using (var ms = new System.IO.MemoryStream(bytes))
                {
                    return Image.FromStream(ms);
                }
            }
        }
        
        private async void txt_BarcodePrescr_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                lbstatus.Text = "-";
                txtbarcode.Focus();
                string labs = "";
                lblabs.Text = "";
                drugallergies_his = "";
                drugallergies_ministry = "";
                DateTime _dtstr = DateTime.Now;
                DateTime _dtend = DateTime.Now;
                _dtstr = _dtstr.AddDays(-2);
                _dtend = _dtend.AddDays(+1);

                bool result_predx = false;
                bool ckP = false;
                string dtstr = clsconvert.convert_en(_dtstr.ToString());
                string dtend = clsconvert.convert_en(_dtend.ToString());
                await clsService.RequestdetailAll(dtstr, dtend, txt_BarcodePrescr.Text.ToString());

                if (clsPackagemaster.db_drug.Rows.Count > 0 && clsPackagemaster.db_data.Rows.Count > 0 && clsPackagemaster.db_packagemaster.Rows.Count > 0)
                {                    
                    foreach(DataRow roP in clsPackagemaster.db_packagemaster.Rows)
                    {
                        if (roP["shelfzone"].ToString().Contains("PREDx"))
                        {
                            ckP = true;                            
                        }
                        else
                        {
                            ckP = false;                            
                        }
                    }
                }

                if(ckP)
                {
                    result_predx = await clsService.update_barcodePredx(txt_BarcodePrescr.Text.ToString());
                }
                else
                {
                    result_predx = true;
                }


                if (result_predx)
                {
                    result_predx = false;                   
                    
                    await clsService.RequestdetailAll(dtstr, dtend, txt_BarcodePrescr.Text.ToString());

                    if (clsPackagemaster.db_drug.Rows.Count > 0 && clsPackagemaster.db_data.Rows.Count > 0 && clsPackagemaster.db_packagemaster.Rows.Count > 0)
                    {
                        DataTable db_packagemaster = new DataTable();
                        DataTable db_data = new DataTable();
                        DataTable db_drug = new DataTable();
                        db_packagemaster = clsPackagemaster.db_packagemaster.Select().CopyToDataTable(); // Copy to a new DataTable
                        db_data = clsPackagemaster.db_data.Select().CopyToDataTable(); // Copy to a new DataTable
                        db_drug = clsPackagemaster.db_drug.Select().CopyToDataTable(); // Copy to a new DataTable
                        liststatus.Items.Clear();
                        if (db_packagemaster.Rows.Count > 0 && db_data.Rows.Count > 0 && db_drug.Rows.Count > 0)
                        {
                            if (db_data.Rows[0]["voiddatetime"].ToString() == "" )
                            {
                                if (db_data.Rows[0]["checkoutdatetime"].ToString() == "")
                                {

                                    DataTable db_Status = await clsService.RequestStatusAN(db_data.Rows[0]["an"].ToString());

                                    if (db_Status.Rows.Count > 0)
                                    {
                                        DataTable status = new DataTable();

                                        //status.Columns.Add("_id", typeof(string));
                                        status.Columns.Add("prescriptionno", typeof(string));
                                        status.Columns.Add("PresSatus", typeof(string));

                                        foreach (DataRow rs in db_Status.Rows)
                                        {

                                            DataRow newRow = status.NewRow();
                                            string PresSatus = "";
                                            //เช็คสถานะใบ
                                            if (rs["medtransferdatetime"].ToString() == "")
                                            {
                                                //ยังไม่ผ่านการจ่าย

                                                if (rs["checkoutdatetime"].ToString() == "")
                                                {
                                                    if (rs["jobdatetime"].ToString() == "")
                                                    {
                                                        PresSatus = "กำลังจัดยา";
                                                    }
                                                    else
                                                    {
                                                        PresSatus = "รอตรวจสอบยา";
                                                    }
                                                }
                                                else
                                                {
                                                    PresSatus = "รอจ่ายยา";
                                                }

                                                status.Rows.Add(rs["prescriptionno"].ToString(), PresSatus);

                                                if (liststatus.ToString() == "")
                                                {
                                                    liststatus.Items.Add(rs["prescriptionno"].ToString() + " : " + PresSatus);
                                                }
                                                else
                                                {
                                                    liststatus.Items.Add(Environment.NewLine + rs["prescriptionno"].ToString() + " : " + PresSatus);
                                                }



                                            }
                                        }

                                    }

                                    //dg_waitMatching.DataSource = db_packagemaster_first;
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
                                    dg_waitMatching.Rows.Clear();
                                    dg_waitMatching.Refresh();
                                    // เพิ่มข้อมูลตัวอย่าง
                                    foreach (DataRow i in db_drug.Rows)
                                    {
                                       
                                        string drugcode = "";
                                        drugcode = i["orderitemcode"].ToString();

                                        DataTable dtAN = new DataTable();
                                        DataRow[] rows = db_packagemaster.Select("orderitemcode = '" + drugcode + "'");
                                        dtAN = rows.CopyToDataTable();

                                        Bitmap bitmap = GetBitmapFromUrl(PrescriptionManagement.Properties.Settings.Default.drugpicUrl + drugcode + "/8");

                                        string instustion = "";
                                        instustion = i["instructiondesc"].ToString() + i["timedesc"].ToString();

                                        dg_waitMatching.Rows.Add(i["_id"].ToString(), 
                                            i["orderitemcode"].ToString(), 
                                            i["orderitemname"].ToString(), 
                                            i["orderqty"].ToString(),
                                            i["orderunitdesc"].ToString(), 
                                            instustion, bitmap, 
                                            "ยกเลิก", 
                                            "MedError", 
                                            i["voiddatetime"].ToString(),
                                            dtAN.Rows[0]["shelfzone"].ToString(),
                                            dtAN.Rows[0]["shelfname"].ToString());

                                        //dg_waitMatching.Rows[i].Cells("imgColumn").Value = bitmap;
                                    }

                                    foreach (DataGridViewRow row in dg_waitMatching.Rows)
                                    {
                                        if (row.Cells["voiddatetime"].Value != null && row.Cells["voiddatetime"].Value.ToString() != "")
                                        {
                                            row.DefaultCellStyle.BackColor = Color.FromArgb(200, 0, 0);
                                            row.DefaultCellStyle.ForeColor = Color.White; // ให้เห็นข้อความชัดขึ้น
                                        }
                                    }

                                    foreach (DataRow i in db_data.Rows)
                                    {
                                        if (i["consultdatetime"].ToString() != "")
                                        {
                                            cb_status1.Checked = true;
                                        }
                                        else
                                        {
                                            cb_status1.Checked = false;
                                        }

                                        if (i["medprogressdatetime"].ToString() != "")
                                        {
                                            cb_status0.Checked = true;
                                        }
                                        else
                                        {
                                            cb_status0.Checked = false;
                                        }
                                    }

                                    #endregion

                                    dg_FinishMatching.Rows.Clear();
                                    string columnName = "jobusername";
                                    string jobusername = "";
                                    string zone = "";
                                    string orderitembarcode = "";
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
                                        if (i["orderitembarcodecase"].ToString() != "")
                                        {
                                            string text = i["orderitembarcodecase"].ToString();
                                            JArray jarr = (JArray)i["orderitembarcodecase"]; // ดึง JArray ออกมา

                                            List<string> arBarcode = jarr.ToObject<List<string>>(); // แปลงเป็น List<string>

                                            // หรือถ้าต้องการ array
                                            string[] arrayBarcode = arBarcode.ToArray();
                                            if (arrayBarcode.Length > 1)
                                            {
                                                foreach (string code in arBarcode)
                                                {
                                                    if (orderitembarcode == "")
                                                    {
                                                        orderitembarcode += code; // จะพิมพ์ออกมา: 1911_0
                                                    }
                                                    else
                                                    {
                                                        orderitembarcode = orderitembarcode + "|" + code;
                                                    }

                                                }

                                            }
                                            else
                                            {
                                                orderitembarcode = arrayBarcode[0];
                                            }
                                        }
                                        else
                                        {
                                            orderitembarcode = i["orderitembarcode"].ToString();
                                        }

                                        if (i["voiddatetime"].ToString() == "")
                                        {

                                            if (i["ordertype"].ToString() != "H")
                                            {
                                                if (i["shelfzone"].ToString() != "FLOORSTOCK")
                                                {
                                                    if (i["shelfzone"].ToString().Contains("PREDx"))
                                                    {
                                                        dg_FinishMatching.Rows.Add(false, orderitembarcode, i["matchingdatetime"].ToString(), i["orderitemcode"].ToString(),
                                                         i["orderitemname"].ToString(),
                                                         i["dosage"].ToString(),
                                                         i["dosageunitcode"].ToString(),
                                                         i["shelfzone"].ToString() + " " + i["shelfname"].ToString(),
                                                         jobusername, i["voiddatetime"].ToString());
                                                    }
                                                    else
                                                    {
                                                        dg_FinishMatching.Rows.Add(false, orderitembarcode, i["matchingdatetime"].ToString(), i["orderitemcode"].ToString(),
                                                         i["orderitemname"].ToString(),
                                                         i["dosage"].ToString(),
                                                         i["dosageunitcode"].ToString(),
                                                         i["shelfzone"].ToString() + " " + i["shelfname"].ToString(),
                                                         jobusername, i["voiddatetime"].ToString());
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (i["shelfzone"].ToString() != "FLOORSTOCK")
                                                {
                                                    dg_FinishMatching.Rows.Add(false, orderitembarcode, i["matchingdatetime"].ToString(), i["orderitemcode"].ToString(),
                                                   i["orderitemname"].ToString(),
                                                   i["dosage"].ToString(),
                                                   i["dosageunitcode"].ToString(),
                                                   i["shelfzone"].ToString() + " " + i["shelfname"].ToString(),
                                                   jobusername, i["voiddatetime"].ToString());
                                                }
                                            }
                                        }

                                        orderitembarcode = "";
                                        foreach (DataGridViewRow row in dg_FinishMatching.Rows)
                                        {
                                            if (row.Cells["voiddate"].Value != null && row.Cells["voiddate"].Value.ToString() != "")
                                            {
                                                row.DefaultCellStyle.BackColor = Color.FromArgb(200, 0, 0);
                                                row.DefaultCellStyle.ForeColor = Color.White; // ให้เห็นข้อความชัดขึ้น
                                            }
                                        }
                                        // รีเฟรช DataGridView
                                    }

                                    if (clsPackagemaster.db_labs.Rows.Count > 0)
                                    {
                                        labs = "";
                                        foreach (DataRow row in clsPackagemaster.db_labs.Rows)
                                        {
                                            if (labs == "" && row["lab_name"].ToString() != "Creatinine")
                                            {
                                                labs += row["lab_name"].ToString() + " " + row["result"].ToString() + " (" + clsconvert.convert_thai(row["date_result"].ToString()) + ")";

                                            }
                                            else if (labs != "" && row["lab_name"].ToString() != "Creatinine")
                                            {
                                                labs += " || " + row["lab_name"].ToString() + " " + row["result"].ToString() + " (" + clsconvert.convert_thai(row["date_result"].ToString()) + ")";
                                            }
                                        }

                                        lblabs.Text = labs;
                                    }
                                    else
                                    {
                                        lblabs.Text = "-";
                                    }


                                    if (clsPackagemaster.db_drugallergies.Rows.Count > 0)
                                    {
                                        string his = "";
                                        string hispharmaco = "";
                                        lsdrugallergies_his.Items.Clear();
                                        lshispharmaco.Items.Clear();
                                        btndrugallergies.Visible = true;
                                        pic1.Visible = true;
                                        int i = 0;
                                        foreach (DataRow row in clsPackagemaster.db_drugallergies.Rows)
                                        {
                                            if (row["drugallergytype"].ToString() == "his")
                                            {
                                                drugallergies_his = "";

                                                drugallergies_his = "Genericname : " + row["genericname"].ToString();
                                                lsdrugallergies_his.Items.Add(drugallergies_his);
                                                his += drugallergies_his;
                                                drugallergies_his = Environment.NewLine + "adverbs : " + row["adverbs"].ToString();
                                                lsdrugallergies_his.Items.Add(drugallergies_his);
                                                his += drugallergies_his;
                                                drugallergies_his = Environment.NewLine + "memo : " + row["memo"].ToString();
                                                lsdrugallergies_his.Items.Add(drugallergies_his);
                                                his += drugallergies_his;
                                            }
                                            else
                                            {
                                                drugallergies_ministry = "";

                                                drugallergies_ministry = "Genericname : " + row["genericname"].ToString();
                                                lshispharmaco.Items.Add(drugallergies_ministry);
                                                hispharmaco += drugallergies_ministry;
                                                drugallergies_ministry = Environment.NewLine + "adverbs : " + row["adverbs"].ToString();
                                                lshispharmaco.Items.Add(drugallergies_ministry);
                                                hispharmaco += drugallergies_ministry;
                                                drugallergies_ministry = Environment.NewLine + "memo : " + row["memo"].ToString();
                                                lshispharmaco.Items.Add(drugallergies_ministry);
                                                hispharmaco += drugallergies_ministry;
                                            }
                                        }

                                        bool result;
                                        frmDrugallergies frm = new frmDrugallergies(clsPackagemaster.db_drugallergies);
                                        frm.ShowDialog();

                                        if (frm.result)
                                        {
                                            frm.Close();
                                            pic1.Visible = false;
                                            pic2.Visible = true;
                                            txtbarcode.Focus();
                                        }
                                        else
                                        {

                                        }

                                    }
                                    else
                                    {

                                    }

                                }
                                else
                                {
                                    DataTable db_Status = await clsService.RequestStatusAN(db_data.Rows[0]["an"].ToString());

                                    if (db_Status.Rows.Count > 0)
                                    {
                                        DataTable status = new DataTable();

                                        //status.Columns.Add("_id", typeof(string));
                                        status.Columns.Add("prescriptionno", typeof(string));
                                        status.Columns.Add("PresSatus", typeof(string));

                                        foreach (DataRow rs in db_Status.Rows)
                                        {

                                            DataRow newRow = status.NewRow();
                                            string PresSatus = "";
                                            //เช็คสถานะใบ
                                            if (rs["medtransferdatetime"].ToString() == "")
                                            {
                                                //ยังไม่ผ่านการจ่าย

                                                if (rs["checkoutdatetime"].ToString() == "")
                                                {
                                                    if (rs["jobdatetime"].ToString() == "")
                                                    {
                                                        PresSatus = "กำลังจัดยา";
                                                    }
                                                    else
                                                    {
                                                        PresSatus = "รอตรวจสอบยา";
                                                    }
                                                }
                                                else
                                                {
                                                    PresSatus = "รอจ่ายยา";
                                                }

                                                status.Rows.Add(rs["prescriptionno"].ToString(), PresSatus);

                                                if (liststatus.ToString() == "")
                                                {
                                                    liststatus.Items.Add(rs["prescriptionno"].ToString() + " : " + PresSatus);
                                                }
                                                else
                                                {
                                                    liststatus.Items.Add(Environment.NewLine + rs["prescriptionno"].ToString() + " : " + PresSatus);
                                                }
                                            }
                                        }

                                    }
                                    foreach (DataRow i in db_data.Rows)
                                    {
                                        if (i["consultdatetime"].ToString() != "")
                                        {
                                            cb_status1.Checked = true;
                                        }
                                        else
                                        {
                                            cb_status1.Checked = false;
                                        }

                                        if (i["medprogressdatetime"].ToString() != "")
                                        {
                                            cb_status0.Checked = true;
                                        }
                                        else
                                        {
                                            cb_status0.Checked = false;
                                        }
                                    }

                                    //dg_waitMatching.DataSource = db_packagemaster_first;
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
                                    dg_waitMatching.Rows.Clear();
                                    dg_waitMatching.Refresh();
                                    // เพิ่มข้อมูลตัวอย่าง
                                    foreach (DataRow i in db_drug.Rows)
                                    {
                                        string columnName_v = "voiddatetime";
                                        string voiddatetime = "";
                                        if (db_drug.Columns.Contains(columnName_v))
                                        {
                                            Console.WriteLine($"Column '{columnName_v}' exists.");
                                            voiddatetime = i["voiddatetime"].ToString();
                                        }
                                        string drugcode = "";
                                        drugcode = i["orderitemcode"].ToString();

                                        if (i["voiddatetime"].ToString() != "")
                                        {
                                            voiddatetime = "voiddatetime";
                                        }


                                        DataTable dtAN = new DataTable();
                                        DataRow[] rows = db_packagemaster.Select("orderitemcode = '" + drugcode + "' ");
                                        dtAN = rows.CopyToDataTable();

                                        Bitmap bitmap = GetBitmapFromUrl(PrescriptionManagement.Properties.Settings.Default.drugpicUrl + drugcode + "/8");
                                        string instustion = "";
                                        instustion = i["instructiondesc"].ToString() + i["timedesc"].ToString();
                                        dg_waitMatching.Rows.Add(i["_id"].ToString(),
                                            i["orderitemcode"].ToString(),
                                            i["orderitemname"].ToString(),
                                            i["orderqty"].ToString(),
                                            i["orderunitdesc"].ToString(),
                                            instustion, bitmap,
                                            "ยกเลิก",
                                            "MedError",
                                            i["voiddatetime"].ToString(),
                                            dtAN.Rows[0]["shelfzone"].ToString(),
                                            dtAN.Rows[0]["shelfname"].ToString());

                                    }

                                    foreach (DataGridViewRow row in dg_waitMatching.Rows)
                                    {
                                        if (row.Cells["voiddatetime"].Value != null && row.Cells["voiddatetime"].Value.ToString() != "")
                                        {
                                            row.DefaultCellStyle.BackColor = Color.FromArgb(200, 0, 0);
                                            row.DefaultCellStyle.ForeColor = Color.White; // ให้เห็นข้อความชัดขึ้น
                                        }
                                    }



                                    #endregion
                                    dg_FinishMatching.Rows.Clear();
                                    string columnName = "jobusername";
                                    string jobusername = "";
                                    string zone = "";
                                    string orderitembarcode = "";
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
                                        if (i["orderitembarcodecase"].ToString() != "")
                                        {
                                            string text = i["orderitembarcodecase"].ToString();
                                            JArray jarr = (JArray)i["orderitembarcodecase"]; // ดึง JArray ออกมา

                                            List<string> arBarcode = jarr.ToObject<List<string>>(); // แปลงเป็น List<string>

                                            // หรือถ้าต้องการ array
                                            string[] arrayBarcode = arBarcode.ToArray();
                                            if (arrayBarcode.Length > 1)
                                            {
                                                foreach (string code in arBarcode)
                                                {
                                                    if (orderitembarcode == "")
                                                    {
                                                        orderitembarcode += code; // จะพิมพ์ออกมา: 1911_0
                                                    }
                                                    else
                                                    {
                                                        orderitembarcode = orderitembarcode + "|" + code;
                                                    }

                                                }

                                            }
                                            else
                                            {
                                                orderitembarcode = arrayBarcode[0];
                                            }
                                        }
                                        else
                                        {
                                            orderitembarcode = i["orderitembarcode"].ToString();
                                        }

                                        if (i["ordertype"].ToString() != "H")
                                        {
                                            if (i["shelfzone"].ToString() != "FLOORSTOCK")
                                            {
                                                dg_FinishMatching.Rows.Add(false, orderitembarcode, i["matchingdatetime"].ToString(), i["orderitemcode"].ToString(),
                                                  i["orderitemname"].ToString(),
                                                  i["dosage"].ToString(),
                                                  i["dosageunitcode"].ToString(),
                                                  i["shelfzone"].ToString() + " " + i["shelfname"].ToString(),
                                                  jobusername, i["voiddatetime"].ToString());
                                            }

                                        }
                                        else
                                        {
                                            if (i["shelfzone"].ToString() != "FLOORSTOCK")
                                            {
                                                dg_FinishMatching.Rows.Add(false, orderitembarcode, i["matchingdatetime"].ToString(), i["orderitemcode"].ToString(),
                                               i["orderitemname"].ToString(),
                                               i["dosage"].ToString(),
                                               i["dosageunitcode"].ToString(),
                                               i["shelfzone"].ToString() + " " + i["shelfname"].ToString(),
                                               jobusername, i["voiddatetime"].ToString());
                                            }

                                        }

                                        orderitembarcode = "";


                                        // รีเฟรช DataGridView
                                    }

                                    if (clsPackagemaster.db_labs.Rows.Count > 0)
                                    {
                                        labs = "";
                                        foreach (DataRow row in clsPackagemaster.db_labs.Rows)
                                        {
                                            if (labs == "" && row["lab_name"].ToString() != "Creatinine")
                                            {
                                                labs += row["lab_name"].ToString() + " " + row["result"].ToString() + " (" + clsconvert.convert_thai(row["date_result"].ToString()) + ")";

                                            }
                                            else if (labs != "" && row["lab_name"].ToString() != "Creatinine")
                                            {
                                                labs += " || " + row["lab_name"].ToString() + " " + row["result"].ToString() + " (" + clsconvert.convert_thai(row["date_result"].ToString()) + ")";
                                            }
                                        }

                                        lblabs.Text = labs;
                                    }
                                    else
                                    {
                                        lblabs.Text = "-";
                                    }


                                    if (clsPackagemaster.db_drugallergies.Rows.Count > 0)
                                    {
                                        string his = "";
                                        string hispharmaco = "";
                                        lsdrugallergies_his.Items.Clear();
                                        lshispharmaco.Items.Clear();
                                        btndrugallergies.Visible = true;
                                        pic1.Visible = true;
                                        int i = 0;
                                        foreach (DataRow row in clsPackagemaster.db_drugallergies.Rows)
                                        {
                                            if (row["drugallergytype"].ToString() == "his")
                                            {
                                                drugallergies_his = "";

                                                drugallergies_his = "Genericname : " + row["genericname"].ToString();
                                                lsdrugallergies_his.Items.Add(drugallergies_his);
                                                his += drugallergies_his;
                                                drugallergies_his = Environment.NewLine + "adverbs : " + row["adverbs"].ToString();
                                                lsdrugallergies_his.Items.Add(drugallergies_his);
                                                his += drugallergies_his;
                                                drugallergies_his = Environment.NewLine + "memo : " + row["memo"].ToString();
                                                lsdrugallergies_his.Items.Add(drugallergies_his);
                                                his += drugallergies_his;
                                            }
                                            else
                                            {
                                                drugallergies_ministry = "";

                                                drugallergies_ministry = "Genericname : " + row["genericname"].ToString();
                                                lshispharmaco.Items.Add(drugallergies_ministry);
                                                hispharmaco += drugallergies_ministry;
                                                drugallergies_ministry = Environment.NewLine + "adverbs : " + row["adverbs"].ToString();
                                                lshispharmaco.Items.Add(drugallergies_ministry);
                                                hispharmaco += drugallergies_ministry;
                                                drugallergies_ministry = Environment.NewLine + "memo : " + row["memo"].ToString();
                                                lshispharmaco.Items.Add(drugallergies_ministry);
                                                hispharmaco += drugallergies_ministry;
                                            }
                                        }

                                        bool result;
                                        frmDrugallergies frm = new frmDrugallergies(clsPackagemaster.db_drugallergies);
                                        frm.ShowDialog();

                                        if (frm.result)
                                        {
                                            frm.Close();
                                            pic1.Visible = false;
                                            pic2.Visible = true;
                                            txtbarcode.Focus();
                                        }
                                        else
                                        {

                                        }

                                    }
                                    else
                                    {

                                    }

                                    foreach (DataGridViewRow row in dg_FinishMatching.Rows)
                                    {
                                        if (row.Cells["voiddate"].Value.ToString().Trim() != "")
                                        {
                                            row.DefaultCellStyle.BackColor = Color.FromArgb(200, 0, 0);
                                            row.DefaultCellStyle.ForeColor = Color.White; // ให้เห็นข้อความชัดขึ้น
                                        }
                                        else
                                        {
                                            row.DefaultCellStyle.BackColor = Color.FromArgb(130, 224, 170);
                                        }
                                       
                                    }
                                    foreach (DataGridViewRow row in dg_waitMatching.Rows)
                                    {
                                        var cellValue = row.Cells["voiddatetime"]?.Value;

                                        if (cellValue != null && cellValue.ToString().Trim() != "")
                                        {
                                            row.DefaultCellStyle.BackColor = Color.FromArgb(200, 0, 0);
                                            row.DefaultCellStyle.ForeColor = Color.White; // ให้เห็นข้อความชัดขึ้น
                                        }
                                    }



                                    lbstatus.Text = " Checkout complete ";
                                    lbstatus.ForeColor = Color.ForestGreen;
                                }

                            }
                            else
                            {
                                frmAlarm frm = new frmAlarm(" ใบสั่งยายกเลิก ");
                                frm.ShowDialog();
                            }
                        }
                        else
                        {
                            frmAlarm frm = new frmAlarm(" ไม่พบข้อมูลใบสั่งยา !!! ");
                            frm.ShowDialog();
                        }
                    }
                }

                dg_waitMatching.ClearSelection();
                dg_FinishMatching.ClearSelection();

            }

            EnableDoubleBuffer(dg_waitMatching);
            EnableDoubleBuffer(dg_FinishMatching);

            SetupWaitMatchingGrid();
            //HighlightRows();
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
        public async void update_checkout()
        {
            DataTable dtprint = new DataTable();
            DataTable dbUser = new DataTable();
            bool result;
            bool checkall = false;
            //dbUser = await clsService.RequestUserID(cls.clsuser.userid);
            
            if (txt_BarcodePrescr.Text != "")
            {
                DateTime _dtstr = DateTime.Now;
                DateTime _dtend = DateTime.Now;
                _dtstr = _dtstr.AddDays(-1);
                string dtstr = clsconvert.convertdate_YYYY_MM_DD_HH_EN_NEW(_dtstr.ToString());
                string dtend = clsconvert.convertdate_YYYY_MM_DD_HH_EN_NEW(_dtend.ToString());
                
                List<object> ListJsonMatching = new List<object>();

                ListJsonMatching = GenJson_matching();
                if (ListJsonMatching.Count > 0)
                {
                    result = await clsService.update_checkout(ListJsonMatching);
                    if (result)
                    {
                        List<object> ListJsonMatching_rx = new List<object>();

                        ListJsonMatching_rx = GenJson_checkout_rx();
                        result = await clsService.update_rx(ListJsonMatching_rx);
                        if (result)
                        {
                            lbstatus.Text = " Checkout complete ";
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
                        lbstatus.Text = " Checkout Not complete !!! ";
                        lbstatus.ForeColor = Color.Red;
                        clsPackagemaster.db_data = new DataTable();
                        clsPackagemaster.db_drug = new DataTable();
                        clsPackagemaster.db_packagemaster = new DataTable();
                    }
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
                            checkoutdatetime = (str_datenow_checkout.ToString().Length > 0) ? Convert.ToDateTime(str_datenow_checkout.ToString()).ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture).ToString() : "",
                            checkoutuserid = cls.clsuser.userid.Trim(),
                            checkoutusername = cls.clsuser.name.Trim(),
                            
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
        public List<object> GenJson_checkout_rx()
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
                            //checkoutdatetime = (str_datenow_checkout.ToString().Length > 0) ? Convert.ToDateTime(str_datenow_checkout.ToString()).ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture).ToString() : "",
                            checkoutdatetime = "datetime"
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
        public List<object> GenJson_matching_rx_medprogress(string str_datenow_checkout)
        {
            var listpackagemaster = new List<object>(); // เปลี่ยนจาก List<Dictionary<..>> เป็น List<object> ถ้าจะ return object

            try
            {
                if (clsPackagemaster.db_packagemaster.Rows.Count > 0 &&
                    clsPackagemaster.db_data.Rows.Count > 0 &&
                    clsPackagemaster.db_drug.Rows.Count > 0)
                {
                    foreach (DataRow rw in clsPackagemaster.db_data.Rows)
                    {
                        if (!string.IsNullOrEmpty(str_datenow_checkout))
                        {
                            string convertedDate = date_utc(str_datenow_checkout.ToString());
                            string formattedDate = Convert
                                .ToDateTime(convertedDate)
                                .AddHours(-7)
                                .ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);

                            listpackagemaster.Add(new Dictionary<string, object>
                            {
                                ["_id"] = rw["_id"].ToString(),
                                ["medprogressdatetime"] = formattedDate
                            });
                        }
                        else
                        {
                            listpackagemaster.Add(new Dictionary<string, object>
                            {
                                ["_id"] = rw["_id"].ToString(),
                                ["medprogressdatetime"] = null
                            });
                        }
                    }
                }

                return listpackagemaster;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return listpackagemaster;
            }
        }

        public List<object> GenJson_matching_rx_consult(string str_datenow_checkout)
        {
            var listpackagemaster = new List<object>(); // เปลี่ยนจาก List<Dictionary<..>> เป็น List<object> ถ้าจะ return object

            try
            {
                if (clsPackagemaster.db_packagemaster.Rows.Count > 0 &&
                    clsPackagemaster.db_data.Rows.Count > 0 &&
                    clsPackagemaster.db_drug.Rows.Count > 0)
                {
                    foreach (DataRow rw in clsPackagemaster.db_data.Rows)
                    {
                        if (!string.IsNullOrEmpty(str_datenow_checkout))
                        {
                            string convertedDate = date_utc(str_datenow_checkout.ToString());
                            string formattedDate = Convert
                                .ToDateTime(convertedDate)
                                .AddHours(-7)
                                .ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);

                            listpackagemaster.Add(new Dictionary<string, object>
                            {
                                ["_id"] = rw["_id"].ToString(),
                                ["consultdatetime"] = formattedDate
                            });
                        }
                        else
                        {
                            listpackagemaster.Add(new Dictionary<string, object>
                            {
                                ["_id"] = rw["_id"].ToString(),
                                ["consultdatetime"] = null
                            });
                        }
                    }
                }

                return listpackagemaster;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return listpackagemaster;
            }
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
        public void clear()
        {
            txt_BarcodePrescr.Text = "";
            //dg_FinishMatching.DataSource = new DataTable();
            //dg_waitMatching.DataSource = new DataTable();
            //lbstatus.Text = "-";
            lbAN.Text = "";
            lbname.Text = "";
            lbPres.Text = "";
            lblabs.Text = "";
            dg_waitMatching.Rows.Clear();
            dg_FinishMatching.Rows.Clear();
            txt_BarcodePrescr.Focus();
            txt_BarcodePrescr.Enabled = true;
            lbbasketno.Text = "-";
            btndrugallergies.Visible = false;
            pic1.Visible = false;
            pic2.Visible = false;
            lsdrugallergies_his.Items.Clear();
            lshispharmaco.Items.Clear();
            liststatus.Items.Clear();
            cb_status0.Checked = false;
            cb_status1.Checked = false;
            
        }        
        private async void btn_MatchingOrder_Click(object sender, EventArgs e)
        {
            
            update_checkout();
        }

        private void btndrugallergies_Click(object sender, EventArgs e)
        {
            bool result;
            frmDrugallergies frm = new frmDrugallergies(clsPackagemaster.db_drugallergies);
            frm.ShowDialog();

            if (frm.result)
            {
                frm.Close();
                pic1.Visible = false;
                pic2.Visible = true;
            }
            else
            {

            }
        }


        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private async void txtbarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {               
                if(dg_FinishMatching.Rows.Count > 0 && dg_waitMatching.Rows.Count >0)
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
                        if (cellValue.Contains(search))
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
                                        if (cellValue.Contains(search))
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
                                                    if(rwp["drugitem_sub"].ToString() != drugitem_sub)
                                                    {
                                                        chpic = true;
                                                    }
                                                }
                                                if(chpic)
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
                        else if(lbPres.Text == search)
                        {
                            foreach (DataGridViewRow roww in dg_FinishMatching.Rows)
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
                        }
                        
                        //dg_FinishMatching_Click(dg_FinishMatching, EventArgs.Empty);
                    }

                    if (found)
                    {
                        txtbarcode.Clear();
                        txtbarcode.Focus();

                        if( db_pic.Rows.Count> 0)
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
                        update_checkout();
                        dg_FinishMatching.Refresh();
                    }
                }
                else if(dg_FinishMatching.Rows.Count > 0 && dg_waitMatching.Rows.Count <=0)
                {
                    
                }
            }
            
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void dg_FinishMatching_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private async void dg_waitMatching_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 6)
            {
                bool result;
                string code = dg_waitMatching.Rows[e.RowIndex].Cells["orderitemcode"].Value.ToString();
                string name = dg_waitMatching.Rows[e.RowIndex].Cells["orderitemname"].Value.ToString();
                frmShowpic frmpic = new frmShowpic(code, name);
                frmpic.ShowDialog();
                if (frmpic.result)
                {
                    frmpic.Close();
                }
            }
            else if (e.RowIndex >= 0 && e.ColumnIndex == 7)
            {
                frmTextbox frmtxt = new frmTextbox(" กรุณาระบุเหตุผลในการยกเลิกรายการยา ");
                frmtxt.ShowDialog();
                bool result = false;
                string _id = "";
                string voiduserid = "";
                string voidusername = "";
                string voiddesc = "";
                voiddesc = frmtxt.inputText;
                if (frmtxt.result && voiddesc != "cancel_void")
                {
                    _id = dg_waitMatching.Rows[e.RowIndex].Cells["_id"].Value.ToString();
                    voiduserid = clsuser.userid;
                    voidusername = clsuser.name;
                    voiddesc = frmtxt.inputText;
                    string userText = frmtxt.inputText;
                    foreach (DataGridViewRow row in dg_FinishMatching.Rows)
                    {
                        var cellValue = dg_waitMatching.Rows[e.RowIndex].Cells["orderitemcode"].Value?.ToString().Trim();
                        if (cellValue.Contains(row.Cells["fin_orderitemcode"].Value.ToString()))
                        {
                            row.Cells["chk_select"].Value = true; // กรณีเจอ barcode ให้ติ๊ก
                            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 166, 150);
                            dg_waitMatching.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 166, 150);

                            dg_waitMatching.Refresh();
                            dg_FinishMatching.Refresh();

                        }
                    }

                    result = await clsService.update_void(_id, voiduserid, voidusername, voiddesc);
                }
                else if (frmtxt.result && voiddesc == "cancel_void")
                {
                    _id = dg_waitMatching.Rows[e.RowIndex].Cells["_id"].Value.ToString();
                    voiduserid = clsuser.userid;
                    result = await clsService.update_void(_id, voiduserid, "", "");
                }

                //txt_BarcodePrescr.KeyPress += txt_BarcodePrescr_KeyPress;


            }
            else if (e.RowIndex >= 0 && e.ColumnIndex == 8)
            {               
              
                var orderQtyValue = dg_waitMatching.Rows[e.RowIndex].Cells["orderqty"].Value?.ToString();
               
                var item = new frmMederror.MedErrorItem()
                {
                    _id_prescription = dg_waitMatching.Rows[e.RowIndex].Cells["_id"].Value?.ToString(),
                    ordercreatedate = DateTime.Now,
                    prescriptionno = lbPres.Text,
                    hn = lbhn.Text,
                    vn = lbAN.Text,
                    orderitemcode = dg_waitMatching.Rows[e.RowIndex].Cells["orderitemcode"].Value?.ToString().Trim(),
                    orderitemname = dg_waitMatching.Rows[e.RowIndex].Cells["orderitemname"].Value?.ToString().Trim(),
                    orderunitdesc = dg_waitMatching.Rows[e.RowIndex].Cells["orderunitcode"].Value?.ToString().Trim(),
                    orderqty = orderQtyValue,
                    shelfzone = dg_waitMatching.Rows[e.RowIndex].Cells["shelfzone"].Value?.ToString().Trim(),
                    shelfname = dg_waitMatching.Rows[e.RowIndex].Cells["shelfname"].Value?.ToString().Trim(),
                    jobuserid = clsuser.userid,
                    jobusername = clsuser.name,
                    mederror_desc = "",
                    mederror_freetext = "",
                    mederror_robot = "",              

                    mederror_userid = clsuser.userid,
                    mederror_username = clsuser.name,
                    mederror_type = "",
                    createddate = DateTime.Now,
                    dateupdate = DateTime.Now
                };
                // Show mederror
                frmMederror frmMe = new frmMederror(item);
               
                frmMe.ShowDialog();
            }
            else
            {

            }
        }

        private void dg_FinishMatching_Click(object sender, EventArgs e)
        {
            
        }

        private void btnhistory_Click(object sender, EventArgs e)
        {
            frmHistory frmhis = new frmHistory(lbhn.Text);
            frmhis.ShowDialog();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void lsdrugallergies_his_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            bool result;
            string medprogress = "";
            string consult = "";

            if (cb_status0.Checked == true)
            {
                medprogress = "[0]";
            }
            else
            {
                medprogress = "";
            }

            if (cb_status1.Checked == true)
            {
                consult = "[1]";
            }
            else
            {
                consult = "";
            }

            

            List<object> ListJsonMatching_rx = new List<object>();
            DateTime utcNow = DateTime.Now;
            DateTime utcNow_checkout = DateTime.Now;
            string str_datenow_checkout = date_utc(utcNow_checkout.ToString());
            if (medprogress != "")
            {
                ListJsonMatching_rx = GenJson_matching_rx_medprogress(str_datenow_checkout);
                result = await clsService.update_rx(ListJsonMatching_rx);
            }
            else
            {
                ListJsonMatching_rx = GenJson_matching_rx_medprogress("");
                result = await clsService.update_rx(ListJsonMatching_rx);
            }

            if (consult != "")
            {
                ListJsonMatching_rx = GenJson_matching_rx_consult(str_datenow_checkout);
                result = await clsService.update_rx(ListJsonMatching_rx);
            }
            else
            {
                ListJsonMatching_rx = GenJson_matching_rx_consult("");
                result = await clsService.update_rx(ListJsonMatching_rx);
            }
        }
        public async Task<bool> RequestQ(string AN)
        {
            bool NQueue = false;
            string Queue = "";
            string ES0 = "";
            string ES1 = "";

            DataTable dt = new DataTable();

            dt = await clsService.callorder();

            if (dt.Rows.Count > 0)
            {
                //มีคิวในระบบ เอามาแสดงที่ datagrid
                //dg_anorder.Rows.Clear();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    long[] arrES;
                    string result;

                    arrES = (long[])dt.Rows[i]["eventstate"];
                    result = string.Join(", ", arrES);

                    bool EvenStatus0 = result.Contains("0");
                    if (EvenStatus0 == true)
                    {
                        ES0 = "Y";
                    }
                    else
                    {
                        ES0 = "";
                    }
                    bool EvenStatus1 = result.Contains("1");
                    if (EvenStatus1 == true)
                    {
                        ES1 = "Y";
                    }
                    else
                    {
                        ES1 = "";
                    }


                    if (result != "")
                    {
                        
                    }

                   
                }

                if (AN != "")
                {
                    DataTable dtAN = new DataTable();
                    DataRow[] rows = dt.Select("an = '" + AN + "' and dispensedate is null ");
                    if (rows.Length > 0)
                    {
                        //มีแล้วส่งเลข Q กลับ
                        dtAN = rows.CopyToDataTable();

                        Queue = dtAN.Rows[0]["queue"].ToString();

                        lbQueue.Text = Queue;
                        lb_id.Text = dtAN.Rows[0]["_id"].ToString();

                        long[] arrES;
                        string result;
                        // show dg
                        if (dt.Rows[0]["eventstate"] != DBNull.Value)
                        {

                            result = "";
                        }
                        else
                        {
                            arrES = (long[])dt.Rows[0]["eventstate"];
                            result = string.Join(", ", arrES);
                        }

                        bool EvenStatus0 = result.Contains("0");
                        if (EvenStatus0 == true)
                        {
                            cb_status0.Checked = true;
                        }
                        else
                        {
                            cb_status0.Checked = false;
                        }
                        bool EvenStatus1 = result.Contains("1");
                        if (EvenStatus1 == true)
                        {
                            cb_status1.Checked = true;
                        }
                        else
                        {
                            cb_status1.Checked = false;
                        }


                        NQueue = false; //ไม่ต้องสร้างใหม่
                    }
                    else
                    {
                        NQueue = true; //สร้างใหม่
                                       // MessageBox.Show("AN นี้ยังไม่มีหมายเลขเอกสาร");

                    }
                }

            }
            else
            {
                NQueue = true; //สร้างใหม่

            }

            return NQueue;

        }

        private async void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyValue == 13)
            {
                string an = "";
                DateTime _dtstr = DateTime.Now;
                DateTime _dtend = DateTime.Now;
                //_dtstr = _dtstr.AddDays();
                _dtend = _dtend.AddDays(+1);
                string dtstr = clsconvert.convert_en(_dtstr.ToString());
                string dtend = clsconvert.convert_en(_dtend.ToString());
                an = txtan.Text;
                DataSet dsorder = new DataSet();
                dsorder = await clsService.RequestdetailAN(dtstr, dtend, an);

                if (dsorder.Tables.Count > 0)
                {
                    // เรียกใช้ DataTable โดยใช้ชื่อ
                    DataTable dtdata = dsorder.Tables["data"];
                    DataTable dtPackage = dsorder.Tables["packagemaster"];
                    DataTable dtDrugs = dsorder.Tables["drugs"];
                    DataTable dtLabs = dsorder.Tables["labs"];
                    DataTable dtAllergies = dsorder.Tables["drugallergies"];
                    if (dsorder.Tables["data"].Rows.Count > 0)
                    {
                        lbAN.Text = dtdata.Rows[0]["an"].ToString();
                        lbhn.Text = dtdata.Rows[0]["hn"].ToString();
                        lbname.Text = dtdata.Rows[0]["prename"].ToString() + dtdata.Rows[0]["patientname"].ToString();
                        
                    }
                }
            }
            
        }

        private void btn_frmstatus_Click(object sender, EventArgs e)
        {
            frm_checkstatus frmstatus = new frm_checkstatus();
            frmstatus.Show();
        }

        private async void btnVoid_Click(object sender, EventArgs e)
        {
            bool result = false;
            string _id = "";
            string voiduserid = "";
            string voidusername = "";
            string voiddesc = "";
            voiddesc = "";
            DialogResult resultMs = MessageBox.Show(
                                  " คุณต้องการยกเลิกใบสั่งยาหรือไม่?",      // ข้อความในกล่อง
                                  "ยืนยันการดำเนินการ",                    // หัวข้อ
                                  MessageBoxButtons.YesNo,                   // ปุ่ม Yes และ No
                                  MessageBoxIcon.Question                    // ไอคอนเครื่องหมายคำถาม
                              );

            if (resultMs == DialogResult.Yes)
            {
               
                _id = clsPackagemaster.db_data.Rows[0]["_id"].ToString();
                voiduserid = clsuser.userid;
                voidusername = clsuser.name;
                voiddesc = "CheckOut ยกเลิกใบสั่งยา "+ DateTime.Now.ToString();
                
                result = await clsService.updatePre_void(_id, cls.clsuser.userid, cls.clsuser.name, voiddesc);

            }
            else
            {

            }

        }
    }
   
}