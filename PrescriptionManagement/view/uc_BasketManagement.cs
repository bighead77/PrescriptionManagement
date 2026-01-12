using PrescriptionManagement.cls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrescriptionManagement.view
{
    public partial class uc_BasketManagement : UserControl
    {
        clsService clsService = new clsService();
        clsconvertdate clsconvert = new clsconvertdate();
        public uc_BasketManagement()
        {
            InitializeComponent();
        }

        private void dg_FinishMatching_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private async void txt_identify_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                DateTime _dtstr = DateTime.Now;
                DateTime _dtend = DateTime.Now;
                _dtstr = _dtstr.AddDays(-1);
                string dtstr = clsconvert.convertdate_YYYY_MM_DD_HH_EN_NEW(_dtstr.ToString());
                string dtend = clsconvert.convertdate_YYYY_MM_DD_HH_EN_NEW(_dtend.ToString());

                await clsService.RequestdetailAll(dtstr, dtend,txt_identify.Text.ToString());
                if (clsPackagemaster.db_packagemaster.Rows.Count > 0)
                {
                    DataTable db_packagemaster_first = new DataTable();
                    db_packagemaster_first = clsPackagemaster.db_packagemaster.Select().CopyToDataTable(); // Copy to a new DataTable

                    if (db_packagemaster_first.Rows.Count > 0)
                    {
                        lbPres.Text = db_packagemaster_first.Rows[0]["prescriptionno"].ToString();
                        lbVN.Text = db_packagemaster_first.Rows[0]["vn"].ToString();
                       
                        #region dgv
                        dg_FinishMatching.Rows.Clear();
                        // เพิ่มข้อมูลตัวอย่าง
                        foreach (DataRow i in db_packagemaster_first.Rows)
                        {
                            dg_FinishMatching.Rows.Add(i["orderitemcode"].ToString(),
                                i["orderitemname"].ToString(),
                                i["orderqty"].ToString(),
                                i["orderunitdesc"].ToString(),
                                i["shelfzone"].ToString() + "-" + i["shelfname"].ToString(),
                                i["orderqty"].ToString(),
                                i["orderunitdesc"].ToString());

                            // รีเฟรช DataGridView

                        }

                        #endregion
                    }

                }

            }
        }

        private async void txtbasket_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                blRFIDNum.Text = txtbasket.Text.Trim();
                await clsService.GetbasketNumber(txtbasket.Text.Trim());
                if (clsPackagemaster.dtbasket.Rows.Count > 0)
                {
                    txtbasket.Text = clsPackagemaster.dtbasket.Rows[0]["no"].ToString();                    
                }
            }
        }

        private void btn_SAVE_Click(object sender, EventArgs e)
        {
            register_basket();
        }
        public async void register_basket()
        {
            if (txtbasket.Text != "")
            {
                bool result =false;
                DataTable dtprint = new DataTable();
                List<object> ListJson = new List<object>();
                
                DateTime utcNow = DateTime.Now;
                string str_datenow = date_utc(utcNow.ToString());
                ListJson = GenJson_RegisUser(str_datenow.Trim());

                //result = await clsService.update_regisbasket(ListJson);

                if(result)
                {
                    lbbasstatus.Text = " เปลี่ยนตะกร้าเรียบร้อยแล้ว ";
                }
                else
                {
                    lbbasstatus.Text = " เปลี่ยนตะกร้าไม่สำเร็จ !!!! ";
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
        public List<object> GenJson_RegisUser(string datetime)
        {
            List<object> listpackagemaster = new List<object>();
            List<object> listJson = new List<object>();
            try
            {
                if (clsPackagemaster.db_packagemaster.Rows.Count > 0)
                {

                    List<object> listpack = new List<object>();
                    foreach (DataRow rw in clsPackagemaster.db_packagemaster.Rows)
                    {
                        listpackagemaster.Add(new
                        {
                            _id = rw["_id"].ToString(),
                            _id_prescription = rw["_id_prescription"].ToString(),
                            seqrun = Convert.ToInt32(rw["seqrun"].ToString()),
                            __v = Convert.ToInt32(rw["__v"].ToString()),
                            dateupdate = datetime,
                            drugaccountcode = rw["drugaccountcode"].ToString(),
                            edned = rw["edned"].ToString(),
                            genorderdatetime = Convert.ToDateTime(rw["genorderdatetime"].ToString()).AddHours(7).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).ToString(),
                            itemidentify = rw["itemidentify"].ToString(),
                            orderitembarcode = rw["orderitembarcode"].ToString(),
                            //orderitemcode = rw["orderitemcode"].ToString(),
                            orderitemname = rw["orderitemname"].ToString(),
                            orderqty = Convert.ToInt32(rw["orderqty"].ToString()),
                            orderunitcode = rw["orderunitcode"].ToString(),
                            orderunitdesc = rw["orderunitdesc"].ToString(),
                            poison = rw["poison"].ToString(),
                            pregnancy = rw["pregnancy"].ToString(),
                            prescriptionno = rw["prescriptionno"].ToString(),
                            prescriptionno_sup = rw["prescriptionno_sup"].ToString(),
                            seq = Convert.ToInt32(rw["seq"].ToString()),
                            seqmax = Convert.ToInt32(rw["seqmax"].ToString()),
                            shelfname = rw["shelfname"].ToString(),
                            shelfzone = rw["shelfzone"].ToString(),
                            orderzone = Convert.ToInt32(rw["orderzone"].ToString()),
                            basketno = txtbasket.Text.Trim(),
                            basketid = blRFIDNum.Text.Trim(),
                            jobdatetime = rw["jobdatetime"].ToString(),
                            jobuserid = rw["jobuserid"].ToString(),
                            jobusername = rw["jobusername"].ToString(),
                            zoneindt = datetime
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

        private void pic_clear_Click(object sender, EventArgs e)
        {
            txtbasket.Clear();
            txt_identify.Clear();
            blRFIDNum.Text = "-";
            //dg_FinishMatching.Rows.Clear();
            dg_FinishMatching.DataSource = new DataTable();
            lbbasstatus.Text = "-";
            lbom.Text = "-";
            lbPres.Text = "-";
            lbVN.Text = "-";
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
