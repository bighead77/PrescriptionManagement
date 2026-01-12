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
using ClosedXML.Excel;

namespace PrescriptionManagement
{
    public partial class frm_checkstatus : Form
    {
        clsconvertdate clsconvert = new clsconvertdate();



        public frm_checkstatus()
        {
            InitializeComponent();
        }

        public DataTable dtward;

        public class ComboBoxItem
        {
            public string Text { get; set; }
            public string Value { get; set; }

            public override string ToString()
            {
                return Text; // บังคับให้ ComboBox โชว์ Text
            }
        }
       
        private void btn_search_Click(object sender, EventArgs e)
        {
            string wardname = "";   // ข้อความที่โชว์
            string wardcode = "";

            if (cbb_select.SelectedItem is ComboBoxItem selected)
            {
                wardname = selected.Text;   // ข้อความที่โชว์
                wardcode = selected.Value; // ค่าที่เก็บจริง
            }

            search(wardcode);
        }

        public async void showselect()
        {
            DataTable dt = new DataTable();
            dt = await clsService.RequestGetWard("");
            if (dt.Rows.Count > 0)
            {
                cbb_select.DisplayMember = "warddesc";
                cbb_select.ValueMember = "wardcode";

                cbb_select.Items.Add(new ComboBoxItem { Text = "ทั้งหมด", Value = "All" });
                cbb_select.Items.Add(new ComboBoxItem { Text = "Take Home", Value = "HM" });

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cbb_select.Items.Add(new ComboBoxItem { Text = dt.Rows[i]["warddescTH"].ToString(), Value = dt.Rows[i]["wardcode"].ToString() });
                }

            }
        }

        public async void search(string select)
        {

            DataTable dtw = new DataTable();

            if (select == "HM")
            {
                dtw = await clsService.LoadWardData("Take Home", "Take Home", dateselect.Value);
            }
            else if (select == "All")
            {
                dtw = await clsService.LoadWardData("All", select, dateselect.Value);
            }
            else
            {
                dtw = await clsService.LoadWardData("wardcode", select, dateselect.Value);
            }


            DataView dv = dtw.DefaultView;
            if (txtSearch.Text != "")
            {
                string selected = cbSelect.SelectedItem.ToString().Trim();
                if (selected.Contains("AN"))
                {
                    dv.RowFilter = $"an LIKE '%{txtSearch.Text.TrimEnd()}%' ";

                }
                else if (selected.Contains("เลขที่ใบสั่ง"))
                {
                    dv.RowFilter = $"prescriptionno LIKE '%{txtSearch.Text.TrimEnd()}%' ";
                }
                else if (selected.Contains("HN"))
                {
                    dv.RowFilter = $"hn LIKE '%{txtSearch.Text.TrimEnd()}%' ";
                }
                else if (selected.Contains("ชื่อ"))
                {
                    dv.RowFilter = $"patientname LIKE '%{txtSearch.Text.TrimEnd()}%' ";
                }
            }
            else
            {
                dv = dtw.DefaultView;
            }

            dgvOrderPrescriptionListOrderNo.Rows.Clear();

            string wtt = "";

            for (int i = 0; i < dv.Count; i++)
            {
                string Pres_status = "";
                //string Pres_callDT = "";
                string Pres_transferDT = "";
                string Pres_smartdischargeDT = "";
                string Pres_dispenseDT = "";
                string Pres_dispenseBy = "";



                if (dv[i]["ordertypedesc"].ToString() != "Take Home")
                {
                    Pres_status = dv[i]["status"].ToString();
                }
                else
                {
                    //เช็คข้อมูลจาก ms_queue / เช็คได้แค่ยากลับบ้าน และถูกกดเรียกคิว
                    DataTable tb = new DataTable();
                    tb = await clsService.RequestDataQueue(dv[i]["hn"].ToString());
                    if (tb.Rows.Count > 0)
                    {
                        if (tb.Rows[0]["transferdatetime"].ToString() != "")
                        {
                            Pres_transferDT = tb.Rows[0]["transferdatetime"].ToString();
                        }

                        if (tb.Rows[0]["smartdischargedatetime"].ToString() != "")
                        {
                            Pres_smartdischargeDT = tb.Rows[0]["smartdischargedatetime"].ToString();
                        }

                        if (tb.Rows[0]["medtransferdatetime"].ToString() != "")
                        {
                            Pres_dispenseDT = tb.Rows[0]["medtransferdatetime"].ToString();
                            Pres_dispenseBy = tb.Rows[0]["userpharmacyname"].ToString();


                        }

                        //เช็คสถานะ
                        if (Pres_dispenseDT.ToString() != "")
                        {
                            Pres_status = "จ่ายยาแล้ว";
                        }
                        else
                        {
                            if (dv[i]["callingdatetime"].ToString() != "")
                            {
                                Pres_status = "ถูกเรียกคิว";
                            }
                            else
                            {
                                if (dv[i]["checkoutdatetime"].ToString() != "")
                                {
                                    Pres_status = "ถูกตรวจสอบแล้ว";
                                }
                                else
                                {
                                    Pres_status = dv[i]["status"].ToString();
                                }

                            }
                        }

                    }
                    else
                    {
                        if (dv[i]["callingdatetime"].ToString() != "")
                        {
                            Pres_status = "ถูกเรียกคิว";
                        }
                        else
                        {
                            if (dv[i]["checkoutdatetime"].ToString() != "")
                            {
                                Pres_status = "ถูกตรวจสอบแล้ว";
                            }
                            else
                            {
                                Pres_status = dv[i]["status"].ToString();
                            }

                        }
                    }
                }


                if (Pres_dispenseDT != "" && dv[i]["genorderdatetime"].ToString() != "")
                {
                    DateTime genorderdatetime = DateTime.Parse(dv[i]["genorderdatetime"].ToString());
                    DateTime medtransferdatetime = DateTime.Parse(clsconvert.convertdate_HH_mm_ss_EN_7H(Pres_dispenseDT));

                    // หาส่วนต่าง
                    TimeSpan diff = medtransferdatetime - genorderdatetime;
                    wtt = diff.ToString();
                }
                else
                {
                    wtt = "";
                }


                dgvOrderPrescriptionListOrderNo.Rows.Add("",
                    dv[i]["ordercreatedate"],
                    dv[i]["ordertypedesc"],
                    dv[i]["wardcode"],
                    dv[i]["wardname"],
                    dv[i]["prescriptionno"],
                    dv[i]["hn"],
                    dv[i]["an"],
                    dv[i]["patientname"],
                    dv[i]["sex"],
                    dv[i]["doctorname"],
                    dv[i]["genorderdatetime"],
                    dv[i]["jobdatetime"],
                    "",
                    dv[i]["checkoutdatetime"],
                    dv[i]["callingdatetime"],
                    clsconvert.convertdate_HH_mm_ss_EN_7H(Pres_transferDT),
                    clsconvert.convertdate_HH_mm_ss_EN_7H(Pres_smartdischargeDT),
                    clsconvert.convertdate_HH_mm_ss_EN_7H(Pres_dispenseDT),
                    Pres_dispenseBy,
                    Pres_status,
                    wtt);

            }

            lbPrescount.Text = "จำนวน " + dv.Count + " รายการ";

        }

        private void frm_checkstatus_Load(object sender, EventArgs e)
        {
            showselect();
        }

        private void cbb_select_SelectedIndexChanged(object sender, EventArgs e)
        {
            string wardname = "";   // ข้อความที่โชว์
            string wardcode = "";

            if (cbb_select.SelectedItem is ComboBoxItem selected)
            {
                wardname = selected.Text;   // ข้อความที่โชว์
                wardcode = selected.Value; // ค่าที่เก็บจริง
            }

            search(wardcode);
        }

        private void dgvOrderPrescriptionListOrderNo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex > -1)
            //{
            //    if (dgvOrderPrescriptionListOrderNo.Rows.Count > 0)
            //    {
            //        frm_showItem frmitem = new frm_showItem();

            //        frmitem.fillter(dgvOrderPrescriptionListOrderNo.Rows[e.RowIndex].Cells["prescriptionno"].Value.ToString(), dateselect.Value);
            //        frmitem.Show();
            //    }
            //}

        }

        private void dgvOrderPrescriptionListOrderNo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (dgvOrderPrescriptionListOrderNo.Rows.Count > 0)
                {
                    frm_showItem frmitem = new frm_showItem();

                    frmitem.fillter(dgvOrderPrescriptionListOrderNo.Rows[e.RowIndex].Cells["prescriptionno"].Value.ToString(), dateselect.Value);
                    frmitem.Show();
                }
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            DataTable dt_excel = new DataTable();

            dt_excel.Clear();
            dt_excel.Columns.Add("ordercreatedate", typeof(string));
            dt_excel.Columns.Add("ordertypedesc", typeof(string));
            dt_excel.Columns.Add("wardcode", typeof(string));
            dt_excel.Columns.Add("wardname", typeof(string));
            dt_excel.Columns.Add("prescriptionno", typeof(string));
            dt_excel.Columns.Add("hn", typeof(string));
            dt_excel.Columns.Add("an", typeof(string));
            dt_excel.Columns.Add("patientname", typeof(string));
            dt_excel.Columns.Add("sex", typeof(string));
            dt_excel.Columns.Add("doctorname", typeof(string));

            dt_excel.Columns.Add("genorderdatetime", typeof(string));
            dt_excel.Columns.Add("jobdatetime", typeof(string));
            dt_excel.Columns.Add("matchingdatetime", typeof(string));
            dt_excel.Columns.Add("checkoutdatetime", typeof(string));
            dt_excel.Columns.Add("calldatetime", typeof(string));
            dt_excel.Columns.Add("transferdatetime", typeof(string));
            dt_excel.Columns.Add("smartdischargedatetime", typeof(string));
            dt_excel.Columns.Add("medtransferdatetime", typeof(string));
            dt_excel.Columns.Add("medtransferuser", typeof(string));
            dt_excel.Columns.Add("status", typeof(string));
            dt_excel.Columns.Add("waittingtime", typeof(string));


            if (dgvOrderPrescriptionListOrderNo.RowCount > 0)
            {
                for (int i = 0; i < dgvOrderPrescriptionListOrderNo.RowCount; i++)
                {
                    DataRow r = dt_excel.NewRow();

                    r["ordercreatedate"] = dgvOrderPrescriptionListOrderNo.Rows[i].Cells["ordercreatedate"].Value.ToString();
                    r["ordertypedesc"] = dgvOrderPrescriptionListOrderNo.Rows[i].Cells["ordertypedesc"].Value.ToString();
                    r["wardcode"] = dgvOrderPrescriptionListOrderNo.Rows[i].Cells["wardcode"].Value.ToString();
                    r["wardname"] = dgvOrderPrescriptionListOrderNo.Rows[i].Cells["wardname"].Value.ToString();
                    r["prescriptionno"] = dgvOrderPrescriptionListOrderNo.Rows[i].Cells["prescriptionno"].Value.ToString();
                    r["hn"] = dgvOrderPrescriptionListOrderNo.Rows[i].Cells["hn"].Value.ToString();
                    r["an"] = dgvOrderPrescriptionListOrderNo.Rows[i].Cells["an"].Value.ToString();
                    r["patientname"] = dgvOrderPrescriptionListOrderNo.Rows[i].Cells["patientname"].Value.ToString();
                    r["sex"] = dgvOrderPrescriptionListOrderNo.Rows[i].Cells["sex"].Value.ToString();
                    r["doctorname"] = dgvOrderPrescriptionListOrderNo.Rows[i].Cells["doctorname"].Value.ToString();

                    r["genorderdatetime"] = dgvOrderPrescriptionListOrderNo.Rows[i].Cells["genorderdatetime"].Value.ToString();
                    r["jobdatetime"] = dgvOrderPrescriptionListOrderNo.Rows[i].Cells["jobdatetime"].Value.ToString();
                    r["matchingdatetime"] = dgvOrderPrescriptionListOrderNo.Rows[i].Cells["matchingdatetime"].Value.ToString();
                    r["checkoutdatetime"] = dgvOrderPrescriptionListOrderNo.Rows[i].Cells["checkoutdatetime"].Value.ToString();
                    r["calldatetime"] = dgvOrderPrescriptionListOrderNo.Rows[i].Cells["calldatetime"].Value.ToString();
                    r["transferdatetime"] = dgvOrderPrescriptionListOrderNo.Rows[i].Cells["transferdatetime"].Value.ToString();
                    r["smartdischargedatetime"] = dgvOrderPrescriptionListOrderNo.Rows[i].Cells["smartdischargedatetime"].Value.ToString();
                    r["medtransferdatetime"] = dgvOrderPrescriptionListOrderNo.Rows[i].Cells["medtransferdatetime"].Value.ToString();
                    r["medtransferuser"] = dgvOrderPrescriptionListOrderNo.Rows[i].Cells["medtransferuser"].Value.ToString();
                    r["status"] = dgvOrderPrescriptionListOrderNo.Rows[i].Cells["status"].Value.ToString();
                    r["waittingtime"] = dgvOrderPrescriptionListOrderNo.Rows[i].Cells["waittingtime"].Value.ToString();


                    dt_excel.Rows.Add(r);

                }

                // Path ที่จะบันทึก
                string filePath = @"C:\Temp\export_datatable.xlsx";


                //instal Install-Package ClosedXML


                // Export ด้วย ClosedXML
                using (var workbook = new XLWorkbook())
                {
                    var ws = workbook.Worksheets.Add(dt_excel, "Sheet1"); // ใส่ DataTable ลง Worksheet
                    ws.Columns().AdjustToContents(); // ปรับความกว้างอัตโนมัติ

                    workbook.SaveAs(filePath);
                }

            }
        }

        private void btnExcel_Click_1(object sender, EventArgs e)
        {
            DataTable dt_excel = new DataTable();

            dt_excel.Clear();
            dt_excel.Columns.Add("ordercreatedate", typeof(string));
            dt_excel.Columns.Add("ordertypedesc", typeof(string));
            dt_excel.Columns.Add("wardcode", typeof(string));
            dt_excel.Columns.Add("wardname", typeof(string));
            dt_excel.Columns.Add("prescriptionno", typeof(string));
            dt_excel.Columns.Add("hn", typeof(string));
            dt_excel.Columns.Add("an", typeof(string));
            dt_excel.Columns.Add("patientname", typeof(string));
            dt_excel.Columns.Add("sex", typeof(string));
            dt_excel.Columns.Add("doctorname", typeof(string));

            dt_excel.Columns.Add("genorderdatetime", typeof(string));
            dt_excel.Columns.Add("jobdatetime", typeof(string));
            dt_excel.Columns.Add("matchingdatetime", typeof(string));
            dt_excel.Columns.Add("checkoutdatetime", typeof(string));
            dt_excel.Columns.Add("calldatetime", typeof(string));
            dt_excel.Columns.Add("transferdatetime", typeof(string));
            dt_excel.Columns.Add("smartdischargedatetime", typeof(string));
            dt_excel.Columns.Add("medtransferdatetime", typeof(string));
            dt_excel.Columns.Add("medtransferuser", typeof(string));
            dt_excel.Columns.Add("status", typeof(string));
            dt_excel.Columns.Add("waittingtime", typeof(string));


            if (dgvOrderPrescriptionListOrderNo.RowCount > 0)
            {
                for (int i = 0; i < dgvOrderPrescriptionListOrderNo.RowCount; i++)
                {
                    DataRow r = dt_excel.NewRow();

                    r["ordercreatedate"] = dgvOrderPrescriptionListOrderNo.Rows[i].Cells["ordercreatedate"].Value.ToString();
                    r["ordertypedesc"] = dgvOrderPrescriptionListOrderNo.Rows[i].Cells["ordertypedesc"].Value.ToString();
                    r["wardcode"] = dgvOrderPrescriptionListOrderNo.Rows[i].Cells["wardcode"].Value.ToString();
                    r["wardname"] = dgvOrderPrescriptionListOrderNo.Rows[i].Cells["wardname"].Value.ToString();
                    r["prescriptionno"] = dgvOrderPrescriptionListOrderNo.Rows[i].Cells["prescriptionno"].Value.ToString();
                    r["hn"] = dgvOrderPrescriptionListOrderNo.Rows[i].Cells["hn"].Value.ToString();
                    r["an"] = dgvOrderPrescriptionListOrderNo.Rows[i].Cells["an"].Value.ToString();
                    r["patientname"] = dgvOrderPrescriptionListOrderNo.Rows[i].Cells["patientname"].Value.ToString();
                    r["sex"] = dgvOrderPrescriptionListOrderNo.Rows[i].Cells["sex"].Value.ToString();
                    r["doctorname"] = dgvOrderPrescriptionListOrderNo.Rows[i].Cells["doctorname"].Value.ToString();

                    r["genorderdatetime"] = dgvOrderPrescriptionListOrderNo.Rows[i].Cells["genorderdatetime"].Value.ToString();
                    r["jobdatetime"] = dgvOrderPrescriptionListOrderNo.Rows[i].Cells["jobdatetime"].Value.ToString();
                    r["matchingdatetime"] = dgvOrderPrescriptionListOrderNo.Rows[i].Cells["matchingdatetime"].Value.ToString();
                    r["checkoutdatetime"] = dgvOrderPrescriptionListOrderNo.Rows[i].Cells["checkoutdatetime"].Value.ToString();
                    r["calldatetime"] = dgvOrderPrescriptionListOrderNo.Rows[i].Cells["calldatetime"].Value.ToString();
                    r["transferdatetime"] = dgvOrderPrescriptionListOrderNo.Rows[i].Cells["transferdatetime"].Value.ToString();
                    r["smartdischargedatetime"] = dgvOrderPrescriptionListOrderNo.Rows[i].Cells["smartdischargedatetime"].Value.ToString();
                    r["medtransferdatetime"] = dgvOrderPrescriptionListOrderNo.Rows[i].Cells["medtransferdatetime"].Value.ToString();
                    r["medtransferuser"] = dgvOrderPrescriptionListOrderNo.Rows[i].Cells["medtransferuser"].Value.ToString();
                    r["status"] = dgvOrderPrescriptionListOrderNo.Rows[i].Cells["status"].Value.ToString();
                    r["waittingtime"] = dgvOrderPrescriptionListOrderNo.Rows[i].Cells["waittingtime"].Value.ToString();


                    dt_excel.Rows.Add(r);

                }

                // Path ที่จะบันทึก
                string filePath = @"C:\Temp\export_datatable.xlsx";


                //instal Install-Package ClosedXML


                // Export ด้วย ClosedXML
                using (var workbook = new XLWorkbook())
                {
                    var ws = workbook.Worksheets.Add(dt_excel, "Sheet1"); // ใส่ DataTable ลง Worksheet
                    ws.Columns().AdjustToContents(); // ปรับความกว้างอัตโนมัติ

                    workbook.SaveAs(filePath);
                }

            }
        }

        private void dgvOrderPrescriptionListOrderNo_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (dgvOrderPrescriptionListOrderNo.Rows.Count > 0)
                {
                    frm_showItem frmitem = new frm_showItem();

                    frmitem.fillter(dgvOrderPrescriptionListOrderNo.Rows[e.RowIndex].Cells["prescriptionno"].Value.ToString(), dateselect.Value);
                    frmitem.Show();
                }
            }
        }
    }
}
