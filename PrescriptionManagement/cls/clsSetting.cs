using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrescriptionManagement.cls
{
    internal class clsSetting
    {
        public static string PicPath { get; set; }
        public static string COM_NAME { get; set; }
        public static string IP { get; set; }
        public static string path { get; set; }
        public static string Reportpath { get; set; }        
        public static string Meddispense_DIHWebservice_DIHPMPFWebservice { get; set; }
        public static string Basket_Volume { get; set; }        
        public static string ADLogin_API { get; set; }
        public static string ADLogout_API { get; set; }
        public static string license { get; set; }        
        public static string Location { get; set; }
        public static string LocationZone1 { get; set; }
        public static string LocationZone2 { get; set; }
        public static string LocationZone3 { get; set; }
        public static string PRINTERNAME_presc { get; set; }
        public static string PRINTERNAME_sticker { get; set; }
        public static string PRINTERNAME_guideslip { get; set; }
        public static string timer1 { get; set; }
        public static string fucEndJob { get; set; }
        public static string ZONE { get; set; }
        public static bool EnterEvent { get; set; } = false;
        public static string orderzone { get; set; }
        public static string isprint { get; set; }
        public static string sortOrder { get; set; }
        public static string username { get; set; }
        public static string usertype { get; set; }
    }
}
