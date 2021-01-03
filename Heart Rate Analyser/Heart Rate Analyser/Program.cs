using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Python.Runtime;
using System;
using System.Diagnostics;

namespace Heart_Rate_Analyser
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        

        [STAThread]
        static void Main()
        {

            //Console.WriteLine("Execute Python Process");
            //python_process();
            Global_Variables.init_serial_uart_channel();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        //static void python_process()
        //{
        //    var psi = new ProcessStartInfo();
        //    psi.FileName = @"C:\Users\Designer\AppData\Local\Programs\Python\Python38\python.exe";

        //    var script = @"C:\Users\Designer\Desktop\BLM5135_Final_Project\Heart Rate Analyser Models Training\parameters.py";
        //    /* add arguments */

        //    psi.Arguments = $"\"{script}\"";
        //    psi.UseShellExecute = false;
        //    psi.CreateNoWindow = true;
        //    psi.RedirectStandardOutput = true;
        //    psi.RedirectStandardError = true;
        //    var errors = "";
        //    var results = "";

        //    using (var process = Process.Start(psi))
        //    {
        //        errors = process.StandardError.ReadToEnd();
        //        results = process.StandardOutput.ReadToEnd();
        //    }
        //    Console.WriteLine("ERRORS");
        //    Console.WriteLine(errors);
        //    Console.WriteLine();
        //    Console.WriteLine("Results");
        //    Console.WriteLine(results);
        //}

    }
}
