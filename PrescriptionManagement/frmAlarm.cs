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
    public partial class frmAlarm : Form
    {
        string text = "";
        public frmAlarm(string mess)
        {
            InitializeComponent();
            text = mess;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();            
            timer1.Enabled = false;
            this.Close();
        }

        private void frmAlarm_Load(object sender, EventArgs e)
        {
            label1.Text = text;
            timer1.Enabled = true;
            timer1.Start();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
