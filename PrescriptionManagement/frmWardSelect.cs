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

namespace PrescriptionManagement
{
    public partial class frmWardSelect : Form
    {
        public class WardResponse
        {
            public string status { get; set; }
            public List<WardItem> data { get; set; }
        }

        public class WardItem
        {
            public string WardCode { get; set; }
            public string WardDescEN { get; set; }
        }
        public frmWardSelect() 
        {
            InitializeComponent();
        }
        public DataTable wardTable;
        private async void frmWardSelect_Load(object sender, EventArgs e)
        {
            wardTable = await LoadWardData();
            SearchWard("");
        }
        private void SearchWard(string keyword)
        {
            if (wardTable != null)
            {
                DataView dv = wardTable.DefaultView;
                if (keyword != "")
                {
                    dv.RowFilter = $"WardCode LIKE '%{keyword}%' OR WardDescEN LIKE '%{keyword}%'";

                }
                dgvWard.Rows.Clear();
                for (int i = 0; i < dv.Count; i++)
                {
                    dgvWard.Rows.Add(dv[i]["WardCode"],
                        dv[i]["WardDescEN"]);
                }
            }
        }

        private async Task<DataTable> LoadWardData()
        {
            string apiUrl = PrescriptionManagement.Properties.Settings.Default.apiUrl + "/emar/ward/all";
            string jsonContent = @"{ }";

            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    // อ่านข้อมูล JSON ที่ได้จาก API
                    string result = await response.Content.ReadAsStringAsync();
                    var wardResponse = JsonConvert.DeserializeObject<WardResponse>(result);

                    DataTable dt = JsonConvert.DeserializeObject<DataTable>(
                        JsonConvert.SerializeObject(wardResponse.data)
                    );

                    return dt;
                }
                else
                {
                    string error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("โหลดข้อมูลไม่สำเร็จ: " + error);
                    return null;
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchWard(txtSearch.Text);
        }

        private void dgvWard_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dgvWard.Rows.Count > 0)
            {
                string wardCode = dgvWard.Rows[dgvWard.CurrentRow.Index].Cells[0].Value.ToString();
                string wardName = dgvWard.Rows[dgvWard.CurrentRow.Index].Cells[1].Value.ToString();

                frmCheckoutHistory frmche = (frmCheckoutHistory)Application.OpenForms["frmCheckoutHistory"];
                if (frmche != null)
                {
                    frmche.SetWardInfo(wardCode, wardName);
                }
                this.Close();

            }
           
        }

        private void pbCleartxtSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
        }
    }
}
