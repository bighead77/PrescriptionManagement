
namespace PrescriptionManagement
{
    partial class frmHistory
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHistory));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dgvHistory = new System.Windows.Forms.DataGridView();
            this.orderitemcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.orderitemname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.orderqty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.orderunit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel5 = new System.Windows.Forms.Panel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.dgvtoday = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbhn = new System.Windows.Forms.Label();
            this.lbage = new System.Windows.Forms.Label();
            this.lbbb = new System.Windows.Forms.Label();
            this.lbsax = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbname = new System.Windows.Forms.Label();
            this.lb = new System.Windows.Forms.Label();
            this.lslabstoday = new System.Windows.Forms.ListBox();
            this.lslabs = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnsearch = new System.Windows.Forms.Button();
            this.txtsearchhn = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.col_status = new System.Windows.Forms.DataGridViewImageColumn();
            this.fin_orderitemcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fin_orderitemname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fin_orderqty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fin_orderunit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).BeginInit();
            this.panel5.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvtoday)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.splitContainer1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1638, 635);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 16);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel5);
            this.splitContainer1.Size = new System.Drawing.Size(1632, 616);
            this.splitContainer1.SplitterDistance = 800;
            this.splitContainer1.TabIndex = 4;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.groupBox4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(800, 616);
            this.panel3.TabIndex = 3;
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox4.Controls.Add(this.dgvHistory);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(800, 616);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "รายการใบสั่งยา";
            // 
            // dgvHistory
            // 
            this.dgvHistory.AllowUserToAddRows = false;
            this.dgvHistory.AllowUserToDeleteRows = false;
            this.dgvHistory.AllowUserToResizeColumns = false;
            this.dgvHistory.AllowUserToResizeRows = false;
            this.dgvHistory.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvHistory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvHistory.ColumnHeadersHeight = 40;
            this.dgvHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.orderitemcode,
            this.orderitemname,
            this.orderqty,
            this.orderunit});
            this.dgvHistory.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvHistory.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHistory.EnableHeadersVisualStyles = false;
            this.dgvHistory.Location = new System.Drawing.Point(3, 22);
            this.dgvHistory.Name = "dgvHistory";
            this.dgvHistory.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvHistory.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvHistory.RowHeadersVisible = false;
            this.dgvHistory.RowHeadersWidth = 45;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dgvHistory.RowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvHistory.RowTemplate.Height = 45;
            this.dgvHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHistory.Size = new System.Drawing.Size(794, 591);
            this.dgvHistory.TabIndex = 5;
            // 
            // orderitemcode
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.orderitemcode.DefaultCellStyle = dataGridViewCellStyle2;
            this.orderitemcode.HeaderText = "รหัสยา";
            this.orderitemcode.MinimumWidth = 6;
            this.orderitemcode.Name = "orderitemcode";
            this.orderitemcode.ReadOnly = true;
            this.orderitemcode.Width = 110;
            // 
            // orderitemname
            // 
            this.orderitemname.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.orderitemname.HeaderText = "ชื่อยา";
            this.orderitemname.MinimumWidth = 6;
            this.orderitemname.Name = "orderitemname";
            this.orderitemname.ReadOnly = true;
            // 
            // orderqty
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.orderqty.DefaultCellStyle = dataGridViewCellStyle3;
            this.orderqty.HeaderText = "จำนวน";
            this.orderqty.MinimumWidth = 6;
            this.orderqty.Name = "orderqty";
            this.orderqty.ReadOnly = true;
            this.orderqty.Width = 110;
            // 
            // orderunit
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.orderunit.DefaultCellStyle = dataGridViewCellStyle4;
            this.orderunit.HeaderText = "หน่วย";
            this.orderunit.MinimumWidth = 6;
            this.orderunit.Name = "orderunit";
            this.orderunit.ReadOnly = true;
            this.orderunit.Width = 110;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.groupBox5);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(828, 616);
            this.panel5.TabIndex = 4;
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox5.Controls.Add(this.dgvtoday);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(0, 0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(828, 616);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "รายการยา";
            // 
            // dgvtoday
            // 
            this.dgvtoday.AllowUserToAddRows = false;
            this.dgvtoday.AllowUserToDeleteRows = false;
            this.dgvtoday.AllowUserToResizeColumns = false;
            this.dgvtoday.AllowUserToResizeRows = false;
            this.dgvtoday.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.AliceBlue;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvtoday.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvtoday.ColumnHeadersHeight = 40;
            this.dgvtoday.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_status,
            this.fin_orderitemcode,
            this.fin_orderitemname,
            this.fin_orderqty,
            this.fin_orderunit});
            this.dgvtoday.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvtoday.DefaultCellStyle = dataGridViewCellStyle12;
            this.dgvtoday.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvtoday.EnableHeadersVisualStyles = false;
            this.dgvtoday.Location = new System.Drawing.Point(3, 22);
            this.dgvtoday.Name = "dgvtoday";
            this.dgvtoday.ReadOnly = true;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvtoday.RowHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.dgvtoday.RowHeadersVisible = false;
            this.dgvtoday.RowHeadersWidth = 45;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dgvtoday.RowsDefaultCellStyle = dataGridViewCellStyle14;
            this.dgvtoday.RowTemplate.Height = 45;
            this.dgvtoday.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvtoday.Size = new System.Drawing.Size(822, 591);
            this.dgvtoday.TabIndex = 4;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.groupBox2.Controls.Add(this.lbhn);
            this.groupBox2.Controls.Add(this.lbage);
            this.groupBox2.Controls.Add(this.lbbb);
            this.groupBox2.Controls.Add(this.lbsax);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.lbname);
            this.groupBox2.Controls.Add(this.lb);
            this.groupBox2.Controls.Add(this.lslabstoday);
            this.groupBox2.Controls.Add(this.lslabs);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.btnsearch);
            this.groupBox2.Controls.Add(this.txtsearchhn);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1638, 181);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            // 
            // lbhn
            // 
            this.lbhn.AutoSize = true;
            this.lbhn.Font = new System.Drawing.Font("Angsana New", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbhn.Location = new System.Drawing.Point(569, 14);
            this.lbhn.Name = "lbhn";
            this.lbhn.Size = new System.Drawing.Size(26, 43);
            this.lbhn.TabIndex = 30;
            this.lbhn.Text = "-";
            // 
            // lbage
            // 
            this.lbage.AutoSize = true;
            this.lbage.Font = new System.Drawing.Font("Angsana New", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbage.Location = new System.Drawing.Point(1367, 14);
            this.lbage.Name = "lbage";
            this.lbage.Size = new System.Drawing.Size(26, 43);
            this.lbage.TabIndex = 29;
            this.lbage.Text = "-";
            // 
            // lbbb
            // 
            this.lbbb.AutoSize = true;
            this.lbbb.Font = new System.Drawing.Font("Angsana New", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbbb.Location = new System.Drawing.Point(1299, 14);
            this.lbbb.Name = "lbbb";
            this.lbbb.Size = new System.Drawing.Size(58, 43);
            this.lbbb.TabIndex = 28;
            this.lbbb.Text = "อายุ:";
            // 
            // lbsax
            // 
            this.lbsax.AutoSize = true;
            this.lbsax.Font = new System.Drawing.Font("Angsana New", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbsax.Location = new System.Drawing.Point(1177, 14);
            this.lbsax.Name = "lbsax";
            this.lbsax.Size = new System.Drawing.Size(26, 43);
            this.lbsax.TabIndex = 27;
            this.lbsax.Text = "-";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Angsana New", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1109, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 43);
            this.label2.TabIndex = 26;
            this.label2.Text = "เพศ:";
            // 
            // lbname
            // 
            this.lbname.AutoSize = true;
            this.lbname.Font = new System.Drawing.Font("Angsana New", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbname.Location = new System.Drawing.Point(756, 14);
            this.lbname.Name = "lbname";
            this.lbname.Size = new System.Drawing.Size(26, 43);
            this.lbname.TabIndex = 25;
            this.lbname.Text = "-";
            this.lbname.Click += new System.EventHandler(this.lbname_Click);
            // 
            // lb
            // 
            this.lb.AutoSize = true;
            this.lb.Font = new System.Drawing.Font("Angsana New", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb.Location = new System.Drawing.Point(486, 14);
            this.lb.Name = "lb";
            this.lb.Size = new System.Drawing.Size(65, 43);
            this.lb.TabIndex = 24;
            this.lb.Text = "HN : ";
            // 
            // lslabstoday
            // 
            this.lslabstoday.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lslabstoday.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lslabstoday.FormattingEnabled = true;
            this.lslabstoday.ItemHeight = 32;
            this.lslabstoday.Location = new System.Drawing.Point(810, 103);
            this.lslabstoday.Name = "lslabstoday";
            this.lslabstoday.ScrollAlwaysVisible = true;
            this.lslabstoday.Size = new System.Drawing.Size(822, 68);
            this.lslabstoday.TabIndex = 23;
            // 
            // lslabs
            // 
            this.lslabs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lslabs.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lslabs.FormattingEnabled = true;
            this.lslabs.ItemHeight = 32;
            this.lslabs.Location = new System.Drawing.Point(12, 103);
            this.lslabs.Name = "lslabs";
            this.lslabs.ScrollAlwaysVisible = true;
            this.lslabs.Size = new System.Drawing.Size(788, 68);
            this.lslabs.TabIndex = 22;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 32);
            this.label5.TabIndex = 21;
            this.label5.Text = "Labs ย้อนหลัง";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(806, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 32);
            this.label4.TabIndex = 20;
            this.label4.Text = "Labs วันนี้";
            // 
            // btnsearch
            // 
            this.btnsearch.BackColor = System.Drawing.SystemColors.Control;
            this.btnsearch.Image = ((System.Drawing.Image)(resources.GetObject("btnsearch.Image")));
            this.btnsearch.Location = new System.Drawing.Point(382, 20);
            this.btnsearch.Name = "btnsearch";
            this.btnsearch.Size = new System.Drawing.Size(62, 48);
            this.btnsearch.TabIndex = 2;
            this.btnsearch.UseVisualStyleBackColor = false;
            this.btnsearch.Click += new System.EventHandler(this.btnsearch_Click);
            // 
            // txtsearchhn
            // 
            this.txtsearchhn.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsearchhn.Location = new System.Drawing.Point(93, 25);
            this.txtsearchhn.Name = "txtsearchhn";
            this.txtsearchhn.Size = new System.Drawing.Size(283, 39);
            this.txtsearchhn.TabIndex = 1;
            this.txtsearchhn.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtsearchhn_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Angsana New", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 49);
            this.label1.TabIndex = 0;
            this.label1.Text = "HN : ";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(245)))), ((int)(((byte)(251)))));
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1638, 181);
            this.panel2.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(245)))), ((int)(((byte)(251)))));
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 181);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1638, 635);
            this.panel1.TabIndex = 8;
            // 
            // col_status
            // 
            this.col_status.HeaderText = "";
            this.col_status.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.col_status.Name = "col_status";
            this.col_status.ReadOnly = true;
            this.col_status.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.col_status.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.col_status.Visible = false;
            // 
            // fin_orderitemcode
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.fin_orderitemcode.DefaultCellStyle = dataGridViewCellStyle9;
            this.fin_orderitemcode.HeaderText = "รหัสยา";
            this.fin_orderitemcode.MinimumWidth = 6;
            this.fin_orderitemcode.Name = "fin_orderitemcode";
            this.fin_orderitemcode.ReadOnly = true;
            this.fin_orderitemcode.Width = 110;
            // 
            // fin_orderitemname
            // 
            this.fin_orderitemname.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.fin_orderitemname.HeaderText = "ชื่อยา";
            this.fin_orderitemname.MinimumWidth = 6;
            this.fin_orderitemname.Name = "fin_orderitemname";
            this.fin_orderitemname.ReadOnly = true;
            // 
            // fin_orderqty
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.fin_orderqty.DefaultCellStyle = dataGridViewCellStyle10;
            this.fin_orderqty.HeaderText = "จำนวน";
            this.fin_orderqty.MinimumWidth = 6;
            this.fin_orderqty.Name = "fin_orderqty";
            this.fin_orderqty.ReadOnly = true;
            this.fin_orderqty.Width = 110;
            // 
            // fin_orderunit
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.fin_orderunit.DefaultCellStyle = dataGridViewCellStyle11;
            this.fin_orderunit.HeaderText = "หน่วย";
            this.fin_orderunit.MinimumWidth = 6;
            this.fin_orderunit.Name = "fin_orderunit";
            this.fin_orderunit.ReadOnly = true;
            this.fin_orderunit.Width = 110;
            // 
            // frmHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(82)))), ((int)(((byte)(152)))));
            this.ClientSize = new System.Drawing.Size(1638, 816);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmHistory";
            this.Load += new System.EventHandler(this.frmHistory_Load);
            this.groupBox1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).EndInit();
            this.panel5.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvtoday)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnsearch;
        private System.Windows.Forms.TextBox txtsearchhn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.DataGridView dgvHistory;
        private System.Windows.Forms.DataGridView dgvtoday;
        private System.Windows.Forms.ListBox lslabstoday;
        private System.Windows.Forms.ListBox lslabs;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn orderitemcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn orderitemname;
        private System.Windows.Forms.DataGridViewTextBoxColumn orderqty;
        private System.Windows.Forms.DataGridViewTextBoxColumn orderunit;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbname;
        private System.Windows.Forms.Label lbage;
        private System.Windows.Forms.Label lbbb;
        private System.Windows.Forms.Label lbsax;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbhn;
        private System.Windows.Forms.Label lb;
        private System.Windows.Forms.DataGridViewImageColumn col_status;
        private System.Windows.Forms.DataGridViewTextBoxColumn fin_orderitemcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn fin_orderitemname;
        private System.Windows.Forms.DataGridViewTextBoxColumn fin_orderqty;
        private System.Windows.Forms.DataGridViewTextBoxColumn fin_orderunit;
    }
}