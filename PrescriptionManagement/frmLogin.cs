using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using PrescriptionManagement.cls;

namespace PrescriptionManagement
{
    public partial class frmLogin : Form
    {
        clsService clsService = new clsService();
        clsconvertdate clsconv = new clsconvertdate();
        bool loginstatus = false;
        public frmLogin()
        {
            InitializeComponent();
        }

        private async void btn_Login_Click(object sender, EventArgs e)
        {
            DataTable tb = new DataTable();
            DataTable dbUser = new DataTable();
            (tb, dbUser) = await clsService.RequestLogig(txt_username.Text.Trim(), txtpass.Text.Trim());
            if (tb.Rows.Count > 0)
            {
                DateTime utcNow = DateTime.Now;
                string str_datenow_log = cls.clsconvertdate.date_utc(utcNow.ToString());
                cls.clsuser.userid = tb.Rows[0]["userID"].ToString();
                //dbUser = await clsService.RequestUserID(cls.clsuser.userid);
                //if (dbUser.Rows.Count > 0)
                //{
                //    cls.clsuser.name = dbUser.Rows[0]["firstname"].ToString() + "  " + dbUser.Rows[0]["lastname"].ToString();
                //    cls.clsuser.department = dbUser.Rows[0]["type"].ToString();

                //    this.Close();
                //}
               
                cls.clsuser.name = tb.Rows[0]["fullname"].ToString();
                cls.clsuser.department = tb.Rows[0]["type"].ToString();

                List<object> listlog = new List<object>();

                listlog.Add(new
                {
                    createddate = (str_datenow_log.ToString().Length > 0) ? Convert.ToDateTime(str_datenow_log.ToString()).AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture).ToString() : "",
                    dateupdate = (str_datenow_log.ToString().Length > 0) ? Convert.ToDateTime(str_datenow_log.ToString()).AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture).ToString() : "",
                    platform = "checkout",
                    function = "checkout",
                    description = "เข้าใช้งานหน้าcheckout",
                    userid = cls.clsuser.userid,
                    username = cls.clsuser.name,
                    __v = 0
                });

                await clsService.update_logevent(listlog);

                this.Close();
            }
            else
            {
                cls.clsuser.name = "";
                cls.clsuser.department = "";
                cls.clsuser.userid = "";
                MessageBox.Show(" รหัสผ่านไม่ถูกต้อง !! ");
                txtpass.SelectAll();

            }


        }

        public async void txtpass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                DataTable tb = new DataTable();
                DataTable dbUser = new DataTable();
                (tb, dbUser) = await clsService.RequestLogig(txt_username.Text.Trim(), txtpass.Text.Trim());
                if (tb.Rows.Count > 0)
                {
                    DateTime utcNow = DateTime.Now;
                    string str_datenow_log = cls.clsconvertdate.date_utc(utcNow.ToString());
                    cls.clsuser.userid = tb.Rows[0]["userID"].ToString();
                    //dbUser = await clsService.RequestUserID(cls.clsuser.userid);
                    //if (dbUser.Rows.Count > 0)
                    //{
                    //    cls.clsuser.name = dbUser.Rows[0]["firstname"].ToString() + "  " + dbUser.Rows[0]["lastname"].ToString();
                    //    cls.clsuser.department = dbUser.Rows[0]["type"].ToString();

                    //    this.Close();
                    //}

                    cls.clsuser.name = tb.Rows[0]["fullname"].ToString();
                    cls.clsuser.department = tb.Rows[0]["type"].ToString();

                    List<object> listlog = new List<object>();

                    listlog.Add(new
                    {
                        createddate = (str_datenow_log.ToString().Length > 0) ? Convert.ToDateTime(str_datenow_log.ToString()).AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture).ToString() : "",
                        dateupdate = (str_datenow_log.ToString().Length > 0) ? Convert.ToDateTime(str_datenow_log.ToString()).AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture).ToString() : "",
                        platform = "checkout",
                        function = "checkout",
                        description = "เข้าใช้งานหน้าcheckout",
                        userid = cls.clsuser.userid,
                        username = cls.clsuser.name,
                        __v = 0
                    });

                    await clsService.update_logevent(listlog);

                    this.Close();
                }
                else
                {
                    cls.clsuser.name = "";
                    cls.clsuser.department = "";
                    cls.clsuser.userid = "";
                    MessageBox.Show(" รหัสผ่านไม่ถูกต้อง !! ");
                    txtpass.SelectAll();

                }
            }
            
        }
        

        public string DecryptData(string strData)
        {
            try
            {
                string strDecrypt = "";
                string strReturn = "";
                //GetLinkApi_Decrypt();
                strDecrypt = PrescriptionManagement.Properties.Settings.Default.apiUrl+"/card/decrypt/" + strData;
                using (var client = new HttpClient())
                {
                    using (HttpResponseMessage response = client.GetAsync(strDecrypt).Result)
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            string responsedata = response.Content.ReadAsStringAsync().Result;

                            strReturn = responsedata;
                        }
                    }
                }
                return strReturn;
            }
            catch (Exception)
            {
                return "";
            }
        }

        private async void txt_username_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                DateTime utcNow = DateTime.Now;
                string str_datenow_log = cls.clsconvertdate.date_utc(utcNow.ToString());
                string[] result;
                if (txt_username.Text != "")
                {
                    result = txt_username.Text.Split('|');
                    if (result[0] == "gd4")
                    {
                        // Bardcode
                        string Data = "";
                        Data = DecryptData(result[1]);
                        loginstatus = await clsService.GetUserLoginScan(Data);
                        if (loginstatus)
                        {

                            //cls.clsuser.name = tb.Rows[0]["fullname"].ToString();
                            //cls.clsuser.department = tb.Rows[0]["type"].ToString();

                            List<object> listlog = new List<object>();

                            listlog.Add(new
                            {
                                createddate = (str_datenow_log.ToString().Length > 0) ? Convert.ToDateTime(str_datenow_log.ToString()).AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture).ToString() : "",
                                dateupdate = (str_datenow_log.ToString().Length > 0) ? Convert.ToDateTime(str_datenow_log.ToString()).AddHours(-7).ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture).ToString() : "",
                                platform = "checkout",
                                function = "checkout",
                                description = "เข้าใช้งานหน้าcheckout",
                                userid = cls.clsuser.userid,
                                username = cls.clsuser.name,
                                __v = 0
                            });

                            await clsService.update_logevent(listlog);

                            this.Close();
                        }
                        else
                        {

                            txt_username.SelectAll();

                        }
                    }
                    else
                    {
                        txt_username.SelectAll();
                    }
                }
                
               
            }
            
        }
       
        private void btn_Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                txtpass.PasswordChar = '\0';
            }
            else
            {
                txtpass.PasswordChar = '*';
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
