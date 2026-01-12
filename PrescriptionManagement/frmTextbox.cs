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
    public partial class frmTextbox : Form
    {
        public bool result = false;
        public string inputText = "";
        public frmTextbox(string mess)
        {
            InitializeComponent();
            lbText.Text = mess;
        }

        private void frmTextbox_Load(object sender, EventArgs e)
        {

        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F1)
            {
                inputText = textBox1.Text;
                result = true;
                this.Close();
                return true;
            }
            else if (keyData == Keys.Escape)
            {                
                result = false;
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnok_Click(object sender, EventArgs e)
        {
            inputText = textBox1.Text;
            result = true;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            inputText = "cancel_void";
            result = true;
            this.Close();
        }
    }
}
