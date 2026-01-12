
namespace PrescriptionManagement
{
    partial class frmpic
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSelectColor = new System.Windows.Forms.Button();
            this.lblZoomFactor = new System.Windows.Forms.Label();
            this.trbZoomFactor = new System.Windows.Forms.TrackBar();
            this.btnLoadImage = new System.Windows.Forms.Button();
            this.picZoom = new System.Windows.Forms.PictureBox();
            this.picImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.trbZoomFactor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picZoom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSelectColor
            // 
            this.btnSelectColor.Location = new System.Drawing.Point(721, 449);
            this.btnSelectColor.Name = "btnSelectColor";
            this.btnSelectColor.Size = new System.Drawing.Size(223, 61);
            this.btnSelectColor.TabIndex = 11;
            this.btnSelectColor.Text = "Background color...";
            this.btnSelectColor.UseVisualStyleBackColor = true;
            // 
            // lblZoomFactor
            // 
            this.lblZoomFactor.AutoSize = true;
            this.lblZoomFactor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblZoomFactor.Location = new System.Drawing.Point(919, 252);
            this.lblZoomFactor.Name = "lblZoomFactor";
            this.lblZoomFactor.Size = new System.Drawing.Size(25, 20);
            this.lblZoomFactor.TabIndex = 10;
            this.lblZoomFactor.Text = "x3";
            // 
            // trbZoomFactor
            // 
            this.trbZoomFactor.LargeChange = 1;
            this.trbZoomFactor.Location = new System.Drawing.Point(711, 243);
            this.trbZoomFactor.Maximum = 6;
            this.trbZoomFactor.Minimum = 2;
            this.trbZoomFactor.Name = "trbZoomFactor";
            this.trbZoomFactor.Size = new System.Drawing.Size(190, 45);
            this.trbZoomFactor.TabIndex = 9;
            this.trbZoomFactor.Value = 3;
            // 
            // btnLoadImage
            // 
            this.btnLoadImage.Location = new System.Drawing.Point(721, 516);
            this.btnLoadImage.Name = "btnLoadImage";
            this.btnLoadImage.Size = new System.Drawing.Size(223, 61);
            this.btnLoadImage.TabIndex = 8;
            this.btnLoadImage.Text = "Load image";
            this.btnLoadImage.UseVisualStyleBackColor = true;
            this.btnLoadImage.Click += new System.EventHandler(this.btnLoadImage_Click_1);
            // 
            // picZoom
            // 
            this.picZoom.BackColor = System.Drawing.Color.White;
            this.picZoom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picZoom.Location = new System.Drawing.Point(711, 12);
            this.picZoom.Name = "picZoom";
            this.picZoom.Size = new System.Drawing.Size(239, 225);
            this.picZoom.TabIndex = 7;
            this.picZoom.TabStop = false;
            // 
            // picImage
            // 
            this.picImage.BackColor = System.Drawing.Color.White;
            this.picImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picImage.Cursor = System.Windows.Forms.Cursors.Cross;
            this.picImage.Location = new System.Drawing.Point(12, 12);
            this.picImage.Name = "picImage";
            this.picImage.Size = new System.Drawing.Size(693, 565);
            this.picImage.TabIndex = 6;
            this.picImage.TabStop = false;
            // 
            // frmpic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(961, 591);
            this.Controls.Add(this.btnSelectColor);
            this.Controls.Add(this.lblZoomFactor);
            this.Controls.Add(this.trbZoomFactor);
            this.Controls.Add(this.btnLoadImage);
            this.Controls.Add(this.picZoom);
            this.Controls.Add(this.picImage);
            this.Name = "frmpic";
            this.Text = "frmpic";
            this.Load += new System.EventHandler(this.frmpic_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trbZoomFactor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picZoom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelectColor;
        private System.Windows.Forms.Label lblZoomFactor;
        private System.Windows.Forms.TrackBar trbZoomFactor;
        private System.Windows.Forms.Button btnLoadImage;
        private System.Windows.Forms.PictureBox picZoom;
        private System.Windows.Forms.PictureBox picImage;
    }
}