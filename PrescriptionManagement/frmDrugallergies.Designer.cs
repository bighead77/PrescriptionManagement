
namespace PrescriptionManagement
{
    partial class frmDrugallergies
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_confirm = new System.Windows.Forms.Button();
            this.lsdrugallergies_his = new System.Windows.Forms.ListBox();
            this.lshispharmaco = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lsdrugallergies_his);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("AngsanaUPC", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1519, 477);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "HIS";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lshispharmaco);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Font = new System.Drawing.Font("AngsanaUPC", 20.25F);
            this.groupBox2.Location = new System.Drawing.Point(0, 480);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1519, 454);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "กระทรวง";
            // 
            // btn_confirm
            // 
            this.btn_confirm.BackColor = System.Drawing.Color.DarkGreen;
            this.btn_confirm.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_confirm.ForeColor = System.Drawing.Color.White;
            this.btn_confirm.Location = new System.Drawing.Point(1167, 12);
            this.btn_confirm.Name = "btn_confirm";
            this.btn_confirm.Size = new System.Drawing.Size(130, 53);
            this.btn_confirm.TabIndex = 5;
            this.btn_confirm.Text = "Confirm";
            this.btn_confirm.UseVisualStyleBackColor = false;
            this.btn_confirm.Visible = false;
            this.btn_confirm.Click += new System.EventHandler(this.btn_confirm_Click);
            // 
            // lsdrugallergies_his
            // 
            this.lsdrugallergies_his.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsdrugallergies_his.FormattingEnabled = true;
            this.lsdrugallergies_his.ItemHeight = 36;
            this.lsdrugallergies_his.Location = new System.Drawing.Point(3, 40);
            this.lsdrugallergies_his.Name = "lsdrugallergies_his";
            this.lsdrugallergies_his.Size = new System.Drawing.Size(1513, 434);
            this.lsdrugallergies_his.TabIndex = 0;
            // 
            // lshispharmaco
            // 
            this.lshispharmaco.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lshispharmaco.FormattingEnabled = true;
            this.lshispharmaco.ItemHeight = 36;
            this.lshispharmaco.Location = new System.Drawing.Point(3, 40);
            this.lshispharmaco.Name = "lshispharmaco";
            this.lshispharmaco.Size = new System.Drawing.Size(1513, 411);
            this.lshispharmaco.TabIndex = 1;
            // 
            // frmDrugallergies
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(1519, 934);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_confirm);
            this.Name = "frmDrugallergies";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmDrugallergies";
            this.Load += new System.EventHandler(this.frmDrugallergies_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_confirm;
        private System.Windows.Forms.ListBox lsdrugallergies_his;
        private System.Windows.Forms.ListBox lshispharmaco;
    }
}