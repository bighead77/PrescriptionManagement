using PrescriptionManagement.view;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PrescriptionManagement.cls;
using System.Globalization;

namespace PrescriptionManagement
{
    public partial class frmMain : Form
    {
        clsService clsService = new clsService();
        clsSetting clsSetting = new clsSetting();
        
        public frmMain()
        {
            InitializeComponent();
        }

        private async void frmMain_Load(object sender, EventArgs e)
        {
            frmLogin f = new frmLogin();
            f.ShowDialog();

            lbPrintername.Visible = false;
            lb_userid.Visible = false;
            lbUsername.Visible = false;
            //เรียก uc มาแสดง
            //panel_main.Controls.Clear();
            //var ctrl = new uc_CheckoutPrescription();
            //ctrl.Height = panel_main.Height;
            //ctrl.Width = panel_main.Width;
            //this.panel_main.Controls.Add(ctrl);


            if (cls.clsuser.name.Length > 0)
            {
                toolStripLabel7.Text = " ชื่อ : " + cls.clsuser.name.Trim();
            }
            else
            {
                toolStripLabel7.Text = " ชื่อ : - ";
            }

            if (cls.clsuser.department.Length > 0)
            {
                toolStripLabel5.Text = " ตำแหน่ง : " + cls.clsuser.department.Trim();
            }
            else
            {
                toolStripLabel5.Text = " ตำแหน่ง : - ";
            }
            if (cls.clsuser.userid.Length > 0)
            {
                toolStripLabel4.Text = " รหัสพนักงาน : " + cls.clsuser.userid.Trim();
            }
            else
            {
                toolStripLabel4.Text = " รหัสพนักงาน : - ";
            }
            clsSetting.COM_NAME = PrescriptionManagement.Properties.Settings.Default.comname;
            //clsSetting.COM_NAME = System.Windows.Forms.SystemInformation.ComputerName;           
            //clsSetting.COM_NAME = "GD4-OLED-1";

            if (clsSetting.COM_NAME.Contains("GD4-IMAT"))
            {
                //เรียก uc มาแสดง
                panel_main.Controls.Clear();
                var ctrl = new uc_MatchingPrescription();
                ctrl.Height = panel_main.Height;
                ctrl.Width = panel_main.Width;
                this.panel_main.Controls.Add(ctrl);

                tbtn_MatchingBasket.Visible = false;
                tbtn_RegisterPres.Visible = true;
            }
            else if (clsSetting.COM_NAME.Contains("GD4-ICHO"))
            {
                //เรียก uc มาแสดง

                panel_main.Controls.Clear();
                var ctrl = new uc_CheckoutPrescription();
                ctrl.Height = panel_main.Height;
                ctrl.Width = panel_main.Width;
                this.panel_main.Controls.Add(ctrl);
                tbtn_RegisterPres.Visible = false;
                tbtn_MatchingBasket.Visible = true;
            }
            else
            {
                //เรียก uc มาแสดง

                panel_main.Controls.Clear();
                var ctrl = new uc_CheckoutPrescription();
                ctrl.Height = panel_main.Height;
                ctrl.Width = panel_main.Width;
                this.panel_main.Controls.Add(ctrl);
                tbtn_RegisterPres.Visible = false;
                tbtn_MatchingBasket.Visible = true;
            }

            await cls.clsService.RequestDevice();
            Setting();


        }
        public void Setting()
        {
            
            toolStripLabel2.Text = "-";
            string zone = "";
            if (clsPackagemaster.db_device.Rows.Count > 0)
            {
                DataTable data = clsPackagemaster.db_device.Select("computerName = '" + clsSetting.COM_NAME + "'").CopyToDataTable();
                if(data.Rows.Count > 0)
                {
                    foreach(DataRow r in data.Rows)
                    {
                        clsSetting.Location = r["deviceCode"].ToString();
                        if(zone == "")
                        {
                            zone +="Zone : "+ r["deviceCode"].ToString();
                        }
                        else
                        {
                            zone += " / "+ r["deviceCode"].ToString();
                        }                        
                        clsSetting.ZONE = r["deviceName"].ToString();
                        toolStripLabel3.Text = "Computer Name: "+ r["computerName"].ToString();
                        toolStripSeqzone.Text = r["orderzone"].ToString();
                        //clsSetting.isprint = r["isprint"].ToString();
                        clsSetting.orderzone = r["orderzone"].ToString();
                        clsSetting.sortOrder = r["sortOrder"].ToString();
                    }
                    toolStripLabel2.Text = zone;
                }                

            }
        }

        private void tbtn_MatchingPres_Click_1(object sender, EventArgs e)
        {
            //เรียก uc มาแสดง
            panel_main.Controls.Clear();
            var ctrl = new uc_MatchingPrescription();
            ctrl.Height = panel_main.Height;
            ctrl.Width = panel_main.Width;
            this.panel_main.Controls.Add(ctrl);

        }

        private async void tbtn_WorkingStatus_Click(object sender, EventArgs e)
        {
           
            panel_main.Controls.Clear();
            var ctrl = new uc_BasketManagement();
            ctrl.Height = panel_main.Height;
            ctrl.Width = panel_main.Width;
            this.panel_main.Controls.Add(ctrl);
        }

        private void tbtn_MatchingBasket_Click(object sender, EventArgs e)
        {
            //เรียก uc มาแสดง

            panel_main.Controls.Clear();
            var ctrl = new uc_CheckoutPrescription();
            ctrl.Height = panel_main.Height;
            ctrl.Width = panel_main.Width;
            this.panel_main.Controls.Add(ctrl);
        }

        private async void btnLogOut_Click(object sender, EventArgs e)
        {
            DateTime utcNow = DateTime.Now;
            string str_datenow_log = cls.clsconvertdate.date_utc(utcNow.ToString());
            List<object> listlog = new List<object>();

            listlog.Add(new
            {
                createddate = (str_datenow_log.ToString().Length > 0) ? Convert.ToDateTime(str_datenow_log.ToString()).AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture).ToString() : "",
                dateupdate = (str_datenow_log.ToString().Length > 0) ? Convert.ToDateTime(str_datenow_log.ToString()).AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture).ToString() : "",
                platform = "checkout",
                function = "checkout",
                description = "ออกจากระบบ checkout",
                userid = cls.clsuser.userid,
                username = cls.clsuser.name,
                __v = 0
            });

            await clsService.update_logevent(listlog);

            Application.Exit();
        }

        private async void toolStripButton2_Click(object sender, EventArgs e)
        {
            await cls.clsService.RequestDevice();

            clsSetting.COM_NAME = System.Windows.Forms.SystemInformation.ComputerName;
            clsSetting.COM_NAME = toolStripTextBox1.Text.Trim();
            toolStripLabel2.Text = "-";
            string zone = "";
            if (clsPackagemaster.db_device.Rows.Count > 0)
            {
                DataTable data = clsPackagemaster.db_device.Select("computerName = '" + clsSetting.COM_NAME + "'").CopyToDataTable();
                if (data.Rows.Count > 0)
                {
                    foreach (DataRow r in data.Rows)
                    {
                        clsSetting.Location = r["deviceCode"].ToString();
                        zone += " / "+r["deviceCode"].ToString();
                        clsSetting.ZONE = r["deviceName"].ToString();
                        toolStripLabel3.Text = "Computer Name: " + r["computerName"].ToString();
                        toolStripSeqzone.Text = r["orderzone"].ToString();
                        //clsSetting.isprint = r["isprint"].ToString();
                        clsSetting.orderzone = r["orderzone"].ToString();
                    }
                    toolStripLabel2.Text = zone;
                }

            }

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            frmCheckoutHistory frm = new frmCheckoutHistory("",""); // สร้าง instance
            frm.ShowDialog();
        }
    }
}
