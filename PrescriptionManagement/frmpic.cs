using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrescriptionManagement
{
    public partial class frmpic : Form
    {
        Bitmap pic;
        public frmpic(Bitmap _pic)
        {
            InitializeComponent();

            _ZoomFactor = trbZoomFactor.Value;
            _BackColor = picImage.BackColor;

            picImage.SizeMode = PictureBoxSizeMode.CenterImage;
            picZoom.SizeMode = PictureBoxSizeMode.StretchImage;
            pic = _pic;
        }

        private int _ZoomFactor;
       
        private Color _BackColor;
       
        private Image _OriginalImage;

       
        private void btnSelectColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            colorDialog.AllowFullOpen = true;
            colorDialog.AnyColor = true;
            colorDialog.Color = _BackColor;
            colorDialog.FullOpen = true;
            colorDialog.ShowHelp = true;
            colorDialog.SolidColorOnly = false;

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                _BackColor = colorDialog.Color;
                ResizeAndDisplayImage();
            }

            colorDialog.Dispose();
        }

       
        private void trbZoomFactor_ValueChanged(object sender, EventArgs e)
        {
            _ZoomFactor = trbZoomFactor.Value;
            lblZoomFactor.Text = string.Format("x{0}", _ZoomFactor);
        }

      
        private void picImage_MouseMove(object sender, MouseEventArgs e)
        {
            // If no picture is loaded, return
            if (picImage.Image == null)
                return;

            UpdateZoomedImage(e);
        }

       
        private void ResizeAndDisplayImage()
        {
            
            picImage.BackColor = _BackColor;
            picZoom.BackColor = _BackColor;

            if (_OriginalImage == null)
                return;

            picImage.Image = _OriginalImage;
            return;
           
            int sourceWidth = _OriginalImage.Width;
            int sourceHeight = _OriginalImage.Height;
            int targetWidth;
            int targetHeight;
            double ratio;

            
            if (sourceWidth > sourceHeight)
            {
               
                targetWidth = picImage.Width;
                ratio = (double)targetWidth / sourceWidth;
                targetHeight = (int)(ratio * sourceHeight);
            }
            else if (sourceWidth < sourceHeight)
            {
               
                targetHeight = picImage.Height;               
                ratio = (double)targetHeight / sourceHeight;
                targetWidth = (int)(ratio * sourceWidth);
            }
            else
            {
                targetHeight = picImage.Height;
                targetWidth = picImage.Width;
            }

            int targetTop = (picImage.Height - targetHeight) / 2;
            int targetLeft = (picImage.Width - targetWidth) / 2;

            Bitmap tempBitmap = new Bitmap(picImage.Width, picImage.Height, PixelFormat.Format24bppRgb);

            tempBitmap.SetResolution(_OriginalImage.HorizontalResolution, _OriginalImage.VerticalResolution);

            Graphics bmGraphics = Graphics.FromImage(tempBitmap);

            bmGraphics.Clear(_BackColor);

            bmGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            bmGraphics.DrawImage(_OriginalImage,
                                 new Rectangle(targetLeft, targetTop, targetWidth, targetHeight),
                                 new Rectangle(0, 0, sourceWidth, sourceHeight),
                                 GraphicsUnit.Pixel);
        
            bmGraphics.Dispose();
            picImage.Image = tempBitmap;
        }

        private void UpdateZoomedImage(MouseEventArgs e)
        {
            int zoomWidth = picZoom.Width / _ZoomFactor;
            int zoomHeight = picZoom.Height / _ZoomFactor;

            int halfWidth = zoomWidth / 2;
            int halfHeight = zoomHeight / 2;

            Bitmap tempBitmap = new Bitmap(zoomWidth, zoomHeight, PixelFormat.Format24bppRgb);

            Graphics bmGraphics = Graphics.FromImage(tempBitmap);

            bmGraphics.Clear(_BackColor);

            // Set the interpolation mode
            bmGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            bmGraphics.DrawImage(picImage.Image,
                                 new Rectangle(0, 0, zoomWidth, zoomHeight),
                                 new Rectangle(e.X - halfWidth, e.Y - halfHeight, zoomWidth, zoomHeight),
                                 GraphicsUnit.Pixel);

            picZoom.Image = tempBitmap;

            bmGraphics.DrawLine(Pens.Black, halfWidth + 1, halfHeight - 4, halfWidth + 1, halfHeight - 1);
            bmGraphics.DrawLine(Pens.Black, halfWidth + 1, halfHeight + 6, halfWidth + 1, halfHeight + 3);
            bmGraphics.DrawLine(Pens.Black, halfWidth - 4, halfHeight + 1, halfWidth - 1, halfHeight + 1);
            bmGraphics.DrawLine(Pens.Black, halfWidth + 6, halfHeight + 1, halfWidth + 3, halfHeight + 1);


            bmGraphics.Dispose();

            picZoom.Refresh();
        }

        private void frmpic_Load(object sender, EventArgs e)
        {
            _OriginalImage = pic;
        }

        private void btnLoadImage_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.AddExtension = false;
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;
            openFileDialog.Filter = "Supported Image File|*.jpg;*.jpeg;*.bmp;*.png;*.dib;*.gif";
            openFileDialog.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            openFileDialog.Multiselect = false;
            openFileDialog.ReadOnlyChecked = false;
            openFileDialog.ShowHelp = true;
            openFileDialog.ShowReadOnly = false;
            openFileDialog.SupportMultiDottedExtensions = true;
            openFileDialog.Title = "Select an image...";
            openFileDialog.ValidateNames = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    //_OriginalImage = Image.FromFile(openFileDialog.FileName);
                    //ResizeAndDisplayImage();
                    _OriginalImage = pic;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occured loading the image " +
                                    openFileDialog.FileName + "\r\n" +
                                    ex.Message +
                                    "Please ensure you select a supported image type.",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }

            openFileDialog.Dispose();
        }
    }
}
