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
    public partial class frmDrugallergies : Form
    {
        public bool result = false;
        string ministry = "";
        string his = "";
        DataTable dt_drugallergies = new DataTable();
        public frmDrugallergies(DataTable tb)
        {
            InitializeComponent();
            dt_drugallergies = tb;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();  // ปิดฟอร์ม
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void frmDrugallergies_Load(object sender, EventArgs e)
        {
            if (dt_drugallergies.Rows.Count > 0)
            {
                string drugallergies_his = "";
                string drugallergies_ministry = "";
                lsdrugallergies_his.Items.Clear();
                lshispharmaco.Items.Clear();
               
                foreach (DataRow row in dt_drugallergies.Rows)
                {

                    if (row["drugallergytype"].ToString() == "his")
                    {
                        drugallergies_his = Environment.NewLine;
                        lsdrugallergies_his.Items.Add(drugallergies_his);
                        drugallergies_his = "Genericname : " + row["genericname"].ToString();
                        lsdrugallergies_his.Items.Add(drugallergies_his);
                  
                        drugallergies_his = Environment.NewLine + "adverbs : " + row["adverbs"].ToString();
                        lsdrugallergies_his.Items.Add(drugallergies_his);
                   
                        drugallergies_his = Environment.NewLine + "memo : " + row["memo"].ToString();
                        lsdrugallergies_his.Items.Add(drugallergies_his);
                      
                    }
                    else 
                    {
                        drugallergies_ministry = Environment.NewLine;
                        lshispharmaco.Items.Add(drugallergies_ministry);
                        drugallergies_ministry = "Genericname : " + row["genericname"].ToString();
                        lshispharmaco.Items.Add(drugallergies_ministry);
                      
                        drugallergies_ministry = Environment.NewLine + "adverbs : " + row["adverbs"].ToString();
                        lshispharmaco.Items.Add(drugallergies_ministry);
                   
                        drugallergies_ministry = Environment.NewLine + "memo : " + row["memo"].ToString();
                        lshispharmaco.Items.Add(drugallergies_ministry);
                        
                    }
                }
            }
            else
            {

            }
        }

        private void btn_confirm_Click(object sender, EventArgs e)
        {
            result = true;
            this.Close();
        }
    }
}
