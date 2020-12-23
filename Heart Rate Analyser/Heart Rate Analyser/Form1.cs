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

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1_open.Enabled = true;
            button2_close.Enabled = false;
            verticalProgressBar1_statusCom.Value = 0;
            comboBox2_baudRate.Text = "9600";

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

            byte received_byte;
            received_byte = (byte)serialPort1.ReadByte();

            Global_Variables.GL_rx_buffer_u8[Global_Variables.GL_buffer_last_u32] = received_byte;
            Global_Variables.GL_buffer_last_u32++;
            if (Global_Variables.GL_buffer_last_u32 >= Simple_Communication.MAX_DATA_LENGTH)
            {
                Global_Variables.GL_buffer_last_u32 = 0;
            }

            if (Global_Variables.GL_buffer_last_u32 % 14 == 0)
            {
                this.BeginInvoke(new EventHandler(ProcessData));
            }
        }

        private void ProcessData(object sender, EventArgs e)
        {
            Simple_Communication.capture_packet_from_bytes();
        }
    }
}
