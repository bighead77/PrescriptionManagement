using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

namespace PrescriptionManagement
{
    public partial class frmMederror : Form
    {
        public class ErrorItem
        {
            public string errordetailcode { get; set; }
            public string errordetail { get; set; }
            public string errortypecode { get; set; }
            public string errortypedesc { get; set; }
        }
        public class TypeItem
        {
            public string errortypecode { get; set; }
            public string errortypedesc { get; set; }
        }
        public MedErrorItem MedItem { get; set; }
        public class MedErrorItem
        {
            public string _id_prescription { get; set; }
            public DateTime ordercreatedate { get; set; }
            public string prescriptionno { get; set; }
            public string hn { get; set; }
            public string vn { get; set; }
            public string orderitemcode { get; set; }
            public string orderitemname { get; set; }
            public string orderunitdesc { get; set; }
            public string orderqty { get; set; }
            public string shelfzone { get; set; }
            public string shelfname { get; set; }
            public string jobuserid { get; set; }
            public string jobusername { get; set; }
            public string mederror_desc { get; set; }
            public string mederror_freetext { get; set; }
            public string mederror_robot { get; set; }
            public string mederror_userid { get; set; }
            public string mederror_username { get; set; }
            public string mederror_type { get; set; }
            public DateTime createddate { get; set; }
            public DateTime dateupdate { get; set; }
        }
        List<ErrorItem> allErrors = new List<ErrorItem>();

        public MedErrorItem ReceivedItem { get; set; }

        public frmMederror(MedErrorItem item)

        {
            InitializeComponent();
            ReceivedItem = item;
        }

        private async void frmMederror_Load(object sender, EventArgs e)
        {  
           
            if (ReceivedItem != null)
            {
                txt_id.Text = ReceivedItem._id_prescription;
                txtprescriptionno.Text = ReceivedItem.prescriptionno;
                txthn.Text = ReceivedItem.hn;
                txtan.Text = ReceivedItem.vn;
                txtOrderItemCode.Text = ReceivedItem.orderitemcode;
                txtOrderItemName.Text = ReceivedItem.orderitemname;
                txtshelfzone.Text = ReceivedItem.shelfzone;
                txtshelfname.Text = ReceivedItem.shelfname;
                txtUnit.Text = ReceivedItem.orderunitdesc;
                txtQty.Text = ReceivedItem.orderqty?.ToString(); // ถ้าเป็น float?
                txtRemark.Text = ReceivedItem.mederror_freetext;
                lbuserid.Text = ReceivedItem.mederror_userid;
                lbusername.Text = ReceivedItem.mederror_username;
            }

            await LoadErrorData();
            cbType.SelectedIndexChanged += cbType_SelectedIndexChanged;
        }
        private async Task LoadErrorData()
        {
            string apiUrl = PrescriptionManagement.Properties.Settings.Default.apiUrl + "/emar/medicationerrordetail";
            string jsonContent = @"{  }";

            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var jObj = JObject.Parse(json);
                    var jArray = (JArray)jObj["data"];
                    allErrors = jArray.ToObject<List<ErrorItem>>();

                    List<TypeItem> typeList = new List<TypeItem>();

                    for (int i = 0; i < allErrors.Count; i++)
                    {
                        var current = allErrors[i];
                        bool exists = false;

                        for (int j = 0; j < typeList.Count; j++)
                        {
                            //if (typeList[j].errortypecode == current.errortypecode &&
                            //    typeList[j].errortypedesc == current.errortypedesc)
                                if (typeList[j].errortypedesc == current.errortypedesc)
                                {
                                exists = true;
                                break;
                            }
                        }

                        if (!exists)
                        {
                            typeList.Add(new TypeItem
                            {
                                errortypecode = current.errortypecode,
                                errortypedesc = current.errortypedesc
                            });
                        }
                    }

                    cbType.DataSource = typeList;
                    cbType.DisplayMember = "errortypedesc";
                    cbType.ValueMember = "errortypecode";

                    if (cbType.SelectedValue != null)
                    {
                        string selectedTypeCode = cbType.SelectedValue.ToString();

                        List<ErrorItem> filtered = new List<ErrorItem>();

                        for (int i = 0; i < allErrors.Count; i++)
                        {
                            if (allErrors[i].errortypecode == selectedTypeCode)
                            {
                                filtered.Add(allErrors[i]);
                            }
                        }

                        cbDetail.DataSource = filtered;
                        cbDetail.DisplayMember = "errordetail";
                        cbDetail.ValueMember = "errordetailcode";
                    }
                }
                else
                {
                    MessageBox.Show("โหลดข้อมูลไม่สำเร็จ");
                }
            }
        }
        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbType.SelectedValue != null)
            {
                string selectedTypeCode = cbType.SelectedValue.ToString();

                List<ErrorItem> filtered = new List<ErrorItem>();

                for (int i = 0; i < allErrors.Count; i++)
                {
                    if (allErrors[i].errortypecode == selectedTypeCode)
                    {
                        filtered.Add(allErrors[i]);
                    }
                }

                cbDetail.DataSource = filtered;
                cbDetail.DisplayMember = "errordetail";
                cbDetail.ValueMember = "errordetailcode";
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnok_Click(object sender, EventArgs e)
        {
            var cDetailid = cbDetail.SelectedValue;
            var ctype = cbType.SelectedValue;
            var item = new MedErrorItem
            {
                _id_prescription = txt_id.Text,
                ordercreatedate = DateTime.Now,
                prescriptionno = txtprescriptionno.Text,
                hn = txthn.Text,
                vn = txtan.Text,
                orderitemcode = txtOrderItemCode.Text,
                orderitemname = txtOrderItemName.Text,
                orderunitdesc = txtUnit.Text,
                orderqty = txtQty.Text,
                shelfzone = txtshelfzone.Text,
                shelfname = txtshelfname.Text,
                jobuserid = "",
                jobusername = "",
                mederror_desc = cbDetail.Text,
                mederror_freetext = txtRemark.Text,
                mederror_robot = cDetailid?.ToString(),
                mederror_userid = lbuserid.Text,
                mederror_username = lbusername.Text,             
                mederror_type = ctype?.ToString(),
                createddate = DateTime.Now,
                dateupdate = DateTime.Now
            };

            var service = new MedErrorService();
            bool success = await service.SaveMedErrorAsync(item);

            if (success)
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("บันทึกไม่สำเร็จ");
            }
        }
        public class MedErrorService
        {
            public async Task<bool> SaveMedErrorAsync(MedErrorItem item)
            {
                string url = PrescriptionManagement.Properties.Settings.Default.apiUrl + "/general/mederror/update";

                using (HttpClient client = new HttpClient())
                {
                    string json = JsonConvert.SerializeObject(item);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        return true; // สำเร็จ
                    }
                    else
                    {
                        return false; // ล้มเหลว
                    }
                }
            }
        }

    }
}
