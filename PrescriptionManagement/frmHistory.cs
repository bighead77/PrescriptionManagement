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

namespace PrescriptionManagement
{
    public partial class frmHistory : Form
    {
        clsService clsService = new clsService();
        clsconvertdate clsconvert = new clsconvertdate();
        public string keyword = "";
        public frmHistory(string _keyword)
        {
            InitializeComponent();
            keyword = _keyword;
        }
        private void frmHistory_Load(object sender, EventArgs e)
        {
            if(keyword == "" || keyword =="-")
            {
                txtsearchhn.Text = "";
                txtsearchhn.Focus();
            }
            else
            {
                txtsearchhn.Text = keyword;
                searhHistory(keyword);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        public async void searhHistory(string _kekword)
        {
            Bitmap bitmap = null;
            DateTime _dtstr = DateTime.Now;
            DateTime _dtend = DateTime.Now;
            _dtstr = _dtstr.AddDays(-90);
            _dtend = _dtend.AddDays(-1);

            string dtstr = clsconvert.convert_en(_dtstr.ToString());
            string dtend = clsconvert.convert_en(_dtend.ToString());

            DataSet ds_his = new DataSet();
            ds_his = await clsService.RequestdetailHN(dtstr, dtend, _kekword);
            // เรียกใช้ DataTable โดยใช้ชื่อ
            DataTable dtdata_ihs = ds_his.Tables["data"];
            DataTable dtPackage_ihs = ds_his.Tables["packagemaster"];
            DataTable dtDrugs_ihs = ds_his.Tables["drugs"];
            DataTable dtLabs_ihs = ds_his.Tables["labs"];
            DataTable dtAllergies_ihs = ds_his.Tables["drugallergies"];

            if (dtdata_ihs.Rows.Count > 0 && dtDrugs_ihs.Rows.Count > 0)
            {
                #region dgv
                
                dgvHistory.Rows.Clear();
                dgvHistory.Refresh();
                lslabs.Items.Clear();
                string labs = "";
                string drugallergies_his = "";
                string drugallergies_ministry = "";
                bool disfin = true;
                
                // เพิ่มข้อมูลตัวอย่าง

                lbname.Text = dtdata_ihs.Rows[0]["prename"].ToString() + " " + dtdata_ihs.Rows[0]["patientname"].ToString();
                if(dtdata_ihs.Rows[0]["sex"].ToString() == "1")
                {
                    lbsax.Text = "ชาย";
                }
                else
                {
                    lbsax.Text = "หญิง";
                }
                

                string yy = clsconvert.convertdate_YYYY_EN(dtdata_ihs.Rows[0]["patientdob"].ToString());
                string mm = clsconvert.convertdate_MM_EN(dtdata_ihs.Rows[0]["patientdob"].ToString());
                string dd = clsconvert.convertdate_DD_EN(dtdata_ihs.Rows[0]["patientdob"].ToString());

                DateTime birthDate = new DateTime(Convert.ToInt16(yy), Convert.ToInt16(mm), Convert.ToInt16(dd));
                lbage.Text = clsconvert.CalculateAge(birthDate);

                #endregion
                foreach (DataRow i in dtDrugs_ihs.Rows)
                {

                    dgvHistory.Rows.Add(i["orderitemcode"].ToString(),
                        i["orderitemname"].ToString(),
                        i["orderqty"].ToString(),
                        i["orderunitdesc"].ToString());
                }
              
                if (dtLabs_ihs.Rows.Count > 0)
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

                    lslabs.Items.Add(labs);
                }
                else
                {
                    lslabs.Items.Add(" ..... ไม่มี ..... ");
                }
            }
            
            DateTime _dtstrt = DateTime.Now;
            DateTime _dtendt = DateTime.Now;
            _dtendt = _dtendt.AddDays(+1);

            string dtstrt = clsconvert.convert_en(_dtstrt.ToString());
            string dtendt = clsconvert.convert_en(_dtendt.ToString());

            DataSet ds_today = new DataSet();
            ds_his = await clsService.RequestdetailHN(dtstrt, dtendt, _kekword);
            // เรียกใช้ DataTable โดยใช้ชื่อ
            DataTable dtdata_today = ds_his.Tables["data"];
            DataTable dtPackage_today = ds_his.Tables["packagemaster"];
            DataTable dtDrugs_today = ds_his.Tables["drugs"];
            DataTable dtLabs_today = ds_his.Tables["labs"];
            DataTable dtAllergies_today = ds_his.Tables["drugallergies"];

            if (dtdata_today.Rows.Count > 0 && dtDrugs_today.Rows.Count > 0)
            {
                #region dgv
                dgvtoday.Rows.Clear();
                dgvtoday.Refresh();                
                lslabstoday.Items.Clear();
                
                string labs = "";
                string drugallergies_his = "";
                string drugallergies_ministry = "";
                bool disfin = true;
                // เพิ่มข้อมูลตัวอย่าง
                lbhn.Text = dtdata_today.Rows[0]["hn"].ToString();
                lbname.Text = dtdata_today.Rows[0]["prename"].ToString() + " " + dtdata_today.Rows[0]["patientname"].ToString();
                lbsax.Text = dtdata_today.Rows[0]["sex"].ToString();

                string yy = clsconvert.convertdate_YYYY_EN(dtdata_ihs.Rows[0]["patientdob"].ToString());
                string mm = clsconvert.convertdate_MM_EN(dtdata_ihs.Rows[0]["patientdob"].ToString());
                string dd = clsconvert.convertdate_DD_EN(dtdata_ihs.Rows[0]["patientdob"].ToString());

                DateTime birthDate = new DateTime(Convert.ToInt16(yy), Convert.ToInt16(mm), Convert.ToInt16(dd));
                lbage.Text = clsconvert.CalculateAge(birthDate);

                #endregion
                foreach (DataRow i in dtDrugs_today.Rows)
                {
                    foreach (DataGridViewRow dg_rw in dgvHistory.Rows)
                    {
                        if (dg_rw.Cells["orderitemcode"].Value?.ToString() == i["orderitemcode"]?.ToString())
                        {
                            int qtyhis = 0;
                            int qtytoday = 0;
                            string myImageLocation = Application.StartupPath;
                            qtyhis = Convert.ToInt32(dg_rw.Cells["orderqty"].Value?.ToString());
                            qtytoday = Convert.ToInt32(i["orderqty"]?.ToString());

                            //if (qtyhis > qtytoday)
                            //{
                            //    myImageLocation += "\\down.png";
                            //}
                            //else if (qtyhis < qtytoday)
                            //{
                            //    myImageLocation += "\\up.png";
                            //}
                            //else if (qtyhis == qtytoday)
                            //{
                            //    myImageLocation += "\\accept.png";
                            //}
                            //else
                            //{
                            //    myImageLocation += "\\null28.png";
                            //}

                            //Image img;
                            //using (var fs = new FileStream(myImageLocation, FileMode.Open, FileAccess.Read))
                            //{
                            //    img = Image.FromStream(fs);
                            //}

                            //bitmap = GetBitmapFromUrl(myImageLocation);

                            dgvtoday.Rows.Add(bitmap, i["orderitemcode"].ToString(), i["orderitemname"].ToString(), i["orderqty"].ToString(), i["orderunitdesc"].ToString());

                            //break;
                        }
                        else
                        {
                            int qtyhis = 0;
                            int qtytoday = 0;
                            //string myImageLocation = Application.StartupPath;
                            //myImageLocation += "\\null28.jpg";

                            //Image img;
                            //using (var fs = new FileStream(myImageLocation, FileMode.Open, FileAccess.Read))
                            //{
                            //    img = Image.FromStream(fs);
                            //}

                            //bitmap = GetBitmapFromUrl(myImageLocation);
                            dgvtoday.Rows.Add(bitmap, i["orderitemcode"].ToString(), i["orderitemname"].ToString(), i["orderqty"].ToString(), i["orderunitdesc"].ToString());
                            //break;
                        }



                    }
                    


                }
               
                if (dtLabs_today.Rows.Count > 0)
                {
                    labs = "";
                    foreach (DataRow row in dtLabs_today.Rows)
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

                    lslabstoday.Items.Add(labs);
                }
                else
                {
                    lslabstoday.Items.Add(" ..... ไม่มี ..... ");
                }

                


            }

        }

        private async void btnsearch_Click(object sender, EventArgs e)
        {
            searhHistory(txtsearchhn.Text);
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

        private void txtsearchhn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                searhHistory(txtsearchhn.Text);
            }
        }

        private void lbname_Click(object sender, EventArgs e)
        {

        }
    }
}
