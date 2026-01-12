using System;
using System.Data;
using System.Windows.Forms;
using Amazon.Util.Internal;
using CrystalDecisions.CrystalReports.Engine;
using Newtonsoft.Json;
using PrescriptionManagement.cls;
using System.Text.Json;
using System.Text;
using System.Collections.Generic;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Drawing;

namespace PrescriptionManagement.view
{

    public partial class uc_RegisterPrescription : UserControl
    {
        clsService clsService = new clsService();
        clsconvertdate clsconvert = new clsconvertdate();
        public uc_RegisterPrescription()
        {
            InitializeComponent();
            timer1.Enabled = true;
            if(txt_BarcodePrescr.Text == "" )
            {
                //timer1.Start();
            }
        }

        private async void btn_MatchingOrder_Click(object sender, EventArgs e)
        {
            register_basket();
        }
        public async void register_basket()
        {
            timer1.Stop();
            DataTable dtprint = new DataTable();
            DataTable dbUser = new DataTable();
            dbUser = await clsService.RequestUserID(txtuserid.Text.Trim());
            if (dbUser.Rows.Count > 0)
            {
                lbusername.Text = dbUser.Rows[0]["firstname"].ToString() + "  " + dbUser.Rows[0]["lastname"].ToString();
                if (clsSetting.EnterEvent)
                {
                    if (txt_BarcodePrescr.Text != "" && txtuserid.Text != "")
                    {
                        List<object> ListJson = new List<object>();
                        dtprint = print_stricker_next(lbPres.Text);
                        rpt_re_stricker(dtprint); // สติ๊กเกอร์ไม่มีหัว

                        DateTime utcNow = DateTime.Now;
                        string str_datenow = date_utc(utcNow.ToString());
                        ListJson = GenJson_RegisUser(str_datenow.Trim());
                        if(ListJson.Count > 0)
                        {
                            //await clsService.update_regisbasket(ListJson);
                        }                        

                    }
                }
                else
                {
                    if (txt_BarcodePrescr.Text != "" && txt_BarcodeBasket.Text != "" && txtuserid.Text != "")
                    {
                        List<object> ListJson = new List<object>();
                        dtprint = print_stricker(lbPres.Text);
                        rpt_stricker(dtprint);
                        
                        DateTime utcNow = DateTime.Now;
                        string str_datenow = date_utc(utcNow.ToString());
                        ListJson = GenJson_Regisbasket(str_datenow.Trim());

                        if (ListJson.Count > 0)
                        {
                            //await clsService.update_regisbasket(ListJson);
                        }


                    }
                }
               
                
            }
            else
            {
                // ไม่พบข้อมูล user
            }

            clear();
            txt_BarcodePrescr.ReadOnly = false;
            blRFIDNum.Text = "-";
            timer1.Start();
        }
        public List<object> GenJson_Regisbasket(string datetime)
        {
            List<object> listpackagemaster = new List<object>();
            List<object> listJson = new List<object>();
            try
            {
                if (clsPackagemaster.db_packagemaster.Rows.Count > 0 && clsPackagemaster.db_drug.Rows.Count > 0 && clsPackagemaster.db_data.Rows.Count > 0)
                {

                    List<object> listpack = new List<object>();
                    foreach (DataRow rw in clsPackagemaster.db_packagemaster.Rows)
                    {
                        if(rw["orderzone"].ToString() == clsSetting.orderzone)
                        {
                            listpackagemaster.Add(new
                            {
                                _id = rw["_id"].ToString(),
                                _id_prescription = rw["_id_prescription"].ToString(),
                                seqrun = Convert.ToInt32(rw["seqrun"].ToString()),
                                __v = Convert.ToInt32(rw["__v"].ToString()),
                                dateupdate = rw["dateupdate"].ToString(),
                                drugaccountcode = rw["drugaccountcode"].ToString(),
                                edned = rw["edned"].ToString(),
                                genorderdatetime = Convert.ToDateTime(rw["genorderdatetime"].ToString()).AddHours(7).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).ToString(),
                                itemidentify = rw["itemidentify"].ToString(),
                                orderitembarcode = rw["orderitembarcode"].ToString(),
                                orderitemcode = rw["orderitemcode"].ToString(),
                                orderitemname = rw["orderitemname"].ToString(),
                                orderqty = Convert.ToInt32(rw["orderqty"].ToString()),
                                orderunitcode = rw["orderunitcode"].ToString(),
                                orderunitdesc = rw["orderunitdesc"].ToString(),
                                poison = rw["poison"].ToString(),
                                pregnancy = rw["pregnancy"].ToString(),
                                prescriptionno = rw["prescriptionno"].ToString(),
                                prescriptionno_sup = rw["prescriptionno_sup"].ToString(),
                                seq = Convert.ToInt32(rw["seq"].ToString()),
                                seqmax = Convert.ToInt32(rw["seqmax"].ToString()),
                                shelfname = rw["shelfname"].ToString(),
                                shelfzone = rw["shelfzone"].ToString(),
                                orderzone = Convert.ToInt32(rw["orderzone"].ToString()),
                                basketno = txt_BarcodeBasket.Text.Trim(),
                                basketid = blRFIDNum.Text.Trim(),
                                jobdatetime = datetime,
                                jobuserid = txtuserid.Text.Trim(),
                                jobusername = lbusername.Text.Trim(),
                                zoneindt = datetime
                            });
                        }
                        else
                        {
                            listpackagemaster.Add(new
                            {
                                _id = rw["_id"].ToString(),
                                _id_prescription = rw["_id_prescription"].ToString(),
                                seqrun = Convert.ToInt32(rw["seqrun"].ToString()),
                                __v = Convert.ToInt32(rw["__v"].ToString()),
                                dateupdate = rw["dateupdate"].ToString(),
                                drugaccountcode = rw["drugaccountcode"].ToString(),
                                edned = rw["edned"].ToString(),
                                genorderdatetime = Convert.ToDateTime(rw["genorderdatetime"].ToString()).AddHours(7).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).ToString(),
                                itemidentify = rw["itemidentify"].ToString(),
                                orderitembarcode = rw["orderitembarcode"].ToString(),
                                orderitemcode = rw["orderitemcode"].ToString(),
                                orderitemname = rw["orderitemname"].ToString(),
                                orderqty = Convert.ToInt32(rw["orderqty"].ToString()),
                                orderunitcode = rw["orderunitcode"].ToString(),
                                orderunitdesc = rw["orderunitdesc"].ToString(),
                                poison = rw["poison"].ToString(),
                                pregnancy = rw["pregnancy"].ToString(),
                                prescriptionno = rw["prescriptionno"].ToString(),
                                prescriptionno_sup = rw["prescriptionno_sup"].ToString(),
                                seq = Convert.ToInt32(rw["seq"].ToString()),
                                seqmax = Convert.ToInt32(rw["seqmax"].ToString()),
                                shelfname = rw["shelfname"].ToString(),
                                shelfzone = rw["shelfzone"].ToString(),
                                orderzone = Convert.ToInt32(rw["orderzone"].ToString()),
                                basketno = txt_BarcodeBasket.Text.Trim(),
                                basketid = blRFIDNum.Text.Trim(),
                                //jobdatetime = datetime,
                                //jobuserid = txtuserid.Text.Trim(),
                                //jobusername = lbusername.Text.Trim(),
                                //zoneindt = datetime
                            });
                        }
                        
                    }
                }
                return listpackagemaster;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return listpackagemaster;
            }
            finally
            {

            }
        }

        public List<object> GenJson_RegisUser(string datetime)
        {
            List<object> listpackagemaster = new List<object>();
            List<object> listJson = new List<object>();
            try
            {
                if (clsPackagemaster.db_packagemaster.Rows.Count > 0)
                {

                    List<object> listpack = new List<object>();
                    foreach (DataRow rw in clsPackagemaster.db_packagemaster.Rows)
                    {
                        listpackagemaster.Add(new
                        {
                            _id = rw["_id"].ToString(),
                            _id_prescription = rw["_id_prescription"].ToString(),
                            seqrun = Convert.ToInt32(rw["seqrun"].ToString()),
                            __v = Convert.ToInt32(rw["__v"].ToString()),
                            dateupdate = rw["dateupdate"].ToString(),
                            drugaccountcode = rw["drugaccountcode"].ToString(),
                            edned = rw["edned"].ToString(),
                            genorderdatetime = Convert.ToDateTime(rw["genorderdatetime"].ToString()).AddHours(7).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).ToString(),
                            itemidentify = rw["itemidentify"].ToString(),
                            orderitembarcode = rw["orderitembarcode"].ToString(),
                            //orderitemcode = rw["orderitemcode"].ToString(),
                            orderitemname = rw["orderitemname"].ToString(),
                            orderqty = Convert.ToInt32(rw["orderqty"].ToString()),
                            orderunitcode = rw["orderunitcode"].ToString(),
                            orderunitdesc = rw["orderunitdesc"].ToString(),
                            poison = rw["poison"].ToString(),
                            pregnancy = rw["pregnancy"].ToString(),
                            prescriptionno = rw["prescriptionno"].ToString(),
                            prescriptionno_sup = rw["prescriptionno_sup"].ToString(),
                            seq = Convert.ToInt32(rw["seq"].ToString()),
                            seqmax = Convert.ToInt32(rw["seqmax"].ToString()),
                            shelfname = rw["shelfname"].ToString(),
                            shelfzone = rw["shelfzone"].ToString(),
                            orderzone = Convert.ToInt32(rw["orderzone"].ToString()),
                            basketno = rw["basketno"].ToString(),
                            basketid = rw["basketid"].ToString(),
                            jobdatetime = datetime,
                            jobuserid = txtuserid.Text.Trim(),
                            jobusername = lbusername.Text.Trim(),
                            zoneindt = datetime
                        });

                    }
                }
                return listpackagemaster;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return listpackagemaster;
            }
            finally
            {

            }
        }
        public List<object> GenJson_printdatetime(string printdatetime)
        {
            List<object> listprescription = new List<object>();
            List<object> listJson = new List<object>();

            try
            {
                if (clsPackagemaster.db_packagemaster.Rows.Count > 0)
                {

                    
                    clsJsonprescription clsJson = new clsJsonprescription();
                    {
                        if (clsPackagemaster.db_drug.Rows.Count > 0)
                        {
                            List<object> listlabs = new List<object>();
                            List<object> listJsondrugsn = new List<object>();
                            List<object> listdrugallergies = new List<object>();
                            List<object> listhis = new List<object>();
                            foreach (DataRow rw in clsPackagemaster.db_drug.Rows)
                            {
                                listJsondrugsn.Add(new
                                {
                                    seq = rw["seq"].ToString(),
                                    orderitemcode = rw["orderitemcode"].ToString(),
                                    orderitemname = rw["orderitemname"].ToString(),
                                    orderqty = rw["orderqty"].ToString(),
                                    strength = rw["strength"].ToString(),
                                    strengthunit = rw["strengthunit"].ToString(),
                                    instructioncode = rw["instructioncode"].ToString(),
                                    instructiondesc = rw["instructiondesc"].ToString(),
                                    dosage = rw["dosage"].ToString(),
                                    dosageunitcode = rw["dosageunitcode"].ToString(),
                                    dosageunitdesc = rw["dosageunitdesc"].ToString(),
                                    frequencycode = rw["frequencycode"].ToString(),
                                    frequencydesc = rw["frequencydesc"].ToString(),
                                    freetext1 = rw["freetext1"].ToString(),
                                    freetext2 = rw["freetext2"].ToString(),
                                    freetext3 = rw["freetext3"].ToString(),
                                    freetext4 = rw["freetext4"].ToString(),
                                    freetext5 = rw["freetext5"].ToString(),
                                    price = rw["price"].ToString(),
                                    precaution_advice_text = rw["precaution_advice_text"].ToString(),
                                    special_advice_text = rw["special_advice_text"].ToString(),
                                    refund = rw["refund"].ToString(),
                                    norefund = rw["norefund"].ToString(),
                                    icsale = rw["icsale"].ToString(),
                                    icrefund = rw["icrefund"].ToString(),
                                    icnorefund = rw["icnorefund"].ToString(),
                                    store = rw["store"].ToString()
                                });

                                #region clsJsondrugs
                                clsJsondrugs drug = new clsJsondrugs()
                                {
                                    seq = rw["seq"].ToString(),
                                    orderitemcode = rw["orderitemcode"].ToString(),
                                    orderitemname = rw["orderitemname"].ToString(),
                                    orderqty = rw["orderqty"].ToString(),
                                    strength = rw["strength"].ToString(),
                                    strengthunit = rw["strengthunit"].ToString(),
                                    instructioncode = rw["instructioncode"].ToString(),
                                    instructiondesc = rw["instructiondesc"].ToString(),
                                    dosage = rw["dosage"].ToString(),
                                    dosageunitcode = rw["dosageunitcode"].ToString(),
                                    dosageunitdesc = rw["dosageunitdesc"].ToString(),
                                    frequencycode = rw["frequencycode"].ToString(),
                                    frequencydesc = rw["frequencydesc"].ToString(),
                                    freetext1 = rw["freetext1"].ToString(),
                                    freetext2 = rw["freetext2"].ToString(),
                                    freetext3 = rw["freetext3"].ToString(),
                                    freetext4 = rw["freetext4"].ToString(),
                                    freetext5 = rw["freetext5"].ToString(),
                                    price = rw["price"].ToString(),
                                    precaution_advice_text = rw["precaution_advice_text"].ToString(),
                                    special_advice_text = rw["special_advice_text"].ToString(),
                                    refund = rw["refund"].ToString(),
                                    norefund = rw["norefund"].ToString(),
                                    icsale = rw["icsale"].ToString(),
                                    icrefund = rw["icrefund"].ToString(),
                                    icnorefund = rw["icnorefund"].ToString(),
                                    store = rw["store"].ToString()
                                };

                                // เพิ่มข้อมูลลงใน List drugs
                                clsJson.drugs.Add(drug);
                                #endregion
                            }
                            // กำหนดค่าของ ContactLabs
                            if (clsPackagemaster.db_labs.Rows.Count > 0)
                            {
                                
                                foreach (DataRow rw in clsPackagemaster.db_labs.Rows)
                                {
                                    listlabs.Add(new
                                    {
                                        lab_no = rw["lab_no"].ToString(),
                                        lab_code = rw["lab_code"].ToString(),
                                        lab_name = rw["lab_name"].ToString(),
                                        result = rw["result"].ToString(),
                                        type_result = rw["type_result"].ToString(),
                                        unit = rw["unit"].ToString(),
                                        minresult = rw["minresult"].ToString(),
                                        maxresult = rw["maxresult"].ToString(),
                                        remark = rw["remark"].ToString(),
                                        date_result = (rw["date_result"].ToString().Length > 0) ? Convert.ToDateTime(rw["date_result"].ToString()).AddHours(7).ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture).ToString() : ""
                                    });
                                    clsJsonlabs labs = new clsJsonlabs
                                    {
                                        lab_no = rw["lab_no"].ToString(),
                                        lab_code = rw["lab_code"].ToString(),
                                        lab_name = rw["lab_name"].ToString(),
                                        result = rw["result"].ToString(),
                                        type_result = rw["type_result"].ToString(),
                                        unit = rw["unit"].ToString(),
                                        minresult = rw["minresult"].ToString(),
                                        maxresult = rw["maxresult"].ToString(),
                                        remark = rw["remark"].ToString(),
                                        date_result = (rw["date_result"].ToString().Length > 0) ? Convert.ToDateTime(rw["date_result"].ToString()).AddHours(7).ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture).ToString() : ""

                                    };
                                    // เพิ่มข้อมูลลงใน List drugs
                                    clsJson.labs.Add(labs);
                                }

                            }
                            

                            if (clsPackagemaster.db_drugallergies.Rows.Count > 0)
                            {                                
                                foreach (DataRow rw in clsPackagemaster.db_drugallergies.Rows)
                                {
                                    listdrugallergies.Add(new
                                    {
                                        allergycode = rw["allergycode"].ToString(),
                                        code = rw["code"].ToString(),
                                        genericname = rw["genericname"].ToString(),
                                        adverbs = rw["adverbs"].ToString(),
                                        memo = rw["memo"].ToString(),
                                        //genorderdatetime = Convert.ToDateTime(rw["genorderdatetime"]).AddHours(7).ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture).ToString()

                                    });
                                    clsJsondrugallergies clsdrugallergies = new clsJsondrugallergies()
                                    {
                                        allergycode = rw["allergycode"].ToString(),
                                        code = rw["code"].ToString(),
                                        genericname = rw["genericname"].ToString(),
                                        adverbs = rw["adverbs"].ToString(),
                                        memo = rw["memo"].ToString(),                                       
                                        //genorderdatetime = Convert.ToDateTime(rw["genorderdatetime"]).AddHours(7).ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture).ToString()

                                    };
                                    clsJson.drugallergies.Add(clsdrugallergies);

                                    
                                }


                            }


                            #region กำหนดค่าให้กับ clsJson
                            clsJson._id = clsPackagemaster.db_packagemaster.Rows[0]["_id_prescription"].ToString();
                            clsJson.__v = clsPackagemaster.db_packagemaster.Rows[0]["__v"].ToString();
                            clsJson.appointmentdate = (clsPackagemaster.db_data.Rows[0]["appointmentdate"].ToString().Length > 0) ? Convert.ToDateTime(clsPackagemaster.db_data.Rows[0]["appointmentdate"].ToString()).AddHours(7).ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture).ToString() : "";
                            clsJson.cid = clsPackagemaster.db_data.Rows[0]["cid"].ToString();
                            clsJson.computername = clsPackagemaster.db_data.Rows[0]["computername"].ToString();
                            clsJson.confirmdatetime = (clsPackagemaster.db_data.Rows[0]["confirmdatetime"].ToString().Length > 0) ? Convert.ToDateTime(clsPackagemaster.db_data.Rows[0]["confirmdatetime"].ToString()).AddHours(7).ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture).ToString() : "";
                            clsJson.confirmuserid = clsPackagemaster.db_data.Rows[0]["confirmuserid"].ToString();
                            clsJson.doctorcode = clsPackagemaster.db_data.Rows[0]["doctorcode"].ToString();
                            clsJson.doctorname = clsPackagemaster.db_data.Rows[0]["doctorname"].ToString();
                            clsJson.expressmed = clsPackagemaster.db_data.Rows[0]["expressmed"].ToString();
                            clsJson.hn = clsPackagemaster.db_data.Rows[0]["hn"].ToString();
                            clsJson.lastupdate = (clsPackagemaster.db_data.Rows[0]["lastupdate"].ToString().Length > 0) ? Convert.ToDateTime(clsPackagemaster.db_data.Rows[0]["lastupdate"].ToString()).AddHours(7).ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture).ToString() : "";
                            clsJson.ordercreatedate = (clsPackagemaster.db_data.Rows[0]["ordercreatedate"].ToString().Length > 0) ? Convert.ToDateTime(clsPackagemaster.db_data.Rows[0]["ordercreatedate"].ToString()).AddHours(7).ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture).ToString() : "";
                            clsJson.patientdob = (clsPackagemaster.db_data.Rows[0]["patientdob"].ToString().Length > 0) ? Convert.ToDateTime(clsPackagemaster.db_data.Rows[0]["patientdob"].ToString()).AddHours(7).ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture).ToString() : "";
                            clsJson.patientname = clsPackagemaster.db_data.Rows[0]["patientname"].ToString();
                            clsJson.pharmacyitemcode = clsPackagemaster.db_data.Rows[0]["pharmacyitemcode"].ToString();
                            clsJson.pharmacyitemdesc = clsPackagemaster.db_data.Rows[0]["pharmacyitemdesc"].ToString();
                            clsJson.prescriptionno = clsPackagemaster.db_data.Rows[0]["prescriptionno"].ToString();
                            clsJson.qn = clsPackagemaster.db_data.Rows[0]["qn"].ToString();
                            clsJson.qrrdumix = clsPackagemaster.db_data.Rows[0]["qrrdumix"].ToString();
                            clsJson.rcvmedno = clsPackagemaster.db_data.Rows[0]["rcvmedno"].ToString();
                            clsJson.readdatetime = (clsPackagemaster.db_data.Rows[0]["readdatetime"].ToString().Length > 0) ? Convert.ToDateTime(clsPackagemaster.db_data.Rows[0]["readdatetime"].ToString()).AddHours(7).ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture).ToString() : "";
                            clsJson.rightid = clsPackagemaster.db_data.Rows[0]["rightid"].ToString();
                            clsJson.rightname = clsPackagemaster.db_data.Rows[0]["rightname"].ToString();
                            clsJson.sex = clsPackagemaster.db_data.Rows[0]["sex"].ToString();
                            clsJson.sphmlct = clsPackagemaster.db_data.Rows[0]["sphmlct"].ToString();
                            clsJson.sphmname = clsPackagemaster.db_data.Rows[0]["sphmname"].ToString();
                            clsJson.total_norefund = clsPackagemaster.db_data.Rows[0]["total_norefund"].ToString();
                            clsJson.total_refund = clsPackagemaster.db_data.Rows[0]["total_refund"].ToString();
                            clsJson.totalprice = clsPackagemaster.db_data.Rows[0]["totalprice"].ToString();
                            clsJson.vn = clsPackagemaster.db_data.Rows[0]["vn"].ToString();
                            clsJson.voiddatetime = (clsPackagemaster.db_data.Rows[0]["voiddatetime"].ToString().Length > 0) ? Convert.ToDateTime(clsPackagemaster.db_data.Rows[0]["voiddatetime"].ToString()).AddHours(7).ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture).ToString() : "";
                            clsJson.wardcode = clsPackagemaster.db_data.Rows[0]["wardcode"].ToString();
                            clsJson.wardname = clsPackagemaster.db_data.Rows[0]["wardname"].ToString();
                            clsJson.firstzone = clsPackagemaster.db_data.Rows[0]["firstzone"].ToString();
                            clsJson.genorderdatetime = (clsPackagemaster.db_data.Rows[0]["genorderdatetime"].ToString().Length > 0) ? Convert.ToDateTime(clsPackagemaster.db_packagemaster.Rows[0]["genorderdatetime"].ToString()).AddHours(7).ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture).ToString() : "";
                            clsJson.printdatetime = printdatetime;
                            #endregion

                            listprescription.Add(new
                            {
                                _id = clsPackagemaster.db_packagemaster.Rows[0]["_id_prescription"].ToString(),
                                __v = clsPackagemaster.db_packagemaster.Rows[0]["__v"].ToString(),
                                appointmentdate = clsJson.appointmentdate ,
                                cid = clsPackagemaster.db_data.Rows[0]["cid"].ToString(),
                                computername = clsPackagemaster.db_data.Rows[0]["computername"].ToString(),
                                confirmdatetime = clsJson.confirmdatetime,
                                confirmuserid = clsPackagemaster.db_data.Rows[0]["confirmuserid"].ToString(),
                                doctorcode = clsPackagemaster.db_data.Rows[0]["doctorcode"].ToString(),
                                doctorname = clsPackagemaster.db_data.Rows[0]["doctorname"].ToString(),
                                expressmed = clsPackagemaster.db_data.Rows[0]["expressmed"].ToString(),
                                hn = clsPackagemaster.db_data.Rows[0]["hn"].ToString(),
                                lastupdate = clsJson.lastupdate,
                                ordercreatedate = clsJson.ordercreatedate,
                                patientdob = clsJson.patientdob,
                                patientname = clsPackagemaster.db_data.Rows[0]["patientname"].ToString(),
                                pharmacyitemcode = clsPackagemaster.db_data.Rows[0]["pharmacyitemcode"].ToString(),
                                pharmacyitemdesc = clsPackagemaster.db_data.Rows[0]["pharmacyitemdesc"].ToString(),
                                prescriptionno = clsPackagemaster.db_data.Rows[0]["prescriptionno"].ToString(),
                                qn = clsPackagemaster.db_data.Rows[0]["qn"].ToString(),
                                qrrdumix = clsPackagemaster.db_data.Rows[0]["qrrdumix"].ToString(),
                                rcvmedno = clsPackagemaster.db_data.Rows[0]["rcvmedno"].ToString(),
                                readdatetime = clsJson.readdatetime,
                                rightid = clsPackagemaster.db_data.Rows[0]["rightid"].ToString(),
                                rightname = clsPackagemaster.db_data.Rows[0]["rightname"].ToString(),
                                sex = clsPackagemaster.db_data.Rows[0]["sex"].ToString(),
                                sphmlct = clsPackagemaster.db_data.Rows[0]["sphmlct"].ToString(),
                                sphmname = clsPackagemaster.db_data.Rows[0]["sphmname"].ToString(),
                                total_norefund = clsPackagemaster.db_data.Rows[0]["total_norefund"].ToString(),
                                total_refund = clsPackagemaster.db_data.Rows[0]["total_refund"].ToString(),
                                totalprice = clsPackagemaster.db_data.Rows[0]["totalprice"].ToString(),
                                vn = clsPackagemaster.db_data.Rows[0]["vn"].ToString(),
                                voiddatetime = clsJson.voiddatetime,
                                wardcode = clsPackagemaster.db_data.Rows[0]["wardcode"].ToString(),
                                wardname = clsPackagemaster.db_data.Rows[0]["wardname"].ToString(),
                                firstzone = clsPackagemaster.db_data.Rows[0]["firstzone"].ToString(),
                                genorderdatetime = clsJson.genorderdatetime,
                                printdatetime = printdatetime,
                                drugs = listJsondrugsn,
                                labs = listlabs,
                                drugallergies = listdrugallergies
                            });

                        }
                      


                    }

                    // แปลงเป็น JSON
                    var jsonString = System.Text.Json.JsonSerializer.Serialize(clsJson, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });

                    Console.WriteLine(jsonString);

                    byte[] utf8Bytes = Encoding.UTF8.GetBytes(jsonString);

                    string text = Encoding.UTF8.GetString(utf8Bytes);

                }

                return listprescription;
            }
            catch(Exception ex)
            {
                return new List<object>();
                MessageBox.Show(ex.ToString());
            }
            finally
            {

            }
            
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        
        public void printguideslip(DataTable db,DataTable db_data,DataTable db_drug)
        {
            try
            {
                #region slip
                string labs = "";
                DataTable db_slip = new DataTable();
                db_slip.Columns.Add("prescriptionno", typeof(String));
                db_slip.Columns.Add("orderitembarcode", typeof(String));
                db_slip.Columns.Add("patientname", typeof(String));
                db_slip.Columns.Add("hn", typeof(String));
                db_slip.Columns.Add("qn", typeof(String));
                db_slip.Columns.Add("wardname", typeof(String));
                db_slip.Columns.Add("orderitemname", typeof(String));
                db_slip.Columns.Add("genericname", typeof(String));
                db_slip.Columns.Add("orderqty", typeof(String));
                db_slip.Columns.Add("Qrcode", typeof(byte[]));
                db_slip.Columns.Add("orderunitdesc", typeof(String));
                db_slip.Columns.Add("orderdate", typeof(String));
                db_slip.Columns.Add("orderitemTHname", typeof(String));
                db_slip.Columns.Add("locationname", typeof(String));
                db_slip.Columns.Add("itemidentify", typeof(String));
                db_slip.Columns.Add("patientGender", typeof(String));
                db_slip.Columns.Add("patientAge", typeof(String));
                db_slip.Columns.Add("rcvmedno", typeof(String));
                db_slip.Columns.Add("expressmed", typeof(String));
                db_slip.Columns.Add("amount", typeof(String));
                db_slip.Columns.Add("gendatetime", typeof(String));
                db_slip.Columns.Add("patientdob", typeof(String));
                db_slip.Columns.Add("vn", typeof(String));
                db_slip.Columns.Add("orderzone", typeof(int));
                db_slip.Columns.Add("instructiondesc", typeof(String));
                db_slip.Columns.Add("dosage", typeof(String));
                db_slip.Columns.Add("dosageunitdesc", typeof(String));
                db_slip.Columns.Add("frequencydesc", typeof(String));
                db_slip.Columns.Add("special_advice_text", typeof(String));
                db_slip.Columns.Add("labs", typeof(String));
                db_slip.TableName = "db_slip";
                #endregion

                if (db.Rows.Count>0 && db_data.Rows.Count > 0 && db_drug.Rows.Count > 0)
                {
                    if(clsPackagemaster.db_labs.Rows.Count > 0)
                    {
                        labs = "";
                        foreach (DataRow row in clsPackagemaster.db_labs.Rows)
                        {
                            if (labs == "" && row["lab_name"].ToString() != "Creatinine")
                            {
                                labs += row["lab_name"].ToString() + " "+ row["result"].ToString()+" ("+ clsconvert.convert_thai(row["date_result"].ToString()) +")";

                            }
                            else if (labs != "" && row["lab_name"].ToString() != "Creatinine")
                            {
                                labs += " || "+row["lab_name"].ToString() + " " + row["result"].ToString() + " (" + clsconvert.convert_thai(row["date_result"].ToString()) + ")";
                            }
                        }
                    }
                    DataTable db_drug_code = new DataTable();
                    foreach(DataRow rw in db.Rows)
                    {
                        string drugitem_sub = "";
                        string s1 = rw["orderitemcode"].ToString();
                        string s2 = s1.IndexOf("^").ToString();
                        if (int.Parse(s2) > 0)
                        {
                            drugitem_sub = s1.Substring(0, int.Parse(s2));
                        }
                        else
                        {
                            drugitem_sub = s1;
                        }
                        db_drug_code = db_drug.Select(" orderitemcode ='" + drugitem_sub + "'").CopyToDataTable(); // Copy to a new DataTable

                        DataRow r = db_slip.Rows.Add();
                        r["instructiondesc"] = db_drug_code.Rows[0]["instructiondesc"].ToString();
                        r["dosage"] = db_drug_code.Rows[0]["dosage"].ToString();
                        r["dosageunitdesc"] = db_drug_code.Rows[0]["dosageunitdesc"].ToString();
                        r["frequencydesc"] = db_drug_code.Rows[0]["frequencydesc"].ToString();
                        r["special_advice_text"] = "";
                        r["prescriptionno"] = rw["prescriptionno"].ToString();
                        r["orderzone"] = Convert.ToInt32(rw["orderzone"].ToString());
                        r["orderitembarcode"] = rw["orderitembarcode"].ToString();
                        r["patientname"] = rw["patientname"].ToString();
                        r["hn"] = db_data.Rows[0]["hn"].ToString();
                        r["wardname"] = db_data.Rows[0]["wardname"].ToString();
                        r["orderitemname"] = rw["orderitemname"].ToString();
                        r["genericname"] = ""; //rw["genericname"].ToString();
                        r["orderqty"] = rw["orderqty"].ToString();
                        r["orderunitdesc"] = rw["orderunitdesc"].ToString();
                        r["orderdate"] = rw["ordercreatedate"].ToString();
                        r["labs"] = labs;
                        if(rw["shelfzone"].ToString() == "SE-MED")
                        {
                            r["locationname"] = rw["shelfzone"].ToString();

                        }
                        else
                        {
                            r["locationname"] = rw["shelfzone"].ToString() + "-" + rw["shelfname"].ToString();
                        }
                       

                        //r["itemidentify"] = rw["itemidentify"].ToString();
                        string genTXT = "";
                        if (rw["itemidentify"].ToString() != "")
                        {
                            genTXT = rw["itemidentify"].ToString();
                            MemoryStream ms = new MemoryStream();
                            clsconvert.genQr(genTXT).Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

                            byte[] bytes = ms.ToArray();
                            r["Qrcode"] = bytes;
                        }
                        else
                        {
                            r["Qrcode"] = "";
                        }
                        
                        r["patientGender"] = db_data.Rows[0]["sex"].ToString();
                        r["qn"] = db_data.Rows[0]["qn"].ToString();

                        if (db_data.Rows[0]["patientdob"].ToString() != "")
                        {
                            string year = "";
                            string yearNow = "";
                            
                            yearNow = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

                            var date = db_data.Rows[0]["patientdob"].ToString();
                            var yNow = yearNow;

                            year = clsconvert.convertdate_YYYY_EN(date);
                            yearNow = clsconvert.convertdate_YYYY_EN(yNow);
                            if (year != "" && yearNow != "")
                            {
                                r["patientAge"] = (Convert.ToInt32(yearNow) - Convert.ToInt32(year));
                            }
                            else
                            {
                                r["patientAge"] = "";
                            }
                        }
                        else
                        {
                            r["patientAge"] = "";
                        }
                        
                        r["rcvmedno"] = "";//rw["rcvmedno"].ToString();
                        r["expressmed"] = "";//rw["expressmed"].ToString();
                        r["amount"] = "";//rw["amount"].ToString();                    
                        r["gendatetime"] = clsconvert.convertdate_HH_mm_ss_EN_NEW(rw["genorderdatetime"].ToString());
                        r["vn"] = db_data.Rows[0]["vn"].ToString();
                    }
                    if(db_slip.Rows.Count > 0)
                    {
                        // ใช้ DataView เพื่อจัดเรียงข้อมูลจากน้อยไปมากตามคอลัมน์ ID
                        DataView view = db_slip.DefaultView;
                        view.Sort = "orderzone ASC";  // หรือ "ID DESC" ถ้าต้องการจากมากไปน้อย

                        db_slip = db_slip.AsEnumerable().OrderBy(x => x.Field<int>("orderzone"))
                        .Select(x => x)
                        .CopyToDataTable();
                        rpt_guideslipr(db_slip);
                    }
                }
                else
                {
                    // ไม่มีข้อมูล
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void reprintguideslip(DataTable db)
        {
            try
            {
                #region slip

                DataTable db_slip = new DataTable();
                db_slip.Columns.Add("prescriptionno", typeof(String));
                db_slip.Columns.Add("orderitembarcode", typeof(String));
                db_slip.Columns.Add("patientname", typeof(String));
                db_slip.Columns.Add("hn", typeof(String));
                db_slip.Columns.Add("qn", typeof(String));
                db_slip.Columns.Add("wardname", typeof(String));
                db_slip.Columns.Add("orderitemname", typeof(String));
                db_slip.Columns.Add("genericname", typeof(String));
                db_slip.Columns.Add("orderqty", typeof(String));
                db_slip.Columns.Add("Qrcode", typeof(byte[]));
                db_slip.Columns.Add("orderunitdesc", typeof(String));
                db_slip.Columns.Add("orderdate", typeof(String));
                db_slip.Columns.Add("orderitemTHname", typeof(String));
                db_slip.Columns.Add("locationname", typeof(String));
                db_slip.Columns.Add("itemidentify", typeof(String));
                db_slip.Columns.Add("patientGender", typeof(String));
                db_slip.Columns.Add("patientAge", typeof(String));
                db_slip.Columns.Add("rcvmedno", typeof(String));
                db_slip.Columns.Add("expressmed", typeof(String));
                db_slip.Columns.Add("amount", typeof(String));
                db_slip.Columns.Add("gendatetime", typeof(String));
                db_slip.Columns.Add("patientdob", typeof(String));
                db_slip.Columns.Add("vn", typeof(String));
                db_slip.Columns.Add("orderzone", typeof(int));
                db_slip.TableName = "db_slip";
                #endregion
                if (db.Rows.Count > 0 && clsPackagemaster.db_data.Rows.Count > 0 && clsPackagemaster.db_drug.Rows.Count > 0)
                {
                    foreach (DataRow rw in db.Rows)
                    {
                        DataRow r = db_slip.Rows.Add();
                        r["prescriptionno"] = rw["prescriptionno"].ToString();
                        r["orderzone"] = Convert.ToInt32(rw["orderzone"].ToString());
                        r["orderitembarcode"] = rw["orderitembarcode"].ToString();
                        r["patientname"] = clsPackagemaster.db_data.Rows[0]["patientname"].ToString();
                        r["hn"] = clsPackagemaster.db_data.Rows[0]["hn"].ToString();
                        r["wardname"] = clsPackagemaster.db_data.Rows[0]["wardname"].ToString();
                        r["orderitemname"] = rw["orderitemname"].ToString();
                        r["genericname"] = ""; //rw["genericname"].ToString();
                        r["orderqty"] = rw["orderqty"].ToString();
                        r["orderunitdesc"] = rw["orderunitdesc"].ToString();
                        r["orderdate"] = rw["ordercreatedate"].ToString();
                        r["orderitemTHname"] = ""; //rw["orderitemTHname"].ToString();
                        if (rw["shelfzone"].ToString() == "SE-MED")
                        {
                            r["locationname"] = rw["shelfzone"].ToString();

                        }
                        else
                        {
                            r["locationname"] = rw["shelfzone"].ToString() + "-" + rw["shelfname"].ToString();
                        }

                        //r["itemidentify"] = rw["itemidentify"].ToString();
                        string genTXT = "";
                        if (rw["itemidentify"].ToString() != "")
                        {
                            genTXT = rw["itemidentify"].ToString();
                            MemoryStream ms = new MemoryStream();
                            clsconvert.genQr(genTXT).Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

                            byte[] bytes = ms.ToArray();
                            r["Qrcode"] = bytes;
                        }
                        else
                        {
                            r["Qrcode"] = "";
                        }

                        r["patientGender"] = clsPackagemaster.db_data.Rows[0]["sex"].ToString();
                        r["qn"] = clsPackagemaster.db_data.Rows[0]["qn"].ToString();

                        if (rw["patientdob"].ToString() != "")
                        {
                            string year = "";
                            string yearNow = "";

                            yearNow = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

                            var date = clsPackagemaster.db_data.Rows[0]["patientdob"].ToString();
                            var yNow = yearNow;

                            year = clsconvert.convertdate_YYYY_EN(date);
                            yearNow = clsconvert.convertdate_YYYY_EN(yNow);
                            if (year != "" && yearNow != "")
                            {
                                r["patientAge"] = (Convert.ToInt32(yearNow) - Convert.ToInt32(year));
                            }
                            else
                            {
                                r["patientAge"] = "";
                            }
                        }
                        else
                        {
                            r["patientAge"] = "";
                        }

                        r["rcvmedno"] = "";//rw["rcvmedno"].ToString();
                        r["expressmed"] = "";//rw["expressmed"].ToString();
                        r["amount"] = "";//rw["amount"].ToString();                    
                        r["gendatetime"] = clsconvert.convertdate_HH_mm_ss_EN_NEW(rw["genorderdatetime"].ToString());
                        r["vn"] = clsPackagemaster.db_data.Rows[0]["vn"].ToString();
                    }
                    if (db_slip.Rows.Count > 0)
                    {
                        // ใช้ DataView เพื่อจัดเรียงข้อมูลจากน้อยไปมากตามคอลัมน์ ID
                        DataView view = db_slip.DefaultView;
                        view.Sort = "orderzone ASC";  // หรือ "ID DESC" ถ้าต้องการจากมากไปน้อย

                        db_slip = db_slip.AsEnumerable().OrderBy(x => x.Field<int>("orderzone"))
                        .Select(x => x)
                        .CopyToDataTable();
                        rpt_guideslipr(db_slip);
                    }
                }
                else
                {
                    // ไม่มีข้อมูล
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void clear()
        {
            txt_BarcodePrescr.Text = "";
            txt_BarcodeBasket.Text = "";
            txtuserid.Text = "";
            txtpresc.Text = "";
            lbHN.Text = "";
            lbVN.Text = "";
            lbname.Text = "";
            lbPres.Text = "";
            dg_waitMatching.Rows.Clear();
            dg_FinishMatching.Rows.Clear();
            txt_BarcodePrescr.Focus();
            lbusername.Text = "-";
            txt_BarcodeBasket.Enabled = true;
            txt_BarcodePrescr.Enabled = true;
            if (btnStartTimer_.Visible == true)
            {
                timer1.Stop();
            }
            else if (btnStopTimer_.Visible == true)
            {
                timer1.Start();
            }
        }
        private async void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                DataTable Queue = new DataTable();
                Queue = await clsService.Requestopdqueuepharmacy();
                if(Queue.Rows.Count > 0)
                {
                    foreach (DataColumn column in Queue.Columns)
                    {
                        if(column.ColumnName.ToString().Contains("comingpoint"))
                        {
                            string colname = column.ColumnName.ToString();
                            int index = colname.Length;
                            colname = colname.Substring(index - 1);
                            if(clsSetting.sortOrder == colname)
                            {
                                lborderwait.Text = Queue.Rows[0][column.ColumnName.ToString()].ToString();
                                if(Convert.ToInt32(lborderwait.Text.ToString()) > 20)
                                {
                                    lborderwait.ForeColor = Color.FromArgb(255, 255, 0, 0);
                                    lbl.ForeColor = Color.FromArgb(255, 255, 0, 0);
                                }
                            }
                        }       
                    }
                }
                // โหลดข้อมูลใหม่จาก API
                clsPackagemaster.db_packagemaster = new DataTable();
               await clsService.RequestPackagemasterfirstzone(clsSetting.COM_NAME);

                // ตรวจสอบว่ามีข้อมูลหรือไม่
                if (clsPackagemaster.db_packagemaster.Rows.Count > 0 && clsPackagemaster.db_data.Rows.Count > 0)
                {
                    txt_BarcodePrescr.ReadOnly = true;
                    timer1.Stop();
                    dg_waitMatching.Rows.Clear();
                    //dg_waitMatching.DataSource = new DataTable();
                    dg_waitMatching.Refresh();
                    // กรองเฉพาะข้อมูลที่ตรงกับ `orderzone`
                    DataRow[] filteredRows = clsPackagemaster.db_packagemaster.Select();

                    if (filteredRows.Length > 0)
                    {
                        timer1.Stop();
                        DataTable tb = filteredRows.CopyToDataTable(); // Copy to DataTable  
                        printguideslip(tb, clsPackagemaster.db_data,clsPackagemaster.db_drug); // ใช้ DataTable ที่กรองแล้ว

                        // อัปเดตเวลาพิมพ์
                        DateTime utcNow = DateTime.Now;
                        string str_datenow = date_utc(utcNow.ToString());
                        List<object> ListJson = GenJson_printdatetime(str_datenow.Trim());

                        await clsService.update_prescription(ListJson);

                        if (tb.Rows.Count > 0)
                        {
                            DataTable tbdg_waitMatching = clsPackagemaster.db_packagemaster.Select(" orderzone ='" + clsSetting.orderzone +"'").CopyToDataTable();
                            // กรอกข้อมูลลง TextBox และ Label
                            txt_BarcodePrescr.Text = tb.Rows[0]["itemidentify"].ToString();
                            lbHN.Text = tb.Rows[0]["hn"].ToString();
                            lbname.Text = tb.Rows[0]["patientname"].ToString();
                            lbPres.Text = tb.Rows[0]["prescriptionno"].ToString();

                            // ล้างข้อมูลเก่าใน DataGridView ก่อนเพิ่มใหม่
                            //dg_waitMatching.Rows.Clear();

                            // เพิ่มข้อมูลลง DataGridView
                            foreach (DataRow r in tbdg_waitMatching.Rows)
                            {
                                int rowId = dg_waitMatching.Rows.Add();
                                DataGridViewRow i = dg_waitMatching.Rows[rowId];
                                i.Cells["orderitemcode"].Value = r["orderitemcode"]?.ToString();
                                i.Cells["orderitemname"].Value = r["orderitemname"]?.ToString();
                                i.Cells["orderqty"].Value = r["orderqty"]?.ToString();
                                i.Cells["orderunitcode"].Value = r["orderunitdesc"]?.ToString();
                                i.Cells["shelfname"].Value = r["shelfzone"]?.ToString();
                            }
                        }

                        txt_BarcodeBasket.Focus();
                    }
                    else
                    {
                        txt_BarcodePrescr.ReadOnly = false;
                        timer1.Start();
                    }
                }
                else
                {
                    txt_BarcodePrescr.ReadOnly = false;
                    timer1.Start();
                }
            }
            
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


        }

        private async void txt_BarcodePrescr_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                timer1.Stop();
                clsSetting.EnterEvent = true;
                DataTable db_packagemaster_first = new DataTable();
                await clsService.RequestPackagemasterByZone(clsSetting.COM_NAME, txt_BarcodePrescr.Text.ToString());
                if(clsPackagemaster.db_packagemaster.Rows.Count > 0 && clsPackagemaster.db_data.Rows.Count > 0 && clsPackagemaster.db_drug.Rows.Count > 0)
                {
                    db_packagemaster_first = clsPackagemaster.db_packagemaster.Select().CopyToDataTable(); // Copy to a new DataTable

                    if (db_packagemaster_first.Rows.Count > 0)
                    {
                        //dg_waitMatching.DataSource = db_packagemaster_first;
                       
                        txt_BarcodeBasket.Text = db_packagemaster_first.Rows[0]["basketno"].ToString();
                        lbPres.Text = db_packagemaster_first.Rows[0]["prescriptionno"].ToString();
                        lbHN.Text = db_packagemaster_first.Rows[0]["hn"].ToString();
                        lbVN.Text = db_packagemaster_first.Rows[0]["vn"].ToString();
                        txt_BarcodePrescr.Text.ToUpper();
                        txt_BarcodePrescr.Enabled = false;
                        txt_BarcodeBasket.Enabled = false;
                        txtuserid.Focus();
                        #region dgv

                        // เพิ่มข้อมูลตัวอย่าง
                        foreach (DataRow i in db_packagemaster_first.Rows)
                        {
                            dg_waitMatching.Rows.Add(i["orderitemcode"].ToString(),
                                i["orderitemname"].ToString(),
                                i["orderqty"].ToString(),
                                i["orderunitdesc"].ToString(),
                                i["shelfzone"].ToString()+"-"+i["shelfname"].ToString());

                            // รีเฟรช DataGridView

                        }

                        #endregion
                    }

                }
                
            }
        }
        public DataTable print_stricker_next(string prescriptionno)
        {
            bool result = false;
            string drugitem = "";
            try
            {
                DataTable db_packagemaster = new DataTable();
                DataTable db_prescription = new DataTable();
                DataTable db_data = new DataTable();
                #region creatdb_stricker
                DataTable db_stricker = new DataTable();
                db_stricker.Columns.Add("prescriptionno", typeof(String));
                db_stricker.Columns.Add("special_advice_text", typeof(String));
                db_stricker.Columns.Add("orderitembarcode", typeof(String));
                db_stricker.Columns.Add("patientname", typeof(String));
                db_stricker.Columns.Add("hn", typeof(String));
                db_stricker.Columns.Add("wardname", typeof(String));
                db_stricker.Columns.Add("orderitemname", typeof(String));
                db_stricker.Columns.Add("genericname", typeof(String));
                db_stricker.Columns.Add("orderqty", typeof(String));
                db_stricker.Columns.Add("Qrcode", typeof(String));
                db_stricker.Columns.Add("orderunitdesc", typeof(String));
                db_stricker.Columns.Add("orderdate", typeof(String));
                db_stricker.Columns.Add("orderitemTHname", typeof(String));
                db_stricker.Columns.Add("locationname", typeof(String));
                db_stricker.Columns.Add("itemidentify", typeof(String));
                db_stricker.Columns.Add("patientGender", typeof(String));
                db_stricker.Columns.Add("patientAge", typeof(String));
                db_stricker.Columns.Add("qn", typeof(String));
                db_stricker.Columns.Add("amount", typeof(String));
                db_stricker.Columns.Add("note", typeof(String));
                db_stricker.Columns.Add("itemlotexpire", typeof(String));
                db_stricker.Columns.Add("itemlotcode", typeof(String));
                db_stricker.Columns.Add("vn", typeof(String));
                db_stricker.Columns.Add("instructiondesc", typeof(String));
                db_stricker.Columns.Add("dosage", typeof(String));
                db_stricker.Columns.Add("dosageunitdesc", typeof(String));
                db_stricker.Columns.Add("frequencydesc", typeof(String));
                db_stricker.Columns.Add("freetext1", typeof(String));
                db_stricker.Columns.Add("freetext2", typeof(String));
                db_stricker.Columns.Add("freetext3", typeof(String));
                db_stricker.Columns.Add("freetext4", typeof(String));
                db_stricker.Columns.Add("freetext5", typeof(String));
                db_stricker.Columns.Add("pregnancy", typeof(String));
                db_stricker.Columns.Add("edned", typeof(String));
                db_stricker.Columns.Add("drugaccountcode", typeof(String));
                db_stricker.Columns.Add("poison", typeof(String));
                db_stricker.Columns.Add("store", typeof(String));
                db_stricker.Columns.Add("patientdob", typeof(String));
                db_stricker.Columns.Add("seqmax", typeof(String));
                db_stricker.Columns.Add("rightname", typeof(String));
                db_stricker.TableName = "db_stricker";
                #endregion

                if (clsPackagemaster.db_packagemaster.Rows.Count > 0 && clsPackagemaster.db_drug.Rows.Count > 0 && clsPackagemaster.db_data.Rows.Count > 0)
                {
                    
                    foreach (DataRow r in clsPackagemaster.db_packagemaster.Rows)
                    {

                        string drugitem_sub = "";
                        string s1 = r["orderitemcode"].ToString();
                        string s2 = s1.IndexOf("^").ToString();
                        if (int.Parse(s2) > 0)
                        {
                            drugitem_sub = s1.Substring(0, int.Parse(s2));
                        }
                        else
                        {
                            drugitem_sub = s1;
                        }
                        DataRow[] rowdb_data = clsPackagemaster.db_data.Select();
                        DataRow[] row = clsPackagemaster.db_packagemaster.Select(" orderitemcode LIKE '" + drugitem_sub + "%'");
                        DataRow[] rw = clsPackagemaster.db_drug.Select(" orderitemcode LIKE '" + drugitem_sub + "%'");
                        string locationname = "";
                        //=== ใบสั่งยา ===
                        if (row.Length > 0)
                        {
                            db_packagemaster = row.CopyToDataTable();
                            db_prescription = rw.CopyToDataTable();
                            db_data = rowdb_data.CopyToDataTable();
                        }


                        bool isprint = false;

                        isprint = r["orderitemcode"].ToString().Contains("LED");

                        if (!isprint)
                        {
                            DataRow addrow = db_stricker.Rows.Add();
                            addrow["prescriptionno"] = prescriptionno;
                            addrow["orderitembarcode"] = "";
                            addrow["patientname"] = db_data.Rows[0]["patientname"].ToString();
                            addrow["hn"] = db_data.Rows[0]["hn"].ToString();
                            addrow["wardname"] = db_data.Rows[0]["wardname"].ToString();
                            addrow["orderitemname"] = db_prescription.Rows[0]["orderitemname"].ToString();
                            addrow["genericname"] = "";
                            addrow["orderqty"] = db_prescription.Rows[0]["orderqty"].ToString();
                            addrow["special_advice_text"] = db_prescription.Rows[0]["special_advice_text"].ToString();
                            addrow["Qrcode"] = "";
                            addrow["orderunitdesc"] = "";
                            addrow["orderdate"] = clsconvert.convertdate_HH_mm_ss_EN_NEW(db_packagemaster.Rows[0]["ordercreatedate"].ToString());
                            addrow["orderitemTHname"] = "";
                            addrow["locationname"] = "";
                            addrow["itemidentify"] = db_packagemaster.Rows[0]["itemidentify"].ToString();
                            addrow["patientGender"] = db_data.Rows[0]["sex"].ToString();

                            if (db_data.Rows[0]["patientdob"].ToString() != "")
                            {
                                string year = "";
                                string yearNow = "";

                                yearNow = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

                                var date = db_data.Rows[0]["patientdob"].ToString();
                                var yNow = yearNow;

                                year = clsconvert.convertdate_YYYY_EN(date);
                                yearNow = clsconvert.convertdate_YYYY_EN(yNow);

                                if (year != "" && yearNow != "")
                                {
                                    addrow["patientAge"] = (Convert.ToInt32(yearNow) - Convert.ToInt32(year));
                                }
                                else
                                {
                                    addrow["patientAge"] = "";
                                }
                            }
                            else
                            {
                                addrow["patientAge"] = "";
                            }
                            addrow["qn"] = db_data.Rows[0]["qn"].ToString();
                            addrow["amount"] = "";
                            addrow["itemlotexpire"] = "";
                            addrow["itemlotcode"] = "";
                            addrow["vn"] = db_data.Rows[0]["vn"].ToString();
                            addrow["instructiondesc"] = db_prescription.Rows[0]["instructiondesc"].ToString();
                            addrow["dosage"] = db_prescription.Rows[0]["dosage"].ToString();
                            addrow["dosageunitdesc"] = db_prescription.Rows[0]["dosageunitdesc"].ToString();
                            addrow["frequencydesc"] = db_prescription.Rows[0]["frequencydesc"].ToString();
                            addrow["freetext1"] = db_prescription.Rows[0]["freetext1"].ToString();
                            addrow["freetext2"] = db_prescription.Rows[0]["freetext2"].ToString();
                            addrow["freetext3"] = db_prescription.Rows[0]["freetext3"].ToString();
                            addrow["freetext4"] = db_prescription.Rows[0]["freetext4"].ToString();
                            addrow["freetext5"] = db_prescription.Rows[0]["freetext5"].ToString();
                            addrow["rightname"] = db_data.Rows[0]["rightname"].ToString();
                            addrow["seqmax"] = db_packagemaster.Rows[0]["seqmax"].ToString();
                            addrow["pregnancy"] = db_packagemaster.Rows[0]["pregnancy"].ToString();
                            addrow["edned"] = db_packagemaster.Rows[0]["edned"].ToString();
                            addrow["poison"] = db_packagemaster.Rows[0]["poison"].ToString();
                            addrow["drugaccountcode"] = db_packagemaster.Rows[0]["drugaccountcode"].ToString();
                            addrow["store"] = db_prescription.Rows[0]["store"].ToString();
                        }
                        

                        
                    }

                }
                return db_stricker;
            }
            catch (Exception ex)
            {
                return new DataTable();
            }
        }

        public DataTable print_stricker(string prescriptionno)
        {
            bool result = false;
            string drugitem = "";
            try
            {
                DataTable db_packagemaster = new DataTable();
                DataTable db_prescription = new DataTable();
                DataTable db_data = new DataTable();
                #region creatdb_stricker
                DataTable db_stricker = new DataTable();
                db_stricker.Columns.Add("prescriptionno", typeof(String));
                db_stricker.Columns.Add("special_advice_text", typeof(String));
                db_stricker.Columns.Add("orderitembarcode", typeof(String));
                db_stricker.Columns.Add("patientname", typeof(String));
                db_stricker.Columns.Add("hn", typeof(String));
                db_stricker.Columns.Add("wardname", typeof(String));
                db_stricker.Columns.Add("orderitemname", typeof(String));
                db_stricker.Columns.Add("genericname", typeof(String));
                db_stricker.Columns.Add("orderqty", typeof(String));
                db_stricker.Columns.Add("Qrcode", typeof(String));
                db_stricker.Columns.Add("orderunitdesc", typeof(String));
                db_stricker.Columns.Add("orderdate", typeof(String));
                db_stricker.Columns.Add("orderitemTHname", typeof(String));
                db_stricker.Columns.Add("locationname", typeof(String));
                db_stricker.Columns.Add("itemidentify", typeof(String));
                db_stricker.Columns.Add("patientGender", typeof(String));
                db_stricker.Columns.Add("patientAge", typeof(String));
                db_stricker.Columns.Add("qn", typeof(String));
                db_stricker.Columns.Add("amount", typeof(String));
                db_stricker.Columns.Add("note", typeof(String));
                db_stricker.Columns.Add("itemlotexpire", typeof(String));
                db_stricker.Columns.Add("itemlotcode", typeof(String));
                db_stricker.Columns.Add("vn", typeof(String));
                db_stricker.Columns.Add("instructiondesc", typeof(String));
                db_stricker.Columns.Add("dosage", typeof(String));
                db_stricker.Columns.Add("dosageunitdesc", typeof(String));
                db_stricker.Columns.Add("frequencydesc", typeof(String));
                db_stricker.Columns.Add("freetext1", typeof(String));
                db_stricker.Columns.Add("freetext2", typeof(String));
                db_stricker.Columns.Add("freetext3", typeof(String));
                db_stricker.Columns.Add("freetext4", typeof(String));
                db_stricker.Columns.Add("freetext5", typeof(String));
                db_stricker.Columns.Add("pregnancy", typeof(String));
                db_stricker.Columns.Add("edned", typeof(String));
                db_stricker.Columns.Add("drugaccountcode", typeof(String));
                db_stricker.Columns.Add("poison", typeof(String));
                db_stricker.Columns.Add("store", typeof(String));
                db_stricker.Columns.Add("patientdob", typeof(String));
                db_stricker.Columns.Add("seqmax", typeof(String));
                db_stricker.Columns.Add("rightname", typeof(String));
                db_stricker.TableName = "db_stricker";
                #endregion

                if (clsPackagemaster.db_packagemaster.Rows.Count > 0 && clsPackagemaster.db_drug.Rows.Count > 0 && clsPackagemaster.db_data.Rows.Count > 0)
                {
                    DataRow[] row = clsPackagemaster.db_packagemaster.Select(" orderzone = '" + clsSetting.orderzone + "'");
                   
                    //=== ใบสั่งยา ===
                    if (row.Length > 0)
                    {
                        db_packagemaster = row.CopyToDataTable();                        
                    }

                    foreach (DataRow r in db_packagemaster.Rows)
                    {

                        string drugitem_sub = "";
                        string s1 = r["orderitemcode"].ToString();
                        string s2 = s1.IndexOf("^").ToString();
                        if (int.Parse(s2) > 0)
                        {
                            drugitem_sub = s1.Substring(0, int.Parse(s2));
                        }
                        else
                        {
                            drugitem_sub = s1;
                        }
                        DataRow[] rowdb_data = clsPackagemaster.db_data.Select();
                        //DataRow[] row = clsPackagemaster.db_packagemaster.Select(" orderzone = '" + clsSetting.orderzone + "'");
                        DataRow[] rw = clsPackagemaster.db_drug.Select(" orderitemcode LIKE '" + drugitem_sub + "%'");
                        string locationname = "";
                        //=== ใบสั่งยา ===
                        if (row.Length > 0)
                        {
                            db_packagemaster = row.CopyToDataTable();
                            db_prescription = rw.CopyToDataTable();
                            db_data = rowdb_data.CopyToDataTable();
                        }

                        bool isprint = false;

                        isprint = r["shelfzone"].ToString().Contains("LED");

                        if (!isprint)
                        {
                            DataRow addrow = db_stricker.Rows.Add();
                            addrow["prescriptionno"] = prescriptionno;
                            addrow["orderitembarcode"] = "";
                            addrow["patientname"] = db_data.Rows[0]["patientname"].ToString();
                            addrow["hn"] = db_data.Rows[0]["hn"].ToString();
                            addrow["wardname"] = db_data.Rows[0]["wardname"].ToString();
                            addrow["orderitemname"] = db_prescription.Rows[0]["orderitemname"].ToString();
                            addrow["genericname"] = "";
                            addrow["orderqty"] = db_prescription.Rows[0]["orderqty"].ToString();
                            addrow["special_advice_text"] = db_prescription.Rows[0]["special_advice_text"].ToString();
                            addrow["Qrcode"] = "";
                            addrow["orderunitdesc"] = "";
                            addrow["orderdate"] = clsconvert.convertdate_HH_mm_ss_EN_NEW(db_packagemaster.Rows[0]["ordercreatedate"].ToString());
                            addrow["orderitemTHname"] = "";
                            addrow["locationname"] = "";
                            addrow["itemidentify"] = db_packagemaster.Rows[0]["itemidentify"].ToString();
                            addrow["patientGender"] = db_data.Rows[0]["sex"].ToString();

                            if (db_data.Rows[0]["patientdob"].ToString() != "")
                            {
                                string year = "";
                                string yearNow = "";

                                yearNow = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

                                var date = db_data.Rows[0]["patientdob"].ToString();
                                var yNow = yearNow;

                                year = clsconvert.convertdate_YYYY_EN(date);
                                yearNow = clsconvert.convertdate_YYYY_EN(yNow);

                                if (year != "" && yearNow != "")
                                {
                                    addrow["patientAge"] = (Convert.ToInt32(yearNow) - Convert.ToInt32(year));
                                }
                                else
                                {
                                    addrow["patientAge"] = "";
                                }
                            }
                            else
                            {
                                addrow["patientAge"] = "";
                            }
                            addrow["qn"] = db_data.Rows[0]["qn"].ToString();
                            addrow["amount"] = "";
                            addrow["itemlotexpire"] = "";
                            addrow["itemlotcode"] = "";
                            addrow["vn"] = db_data.Rows[0]["vn"].ToString();
                            addrow["instructiondesc"] = db_prescription.Rows[0]["instructiondesc"].ToString();
                            addrow["dosage"] = db_prescription.Rows[0]["dosage"].ToString();
                            addrow["dosageunitdesc"] = db_prescription.Rows[0]["dosageunitdesc"].ToString();
                            addrow["frequencydesc"] = db_prescription.Rows[0]["frequencydesc"].ToString();
                            addrow["freetext1"] = db_prescription.Rows[0]["freetext1"].ToString();
                            addrow["freetext2"] = db_prescription.Rows[0]["freetext2"].ToString();
                            addrow["freetext3"] = db_prescription.Rows[0]["freetext3"].ToString();
                            addrow["freetext4"] = db_prescription.Rows[0]["freetext4"].ToString();
                            addrow["freetext5"] = db_prescription.Rows[0]["freetext5"].ToString();
                            addrow["rightname"] = db_data.Rows[0]["rightname"].ToString();
                            addrow["seqmax"] = db_packagemaster.Rows[0]["seqmax"].ToString();
                            addrow["pregnancy"] = db_packagemaster.Rows[0]["pregnancy"].ToString();
                            addrow["edned"] = db_packagemaster.Rows[0]["edned"].ToString();
                            addrow["poison"] = db_packagemaster.Rows[0]["poison"].ToString();
                            addrow["drugaccountcode"] = db_packagemaster.Rows[0]["drugaccountcode"].ToString();
                            addrow["store"] = db_prescription.Rows[0]["store"].ToString();
                        }
                        
                    }

                }
                return db_stricker;
            }
            catch (Exception ex)
            {
                return new DataTable();
            }
        }

        public DataTable reprint_stricker(string prescriptionno)
        {
            bool result = false;
            string drugitem = "";
            try
            {
                DataTable db_packagemaster = new DataTable();
                DataTable db_prescription = new DataTable();
                DataTable db_data = new DataTable();
                #region creatdb_stricker
                DataTable db_stricker = new DataTable();
                db_stricker.Columns.Add("prescriptionno", typeof(String));
                db_stricker.Columns.Add("special_advice_text", typeof(String));
                db_stricker.Columns.Add("orderitembarcode", typeof(String));
                db_stricker.Columns.Add("patientname", typeof(String));
                db_stricker.Columns.Add("hn", typeof(String));
                db_stricker.Columns.Add("wardname", typeof(String));
                db_stricker.Columns.Add("orderitemname", typeof(String));
                db_stricker.Columns.Add("genericname", typeof(String));
                db_stricker.Columns.Add("orderqty", typeof(String));
                db_stricker.Columns.Add("Qrcode", typeof(String));
                db_stricker.Columns.Add("orderunitdesc", typeof(String));
                db_stricker.Columns.Add("orderdate", typeof(String));
                db_stricker.Columns.Add("orderitemTHname", typeof(String));
                db_stricker.Columns.Add("locationname", typeof(String));
                db_stricker.Columns.Add("itemidentify", typeof(String));
                db_stricker.Columns.Add("patientGender", typeof(String));
                db_stricker.Columns.Add("patientAge", typeof(String));
                db_stricker.Columns.Add("qn", typeof(String));
                db_stricker.Columns.Add("amount", typeof(String));
                db_stricker.Columns.Add("note", typeof(String));
                db_stricker.Columns.Add("itemlotexpire", typeof(String));
                db_stricker.Columns.Add("itemlotcode", typeof(String));
                db_stricker.Columns.Add("vn", typeof(String));
                db_stricker.Columns.Add("instructiondesc", typeof(String));
                db_stricker.Columns.Add("dosage", typeof(String));
                db_stricker.Columns.Add("dosageunitdesc", typeof(String));
                db_stricker.Columns.Add("frequencydesc", typeof(String));
                db_stricker.Columns.Add("freetext1", typeof(String));
                db_stricker.Columns.Add("freetext2", typeof(String));
                db_stricker.Columns.Add("freetext3", typeof(String));
                db_stricker.Columns.Add("freetext4", typeof(String));
                db_stricker.Columns.Add("freetext5", typeof(String));
                db_stricker.Columns.Add("pregnancy", typeof(String));
                db_stricker.Columns.Add("edned", typeof(String));
                db_stricker.Columns.Add("drugaccountcode", typeof(String));
                db_stricker.Columns.Add("poison", typeof(String));
                db_stricker.Columns.Add("store", typeof(String));
                db_stricker.Columns.Add("patientdob", typeof(String));
                db_stricker.Columns.Add("seqmax", typeof(String));
                db_stricker.Columns.Add("rightname", typeof(String));
                db_stricker.TableName = "db_stricker";
                #endregion

                if (clsPackagemaster.db_packagemaster.Rows.Count > 0 && clsPackagemaster.db_drug.Rows.Count > 0 && clsPackagemaster.db_data.Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt = clsPackagemaster.db_packagemaster.Clone();
                    foreach (DataGridViewRow row in dg_FinishMatching.Rows)
                    {
                        if (row.Cells["fin_orderitemcode"].Value != null && row.Cells["chkFin"].Value.ToString().ToUpper() == "true".ToUpper()) // แทน "ColumnName" ด้วยชื่อคอลัมน์จริง
                        {
                            string drugitem_sub = "";
                            Console.WriteLine(row.Cells["fin_orderitemcode"].Value.ToString());
                            string s1 = row.Cells["fin_orderitemcode"].Value.ToString();
                            string s2 = s1.IndexOf("^").ToString();
                            if (int.Parse(s2) > 0)
                            {
                                drugitem_sub = s1.Substring(0, int.Parse(s2));
                            }
                            else
                            {
                                drugitem_sub = s1;
                            }
                            
                            // ค้นหาแถวที่ตรงกับเงื่อนไข
                            DataRow[] rr = clsPackagemaster.db_packagemaster.Select("orderitemcode ='" + drugitem_sub + "'");
                            
                            // วนลูปเพิ่มแถวทีละตัว
                            foreach (DataRow r in rr)
                            {
                                dt.ImportRow(r);
                            }
                        }
                    }
                    
                    foreach (DataRow r in dt.Rows)
                    {

                        string drugitem_sub = "";
                        string s1 = r["orderitemcode"].ToString();
                        string s2 = s1.IndexOf("^").ToString();
                        if (int.Parse(s2) > 0)
                        {
                            drugitem_sub = s1.Substring(0, int.Parse(s2));
                        }
                        else
                        {
                            drugitem_sub = s1;
                        }

                        DataRow[] row = dt.Select(" orderitemcode LIKE '" + drugitem_sub + "%'");
                        DataRow[] rw = clsPackagemaster.db_drug.Select(" orderitemcode LIKE '" + drugitem_sub + "%'");
                        string locationname = "";
                        //=== ใบสั่งยา ===
                        if (row.Length > 0)
                        {
                            db_packagemaster = row.CopyToDataTable();
                            db_prescription = rw.CopyToDataTable();
                            db_data = clsPackagemaster.db_data;
                        }

                        DataRow addrow = db_stricker.Rows.Add();
                        addrow["prescriptionno"] = prescriptionno;
                        addrow["orderitembarcode"] = "";
                        addrow["patientname"] = db_data.Rows[0]["patientname"].ToString();
                        addrow["hn"] = db_data.Rows[0]["hn"].ToString();
                        addrow["wardname"] = db_data.Rows[0]["wardname"].ToString();
                        addrow["orderitemname"] = db_prescription.Rows[0]["orderitemname"].ToString();
                        addrow["genericname"] = "";
                        addrow["orderqty"] = db_prescription.Rows[0]["orderqty"].ToString();
                        addrow["special_advice_text"] = db_prescription.Rows[0]["special_advice_text"].ToString();
                        addrow["Qrcode"] = "";
                        addrow["orderunitdesc"] = "";
                        addrow["orderdate"] = clsconvert.convertdate_HH_mm_ss_EN_NEW(db_packagemaster.Rows[0]["ordercreatedate"].ToString());
                        addrow["orderitemTHname"] = "";
                        addrow["locationname"] = "";
                        addrow["itemidentify"] = db_packagemaster.Rows[0]["itemidentify"].ToString();
                        addrow["patientGender"] = db_data.Rows[0]["sex"].ToString();

                        if (db_data.Rows[0]["patientdob"].ToString() != "")
                        {
                            string year = "";
                            string yearNow = "";

                            yearNow = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

                            var date = db_data.Rows[0]["patientdob"].ToString();
                            var yNow = yearNow;

                            year = clsconvert.convertdate_YYYY_EN(date);
                            yearNow = clsconvert.convertdate_YYYY_EN(yNow);

                            if (year != "" && yearNow != "")
                            {
                                addrow["patientAge"] = (Convert.ToInt32(yearNow) - Convert.ToInt32(year));
                            }
                            else
                            {
                                addrow["patientAge"] = "";
                            }
                        }
                        else
                        {
                            addrow["patientAge"] = "";
                        }
                        addrow["qn"] = db_data.Rows[0]["qn"].ToString();
                        addrow["amount"] = "";
                        addrow["itemlotexpire"] = "";
                        addrow["itemlotcode"] = "";
                        addrow["vn"] = db_data.Rows[0]["vn"].ToString();
                        addrow["instructiondesc"] = db_prescription.Rows[0]["instructiondesc"].ToString();
                        addrow["dosage"] = db_prescription.Rows[0]["dosage"].ToString();
                        addrow["dosageunitdesc"] = db_prescription.Rows[0]["dosageunitdesc"].ToString();
                        addrow["frequencydesc"] = db_prescription.Rows[0]["frequencydesc"].ToString();
                        addrow["freetext1"] = db_prescription.Rows[0]["freetext1"].ToString();
                        addrow["freetext2"] = db_prescription.Rows[0]["freetext2"].ToString();
                        addrow["freetext3"] = db_prescription.Rows[0]["freetext3"].ToString();
                        addrow["freetext4"] = db_prescription.Rows[0]["freetext4"].ToString();
                        addrow["freetext5"] = db_prescription.Rows[0]["freetext5"].ToString();
                        addrow["rightname"] = db_data.Rows[0]["rightname"].ToString();
                        addrow["seqmax"] = db_packagemaster.Rows[0]["seqmax"].ToString();
                        addrow["pregnancy"] = db_packagemaster.Rows[0]["pregnancy"].ToString();
                        addrow["edned"] = db_packagemaster.Rows[0]["edned"].ToString();
                        addrow["poison"] = db_packagemaster.Rows[0]["poison"].ToString();
                        addrow["drugaccountcode"] = db_packagemaster.Rows[0]["drugaccountcode"].ToString();
                        addrow["store"] = db_prescription.Rows[0]["store"].ToString();
                    }

                }
                return db_stricker;
            }
            catch (Exception ex)
            {
                return new DataTable();
            }
        }

        public static DataTable print_guideslip(string prescriptionno)
        {
            bool result = false;
            string drugitem = "";
            try
            {
                DataTable db_packagemaster = new DataTable();
                #region creatdb_guideslip
                DataTable db_guideslip = new DataTable();
                db_guideslip.Columns.Add("prescriptionno", typeof(String));
                db_guideslip.Columns.Add("orderitembarcode", typeof(String));
                db_guideslip.Columns.Add("patientname", typeof(String));
                db_guideslip.Columns.Add("hn", typeof(String));
                db_guideslip.Columns.Add("wardname", typeof(String));
                db_guideslip.Columns.Add("orderitemname", typeof(String));
                db_guideslip.Columns.Add("genericname", typeof(String));
                db_guideslip.Columns.Add("orderqty", typeof(String));
                db_guideslip.Columns.Add("Qrcode", typeof(String));
                db_guideslip.Columns.Add("orderunitdesc", typeof(String));
                db_guideslip.Columns.Add("orderdate", typeof(String));
                db_guideslip.Columns.Add("orderitemTHname", typeof(String));
                db_guideslip.Columns.Add("locationname", typeof(String));
                db_guideslip.Columns.Add("itemidentify", typeof(String));
                db_guideslip.Columns.Add("patientGender", typeof(String));
                db_guideslip.Columns.Add("patientAge", typeof(String));
                db_guideslip.Columns.Add("qn", typeof(String));
                db_guideslip.Columns.Add("vn", typeof(String));
                db_guideslip.Columns.Add("rcvmedno", typeof(String));
                db_guideslip.Columns.Add("expressmed", typeof(String));
                db_guideslip.Columns.Add("amount", typeof(String));
                db_guideslip.Columns.Add("gendatetime", typeof(String));
                

                #endregion

                if (clsPackagemaster.db_packagemaster.Rows.Count > 0 && clsPackagemaster.db_drug.Rows.Count > 0)
                {
                    foreach (DataRow r in clsPackagemaster.db_packagemaster.Rows)
                    {

                        string drugitem_sub = "";
                        string s1 = r["orderitemcode"].ToString();
                        string s2 = s1.IndexOf("^").ToString();
                        if (int.Parse(s2) > 0)
                        {
                            drugitem_sub = s1.Substring(0, int.Parse(s2));
                        }
                        else
                        {
                            drugitem_sub = s1;
                        }

                        DataRow[] row = clsPackagemaster.db_drug.Select(" orderitemcode_ori LIKE '" + drugitem_sub + "%'");


                        string locationname = "";
                        //=== ใบสั่งยา ===
                        if (row.Length > 0)
                        {

                            db_packagemaster = row.CopyToDataTable();
                        }

                        DataRow addrow = db_guideslip.Rows.Add();
                        addrow["prescriptionno"] = prescriptionno;
                        addrow["orderitembarcode"] = "";
                        addrow["patientname"] = r["patientname"].ToString();
                        addrow["hn"] = r["hn"].ToString();
                        addrow["wardname"] = r["wardname"].ToString();
                        addrow["orderitemname"] = "";
                        addrow["genericname"] = "";
                        addrow["orderqty"] = db_packagemaster.Rows[0]["orderqty_ori"].ToString();
                        addrow["special_advice_text"] = db_packagemaster.Rows[0]["special_advice_text"].ToString();
                        addrow["Qrcode"] = "";
                        addrow["orderunitdesc"] = r["orderunitdesc"].ToString();
                        addrow["orderdate"] = "";
                        addrow["orderitemTHname"] = "";
                        addrow["locationname"] = "";
                        addrow["itemidentify"] = "";
                        addrow["patientGender"] = "";
                        addrow["patientAge"] = "";
                        addrow["qn"] = "";
                        addrow["amount"] = "";
                        addrow["itemlotexpire"] = "";
                        addrow["itemlotcode"] = "";
                        addrow["vn"] = r["vn"].ToString();
                        addrow["instructiondesc"] = db_packagemaster.Rows[0]["instructiondesc"].ToString();
                        addrow["dosage"] = db_packagemaster.Rows[0]["dosage"].ToString();
                        addrow["dosageunitdesc"] = db_packagemaster.Rows[0]["dosageunitdesc"].ToString();
                        addrow["frequencydesc"] = db_packagemaster.Rows[0]["frequencydesc"].ToString();
                        addrow["freetext1"] = db_packagemaster.Rows[0]["freetext1"].ToString();
                        addrow["freetext2"] = db_packagemaster.Rows[0]["freetext2"].ToString();
                        addrow["freetext3"] = db_packagemaster.Rows[0]["freetext3"].ToString();
                        addrow["freetext4"] = db_packagemaster.Rows[0]["freetext4"].ToString();
                        addrow["freetext5"] = db_packagemaster.Rows[0]["freetext5"].ToString();
                        addrow["rightname"] = db_packagemaster.Rows[0]["rightname"].ToString();

                    }

                }
                return db_guideslip;
            }
            catch (Exception ex)
            {
                return new DataTable();
            }
        }

        private void btn_PrintSticker_Click(object sender, EventArgs e)
        {
            DataTable dtprint = new DataTable();
            dtprint = reprint_stricker(lbPres.Text);
            rpt_re_stricker(dtprint);

        }
        public void rpt_stricker(DataTable dtprint)
        {
            ReportDocument rpt = new ReportDocument();
            try
            {
                //rpt.Load(Application.StartupPath + "\\Report\\crpSticker.rpt");
                rpt.Load("D:\\Projects\\นครพิงค์\\PrescriptionManagement\\PrescriptionManagement\\report\\crpSticker.rpt");
                rpt.SetDataSource(dtprint);
                rpt.PrintOptions.PrinterName = "Microsoft Print to PDF";
                rpt.PrintToPrinter(1, false, 0, 0);

            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // Ensure the report document is properly closed and disposed
                rpt.Close();
                rpt.Dispose();

            }
        }
        public void rpt_re_stricker(DataTable dtprint)
        {
            ReportDocument rpt = new ReportDocument();
            try
            {
                //rpt.Load(Application.StartupPath + "\\Report\\crpSticker.rpt");
                rpt.Load("D:\\Projects\\นครพิงค์\\PrescriptionManagement\\PrescriptionManagement\\report\\crpStickerNohead.rpt");
                rpt.SetDataSource(dtprint);
                rpt.PrintOptions.PrinterName = "Microsoft Print to PDF";
                rpt.PrintToPrinter(1, false, 0, 0);

            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // Ensure the report document is properly closed and disposed
                rpt.Close();
                rpt.Dispose();

            }
        }
        public void rpt_guideslipr(DataTable dtprint)
        {
            ReportDocument rpt = new ReportDocument();
            try
            {
                rpt.Load(Application.StartupPath + "\\report\\crpguideslip.rpt");
                //rpt.Load("D:\\Projects\\นครพิงค์\\PrescriptionManagement\\PrescriptionManagement\report\\crpguideslip.rpt");
                rpt.SetDataSource(dtprint);
                rpt.PrintOptions.PrinterName = "Microsoft Print to PDF";
                rpt.PrintToPrinter(1, false, 0, 0);

            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // Ensure the report document is properly closed and disposed
                rpt.Close();
                rpt.Dispose();

            }
        }
        public void rpt_guideslip(DataTable dtprint)
        {
            ReportDocument rpt = new ReportDocument();
            try
            {
                rpt.Load(Application.StartupPath + "\\report\\crpguideslip.rpt");
                //rpt.Load("D:\\Projects\\นครพิงค์\\PrescriptionManagement\\PrescriptionManagement\\report\\crpguideslip.rpt");
                rpt.SetDataSource(dtprint);
                rpt.PrintOptions.PrinterName = "Microsoft Print to PDF";
                rpt.PrintToPrinter(1, false, 0, 0);

            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // Ensure the report document is properly closed and disposed
                rpt.Close();
                rpt.Dispose();

            }
        }


        private void pic_clear_Click(object sender, EventArgs e)
        {
            txt_BarcodePrescr.Text = "";
            txt_BarcodeBasket.Text = "";
            txtuserid.Text = "";
            txtpresc.Text = "";
            lbHN.Text = "";
            lbVN.Text = "";
            lbname.Text = "";
            lbPres.Text = "";
            dg_waitMatching.Rows.Clear();
            dg_FinishMatching.Rows.Clear();
            txt_BarcodePrescr.Focus();
            lbusername.Text = "-";
            txt_BarcodeBasket.Enabled = true;
            txt_BarcodePrescr.Enabled = true;
            if (btnStartTimer_.Visible == true)
            {
                timer1.Stop();
            }
            else if (btnStopTimer_.Visible == true)
            {
                timer1.Start();
            }
        }

        private void btnStopTimer__Click(object sender, EventArgs e)
        {

        }

        private async void txt_BarcodeBasket_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                blRFIDNum.Text = txt_BarcodeBasket.Text.Trim();
                await clsService.GetbasketNumber(txt_BarcodeBasket.Text.Trim());
                if(clsPackagemaster.dtbasket.Rows.Count > 0)
                {
                    txt_BarcodeBasket.Text = clsPackagemaster.dtbasket.Rows[0]["no"].ToString();
                    txtuserid.Focus();
                }
            }
            
        }

        private async void txtuserid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                register_basket();
            }
        }
        public static string date_utc(string date_value)
        {
            string str_datenow = "";
            if (date_value != "" )
            {
                DateTime utcNow = Convert.ToDateTime(date_value);
                str_datenow = utcNow.ToString("o");
                //DateTime utcDateTime = utcNow.ToUniversalTime();
                str_datenow = utcNow.ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
            }
            else
            {
                str_datenow = "";
            }
            
            return str_datenow;
        }

        private void btnslip_Click(object sender, EventArgs e)
        {
            if (clsPackagemaster.db_packagemaster.Rows.Count > 0)
            {
                //DataTable tb = clsPackagemaster.db_packagemaster.Select().CopyToDataTable(); // Copy to a new DataTable
                reprintguideslip(clsPackagemaster.db_packagemaster);
            }
        }

        private async void txtpresc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                timer1.Stop();
                DateTime _dtstr = DateTime.Now;
                DateTime _dtend = DateTime.Now;
                _dtstr = _dtstr.AddDays(-1);
                string dtstr = clsconvert.convertdate_YYYY_MM_DD_HH_EN_NEW(_dtstr.ToString());
                string dtend = clsconvert.convertdate_YYYY_MM_DD_HH_EN_NEW(_dtend.ToString());

                await clsService.RequestdetailAll(dtstr, dtend,txtpresc.Text.ToString());
                if(clsPackagemaster.db_packagemaster.Rows.Count > 0)
                {
                    DataTable db_packagemaster_first = new DataTable();
                    db_packagemaster_first = clsPackagemaster.db_packagemaster.Select().CopyToDataTable(); // Copy to a new DataTable

                    if (db_packagemaster_first.Rows.Count > 0)
                    {
                        //dg_waitMatching.DataSource = db_packagemaster_first;
                        //txt_BarcodeBasket.Text = db_packagemaster_first.Rows[0]["basketno"].ToString();
                        lbPres.Text = db_packagemaster_first.Rows[0]["prescriptionno"].ToString();
                        lbHN.Text = db_packagemaster_first.Rows[0]["hn"].ToString();
                        lbVN.Text = db_packagemaster_first.Rows[0]["vn"].ToString();
                        //txt_BarcodePrescr.Text.ToUpper();
                        //txt_BarcodePrescr.Enabled = false;
                        //txt_BarcodeBasket.Enabled = false;
                        //txtuserid.Focus();
                        #region dgv
                        dg_FinishMatching.Rows.Clear();
                        // เพิ่มข้อมูลตัวอย่าง
                        foreach (DataRow i in db_packagemaster_first.Rows)
                        {
                            dg_FinishMatching.Rows.Add(true,i["orderitemcode"].ToString(),
                                i["orderitemname"].ToString(),
                                i["orderqty"].ToString(),
                                i["dosageunitcode"].ToString(),
                                i["shelfzone"].ToString()+"-"+i["shelfname"].ToString());

                            // รีเฟรช DataGridView

                        }

                        #endregion
                    }

                }
                
            }
        }

        private void btn_UnselectAll_print_Click(object sender, EventArgs e)
        {
            //Check For Print
            int totalDtgM = dg_FinishMatching.Rows.Count;
            for (int i = 0; i < totalDtgM; i++)
            {
                dg_FinishMatching.Rows[i].Cells["chkFin"].Value = false;
            }
        }

        private void btn_selectAll_print_Click(object sender, EventArgs e)
        {
            //Check For Print
            int totalDtgM = dg_FinishMatching.Rows.Count;
            for (int i = 0; i < totalDtgM; i++)
            {
                dg_FinishMatching.Rows[i].Cells["chkFin"].Value = true;
            }
        }

        private void txtuserid_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }

}
