namespace SeesharpTools.JXI.Mathematics.example
{
    partial class StandardDeviationForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.nudTestTimes = new System.Windows.Forms.NumericUpDown();
            this.txtboxStandardDeviation = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtboxTimeElapsed = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nudLength = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.easyChart1 = new SeeSharpTools.JY.GUI.EasyChart();
            ((System.ComponentModel.ISupportInitialize)(this.nudTestTimes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLength)).BeginInit();
            this.SuspendLayout();
            // 
            // nudTestTimes
            // 
            this.nudTestTimes.Location = new System.Drawing.Point(640, 348);
            this.nudTestTimes.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudTestTimes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTestTimes.Name = "nudTestTimes";
            this.nudTestTimes.Size = new System.Drawing.Size(178, 25);
            this.nudTestTimes.TabIndex = 43;
            this.nudTestTimes.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudTestTimes.ValueChanged += new System.EventHandler(this.EfficiencyEvent);
            // 
            // txtboxStandardDeviation
            // 
            this.txtboxStandardDeviation.Location = new System.Drawing.Point(640, 175);
            this.txtboxStandardDeviation.Name = "txtboxStandardDeviation";
            this.txtboxStandardDeviation.Size = new System.Drawing.Size(179, 25);
            this.txtboxStandardDeviation.TabIndex = 39;
            this.txtboxStandardDeviation.Text = " ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(643, 330);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 15);
            this.label3.TabIndex = 41;
            this.label3.Text = "LoopTimes";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(637, 157);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(143, 15);
            this.label7.TabIndex = 38;
            this.label7.Text = "StandardDeviation";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(637, 390);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(111, 15);
            this.label9.TabIndex = 42;
            this.label9.Text = "Efficiency/ms";
            // 
            // txtboxTimeElapsed
            // 
            this.txtboxTimeElapsed.Location = new System.Drawing.Point(640, 409);
            this.txtboxTimeElapsed.Margin = new System.Windows.Forms.Padding(4);
            this.txtboxTimeElapsed.Name = "txtboxTimeElapsed";
            this.txtboxTimeElapsed.Size = new System.Drawing.Size(178, 25);
            this.txtboxTimeElapsed.TabIndex = 40;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(81, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(279, 15);
            this.label1.TabIndex = 47;
            this.label1.Text = "Random Data Length(To be analyzed)";
            // 
            // nudLength
            // 
            this.nudLength.Location = new System.Drawing.Point(366, 81);
            this.nudLength.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudLength.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudLength.Name = "nudLength";
            this.nudLength.Size = new System.Drawing.Size(179, 25);
            this.nudLength.TabIndex = 46;
            this.nudLength.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudLength.ValueChanged += new System.EventHandler(this.StandardDeviationEvent);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(76, 9);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(658, 44);
            this.label5.TabIndex = 45;
            this.label5.Text = "StandardDeviation Calculation";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(326, 100);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 15);
            this.label2.TabIndex = 44;
            // 
            // easyChart1
            // 
            this.easyChart1.AxisYMax = double.NaN;
            this.easyChart1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.None;
            this.easyChart1.ChartAreaBackColor = System.Drawing.Color.Empty;
            this.easyChart1.EasyChartBackColor = System.Drawing.Color.White;
            this.easyChart1.FixAxisX = false;
            this.easyChart1.LegendBackColor = System.Drawing.Color.Transparent;
            this.easyChart1.LegendVisible = false;
            this.easyChart1.Location = new System.Drawing.Point(24, 111);
            this.easyChart1.MajorGridColor = System.Drawing.Color.Black;
            this.easyChart1.MajorGridEnabled = true;
            this.easyChart1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.easyChart1.MinorGridColor = System.Drawing.Color.Black;
            this.easyChart1.MinorGridEnabled = false;
            this.easyChart1.MinorGridType = SeeSharpTools.JY.GUI.EasyChart.GridStyle.Solid;
            this.easyChart1.Name = "easyChart1";
            this.easyChart1.Palette = new System.Drawing.Color[] {
        System.Drawing.Color.Red,
        System.Drawing.Color.Blue,
        System.Drawing.Color.DeepPink,
        System.Drawing.Color.Navy,
        System.Drawing.Color.DarkGreen,
        System.Drawing.Color.OrangeRed,
        System.Drawing.Color.DarkCyan,
        System.Drawing.Color.Black};
            this.easyChart1.SeriesNames = new string[] {
        "Series1",
        "Series2",
        "Series3",
        "Series4",
        "Series5",
        "Series6",
        "Series7",
        "Series8",
        "Series9",
        "Series10",
        "Series11",
        "Series12",
        "Series13",
        "Series14",
        "Series15",
        "Series16",
        "Series17",
        "Series18",
        "Series19",
        "Series20",
        "Series21",
        "Series22",
        "Series23",
        "Series24",
        "Series25",
        "Series26",
        "Series27",
        "Series28",
        "Series29",
        "Series30",
        "Series31",
        "Series32"};
            this.easyChart1.Size = new System.Drawing.Size(600, 400);
            this.easyChart1.TabIndex = 48;
            this.easyChart1.XAxisLogarithmic = false;
            this.easyChart1.YAutoEnable = true;
            this.easyChart1.YAxisLogarithmic = false;
            // 
            // StandardDeviationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(842, 543);
            this.Controls.Add(this.easyChart1);
            this.Controls.Add(this.nudTestTimes);
            this.Controls.Add(this.txtboxStandardDeviation);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtboxTimeElapsed);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nudLength);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "StandardDeviationForm";
            this.Text = "StandardDeviationCalculationDemo";
            ((System.ComponentModel.ISupportInitialize)(this.nudTestTimes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLength)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nudTestTimes;
        private System.Windows.Forms.TextBox txtboxStandardDeviation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtboxTimeElapsed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudLength;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private SeeSharpTools.JY.GUI.EasyChart easyChart1;
    }
}

