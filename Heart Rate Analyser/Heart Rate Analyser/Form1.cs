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

                button1_open.Enabled = true;
                button2_close.Enabled = false;
                verticalProgressBar1_statusCom.Value = 100;

            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }
    }
}
