
namespace PrescriptionManagement
{
    partial class frm_checkstatus
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_checkstatus));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lbPrescount = new System.Windows.Forms.Label();
            this.Panel6 = new System.Windows.Forms.Panel();
            this.Label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.btnExcel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbSelect = new System.Windows.Forms.ComboBox();
            this.btn_search = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dateselect = new System.Windows.Forms.DateTimePicker();
            this.cbb_select = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgvOrderPrescriptionListOrderNo = new System.Windows.Forms.DataGridView();
            this._id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ordercreatedate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ordertypedesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wardcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wardname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prescriptionno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.an = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patientname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.doctorname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.genorderdatetime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.jobdatetime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.jobname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.matchingdatetime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.checkoutdatetime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.checkname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.calldatetime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.transferdatetime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.smartdischargedatetime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.medtransferdatetime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.medtransferuser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.waittingtime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Panel6.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderPrescriptionListOrderNo)).BeginInit();
            this.SuspendLayout();
            // 
            // lbPrescount
            // 
            this.lbPrescount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbPrescount.AutoSize = true;
            this.lbPrescount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbPrescount.Location = new System.Drawing.Point(3, 5);
            this.lbPrescount.Name = "lbPrescount";
            this.lbPrescount.Size = new System.Drawing.Size(104, 18);
            this.lbPrescount.TabIndex = 64;
            this.lbPrescount.Text = "จำนวน 0 รายการ";
            // 
            // Panel6
            // 
            this.Panel6.Controls.Add(this.lbPrescount);
            this.Panel6.Controls.Add(this.Label1);
            this.Panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel6.Location = new System.Drawing.Point(0, 0);
            this.Panel6.Name = "Panel6";
            this.Panel6.Size = new System.Drawing.Size(1744, 31);
            this.Panel6.TabIndex = 63;
            // 
            // Label1
            // 
            this.Label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Label1.Location = new System.Drawing.Point(10, -69);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(44, 18);
            this.Label1.TabIndex = 71;
            this.Label1.Text = "ค้นหา:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.btnExcel);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1744, 153);
            this.panel1.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(1004, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(236, 24);
            this.label6.TabIndex = 82;
            this.label6.Text = "Path Export File : C:\\Temp\\";
            // 
            // btnExcel
            // 
            this.btnExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel.Image")));
            this.btnExcel.Location = new System.Drawing.Point(1088, 89);
            this.btnExcel.Margin = new System.Windows.Forms.Padding(2);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(146, 42);
            this.btnExcel.TabIndex = 82;
            this.btnExcel.Text = "ดึงรายงาน";
            this.btnExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click_1);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.cbSelect);
            this.groupBox2.Controls.Add(this.btn_search);
            this.groupBox2.Controls.Add(this.txtSearch);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.groupBox2.Location = new System.Drawing.Point(523, 28);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(561, 117);
            this.groupBox2.TabIndex = 82;
            this.groupBox2.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(232, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 24);
            this.label4.TabIndex = 81;
            this.label4.Text = "กรอกข้อมูล";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(1, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(139, 24);
            this.label5.TabIndex = 80;
            this.label5.Text = "ค้นหาเพิ่มเติมตาม";
            // 
            // cbSelect
            // 
            this.cbSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSelect.FormattingEnabled = true;
            this.cbSelect.Items.AddRange(new object[] {
            "กรุณาเลือก",
            "AN",
            "เลขที่ใบสั่ง",
            "HN",
            "ชื่อ"});
            this.cbSelect.Location = new System.Drawing.Point(5, 65);
            this.cbSelect.Name = "cbSelect";
            this.cbSelect.Size = new System.Drawing.Size(214, 34);
            this.cbSelect.TabIndex = 77;
            this.cbSelect.Text = "กรุณาเลือก";
            // 
            // btn_search
            // 
            this.btn_search.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btn_search.Image = ((System.Drawing.Image)(resources.GetObject("btn_search.Image")));
            this.btn_search.Location = new System.Drawing.Point(456, 61);
            this.btn_search.Margin = new System.Windows.Forms.Padding(2);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(94, 42);
            this.btn_search.TabIndex = 38;
            this.btn_search.Text = "ค้นหา";
            this.btn_search.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtSearch.Location = new System.Drawing.Point(236, 65);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(209, 32);
            this.txtSearch.TabIndex = 75;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(10, 3);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(171, 31);
            this.label13.TabIndex = 37;
            this.label13.Text = "สถานะใบสั่งยา";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dateselect);
            this.groupBox1.Controls.Add(this.cbb_select);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.groupBox1.Location = new System.Drawing.Point(15, 28);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(478, 117);
            this.groupBox1.TabIndex = 79;
            this.groupBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(170, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 24);
            this.label3.TabIndex = 81;
            this.label3.Text = "เลือกประเถท";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 24);
            this.label2.TabIndex = 80;
            this.label2.Text = "วันที่ค้นหา";
            // 
            // dateselect
            // 
            this.dateselect.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dateselect.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateselect.Location = new System.Drawing.Point(5, 65);
            this.dateselect.Name = "dateselect";
            this.dateselect.Size = new System.Drawing.Size(158, 32);
            this.dateselect.TabIndex = 76;
            // 
            // cbb_select
            // 
            this.cbb_select.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbb_select.FormattingEnabled = true;
            this.cbb_select.Location = new System.Drawing.Point(175, 65);
            this.cbb_select.Name = "cbb_select";
            this.cbb_select.Size = new System.Drawing.Size(287, 34);
            this.cbb_select.TabIndex = 78;
            this.cbb_select.Text = "กรุณาเลือก";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgvOrderPrescriptionListOrderNo);
            this.panel2.Controls.Add(this.Panel6);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1744, 800);
            this.panel2.TabIndex = 3;
            // 
            // dgvOrderPrescriptionListOrderNo
            // 
            this.dgvOrderPrescriptionListOrderNo.AllowUserToAddRows = false;
            this.dgvOrderPrescriptionListOrderNo.AllowUserToDeleteRows = false;
            this.dgvOrderPrescriptionListOrderNo.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvOrderPrescriptionListOrderNo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvOrderPrescriptionListOrderNo.ColumnHeadersHeight = 40;
            this.dgvOrderPrescriptionListOrderNo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._id,
            this.ordercreatedate,
            this.ordertypedesc,
            this.wardcode,
            this.wardname,
            this.prescriptionno,
            this.hn,
            this.an,
            this.patientname,
            this.sex,
            this.doctorname,
            this.genorderdatetime,
            this.jobdatetime,
            this.jobname,
            this.matchingdatetime,
            this.checkoutdatetime,
            this.checkname,
            this.calldatetime,
            this.transferdatetime,
            this.smartdischargedatetime,
            this.medtransferdatetime,
            this.medtransferuser,
            this.status,
            this.waittingtime});
            this.dgvOrderPrescriptionListOrderNo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvOrderPrescriptionListOrderNo.Location = new System.Drawing.Point(0, 150);
            this.dgvOrderPrescriptionListOrderNo.Name = "dgvOrderPrescriptionListOrderNo";
            this.dgvOrderPrescriptionListOrderNo.RowHeadersVisible = false;
            this.dgvOrderPrescriptionListOrderNo.RowHeadersWidth = 51;
            this.dgvOrderPrescriptionListOrderNo.RowTemplate.Height = 50;
            this.dgvOrderPrescriptionListOrderNo.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvOrderPrescriptionListOrderNo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrderPrescriptionListOrderNo.Size = new System.Drawing.Size(1744, 650);
            this.dgvOrderPrescriptionListOrderNo.TabIndex = 62;
            this.dgvOrderPrescriptionListOrderNo.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrderPrescriptionListOrderNo_CellContentDoubleClick);
            // 
            // _id
            // 
            this._id.HeaderText = "_id";
            this._id.MinimumWidth = 6;
            this._id.Name = "_id";
            this._id.Visible = false;
            this._id.Width = 125;
            // 
            // ordercreatedate
            // 
            this.ordercreatedate.HeaderText = "วันที่สร้างใบสั่งยา";
            this.ordercreatedate.MinimumWidth = 6;
            this.ordercreatedate.Name = "ordercreatedate";
            this.ordercreatedate.Width = 130;
            // 
            // ordertypedesc
            // 
            this.ordertypedesc.HeaderText = "สถานะใบสั่งยา";
            this.ordertypedesc.MinimumWidth = 6;
            this.ordertypedesc.Name = "ordertypedesc";
            this.ordertypedesc.Width = 120;
            // 
            // wardcode
            // 
            this.wardcode.HeaderText = "wardcode";
            this.wardcode.MinimumWidth = 6;
            this.wardcode.Name = "wardcode";
            this.wardcode.Visible = false;
            this.wardcode.Width = 125;
            // 
            // wardname
            // 
            this.wardname.HeaderText = "wardname";
            this.wardname.MinimumWidth = 6;
            this.wardname.Name = "wardname";
            this.wardname.Width = 125;
            // 
            // prescriptionno
            // 
            this.prescriptionno.HeaderText = "เลขที่ใบสั่งยา";
            this.prescriptionno.MinimumWidth = 6;
            this.prescriptionno.Name = "prescriptionno";
            this.prescriptionno.Width = 125;
            // 
            // hn
            // 
            this.hn.HeaderText = "hn";
            this.hn.MinimumWidth = 6;
            this.hn.Name = "hn";
            this.hn.Width = 80;
            // 
            // an
            // 
            this.an.HeaderText = "an";
            this.an.MinimumWidth = 6;
            this.an.Name = "an";
            this.an.Width = 80;
            // 
            // patientname
            // 
            this.patientname.HeaderText = "ชื่อผู้ป่วย";
            this.patientname.MinimumWidth = 6;
            this.patientname.Name = "patientname";
            this.patientname.Width = 150;
            // 
            // sex
            // 
            this.sex.HeaderText = "เพศ";
            this.sex.MinimumWidth = 6;
            this.sex.Name = "sex";
            this.sex.Width = 60;
            // 
            // doctorname
            // 
            this.doctorname.HeaderText = "ชื่อแพทย์";
            this.doctorname.MinimumWidth = 6;
            this.doctorname.Name = "doctorname";
            this.doctorname.Width = 150;
            // 
            // genorderdatetime
            // 
            this.genorderdatetime.HeaderText = "เริ่มจัด";
            this.genorderdatetime.MinimumWidth = 6;
            this.genorderdatetime.Name = "genorderdatetime";
            this.genorderdatetime.Width = 125;
            // 
            // jobdatetime
            // 
            this.jobdatetime.HeaderText = "เวลาจัดยาเสร็จ";
            this.jobdatetime.MinimumWidth = 6;
            this.jobdatetime.Name = "jobdatetime";
            this.jobdatetime.Width = 125;
            // 
            // jobname
            // 
            this.jobname.HeaderText = "จัดโดย";
            this.jobname.Name = "jobname";
            // 
            // matchingdatetime
            // 
            this.matchingdatetime.HeaderText = "Matching";
            this.matchingdatetime.MinimumWidth = 6;
            this.matchingdatetime.Name = "matchingdatetime";
            this.matchingdatetime.Visible = false;
            this.matchingdatetime.Width = 125;
            // 
            // checkoutdatetime
            // 
            this.checkoutdatetime.HeaderText = "เวลาตรวจ";
            this.checkoutdatetime.MinimumWidth = 6;
            this.checkoutdatetime.Name = "checkoutdatetime";
            this.checkoutdatetime.Width = 125;
            // 
            // checkname
            // 
            this.checkname.HeaderText = "ตรวจโดย";
            this.checkname.Name = "checkname";
            // 
            // calldatetime
            // 
            this.calldatetime.HeaderText = "เวลาเรียกคิว";
            this.calldatetime.MinimumWidth = 6;
            this.calldatetime.Name = "calldatetime";
            this.calldatetime.Width = 125;
            // 
            // transferdatetime
            // 
            this.transferdatetime.HeaderText = "เวลารับยาขึ้นตึก";
            this.transferdatetime.MinimumWidth = 6;
            this.transferdatetime.Name = "transferdatetime";
            this.transferdatetime.Width = 125;
            // 
            // smartdischargedatetime
            // 
            this.smartdischargedatetime.HeaderText = "เวลา smartdischarge";
            this.smartdischargedatetime.MinimumWidth = 6;
            this.smartdischargedatetime.Name = "smartdischargedatetime";
            this.smartdischargedatetime.Width = 125;
            // 
            // medtransferdatetime
            // 
            this.medtransferdatetime.HeaderText = "เวลาจ่ายยา";
            this.medtransferdatetime.MinimumWidth = 6;
            this.medtransferdatetime.Name = "medtransferdatetime";
            this.medtransferdatetime.Width = 125;
            // 
            // medtransferuser
            // 
            this.medtransferuser.HeaderText = "จ่ายยาโดย";
            this.medtransferuser.MinimumWidth = 6;
            this.medtransferuser.Name = "medtransferuser";
            this.medtransferuser.Width = 125;
            // 
            // status
            // 
            this.status.HeaderText = "สถานะ";
            this.status.MinimumWidth = 6;
            this.status.Name = "status";
            this.status.Width = 125;
            // 
            // waittingtime
            // 
            this.waittingtime.HeaderText = "waittingtime";
            this.waittingtime.MinimumWidth = 6;
            this.waittingtime.Name = "waittingtime";
            this.waittingtime.Width = 125;
            // 
            // frm_checkstatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1744, 800);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "frm_checkstatus";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frm_checkstatus";
            this.Load += new System.EventHandler(this.frm_checkstatus_Load);
            this.Panel6.ResumeLayout(false);
            this.Panel6.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderPrescriptionListOrderNo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Label lbPrescount;
        internal System.Windows.Forms.Panel Panel6;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        internal System.Windows.Forms.ComboBox cbSelect;
        private System.Windows.Forms.Button btn_search;
        internal System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.DateTimePicker dateselect;
        internal System.Windows.Forms.ComboBox cbb_select;
        private System.Windows.Forms.Panel panel2;
        internal System.Windows.Forms.DataGridView dgvOrderPrescriptionListOrderNo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewTextBoxColumn _id;
        private System.Windows.Forms.DataGridViewTextBoxColumn ordercreatedate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ordertypedesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn wardcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn wardname;
        private System.Windows.Forms.DataGridViewTextBoxColumn prescriptionno;
        private System.Windows.Forms.DataGridViewTextBoxColumn hn;
        private System.Windows.Forms.DataGridViewTextBoxColumn an;
        private System.Windows.Forms.DataGridViewTextBoxColumn patientname;
        private System.Windows.Forms.DataGridViewTextBoxColumn sex;
        private System.Windows.Forms.DataGridViewTextBoxColumn doctorname;
        private System.Windows.Forms.DataGridViewTextBoxColumn genorderdatetime;
        private System.Windows.Forms.DataGridViewTextBoxColumn jobdatetime;
        private System.Windows.Forms.DataGridViewTextBoxColumn jobname;
        private System.Windows.Forms.DataGridViewTextBoxColumn matchingdatetime;
        private System.Windows.Forms.DataGridViewTextBoxColumn checkoutdatetime;
        private System.Windows.Forms.DataGridViewTextBoxColumn checkname;
        private System.Windows.Forms.DataGridViewTextBoxColumn calldatetime;
        private System.Windows.Forms.DataGridViewTextBoxColumn transferdatetime;
        private System.Windows.Forms.DataGridViewTextBoxColumn smartdischargedatetime;
        private System.Windows.Forms.DataGridViewTextBoxColumn medtransferdatetime;
        private System.Windows.Forms.DataGridViewTextBoxColumn medtransferuser;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewTextBoxColumn waittingtime;
    }
}