namespace PrescriptionManagement
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tbtn_PharmacyConsult = new System.Windows.Forms.ToolStripButton();
            this.tbtn_MatchingBasket = new System.Windows.Forms.ToolStripButton();
            this.tbtn_WorkingStatus = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.lbUsername = new System.Windows.Forms.ToolStripLabel();
            this.lb_userid = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnLogOut = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSetting = new System.Windows.Forms.ToolStripButton();
            this.tbtn_RegisterPres = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel7 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.panel_main = new System.Windows.Forms.Panel();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.lbComputername = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.lbIP = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel6 = new System.Windows.Forms.ToolStripLabel();
            this.lbversion = new System.Windows.Forms.ToolStripLabel();
            this.ts_btn_printer = new System.Windows.Forms.ToolStripButton();
            this.lbPrintername = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeqzone = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.toolStripLabel2.ForeColor = System.Drawing.Color.White;
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(68, 52);
            this.toolStripLabel2.Text = "ZONE:";
            this.toolStripLabel2.Visible = false;
            // 
            // tbtn_PharmacyConsult
            // 
            this.tbtn_PharmacyConsult.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbtn_PharmacyConsult.ForeColor = System.Drawing.Color.White;
            this.tbtn_PharmacyConsult.Image = ((System.Drawing.Image)(resources.GetObject("tbtn_PharmacyConsult.Image")));
            this.tbtn_PharmacyConsult.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtn_PharmacyConsult.Name = "tbtn_PharmacyConsult";
            this.tbtn_PharmacyConsult.Size = new System.Drawing.Size(142, 52);
            this.tbtn_PharmacyConsult.Text = "เบิกยาใบฎีกา";
            this.tbtn_PharmacyConsult.Visible = false;
            // 
            // tbtn_MatchingBasket
            // 
            this.tbtn_MatchingBasket.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbtn_MatchingBasket.ForeColor = System.Drawing.Color.White;
            this.tbtn_MatchingBasket.Image = ((System.Drawing.Image)(resources.GetObject("tbtn_MatchingBasket.Image")));
            this.tbtn_MatchingBasket.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbtn_MatchingBasket.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtn_MatchingBasket.Name = "tbtn_MatchingBasket";
            this.tbtn_MatchingBasket.Size = new System.Drawing.Size(245, 52);
            this.tbtn_MatchingBasket.Text = "Checkout Prescription";
            this.tbtn_MatchingBasket.Click += new System.EventHandler(this.tbtn_MatchingBasket_Click);
            // 
            // tbtn_WorkingStatus
            // 
            this.tbtn_WorkingStatus.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbtn_WorkingStatus.ForeColor = System.Drawing.Color.White;
            this.tbtn_WorkingStatus.Image = ((System.Drawing.Image)(resources.GetObject("tbtn_WorkingStatus.Image")));
            this.tbtn_WorkingStatus.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbtn_WorkingStatus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtn_WorkingStatus.Name = "tbtn_WorkingStatus";
            this.tbtn_WorkingStatus.Size = new System.Drawing.Size(245, 52);
            this.tbtn_WorkingStatus.Text = "Basket Management";
            this.tbtn_WorkingStatus.Visible = false;
            this.tbtn_WorkingStatus.Click += new System.EventHandler(this.tbtn_WorkingStatus_Click);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel4.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.toolStripLabel4.ForeColor = System.Drawing.Color.White;
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(129, 52);
            this.toolStripLabel4.Text = "รหัสพนักงาน :";
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel5.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.toolStripLabel5.ForeColor = System.Drawing.Color.White;
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(101, 52);
            this.toolStripLabel5.Text = "หน่วยงาน :";
            // 
            // lbUsername
            // 
            this.lbUsername.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.lbUsername.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUsername.ForeColor = System.Drawing.Color.White;
            this.lbUsername.Name = "lbUsername";
            this.lbUsername.Size = new System.Drawing.Size(0, 52);
            // 
            // lb_userid
            // 
            this.lb_userid.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.lb_userid.ForeColor = System.Drawing.Color.White;
            this.lb_userid.Name = "lb_userid";
            this.lb_userid.Size = new System.Drawing.Size(0, 52);
            this.lb_userid.Visible = false;
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 55);
            // 
            // btnLogOut
            // 
            this.btnLogOut.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnLogOut.ForeColor = System.Drawing.Color.White;
            this.btnLogOut.Image = ((System.Drawing.Image)(resources.GetObject("btnLogOut.Image")));
            this.btnLogOut.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnLogOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLogOut.Name = "btnLogOut";
            this.btnLogOut.Size = new System.Drawing.Size(88, 52);
            this.btnLogOut.Text = "Logout";
            this.btnLogOut.Click += new System.EventHandler(this.btnLogOut_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 55);
            // 
            // btnSetting
            // 
            this.btnSetting.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnSetting.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSetting.Image = ((System.Drawing.Image)(resources.GetObject("btnSetting.Image")));
            this.btnSetting.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSetting.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(40, 52);
            this.btnSetting.Text = "toolStripButton1";
            this.btnSetting.Visible = false;
            // 
            // tbtn_RegisterPres
            // 
            this.tbtn_RegisterPres.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbtn_RegisterPres.ForeColor = System.Drawing.Color.White;
            this.tbtn_RegisterPres.Image = ((System.Drawing.Image)(resources.GetObject("tbtn_RegisterPres.Image")));
            this.tbtn_RegisterPres.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbtn_RegisterPres.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtn_RegisterPres.Name = "tbtn_RegisterPres";
            this.tbtn_RegisterPres.Size = new System.Drawing.Size(261, 52);
            this.tbtn_RegisterPres.Text = "Matching Prescription";
            this.tbtn_RegisterPres.Click += new System.EventHandler(this.tbtn_MatchingPres_Click_1);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 55);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 55);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(82)))), ((int)(((byte)(152)))));
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.toolStripSeparator7,
            this.tbtn_MatchingBasket,
            this.tbtn_RegisterPres,
            this.btnSetting,
            this.toolStripSeparator6,
            this.btnLogOut,
            this.toolStripSeparator4,
            this.lb_userid,
            this.lbUsername,
            this.toolStripLabel5,
            this.toolStripLabel7,
            this.toolStripLabel4,
            this.tbtn_WorkingStatus,
            this.toolStripButton1,
            this.tbtn_PharmacyConsult,
            this.toolStripLabel2,
            this.toolStripButton3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(1708, 55);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel7
            // 
            this.toolStripLabel7.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel7.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.toolStripLabel7.ForeColor = System.Drawing.Color.White;
            this.toolStripLabel7.Name = "toolStripLabel7";
            this.toolStripLabel7.Size = new System.Drawing.Size(47, 52);
            this.toolStripLabel7.Text = "ชื่อ :";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton1.ForeColor = System.Drawing.Color.White;
            this.toolStripButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(125, 52);
            this.toolStripButton1.Text = "ปิดงานจ่ายยา";
            this.toolStripButton1.Visible = false;
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton3.ForeColor = System.Drawing.Color.White;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(165, 52);
            this.toolStripButton3.Text = "สถานะใบสั่งยา";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // panel_main
            // 
            this.panel_main.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_main.BackColor = System.Drawing.Color.Transparent;
            this.panel_main.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panel_main.Location = new System.Drawing.Point(0, 50);
            this.panel_main.Name = "panel_main";
            this.panel_main.Size = new System.Drawing.Size(1708, 710);
            this.panel_main.TabIndex = 5;
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripLabel3.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.toolStripLabel3.ForeColor = System.Drawing.Color.White;
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(164, 25);
            this.toolStripLabel3.Text = "Computer Name:";
            // 
            // lbComputername
            // 
            this.lbComputername.ForeColor = System.Drawing.Color.White;
            this.lbComputername.Name = "lbComputername";
            this.lbComputername.Size = new System.Drawing.Size(0, 25);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 28);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripLabel1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.toolStripLabel1.ForeColor = System.Drawing.Color.White;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(35, 25);
            this.toolStripLabel1.Text = "IP:";
            // 
            // lbIP
            // 
            this.lbIP.ForeColor = System.Drawing.Color.White;
            this.lbIP.Name = "lbIP";
            this.lbIP.Size = new System.Drawing.Size(0, 25);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 28);
            // 
            // toolStripLabel6
            // 
            this.toolStripLabel6.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel6.ForeColor = System.Drawing.Color.White;
            this.toolStripLabel6.Name = "toolStripLabel6";
            this.toolStripLabel6.Size = new System.Drawing.Size(135, 25);
            this.toolStripLabel6.Text = "version : 2.0.1";
            // 
            // lbversion
            // 
            this.lbversion.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.lbversion.ForeColor = System.Drawing.Color.White;
            this.lbversion.Name = "lbversion";
            this.lbversion.Size = new System.Drawing.Size(69, 25);
            this.lbversion.Text = "Memo";
            // 
            // ts_btn_printer
            // 
            this.ts_btn_printer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ts_btn_printer.ForeColor = System.Drawing.Color.White;
            this.ts_btn_printer.Image = ((System.Drawing.Image)(resources.GetObject("ts_btn_printer.Image")));
            this.ts_btn_printer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btn_printer.Name = "ts_btn_printer";
            this.ts_btn_printer.Size = new System.Drawing.Size(140, 29);
            this.ts_btn_printer.Text = "Printer Name:";
            this.ts_btn_printer.Visible = false;
            // 
            // lbPrintername
            // 
            this.lbPrintername.ForeColor = System.Drawing.Color.White;
            this.lbPrintername.Name = "lbPrintername";
            this.lbPrintername.Size = new System.Drawing.Size(0, 25);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 28);
            // 
            // toolStrip2
            // 
            this.toolStrip2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(82)))), ((int)(((byte)(152)))));
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel3,
            this.lbComputername,
            this.toolStripSeparator2,
            this.toolStripLabel1,
            this.lbIP,
            this.toolStripSeparator5,
            this.toolStripLabel6,
            this.lbversion,
            this.ts_btn_printer,
            this.lbPrintername,
            this.toolStripSeparator3,
            this.toolStripSeqzone,
            this.toolStripTextBox1,
            this.toolStripButton2});
            this.toolStrip2.Location = new System.Drawing.Point(0, 763);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip2.Size = new System.Drawing.Size(1708, 28);
            this.toolStrip2.TabIndex = 4;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripSeqzone
            // 
            this.toolStripSeqzone.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeqzone.ForeColor = System.Drawing.Color.White;
            this.toolStripSeqzone.Name = "toolStripSeqzone";
            this.toolStripSeqzone.Size = new System.Drawing.Size(20, 25);
            this.toolStripSeqzone.Text = "-";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 28);
            this.toolStripTextBox1.Text = "GD4-OLED-1";
            this.toolStripTextBox1.Visible = false;
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(24, 25);
            this.toolStripButton2.Text = "toolStripButton2";
            this.toolStripButton2.Visible = false;
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1708, 791);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel_main);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "frmMain";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripButton tbtn_PharmacyConsult;
        private System.Windows.Forms.ToolStripButton tbtn_MatchingBasket;
        private System.Windows.Forms.ToolStripButton tbtn_WorkingStatus;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripLabel toolStripLabel5;
        private System.Windows.Forms.ToolStripLabel lbUsername;
        private System.Windows.Forms.ToolStripLabel lb_userid;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnLogOut;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton btnSetting;
        private System.Windows.Forms.ToolStripButton tbtn_RegisterPres;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.Panel panel_main;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripLabel lbComputername;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel lbIP;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripLabel toolStripLabel6;
        private System.Windows.Forms.ToolStripLabel lbversion;
        private System.Windows.Forms.ToolStripButton ts_btn_printer;
        private System.Windows.Forms.ToolStripLabel lbPrintername;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel toolStripSeqzone;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel7;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
    }
}