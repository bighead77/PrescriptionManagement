
namespace PrescriptionManagement
{
    partial class frmCheckoutHistory
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCheckoutHistory));
            this.Label1 = new System.Windows.Forms.Label();
            this.Panel4 = new System.Windows.Forms.Panel();
            this.gbPrescription = new System.Windows.Forms.GroupBox();
            this.dgvOrderPrescriptionListOrderNo = new System.Windows.Forms.DataGridView();
            this.Panel6 = new System.Windows.Forms.Panel();
            this.lbwardcode = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.lbwardname = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.lbPrescount = new System.Windows.Forms.Label();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.pntop = new System.Windows.Forms.Panel();
            this.Button1 = new System.Windows.Forms.Button();
            this.Label4 = new System.Windows.Forms.Label();
            this.cbSelect = new System.Windows.Forms.ComboBox();
            this.datecheckout = new System.Windows.Forms.DateTimePicker();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.Timer1 = new System.Windows.Forms.Timer(this.components);
            this.BackgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.รายละเอยดผปวยToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.กรอกใบสงยาToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ขอมลใบสงยาToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.ออกจากระบบToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStrip1 = new System.Windows.Forms.MenuStrip();
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
            this.jobusername = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.matchingdatetime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.checkoutdatetime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.checkoutusername = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.calldatetime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.transferdatetime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.smartdischargedatetime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.medtransferdatetime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.medtransferuser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.waittingtime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Panel4.SuspendLayout();
            this.gbPrescription.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderPrescriptionListOrderNo)).BeginInit();
            this.Panel6.SuspendLayout();
            this.pntop.SuspendLayout();
            this.MenuStrip1.SuspendLayout();
            this.SuspendLayout();
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
            // Panel4
            // 
            this.Panel4.Controls.Add(this.gbPrescription);
            this.Panel4.Controls.Add(this.Panel2);
            this.Panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel4.Location = new System.Drawing.Point(0, 47);
            this.Panel4.Name = "Panel4";
            this.Panel4.Size = new System.Drawing.Size(1632, 714);
            this.Panel4.TabIndex = 54;
            // 
            // gbPrescription
            // 
            this.gbPrescription.Controls.Add(this.dgvOrderPrescriptionListOrderNo);
            this.gbPrescription.Controls.Add(this.Panel6);
            this.gbPrescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbPrescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.gbPrescription.Location = new System.Drawing.Point(0, 0);
            this.gbPrescription.Name = "gbPrescription";
            this.gbPrescription.Size = new System.Drawing.Size(1632, 679);
            this.gbPrescription.TabIndex = 15;
            this.gbPrescription.TabStop = false;
            this.gbPrescription.Text = "สถานะข้อมูลผู้ป่วย";
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
            this.jobusername,
            this.matchingdatetime,
            this.checkoutdatetime,
            this.checkoutusername,
            this.calldatetime,
            this.transferdatetime,
            this.smartdischargedatetime,
            this.medtransferdatetime,
            this.medtransferuser,
            this.status,
            this.waittingtime});
            this.dgvOrderPrescriptionListOrderNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrderPrescriptionListOrderNo.Location = new System.Drawing.Point(3, 49);
            this.dgvOrderPrescriptionListOrderNo.Name = "dgvOrderPrescriptionListOrderNo";
            this.dgvOrderPrescriptionListOrderNo.RowHeadersVisible = false;
            this.dgvOrderPrescriptionListOrderNo.RowHeadersWidth = 51;
            this.dgvOrderPrescriptionListOrderNo.RowTemplate.Height = 50;
            this.dgvOrderPrescriptionListOrderNo.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvOrderPrescriptionListOrderNo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrderPrescriptionListOrderNo.Size = new System.Drawing.Size(1626, 627);
            this.dgvOrderPrescriptionListOrderNo.TabIndex = 63;
            // 
            // Panel6
            // 
            this.Panel6.Controls.Add(this.lbwardcode);
            this.Panel6.Controls.Add(this.Label3);
            this.Panel6.Controls.Add(this.lbwardname);
            this.Panel6.Controls.Add(this.Label2);
            this.Panel6.Controls.Add(this.lbPrescount);
            this.Panel6.Controls.Add(this.Label1);
            this.Panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel6.Location = new System.Drawing.Point(3, 18);
            this.Panel6.Name = "Panel6";
            this.Panel6.Size = new System.Drawing.Size(1626, 31);
            this.Panel6.TabIndex = 61;
            // 
            // lbwardcode
            // 
            this.lbwardcode.AutoSize = true;
            this.lbwardcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbwardcode.Location = new System.Drawing.Point(279, 11);
            this.lbwardcode.Name = "lbwardcode";
            this.lbwardcode.Size = new System.Drawing.Size(15, 16);
            this.lbwardcode.TabIndex = 75;
            this.lbwardcode.Text = " -";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Label3.Location = new System.Drawing.Point(196, 10);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(82, 16);
            this.Label3.TabIndex = 74;
            this.Label3.Text = "รหัสหอผู้ป่วย:";
            // 
            // lbwardname
            // 
            this.lbwardname.AutoSize = true;
            this.lbwardname.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbwardname.Location = new System.Drawing.Point(429, 10);
            this.lbwardname.Name = "lbwardname";
            this.lbwardname.Size = new System.Drawing.Size(15, 16);
            this.lbwardname.TabIndex = 73;
            this.lbwardname.Text = " -";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Label2.Location = new System.Drawing.Point(370, 10);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(58, 16);
            this.Label2.TabIndex = 72;
            this.Label2.Text = "หอผู้ป่วย:";
            // 
            // lbPrescount
            // 
            this.lbPrescount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbPrescount.AutoSize = true;
            this.lbPrescount.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbPrescount.Location = new System.Drawing.Point(3, 10);
            this.lbPrescount.Name = "lbPrescount";
            this.lbPrescount.Size = new System.Drawing.Size(104, 18);
            this.lbPrescount.TabIndex = 64;
            this.lbPrescount.Text = "จำนวน 0 รายการ";
            // 
            // Panel2
            // 
            this.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Panel2.Location = new System.Drawing.Point(0, 679);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(1632, 35);
            this.Panel2.TabIndex = 51;
            // 
            // pntop
            // 
            this.pntop.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pntop.Controls.Add(this.Button1);
            this.pntop.Controls.Add(this.Label4);
            this.pntop.Controls.Add(this.cbSelect);
            this.pntop.Controls.Add(this.datecheckout);
            this.pntop.Controls.Add(this.btnSearch);
            this.pntop.Controls.Add(this.txtSearch);
            this.pntop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pntop.Location = new System.Drawing.Point(0, 0);
            this.pntop.Name = "pntop";
            this.pntop.Size = new System.Drawing.Size(1632, 47);
            this.pntop.TabIndex = 56;
            // 
            // Button1
            // 
            this.Button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Button1.Image = ((System.Drawing.Image)(resources.GetObject("Button1.Image")));
            this.Button1.Location = new System.Drawing.Point(85, 4);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(134, 40);
            this.Button1.TabIndex = 74;
            this.Button1.Text = "เลิอกหอผู้ป่วย";
            this.Button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Label4.Location = new System.Drawing.Point(14, 15);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(71, 20);
            this.Label4.TabIndex = 73;
            this.Label4.Text = "ค้นหาจาก:";
            // 
            // cbSelect
            // 
            this.cbSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSelect.FormattingEnabled = true;
            this.cbSelect.Items.AddRange(new object[] {
            "กรุณาเลือก",
            "AN",
            "เลขที่ใบสั่ง",
            "HN",
            "ชื่อ"});
            this.cbSelect.Location = new System.Drawing.Point(435, 5);
            this.cbSelect.Name = "cbSelect";
            this.cbSelect.Size = new System.Drawing.Size(214, 32);
            this.cbSelect.TabIndex = 72;
            this.cbSelect.Text = "กรุณาเลือก";
            // 
            // datecheckout
            // 
            this.datecheckout.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.datecheckout.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datecheckout.Location = new System.Drawing.Point(225, 7);
            this.datecheckout.Name = "datecheckout";
            this.datecheckout.Size = new System.Drawing.Size(200, 29);
            this.datecheckout.TabIndex = 71;
            this.datecheckout.ValueChanged += new System.EventHandler(this.datecheckout_ValueChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.Location = new System.Drawing.Point(870, 4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(98, 40);
            this.btnSearch.TabIndex = 70;
            this.btnSearch.Text = "ค้นหา";
            this.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtSearch.Location = new System.Drawing.Point(655, 8);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(209, 29);
            this.txtSearch.TabIndex = 65;
            // 
            // รายละเอยดผปวยToolStripMenuItem
            // 
            this.รายละเอยดผปวยToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.กรอกใบสงยาToolStripMenuItem,
            this.ขอมลใบสงยาToolStripMenuItem,
            this.ToolStripMenuItem1,
            this.ออกจากระบบToolStripMenuItem});
            this.รายละเอยดผปวยToolStripMenuItem.Name = "รายละเอยดผปวยToolStripMenuItem";
            this.รายละเอยดผปวยToolStripMenuItem.Size = new System.Drawing.Size(121, 25);
            this.รายละเอยดผปวยToolStripMenuItem.Text = "รายละเอียดผู้ป่วย";
            // 
            // กรอกใบสงยาToolStripMenuItem
            // 
            this.กรอกใบสงยาToolStripMenuItem.Name = "กรอกใบสงยาToolStripMenuItem";
            this.กรอกใบสงยาToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.N)));
            this.กรอกใบสงยาToolStripMenuItem.Size = new System.Drawing.Size(210, 26);
            this.กรอกใบสงยาToolStripMenuItem.Text = "กรอกใบสั่งยา";
            // 
            // ขอมลใบสงยาToolStripMenuItem
            // 
            this.ขอมลใบสงยาToolStripMenuItem.Name = "ขอมลใบสงยาToolStripMenuItem";
            this.ขอมลใบสงยาToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D)));
            this.ขอมลใบสงยาToolStripMenuItem.Size = new System.Drawing.Size(210, 26);
            this.ขอมลใบสงยาToolStripMenuItem.Text = "ข้อมูลใบสั่งยา";
            // 
            // ToolStripMenuItem1
            // 
            this.ToolStripMenuItem1.Name = "ToolStripMenuItem1";
            this.ToolStripMenuItem1.Size = new System.Drawing.Size(207, 6);
            // 
            // ออกจากระบบToolStripMenuItem
            // 
            this.ออกจากระบบToolStripMenuItem.Name = "ออกจากระบบToolStripMenuItem";
            this.ออกจากระบบToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.X)));
            this.ออกจากระบบToolStripMenuItem.Size = new System.Drawing.Size(210, 26);
            this.ออกจากระบบToolStripMenuItem.Text = "ออกจากระบบ";
            this.ออกจากระบบToolStripMenuItem.Visible = false;
            // 
            // MenuStrip1
            // 
            this.MenuStrip1.BackColor = System.Drawing.Color.AntiqueWhite;
            this.MenuStrip1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MenuStrip1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.MenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.รายละเอยดผปวยToolStripMenuItem});
            this.MenuStrip1.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip1.Name = "MenuStrip1";
            this.MenuStrip1.Size = new System.Drawing.Size(937, 29);
            this.MenuStrip1.TabIndex = 55;
            this.MenuStrip1.Text = "MenuStrip1";
            this.MenuStrip1.Visible = false;
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
            // jobusername
            // 
            this.jobusername.HeaderText = "จัดโดย";
            this.jobusername.Name = "jobusername";
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
            // checkoutusername
            // 
            this.checkoutusername.HeaderText = "ตรวจโดย";
            this.checkoutusername.Name = "checkoutusername";
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
            // frmCheckoutHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1632, 761);
            this.Controls.Add(this.Panel4);
            this.Controls.Add(this.pntop);
            this.Controls.Add(this.MenuStrip1);
            this.Name = "frmCheckoutHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmCheckoutHistory";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmCheckoutHistory_Load);
            this.Panel4.ResumeLayout(false);
            this.gbPrescription.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderPrescriptionListOrderNo)).EndInit();
            this.Panel6.ResumeLayout(false);
            this.Panel6.PerformLayout();
            this.pntop.ResumeLayout(false);
            this.pntop.PerformLayout();
            this.MenuStrip1.ResumeLayout(false);
            this.MenuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Panel Panel4;
        internal System.Windows.Forms.GroupBox gbPrescription;
        internal System.Windows.Forms.Panel Panel6;
        internal System.Windows.Forms.Label lbwardcode;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label lbwardname;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label lbPrescount;
        internal System.Windows.Forms.Panel Panel2;
        internal System.Windows.Forms.Panel pntop;
        internal System.Windows.Forms.Button Button1;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.DateTimePicker datecheckout;
        internal System.Windows.Forms.Button btnSearch;
        internal System.Windows.Forms.TextBox txtSearch;
        internal System.Windows.Forms.Timer Timer1;
        internal System.ComponentModel.BackgroundWorker BackgroundWorker1;
        internal System.Windows.Forms.ToolStripMenuItem รายละเอยดผปวยToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem กรอกใบสงยาToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem ขอมลใบสงยาToolStripMenuItem;
        internal System.Windows.Forms.ToolStripSeparator ToolStripMenuItem1;
        internal System.Windows.Forms.ToolStripMenuItem ออกจากระบบToolStripMenuItem;
        internal System.Windows.Forms.MenuStrip MenuStrip1;
        internal System.Windows.Forms.ComboBox cbSelect;
        internal System.Windows.Forms.DataGridView dgvOrderPrescriptionListOrderNo;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn jobusername;
        private System.Windows.Forms.DataGridViewTextBoxColumn matchingdatetime;
        private System.Windows.Forms.DataGridViewTextBoxColumn checkoutdatetime;
        private System.Windows.Forms.DataGridViewTextBoxColumn checkoutusername;
        private System.Windows.Forms.DataGridViewTextBoxColumn calldatetime;
        private System.Windows.Forms.DataGridViewTextBoxColumn transferdatetime;
        private System.Windows.Forms.DataGridViewTextBoxColumn smartdischargedatetime;
        private System.Windows.Forms.DataGridViewTextBoxColumn medtransferdatetime;
        private System.Windows.Forms.DataGridViewTextBoxColumn medtransferuser;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewTextBoxColumn waittingtime;
    }
}