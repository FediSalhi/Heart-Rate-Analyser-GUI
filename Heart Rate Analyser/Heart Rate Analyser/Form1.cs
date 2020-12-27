using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace Heart_Rate_Analyser
{
    public partial class Form1 : Form
    {

        byte[] rx_buffer = new byte[100];
        int received_bytes = 0;
        int counter = 0;
        int returned = 0;

        long max = 30;
        long min = 0;

        /* used to update textBox every 1 second */
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        
        
        

        public Form1()
        {
            InitializeComponent();

            timer.Interval = (10); //ms
            timer.Tick += new EventHandler(update_text_box);
            timer.Start();
        }


        /* update textbox with new measurment */
        private void update_text_box(object sender, EventArgs e)
        {
            Simple_Communication.communication_loop();
            textBox1.Text = Global_Variables.GL_sensor_measurement.ToString();

            chart1.ChartAreas[0].AxisX.Minimum = min;
            chart1.ChartAreas[0].AxisX.Maximum = max;

            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Maximum = 3300;

            chart1.ChartAreas[0].AxisX.ScaleView.Zoom(min, max);

            chart1.Series[0].Points.AddXY((min + max) / 2, Global_Variables.GL_sensor_measurement);

            max++;
            min++;



        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1_open.Enabled = true;
            button2_close.Enabled = false;
            verticalProgressBar1_statusCom.Value = 0;
            comboBox2_baudRate.Text = "115200";

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_comPort_DropDown(object sender, EventArgs e)
        {
            string[] portLists = SerialPort.GetPortNames();
            comboBox1_comPort.Items.Clear();
            comboBox1_comPort.Items.AddRange(portLists);

        }

        private void button1_open_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.PortName = comboBox1_comPort.Text;
                serialPort1.BaudRate = Convert.ToInt32(comboBox2_baudRate.Text);
                serialPort1.Open();

                button1_open.Enabled = false;
                button2_close.Enabled = true;
                verticalProgressBar1_statusCom.Value = 100;

            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void button2_close_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Close();
                button1_open.Enabled = true;
                button2_close.Enabled = false;
                verticalProgressBar1_statusCom.Value = 0;

            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                serialPort1.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //serialPort1.Read(rx_buffer, received_bytes, counter);


            byte received_byte = 0;

            byte[] buffer  =  new byte[20];
            int offset = 0;
            int count = 20;
            int okunan = 0;
            int index = 0;
           // received_byte = (byte)serialPort1.ReadByte();
            //string received_byte = serialPort1.ReadExisting();
            okunan = serialPort1.Read(buffer, offset, count);
            //add timer out exception
            //https://docs.microsoft.com/tr-tr/dotnet/api/system.timeoutexception?view=net-5.0


            //Console.WriteLine(received_byte);

            //Global_Variables.GL_rx_buffer_u8[Global_Variables.GL_buffer_last_u32] = received_byte;
            //Global_Variables.GL_buffer_last_u32++;

            for (index = 0; index < okunan; index++)
            {
                Global_Variables.GL_rx_buffer_u8[Global_Variables.GL_buffer_last_u32] = buffer[index];
                Global_Variables.GL_buffer_last_u32++;

                if (Global_Variables.GL_buffer_last_u32 >= Simple_Communication.MAX_DATA_LENGTH)
                {
                    Global_Variables.GL_buffer_last_u32 = 0;
                }
            }


        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_baudRate_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
