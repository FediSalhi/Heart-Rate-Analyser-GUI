namespace Heart_Rate_Analyser
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1_comPort = new System.Windows.Forms.ComboBox();
            this.comboBox2_baudRate = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1_open = new System.Windows.Forms.Button();
            this.button2_close = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.verticalProgressBar1_statusCom = new Heart_Rate_Analyser.VerticalProgressBar();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(882, 500);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.verticalProgressBar1_statusCom);
            this.groupBox1.Controls.Add(this.button2_close);
            this.groupBox1.Controls.Add(this.button1_open);
            this.groupBox1.Controls.Add(this.comboBox2_baudRate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.comboBox1_comPort);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 520);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(882, 133);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "COM PORT SETTINGS";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(76, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "COM PORT :";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // comboBox1_comPort
            // 
            this.comboBox1_comPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1_comPort.FormattingEnabled = true;
            this.comboBox1_comPort.Location = new System.Drawing.Point(196, 39);
            this.comboBox1_comPort.Name = "comboBox1_comPort";
            this.comboBox1_comPort.Size = new System.Drawing.Size(164, 31);
            this.comboBox1_comPort.TabIndex = 1;
            this.comboBox1_comPort.DropDown += new System.EventHandler(this.comboBox1_comPort_DropDown);
            // 
            // comboBox2_baudRate
            // 
            this.comboBox2_baudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2_baudRate.FormattingEnabled = true;
            this.comboBox2_baudRate.Items.AddRange(new object[] {
            "9600",
            "115200"});
            this.comboBox2_baudRate.Location = new System.Drawing.Point(196, 76);
            this.comboBox2_baudRate.Name = "comboBox2_baudRate";
            this.comboBox2_baudRate.Size = new System.Drawing.Size(164, 31);
            this.comboBox2_baudRate.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(76, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "BAUD RATE : ";
            // 
            // button1_open
            // 
            this.button1_open.Location = new System.Drawing.Point(509, 56);
            this.button1_open.Name = "button1_open";
            this.button1_open.Size = new System.Drawing.Size(122, 37);
            this.button1_open.TabIndex = 4;
            this.button1_open.Text = "OPEN";
            this.button1_open.UseVisualStyleBackColor = true;
            this.button1_open.Click += new System.EventHandler(this.button1_open_Click);
            // 
            // button2_close
            // 
            this.button2_close.Location = new System.Drawing.Point(648, 56);
            this.button2_close.Name = "button2_close";
            this.button2_close.Size = new System.Drawing.Size(122, 37);
            this.button2_close.TabIndex = 5;
            this.button2_close.Text = "CLOSE";
            this.button2_close.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(262, 351);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(256, 30);
            this.textBox1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(298, 309);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(176, 23);
            this.label3.TabIndex = 1;
            this.label3.Text = "Sensor Measurement ";
            // 
            // verticalProgressBar1_statusCom
            // 
            this.verticalProgressBar1_statusCom.Location = new System.Drawing.Point(24, 42);
            this.verticalProgressBar1_statusCom.Name = "verticalProgressBar1_statusCom";
            this.verticalProgressBar1_statusCom.Size = new System.Drawing.Size(25, 65);
            this.verticalProgressBar1_statusCom.TabIndex = 0;
            this.verticalProgressBar1_statusCom.Value = 40;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 653);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hear Rate Analyser";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button2_close;
        private System.Windows.Forms.Button button1_open;
        private System.Windows.Forms.ComboBox comboBox2_baudRate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1_comPort;
        private System.Windows.Forms.Label label1;
        private VerticalProgressBar verticalProgressBar1_statusCom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.IO.Ports.SerialPort serialPort1;
    }
}

