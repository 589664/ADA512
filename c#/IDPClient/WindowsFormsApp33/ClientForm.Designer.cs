namespace IDPClient
{
    partial class ClientForm
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
            this.cartesianChart1 = new LiveCharts.WinForms.CartesianChart();
            this.button1 = new System.Windows.Forms.Button();
            this.plus = new System.Windows.Forms.Button();
            this.minus = new System.Windows.Forms.Button();
            this.setKP = new System.Windows.Forms.TextBox();
            this.setKI = new System.Windows.Forms.TextBox();
            this.setKD = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.startButton = new System.Windows.Forms.Button();
            this.radioButtonUDP = new System.Windows.Forms.RadioButton();
            this.radioButtonTCP = new System.Windows.Forms.RadioButton();
            this.PIDControl = new System.Windows.Forms.GroupBox();
            this.transmissionSimGroup = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.transmissionDelay = new System.Windows.Forms.NumericUpDown();
            this.ipAdress = new System.Windows.Forms.TextBox();
            this.transmissionChart = new LiveCharts.WinForms.CartesianChart();
            this.PIDControl.SuspendLayout();
            this.transmissionSimGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.transmissionDelay)).BeginInit();
            this.SuspendLayout();
            // 
            // cartesianChart1
            // 
            this.cartesianChart1.Location = new System.Drawing.Point(69, 30);
            this.cartesianChart1.Name = "cartesianChart1";
            this.cartesianChart1.Size = new System.Drawing.Size(567, 618);
            this.cartesianChart1.TabIndex = 0;
            this.cartesianChart1.Text = "cartesianChart1";
            this.cartesianChart1.ChildChanged += new System.EventHandler<System.Windows.Forms.Integration.ChildChangedEventArgs>(this.cartesianChart1_ChildChanged);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button1.FlatAppearance.BorderSize = 3;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(89, 364);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(143, 37);
            this.button1.TabIndex = 1;
            this.button1.Text = "AddRandom";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.randomValueButton);
            // 
            // plus
            // 
            this.plus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.plus.Location = new System.Drawing.Point(148, 36);
            this.plus.Name = "plus";
            this.plus.Size = new System.Drawing.Size(72, 54);
            this.plus.TabIndex = 2;
            this.plus.Text = "+";
            this.plus.UseVisualStyleBackColor = true;
            this.plus.Click += new System.EventHandler(this.plus_Click);
            // 
            // minus
            // 
            this.minus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minus.Location = new System.Drawing.Point(238, 36);
            this.minus.Name = "minus";
            this.minus.Size = new System.Drawing.Size(72, 54);
            this.minus.TabIndex = 3;
            this.minus.Text = "-";
            this.minus.UseVisualStyleBackColor = true;
            this.minus.Click += new System.EventHandler(this.minus_Click);
            // 
            // setKP
            // 
            this.setKP.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.setKP.Location = new System.Drawing.Point(75, 109);
            this.setKP.Name = "setKP";
            this.setKP.Size = new System.Drawing.Size(110, 35);
            this.setKP.TabIndex = 4;
            this.setKP.TextChanged += new System.EventHandler(this.setKP_TextChanged);
            // 
            // setKI
            // 
            this.setKI.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.setKI.Location = new System.Drawing.Point(76, 163);
            this.setKI.Name = "setKI";
            this.setKI.Size = new System.Drawing.Size(109, 35);
            this.setKI.TabIndex = 5;
            this.setKI.TextChanged += new System.EventHandler(this.setKI_TextChanged);
            // 
            // setKD
            // 
            this.setKD.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.setKD.Location = new System.Drawing.Point(76, 212);
            this.setKD.Name = "setKD";
            this.setKD.Size = new System.Drawing.Size(110, 35);
            this.setKD.TabIndex = 6;
            this.setKD.TextChanged += new System.EventHandler(this.setKD_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 29);
            this.label1.TabIndex = 7;
            this.label1.Text = "setPoint";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(19, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 29);
            this.label2.TabIndex = 8;
            this.label2.Text = "kP:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(19, 163);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 29);
            this.label3.TabIndex = 9;
            this.label3.Text = "kI:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(19, 212);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 29);
            this.label4.TabIndex = 10;
            this.label4.Text = "kD:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(34, 314);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(109, 35);
            this.textBox1.TabIndex = 11;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(176, 314);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(109, 35);
            this.textBox2.TabIndex = 12;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(29, 276);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(125, 26);
            this.label5.TabIndex = 13;
            this.label5.Text = "count LOC";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(171, 276);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(117, 26);
            this.label6.TabIndex = 14;
            this.label6.Text = "count PID";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // startButton
            // 
            this.startButton.BackColor = System.Drawing.Color.LightGreen;
            this.startButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startButton.Location = new System.Drawing.Point(179, 41);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(95, 36);
            this.startButton.TabIndex = 15;
            this.startButton.Text = "START";
            this.startButton.UseVisualStyleBackColor = false;
            this.startButton.Click += new System.EventHandler(this.startTransmission);
            // 
            // radioButtonUDP
            // 
            this.radioButtonUDP.AutoSize = true;
            this.radioButtonUDP.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonUDP.Location = new System.Drawing.Point(23, 31);
            this.radioButtonUDP.Name = "radioButtonUDP";
            this.radioButtonUDP.Size = new System.Drawing.Size(87, 30);
            this.radioButtonUDP.TabIndex = 16;
            this.radioButtonUDP.TabStop = true;
            this.radioButtonUDP.Text = "UDP";
            this.radioButtonUDP.UseVisualStyleBackColor = true;
            // 
            // radioButtonTCP
            // 
            this.radioButtonTCP.AutoSize = true;
            this.radioButtonTCP.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonTCP.Location = new System.Drawing.Point(23, 67);
            this.radioButtonTCP.Name = "radioButtonTCP";
            this.radioButtonTCP.Size = new System.Drawing.Size(83, 30);
            this.radioButtonTCP.TabIndex = 17;
            this.radioButtonTCP.TabStop = true;
            this.radioButtonTCP.Text = "TCP";
            this.radioButtonTCP.UseVisualStyleBackColor = true;
            // 
            // PIDControl
            // 
            this.PIDControl.Controls.Add(this.plus);
            this.PIDControl.Controls.Add(this.minus);
            this.PIDControl.Controls.Add(this.label1);
            this.PIDControl.Controls.Add(this.setKP);
            this.PIDControl.Controls.Add(this.button1);
            this.PIDControl.Controls.Add(this.label6);
            this.PIDControl.Controls.Add(this.label2);
            this.PIDControl.Controls.Add(this.textBox2);
            this.PIDControl.Controls.Add(this.label5);
            this.PIDControl.Controls.Add(this.label3);
            this.PIDControl.Controls.Add(this.setKI);
            this.PIDControl.Controls.Add(this.textBox1);
            this.PIDControl.Controls.Add(this.label4);
            this.PIDControl.Controls.Add(this.setKD);
            this.PIDControl.Location = new System.Drawing.Point(1309, 30);
            this.PIDControl.Name = "PIDControl";
            this.PIDControl.Size = new System.Drawing.Size(324, 419);
            this.PIDControl.TabIndex = 18;
            this.PIDControl.TabStop = false;
            this.PIDControl.Text = "PID Controller";
            this.PIDControl.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // transmissionSimGroup
            // 
            this.transmissionSimGroup.BackColor = System.Drawing.Color.Transparent;
            this.transmissionSimGroup.Controls.Add(this.label8);
            this.transmissionSimGroup.Controls.Add(this.label7);
            this.transmissionSimGroup.Controls.Add(this.transmissionDelay);
            this.transmissionSimGroup.Controls.Add(this.ipAdress);
            this.transmissionSimGroup.Controls.Add(this.radioButtonUDP);
            this.transmissionSimGroup.Controls.Add(this.radioButtonTCP);
            this.transmissionSimGroup.Controls.Add(this.startButton);
            this.transmissionSimGroup.Location = new System.Drawing.Point(1309, 475);
            this.transmissionSimGroup.Name = "transmissionSimGroup";
            this.transmissionSimGroup.Size = new System.Drawing.Size(324, 202);
            this.transmissionSimGroup.TabIndex = 19;
            this.transmissionSimGroup.TabStop = false;
            this.transmissionSimGroup.Text = "Data Transmission";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(18, 152);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(86, 29);
            this.label8.TabIndex = 21;
            this.label8.Text = "Delay:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(18, 110);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 29);
            this.label7.TabIndex = 20;
            this.label7.Text = "IP:";
            // 
            // transmissionDelay
            // 
            this.transmissionDelay.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.transmissionDelay.Location = new System.Drawing.Point(131, 148);
            this.transmissionDelay.Name = "transmissionDelay";
            this.transmissionDelay.Size = new System.Drawing.Size(120, 35);
            this.transmissionDelay.TabIndex = 19;
            this.transmissionDelay.ValueChanged += new System.EventHandler(this.transmissionDelay_ValueChanged);
            // 
            // ipAdress
            // 
            this.ipAdress.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ipAdress.Location = new System.Drawing.Point(131, 107);
            this.ipAdress.Name = "ipAdress";
            this.ipAdress.Size = new System.Drawing.Size(157, 35);
            this.ipAdress.TabIndex = 18;
            this.ipAdress.TextChanged += new System.EventHandler(this.ipAdress_TextChanged);
            // 
            // transmissionChart
            // 
            this.transmissionChart.Location = new System.Drawing.Point(664, 30);
            this.transmissionChart.Name = "transmissionChart";
            this.transmissionChart.Size = new System.Drawing.Size(577, 618);
            this.transmissionChart.TabIndex = 20;
            this.transmissionChart.Text = "cartesianChart2";
            this.transmissionChart.ChildChanged += new System.EventHandler<System.Windows.Forms.Integration.ChildChangedEventArgs>(this.transmissionChart_ChildChanged);
            // 
            // GraphForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1676, 702);
            this.Controls.Add(this.transmissionChart);
            this.Controls.Add(this.transmissionSimGroup);
            this.Controls.Add(this.PIDControl);
            this.Controls.Add(this.cartesianChart1);
            this.Name = "GraphForm";
            this.Text = "Form1";
            this.PIDControl.ResumeLayout(false);
            this.PIDControl.PerformLayout();
            this.transmissionSimGroup.ResumeLayout(false);
            this.transmissionSimGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.transmissionDelay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private LiveCharts.WinForms.CartesianChart cartesianChart1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button plus;
        private System.Windows.Forms.Button minus;
        private System.Windows.Forms.TextBox setKP;
        private System.Windows.Forms.TextBox setKI;
        private System.Windows.Forms.TextBox setKD;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.RadioButton radioButtonUDP;
        private System.Windows.Forms.RadioButton radioButtonTCP;
        private System.Windows.Forms.GroupBox PIDControl;
        private System.Windows.Forms.GroupBox transmissionSimGroup;
        private System.Windows.Forms.TextBox ipAdress;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown transmissionDelay;
        private LiveCharts.WinForms.CartesianChart transmissionChart;
    }
}

