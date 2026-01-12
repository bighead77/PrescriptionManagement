using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrescriptionManagement
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Thread t = new Thread(thread_test);
            t.CurrentCulture = new CultureInfo("en-US");
            t.Start();
            t.Join();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
        static void thread_test()
        {
            Console.WriteLine("Culture: {0}", CultureInfo.CurrentCulture.DisplayName);
        }
    }
}
