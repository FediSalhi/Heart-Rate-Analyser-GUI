using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

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
            Global_Variables.init_serial_uart_channel();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Thread com_thread = new Thread(new ThreadStart(Simple_Communication.communication_loop));
            //com_thread.Start();

            Application.Run(new Form1());
        }

    }
}
