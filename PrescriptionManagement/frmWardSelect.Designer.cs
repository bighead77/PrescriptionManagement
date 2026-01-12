
namespace PrescriptionManagement
{
    partial class frmWardSelect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWardSelect));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lbcount = new System.Windows.Forms.Label();
            this.pbCleartxtSearch = new System.Windows.Forms.PictureBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnKeyboard = new System.Windows.Forms.Button();
            this.dgvWard = new System.Windows.Forms.DataGridView();
            this.Panel3 = new System.Windows.Forms.Panel();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.wardcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.warddescfull = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.pbCleartxtSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWard)).BeginInit();
            this.Panel3.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbcount
            // 
            this.lbcount.AutoSize = true;
            this.lbcount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbcount.Location = new System.Drawing.Point(308, 27);
            this.lbcount.Name = "lbcount";
            this.lbcount.Size = new System.Drawing.Size(111, 20);
            this.lbcount.TabIndex = 59;
            this.lbcount.Text = "จำนวน 0 รายการ";
            // 
            // pbCleartxtSearch
            // 
            this.pbCleartxtSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.pbCleartxtSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pbCleartxtSearch.Image = ((System.Drawing.Image)(resources.GetObject("pbCleartxtSearch.Image")));
            this.pbCleartxtSearch.Location = new System.Drawing.Point(256, 17);
            this.pbCleartxtSearch.Name = "pbCleartxtSearch";
            this.pbCleartxtSearch.Size = new System.Drawing.Size(26, 25);
            this.pbCleartxtSearch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbCleartxtSearch.TabIndex = 58;
            this.pbCleartxtSearch.TabStop = false;
            this.pbCleartxtSearch.Visible = false;
            this.pbCleartxtSearch.Click += new System.EventHandler(this.pbCleartxtSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtSearch.ForeColor = System.Drawing.Color.DimGray;
            this.txtSearch.Location = new System.Drawing.Point(53, 12);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(197, 35);
            this.txtSearch.TabIndex = 57;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // btnKeyboard
            // 
            this.btnKeyboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.btnKeyboard.FlatAppearance.BorderSize = 0;
            this.btnKeyboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKeyboard.Image = ((System.Drawing.Image)(resources.GetObject("btnKeyboard.Image")));
            this.btnKeyboard.Location = new System.Drawing.Point(6, 10);
            this.btnKeyboard.Name = "btnKeyboard";
            this.btnKeyboard.Size = new System.Drawing.Size(41, 37);
            this.btnKeyboard.TabIndex = 54;
            this.btnKeyboard.UseVisualStyleBackColor = false;
            // 
            // dgvWard
            // 
            this.dgvWard.AllowUserToAddRows = false;
            this.dgvWard.AllowUserToDeleteRows = false;
            this.dgvWard.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvWard.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvWard.ColumnHeadersHeight = 40;
            this.dgvWard.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvWard.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.wardcode,
            this.warddescfull});
            this.dgvWard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvWard.Location = new System.Drawing.Point(3, 82);
            this.dgvWard.Name = "dgvWard";
            this.dgvWard.ReadOnly = true;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvWard.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvWard.RowHeadersVisible = false;
            this.dgvWard.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvWard.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dgvWard.RowTemplate.Height = 40;
            this.dgvWard.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvWard.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvWard.Size = new System.Drawing.Size(429, 341);
            this.dgvWard.TabIndex = 65;
            this.dgvWard.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWard_CellClick);
            // 
            // Panel3
            // 
            this.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel3.Controls.Add(this.lbcount);
            this.Panel3.Controls.Add(this.pbCleartxtSearch);
            this.Panel3.Controls.Add(this.txtSearch);
            this.Panel3.Controls.Add(this.btnKeyboard);
            this.Panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel3.Location = new System.Drawing.Point(3, 22);
            this.Panel3.Name = "Panel3";
            this.Panel3.Size = new System.Drawing.Size(429, 60);
            this.Panel3.TabIndex = 63;
            // 
            // GroupBox1
            // 
            this.GroupBox1.BackColor = System.Drawing.Color.Transparent;
            this.GroupBox1.Controls.Add(this.dgvWard);
            this.GroupBox1.Controls.Add(this.Panel3);
            this.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GroupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.GroupBox1.Location = new System.Drawing.Point(0, 0);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(435, 426);
            this.GroupBox1.TabIndex = 143;
            this.GroupBox1.TabStop = false;
            // 
            // wardcode
            // 
            this.wardcode.DataPropertyName = "wardcode";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.wardcode.DefaultCellStyle = dataGridViewCellStyle2;
            this.wardcode.HeaderText = "รหัสหอผู้ป่วย";
            this.wardcode.Name = "wardcode";
            this.wardcode.ReadOnly = true;
            this.wardcode.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // warddescfull
            // 
            this.warddescfull.DataPropertyName = "warddesc";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.warddescfull.DefaultCellStyle = dataGridViewCellStyle3;
            this.warddescfull.HeaderText = "ชื่อหอผู้ป่วย";
            this.warddescfull.Name = "warddescfull";
            this.warddescfull.ReadOnly = true;
            this.warddescfull.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.warddescfull.Width = 250;
            // 
            // frmWardSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 426);
            this.Controls.Add(this.GroupBox1);
            this.Name = "frmWardSelect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmWardSelect";
            this.Load += new System.EventHandler(this.frmWardSelect_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbCleartxtSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWard)).EndInit();
            this.Panel3.ResumeLayout(false);
            this.Panel3.PerformLayout();
            this.GroupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Label lbcount;
        internal System.Windows.Forms.PictureBox pbCleartxtSearch;
        internal System.Windows.Forms.TextBox txtSearch;
        internal System.Windows.Forms.Button btnKeyboard;
        internal System.Windows.Forms.DataGridView dgvWard;
        internal System.Windows.Forms.Panel Panel3;
        internal System.Windows.Forms.GroupBox GroupBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn wardcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn warddescfull;
    }
}