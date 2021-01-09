using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Tensorflow.KerasApi;
using Tensorflow;
using System.Diagnostics;
using NumSharp.Utilities;

namespace Heart_Rate_Analyser
{
    class Artificial_Intelligence
    {

        public static double[] model_input = new double[1000];
        public static bool diagnostig_started = false;
        public static int  i = 0;

        public void load_neural_network_model()
        {
            //TODO: implement this one

        }

        public static void load_cnn_model()
        {

        }

        public void load_xgboost()
        {
            //TODO:
        }

        public void get_prediction_neural_network()
        {
            //TODO: create an array of floats 
            // this array contains window time values of Global_Variables.GL_sensor_measurement
            // feed this array to model and get prediction
        }

        public static void get_prediction_cnn()
        {
                python_process();         
        }

        public static void get_prediction_xgboost()
        {
            //TODO:
        }

        static void python_process()
        {
            var psi = new ProcessStartInfo();
            psi.FileName = @"C:\Users\Designer\AppData\Local\Programs\Python\Python38\python.exe";

            var script = @"C:\Users\Designer\Desktop\BLM5135_Final_Project\Heart Rate Analyser Models Training\real_time_test_process.py";
            /* add arguments */
            double[] input = Global_Variables.GL_rx_measurement_u64;
            string txt = "";
            foreach (double element in input)
            {
                txt += element.ToString() + " ";
            }
            psi.Arguments = $"\"{script}\" \"{txt}\"";
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            var errors = "";
            var results = "";

            using (var process = Process.Start(psi))
            {
                //errors = process.StandardError.ReadToEnd();
                results = process.StandardOutput.ReadToEnd();
            }
            Global_Variables.GL_analysis_result_string = results;
            //Console.WriteLine("ERRORS");
            //Console.WriteLine(errors);
            //Console.WriteLine();
            Console.WriteLine("Results");
            Console.WriteLine(results);
        }

    }
}
