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
    public partial class frmShowpic : Form
    {
        public bool result = false;
        string name = "";
        string code = "";
        float zoomFactor = 1.0f;
        Image originalImage;
        public frmShowpic(string _code,string _name)
        {
            InitializeComponent();
            code = _code;
            name = _name;
        }

        private void frmShowpic_Load(object sender, EventArgs e)
        {
            label1.Text = code;
            label2.Text = name;

            pic1.Load(PrescriptionManagement.Properties.Settings.Default.drugpicUrl + $@"/{code}" + "/1");
            pic2.Load(PrescriptionManagement.Properties.Settings.Default.drugpicUrl + $@"/{code}" + "/2");
            pic3.Load(PrescriptionManagement.Properties.Settings.Default.drugpicUrl + $@"/{code}" + "/3");
            pic4.Load(PrescriptionManagement.Properties.Settings.Default.drugpicUrl + $@"/{code}" + "/4");
            pic5.Load(PrescriptionManagement.Properties.Settings.Default.drugpicUrl + $@"/{code}" + "/5");
            pic6.Load(PrescriptionManagement.Properties.Settings.Default.drugpicUrl + $@"/{code}" + "/6");
            pic7.Load(PrescriptionManagement.Properties.Settings.Default.drugpicUrl + $@"/{code}" + "/7");
            pic8.Load(PrescriptionManagement.Properties.Settings.Default.drugpicUrl + $@"/{code}" + "/8");


            //pic1.Image = Image.FromFile(PrescriptionManagement.Properties.Settings.Default.drugpicUrl + $@"/{code}" + "/1.jpg");
            //pic2.Image = Image.FromFile(PrescriptionManagement.Properties.Settings.Default.drugpicUrl + $@"/{code}" + "/2.jpg");
            //pic3.Image = Image.FromFile(PrescriptionManagement.Properties.Settings.Default.drugpicUrl + $@"/{code}" + "/3.jpg");
            //pic4.Image = Image.FromFile(PrescriptionManagement.Properties.Settings.Default.drugpicUrl + $@"\{code}" + "/4.jpg");
            //pic5.Image = Image.FromFile(PrescriptionManagement.Properties.Settings.Default.drugpicUrl + $@"\{code}" + "/5.jpg");
            //pic6.Image = Image.FromFile(PrescriptionManagement.Properties.Settings.Default.drugpicUrl + $@"\{code}" + "/6.jpg");
            //pic7.Image = Image.FromFile(PrescriptionManagement.Properties.Settings.Default.drugpicUrl + $@"\{code}" + "/7.jpg");
            //pic8.Image = Image.FromFile(PrescriptionManagement.Properties.Settings.Default.drugpicUrl + $@"\{code}" + "/8.jpg");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            result = true;
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pic1_Click(object sender, EventArgs e)
        {
            int imgW = 0;
            int imgH = 0;
            originalImage = pic1.Image;
            imgW = originalImage.Width / 3;
            imgH = originalImage.Height / 3;

            if (originalImage == null) // ป้องกัน null
            {
                MessageBox.Show("ยังไม่ได้โหลดภาพ");
                return;
            }

            zoomFactor = 1.5f;

            // จำกัดค่าไม่ให้เล็ก/ใหญ่เกินไป
            zoomFactor = Math.Max(0.1f, Math.Min(zoomFactor, 10f));

            int newWidth = (int)(imgW * zoomFactor);
            int newHeight = (int)(imgH * zoomFactor);

            Bitmap zoomedImage = new Bitmap(originalImage, newWidth, newHeight);
            //pic1.Image = zoomedImage;
            piczoom.Width = newWidth;
            piczoom.Height = newHeight;
            piczoom.Image = zoomedImage;
            piczoom.Visible = true;
            piczoom.BringToFront();
        }

        private void pic2_Click(object sender, EventArgs e)
        {
            int imgW = 0;
            int imgH = 0;
            originalImage = pic2.Image;
            imgW = originalImage.Width / 3;
            imgH = originalImage.Height / 3;

            if (originalImage == null) // ป้องกัน null
            {
                MessageBox.Show("ยังไม่ได้โหลดภาพ");
                return;
            }

            zoomFactor = 1.5f;

            // จำกัดค่าไม่ให้เล็ก/ใหญ่เกินไป
            zoomFactor = Math.Max(0.1f, Math.Min(zoomFactor, 10f));

            int newWidth = (int)(imgW * zoomFactor);
            int newHeight = (int)(imgH * zoomFactor);

            Bitmap zoomedImage = new Bitmap(originalImage, newWidth, newHeight);
            //pic1.Image = zoomedImage;
            piczoom.Width = newWidth;
            piczoom.Height = newHeight;
            piczoom.Image = zoomedImage;
            piczoom.Visible = true;
            piczoom.BringToFront();
        }

        private void pic3_Click(object sender, EventArgs e)
        {
            int imgW = 0;
            int imgH = 0;
            originalImage = pic3.Image;
            imgW = originalImage.Width / 3;
            imgH = originalImage.Height / 3;

            if (originalImage == null) // ป้องกัน null
            {
                MessageBox.Show("ยังไม่ได้โหลดภาพ");
                return;
            }

            zoomFactor = 1.5f;

            // จำกัดค่าไม่ให้เล็ก/ใหญ่เกินไป
            zoomFactor = Math.Max(0.1f, Math.Min(zoomFactor, 10f));

            int newWidth = (int)(imgW * zoomFactor);
            int newHeight = (int)(imgH * zoomFactor);

            Bitmap zoomedImage = new Bitmap(originalImage, newWidth, newHeight);
            //pic1.Image = zoomedImage;
            piczoom.Width = newWidth;
            piczoom.Height = newHeight;
            piczoom.Image = zoomedImage;
            piczoom.Visible = true;
            piczoom.BringToFront();
        }

        private void pic4_Click(object sender, EventArgs e)
        {
            int imgW = 0;
            int imgH = 0;
            originalImage = pic4.Image;
            imgW = originalImage.Width / 3;
            imgH = originalImage.Height / 3;

            if (originalImage == null) // ป้องกัน null
            {
                MessageBox.Show("ยังไม่ได้โหลดภาพ");
                return;
            }

            zoomFactor = 1.5f;

            // จำกัดค่าไม่ให้เล็ก/ใหญ่เกินไป
            zoomFactor = Math.Max(0.1f, Math.Min(zoomFactor, 10f));

            int newWidth = (int)(imgW * zoomFactor);
            int newHeight = (int)(imgH * zoomFactor);

            Bitmap zoomedImage = new Bitmap(originalImage, newWidth, newHeight);
            //pic1.Image = zoomedImage;
            piczoom.Width = newWidth;
            piczoom.Height = newHeight;
            piczoom.Image = zoomedImage;
            piczoom.Visible = true;
            piczoom.BringToFront();
        }

        private void pic5_Click(object sender, EventArgs e)
        {
            int imgW = 0;
            int imgH = 0;
            originalImage = pic5.Image;
            imgW = originalImage.Width / 3;
            imgH = originalImage.Height / 3;

            if (originalImage == null) // ป้องกัน null
            {
                MessageBox.Show("ยังไม่ได้โหลดภาพ");
                return;
            }

            zoomFactor = 1.5f;

            // จำกัดค่าไม่ให้เล็ก/ใหญ่เกินไป
            zoomFactor = Math.Max(0.1f, Math.Min(zoomFactor, 10f));

            int newWidth = (int)(imgW * zoomFactor);
            int newHeight = (int)(imgH * zoomFactor);

            Bitmap zoomedImage = new Bitmap(originalImage, newWidth, newHeight);
            //pic1.Image = zoomedImage;
            piczoom.Width = newWidth;
            piczoom.Height = newHeight;
            piczoom.Image = zoomedImage;
            piczoom.Visible = true;
            piczoom.BringToFront();
        }

        private void pic6_Click(object sender, EventArgs e)
        {
            int imgW = 0;
            int imgH = 0;
            originalImage = pic6.Image;
            imgW = originalImage.Width / 3;
            imgH = originalImage.Height / 3;

            if (originalImage == null) // ป้องกัน null
            {
                MessageBox.Show("ยังไม่ได้โหลดภาพ");
                return;
            }

            zoomFactor = 1.5f;

            // จำกัดค่าไม่ให้เล็ก/ใหญ่เกินไป
            zoomFactor = Math.Max(0.1f, Math.Min(zoomFactor, 10f));

            int newWidth = (int)(imgW * zoomFactor);
            int newHeight = (int)(imgH * zoomFactor);

            Bitmap zoomedImage = new Bitmap(originalImage, newWidth, newHeight);
            //pic1.Image = zoomedImage;
            piczoom.Width = newWidth;
            piczoom.Height = newHeight;
            piczoom.Image = zoomedImage;
            piczoom.Visible = true;
            piczoom.BringToFront();
        }

        private void pic7_Click(object sender, EventArgs e)
        {
            int imgW = 0;
            int imgH = 0;
            originalImage = pic7.Image;
            imgW = originalImage.Width/3;
            imgH = originalImage.Height /3;

            if (originalImage == null) // ป้องกัน null
            {
                MessageBox.Show("ยังไม่ได้โหลดภาพ");
                return;
            }

            zoomFactor = 1.5f;

            // จำกัดค่าไม่ให้เล็ก/ใหญ่เกินไป
            zoomFactor = Math.Max(0.1f, Math.Min(zoomFactor, 10f));

            int newWidth = (int)(imgW * zoomFactor);
            int newHeight = (int)(imgH * zoomFactor);

            Bitmap zoomedImage = new Bitmap(originalImage, newWidth, newHeight);
            //pic1.Image = zoomedImage;
            piczoom.Width = newWidth;
            piczoom.Height = newHeight;
            piczoom.Image = zoomedImage;
            //piczoom.SizeMode = 
            piczoom.Visible = true;
            piczoom.BringToFront();
        }

        private void pic8_Click(object sender, EventArgs e)
        {
            int imgW = 0;
            int imgH = 0;
            originalImage = pic8.Image;
            imgW = originalImage.Width / 3;
            imgH = originalImage.Height / 3;

            if (originalImage == null) // ป้องกัน null
            {
                MessageBox.Show("ยังไม่ได้โหลดภาพ");
                return;
            }

            zoomFactor = 1.5f;

            // จำกัดค่าไม่ให้เล็ก/ใหญ่เกินไป
            zoomFactor = Math.Max(0.1f, Math.Min(zoomFactor, 10f));

            int newWidth = (int)(imgW * zoomFactor);
            int newHeight = (int)(imgH * zoomFactor);

            Bitmap zoomedImage = new Bitmap(originalImage, newWidth, newHeight);
            piczoom.Width = newWidth;
            piczoom.Height = newHeight;
            piczoom.Image = zoomedImage;
            piczoom.Visible = true;
            piczoom.BringToFront();
        }

        private void piczoom_Click(object sender, EventArgs e)
        {
            piczoom.Visible = false;
        }
    }
}
