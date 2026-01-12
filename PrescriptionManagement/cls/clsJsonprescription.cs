using Microsoft.Azure.KeyVault.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PrescriptionManagement.cls
{
    public class clsJsonprescription
    {
        public string _id { get; set; }
        public string __v { get; set; }
        public string appointmentdate { get; set; }
        public string cid { get; set; }
        public string computername { get; set; }
        public string confirmdatetime { get; set; }
        public string confirmuserid { get; set; }
        public string doctorcode { get; set; }
        public string doctorname { get; set; }
        public string expressmed { get; set; }
        public string hn { get; set; }
        public string lastupdate { get; set; }
        public string ordercreatedate { get; set; }
        public string patientdob { get; set; }
        public string patientname { get; set; }
        public string pharmacyitemcode { get; set; }
        public string pharmacyitemdesc { get; set; }
        public string prescriptionno { get; set; }
        public string qn { get; set; }
        public string qrrdumix { get; set; }
        public string rcvmedno { get; set; }
        public string readdatetime { get; set; }
        public string rightid { get; set; }
        public string rightname { get; set; }
        public string sex { get; set; }
        public string sphmlct { get; set; }
        public string sphmname { get; set; }
        public string total_norefund { get; set; }
        public string total_refund { get; set; }
        public string totalprice { get; set; }
        public string vn { get; set; }
        public string voiddatetime { get; set; }
        public string wardcode { get; set; }
        public string wardname { get; set; }
        public string firstzone { get; set; }
        public string genorderdatetime { get; set; }        
        public string printdatetime { get; set; }
        public List<clsJsonlabs> labs { get; set; } = new List<clsJsonlabs>();
        public List<clsJsondrugs> drugs { get; set; } = new List<clsJsondrugs>();
        public List<clsJsondrugallergies> drugallergies { get; set; } = new List<clsJsondrugallergies>();
        
    }
    public class clsJsondrugs
    {
        public string seq { get; set; }
        public string orderitemcode { get; set; }
        public string orderitemname { get; set; }
        public string orderqty { get; set; }
        public string orderunitcode { get; set; }
        public string orderunitdesc { get; set; }
        public string strength { get; set; }
        public string strengthunit { get; set; }
        public string instructioncode { get; set; }
        public string instructiondesc { get; set; }
        public string dosage { get; set; }
        public string dosageunitcode { get; set; }
        public string dosageunitdesc { get; set; }
        public string frequencycode { get; set; }
        public string frequencydesc { get; set; }
        public string freetext1 { get; set; }
        public string freetext2 { get; set; }
        public string freetext3 { get; set; }
        public string freetext4 { get; set; }
        public string freetext5 { get; set; }
        public string price { get; set; }
        public string precaution_advice_text { get; set; }
        public string special_advice_text { get; set; }
        public string refund { get; set; }
        public string norefund { get; set; }
        public string icsale { get; set; }
        public string icrefund { get; set; }
        public string icnorefund { get; set; }
        public string store { get; set; }
        public string _id { get; set; }
    
    }
    public class clsJsonlabs
    {
        public string lab_no { get; set; }
        public string lab_code { get; set; }
        public string lab_name { get; set; }
        public string result { get; set; }
        public string type_result { get; set; }
        public string unit { get; set; }
        public string minresult { get; set; }
        public string maxresult { get; set; }
        public string remark { get; set; }
        public string date_result { get; set; }
    }
    public class clsJsondrugallergies
    {        
        public string allergycode { get; set; }
        public string code { get; set; }
        public string genericname { get; set; }
        public string adverbs { get; set; }
        public string memo { get; set; }
       
    }
    public class clsJsonhis
    {
        public string allergycode { get; set; }
        public string code { get; set; }
        public string genericname { get; set; }
        public string adverbs { get; set; }
        public string memo { get; set; }
        public string lastmodified { get; set; }
    }



}
