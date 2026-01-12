using PrescriptionManagement.cls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrescriptionManagement
{
    public partial class frm_showItem : Form
    {
        string prescriptionno;
        DateTime prescriptiondate;
        public frm_showItem()
        {
            InitializeComponent();
        }

        public void fillter(string presno, DateTime presdate)
        {
            prescriptionno = presno;

            prescriptiondate = presdate;


        }

        private async void frm_showItem_Load(object sender, EventArgs e)
        {
            clear();


            clsPackagemaster.db_drug = new DataTable();
            clsPackagemaster.db_data = new DataTable();
            clsPackagemaster.db_packagemaster = new DataTable();

            await clsService.RequestPackagemaster(prescriptionno, prescriptiondate);

            if (clsPackagemaster.db_drug.Rows.Count > 0 && clsPackagemaster.db_data.Rows.Count > 0 && clsPackagemaster.db_packagemaster.Rows.Count > 0)
            {
                DataTable db_packagemaster = new DataTable();
                DataTable db_data = new DataTable();
                DataTable db_drug = new DataTable();


                db_packagemaster = clsPackagemaster.db_packagemaster.Select().CopyToDataTable(); // Copy to a new DataTable
                db_data = clsPackagemaster.db_data.Select().CopyToDataTable(); // Copy to a new DataTable
                db_drug = clsPackagemaster.db_drug.Select().CopyToDataTable(); // Copy to a new DataTable

                lbPres.Text = db_data.Rows[0]["prescriptionno"].ToString();
                lbHN.Text = db_data.Rows[0]["hn"].ToString();
                lbAN.Text = db_data.Rows[0]["an"].ToString();
                lbPname.Text = db_data.Rows[0]["patientname"].ToString();

                lbsex.Text = db_data.Rows[0]["sex"].ToString();
                lbWeight.Text = db_data.Rows[0]["weight"].ToString();
                if (db_data.Rows[0]["sex"].ToString() == "1")
                {
                    lbsex.Text = "ชาย";
                }
                else
                {
                    lbsex.Text = "หญิง";
                }
                lbage.Text = db_packagemaster.Rows[0]["age"].ToString();

                if (db_packagemaster.Rows.Count > 0 && db_data.Rows.Count > 0 && db_drug.Rows.Count > 0)
                {
                    #region dgv
                    dg_item.Rows.Clear();
                    dg_item.Refresh();

                    string frequencytext = "";

                    int coutpres = 0;
                    // เพิ่มข้อมูลตัวอย่าง
                    foreach (DataRow i in db_packagemaster.Rows)
                    {
                        coutpres = coutpres + 1;


                        frequencytext = i["instructiondesc"].ToString() + Environment.NewLine;
                        frequencytext += i["frequencytimedesc"].ToString() + Environment.NewLine;
                        frequencytext += "--------------------------------------" + Environment.NewLine;
                        frequencytext += i["freetext1"].ToString() + Environment.NewLine;
                        frequencytext += i["freetext2"].ToString() + Environment.NewLine;
                        frequencytext += i["freetext3"].ToString() + Environment.NewLine;

                        dg_item.Rows.Add(coutpres,
                            i["orderitemcode"].ToString(),
                            i["orderitemname"].ToString(),
                            i["orderqty"].ToString(),
                            i["dosageunitcode"].ToString(),
                            frequencytext);

                        // รีเฟรช DataGridView


                    }

                    lbPrescount.Text = "รายการยา " + coutpres;

                    dg_item.ClearSelection();

                    #endregion
                }
            }


        }


        public void clear()
        {
            // txtScanOrder.Enabled = true;

            lbHN.Text = "";
            lbAN.Text = "";
            lbPname.Text = "";
            lbPres.Text = "";
            lbsex.Text = "";
            lbWeight.Text = "";
            lbage.Text = "";

            dg_item.Rows.Clear();

            lbPrescount.Text = "รายการยา 0 ";



        }
    }
}
