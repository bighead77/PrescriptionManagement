using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PrescriptionManagement.cls;
using System.Globalization;
using Newtonsoft.Json.Linq;

namespace PrescriptionManagement
{
    public partial class frmCheckoutHistory : Form
    {
        clsconvertdate clsconvert = new clsconvertdate();
        public class WardResponse
        {
            public string status { get; set; }
            public List<WardItem> data { get; set; }
        }

        public class WardItem
      
        {
            public string _id { get; set; }
            public string prescriptionno { get; set; }
            public string an { get; set; }
            public string doctorname { get; set; }
            public string hn { get; set; }
            public string matchingdatetime { get; set; }
            public string ordertypedesc { get; set; }
            public string patientname { get; set; }
            public string wardcode { get; set; }
            public string wardname { get; set; }
            public string ordercreatedate { get; set; }
            public string genorderdatetime { get; set; }
            public string jobdatetime { get; set; }
            public string checkoutdatetime { get; set; }
            public string callingdatetime { get; set; }
            public string medtransferdatetime { get; set; }
            public string voiddatetime { get; set; }
            public string sex { get; set; }
            public string status { get; set; }
        }
        public frmCheckoutHistory(string wardCode, string wardName)
        {
            InitializeComponent();
            lbwardcode.Text = wardCode;
            lbwardname.Text = wardName;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var wardForm = new frmWardSelect(); // สร้าง instance
            wardForm.Show();
        }
        public DataTable dtward;
        public async void SetWardInfo(string wardCode, string wardName)
        {
            lbwardcode.Text = wardCode;
            lbwardname.Text = wardName;


            dtward = await LoadWardData();

            if(dtward.Rows.Count > 0)
            {
                foreach(DataRow rw in dtward.Rows)
                {
                    if(rw["ordertypedesc"].ToString() == "Take Home")
                    {

                    }
                }
                //dgvOrderPrescriptionListOrderNo.DataSource = dtward;
                DataView dv = dtward.DefaultView;

                //if (keyword != "")
                //{
                //    dv.RowFilter = $"WardCode LIKE '%{keyword}%' OR WardDescEN LIKE '%{keyword}%'";

                //}

                dgvOrderPrescriptionListOrderNo.Rows.Clear();
                //for (int i = 0; i < dv.Count; i++)
                //{

                //    dgvOrderPrescriptionListOrderNo.Rows.Add("",
                //        dv[i]["ordercreatedate"],
                //        dv[i]["ordertypedesc"],
                //        dv[i]["prescriptionno"],
                //        dv[i]["hn"],
                //        dv[i]["an"],
                //        dv[i]["patientname"],
                //        dv[i]["sex"],
                //        dv[i]["doctorname"],
                //        dv[i]["genorderdatetime"],
                //        "",
                //        "jobname",
                //        dv[i]["checkoutdatetime"],
                //        "checkname",
                //        "",
                //        "",
                //        "medtrasfername",
                //        dv[i]["status"]);

                //}

                
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
                    //MessageBox.Show(dv[i]["hn"].ToString());
                    //เช็คข้อมูลจาก ms_queue / เช็คได้แค่ยากลับบ้าน และถูกกดเรียกคิว
                    DataTable tb = new DataTable();
                    tb = await clsService.RequestDataQueue(dv[i]["hn"].ToString(), "HN");
                    //if (tb.Rows.Count > 0 && dv[i]["hn"].ToString() == "64119107")
                    if (tb.Rows.Count > 0)
                    {
                       
                        var filteredRows = tb.AsEnumerable()
                                             .Where(r => clsconvert.date_utcYMD(r.Field<string>("createddate").ToString()) == clsconvert.date_utcYMD(datecheckout.Value.ToString()));

                        DataTable resultTable = filteredRows.Any() ? filteredRows.CopyToDataTable() : tb.Clone();

                        if (resultTable.Rows.Count > 0)
                        {
                            if (resultTable.Rows[0]["transferdatetime"].ToString() != "")
                            {
                                Pres_transferDT = resultTable.Rows[0]["transferdatetime"].ToString();
                            }

                            if (resultTable.Rows[0]["smartdischargedatetime"].ToString() != "")
                            {
                                Pres_smartdischargeDT = resultTable.Rows[0]["smartdischargedatetime"].ToString();
                            }

                            if (resultTable.Rows[0]["medtransferdatetime"].ToString() != "")
                            {
                                Pres_dispenseDT = resultTable.Rows[0]["medtransferdatetime"].ToString();
                                Pres_dispenseBy = resultTable.Rows[0]["userpharmacyname"].ToString();
                            }
                        }
                        else
                        {
                            //if (tb.Rows[0]["transferdatetime"].ToString() != "")
                            //{
                            //    Pres_transferDT = tb.Rows[0]["transferdatetime"].ToString();
                            //}

                            //if (tb.Rows[0]["smartdischargedatetime"].ToString() != "")
                            //{
                            //    Pres_smartdischargeDT = tb.Rows[0]["smartdischargedatetime"].ToString();
                            //}

                            //if (tb.Rows[0]["medtransferdatetime"].ToString() != "")
                            //{
                            //    Pres_dispenseDT = tb.Rows[0]["medtransferdatetime"].ToString();
                            //    Pres_dispenseBy = tb.Rows[0]["userpharmacyname"].ToString();
                            //}
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
                    dv[i]["jobusername"],
                    "",
                    dv[i]["checkoutdatetime"],
                    dv[i]["checkoutusername"],                    
                    dv[i]["callingdatetime"],
                    clsconvert.convertdate_HH_mm_ss_EN_7H(Pres_transferDT),
                    clsconvert.convertdate_HH_mm_ss_EN_7H(Pres_smartdischargeDT),
                    clsconvert.convertdate_HH_mm_ss_EN_7H(Pres_dispenseDT),
                    Pres_dispenseBy,
                    Pres_status,
                    wtt);

            }
            }

        }
        public void search()           
        {
            DataView dv = dtward.DefaultView;
            if (txtSearch.Text != "")
            {
                string selected = cbSelect.SelectedItem.ToString();
                if (selected == "AN")
                {
                    dv.RowFilter = $"an LIKE '%{txtSearch.Text.TrimEnd()}%' ";

                }
                else if (selected == "เลขที่ใบสั่งยา")
                {
                    dv.RowFilter = $"prescriptionno LIKE '%{txtSearch.Text.TrimEnd()}%' ";
                }
                else if (selected == "HN")
                {
                    dv.RowFilter = $"hn LIKE '%{txtSearch.Text.TrimEnd()}%' ";
                }
                else if (selected == "ชื่อ")
                {
                    dv.RowFilter = $"patientname LIKE '%{txtSearch.Text.TrimEnd()}%' ";
                }
            }
            else
            {
                dv = dtward.DefaultView;
            }          
           
            dgvOrderPrescriptionListOrderNo.Rows.Clear();
            for (int i = 0; i < dv.Count; i++)
            {
                dgvOrderPrescriptionListOrderNo.Rows.Add("",
                    dv[i]["ordercreatedate"],
                    dv[i]["ordertypedesc"],
                    dv[i]["prescriptionno"],
                    dv[i]["hn"],
                    dv[i]["an"],
                    dv[i]["patientname"],
                    dv[i]["sex"],
                    dv[i]["doctorname"],
                    dv[i]["genorderdatetime"],
                    "",
                    "",
                    dv[i]["checkoutdatetime"],
                    "",
                    "",
                    "",
                    dv[i]["status"]);

            }

        }

        private async Task<DataTable> LoadWardData()
        {
            string apiUrl = PrescriptionManagement.Properties.Settings.Default.apiUrl + "/order/prescriptionstatusward";
            
            string startDate = datecheckout.Value.ToString("yyyy-MM-dd", CultureInfo.GetCultureInfo("en-US"));
            string endDate = datecheckout.Value.ToString("yyyy-MM-dd", CultureInfo.GetCultureInfo("en-US"));
            string wardCode = lbwardcode.Text;
            DataTable db = new DataTable();
            string jsonContent = $@"
                {{
                    ""startdate"": ""{startDate}"",
                    ""enddate"": ""{endDate}"",
                    ""wardcode"": ""{wardCode}""
                }}";

            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {

                    string responseBody = await response.Content.ReadAsStringAsync();
                    //responseBody = responseBody.Substring(1, responseBody.Length - 2);
                    //Console.WriteLine(responseBody);
                    JObject obj = JObject.Parse(responseBody);
                    if (obj["status"].ToString() == "200")
                    {
                        Console.WriteLine($"Find Success");

                        JArray dataArray = (JArray)obj["data"];
                        DataTable dtobjdata = new DataTable();

                        // แปลง JArray เป็น List ของ Dictionary
                        var objList = dataArray.ToObject<List<Dictionary<string, object>>>();

                        if (objList.Count > 0)
                        {
                            // เพิ่มคอลัมน์ครั้งเดียวจาก object แรก
                            foreach (var key in objList[0].Keys)
                            {
                                dtobjdata.Columns.Add(key, typeof(object));
                            }

                            // เติมแถว
                            foreach (var item in objList)
                            {
                                DataRow row = dtobjdata.NewRow();

                                foreach (var key in item.Keys)
                                {
                                    row[key] = item[key] ?? DBNull.Value;
                                }

                                dtobjdata.Rows.Add(row);
                            }
                        }

                        return dtobjdata;
                    }
                    else
                    {
                        Console.WriteLine($"Can't Find Success");
                        return null;
                    }
                }
                else
                {
                    string error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("โหลดข้อมูลไม่สำเร็จ: " + error);
                    return null;
                }
            }
        }
        private void frmCheckoutHistory_Load(object sender, EventArgs e)
        {

        }

        private void datecheckout_ValueChanged(object sender, EventArgs e)
        {
            SetWardInfo(lbwardcode.Text, lbwardname.Text);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (dtward.Rows .Count > 0)
            {
                search();
            }
            else
            {
                MessageBox.Show("กรุณาเลือกหอผู้ป่วย");
            }
            
        }
    }
}
