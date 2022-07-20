namespace FormApp3
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.StripChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label_ConnectedIdentifier = new System.Windows.Forms.Label();
            this.label_ConnectionDisplay = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.comboBox_Dates = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.StripChart)).BeginInit();
            this.SuspendLayout();
            // 
            // StripChart
            // 
            this.StripChart.Anchor = System.Windows.Forms.AnchorStyles.None;
            chartArea2.Name = "ChartArea1";
            this.StripChart.ChartAreas.Add(chartArea2);
            legend2.Enabled = false;
            legend2.Name = "Legend1";
            this.StripChart.Legends.Add(legend2);
            this.StripChart.Location = new System.Drawing.Point(50, 50);
            this.StripChart.Margin = new System.Windows.Forms.Padding(0);
            this.StripChart.Name = "StripChart";
            series3.BorderWidth = 5;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Legend = "Legend1";
            series3.Name = "FilterData";
            series4.BorderWidth = 5;
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Legend = "Legend1";
            series4.Name = "TempData";
            this.StripChart.Series.Add(series3);
            this.StripChart.Series.Add(series4);
            this.StripChart.Size = new System.Drawing.Size(716, 389);
            this.StripChart.TabIndex = 0;
            this.StripChart.Text = "chart1";
            this.StripChart.Click += new System.EventHandler(this.StripChart_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label_ConnectedIdentifier
            // 
            this.label_ConnectedIdentifier.AutoSize = true;
            this.label_ConnectedIdentifier.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_ConnectedIdentifier.Location = new System.Drawing.Point(50, 9);
            this.label_ConnectedIdentifier.Name = "label_ConnectedIdentifier";
            this.label_ConnectedIdentifier.Size = new System.Drawing.Size(192, 26);
            this.label_ConnectedIdentifier.TabIndex = 1;
            this.label_ConnectedIdentifier.Text = "Connection status:";
            this.label_ConnectedIdentifier.Click += new System.EventHandler(this.label1_Click);
            // 
            // label_ConnectionDisplay
            // 
            this.label_ConnectionDisplay.AutoSize = true;
            this.label_ConnectionDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_ConnectionDisplay.Location = new System.Drawing.Point(248, 9);
            this.label_ConnectionDisplay.Name = "label_ConnectionDisplay";
            this.label_ConnectionDisplay.Size = new System.Drawing.Size(156, 26);
            this.label_ConnectionDisplay.TabIndex = 2;
            this.label_ConnectionDisplay.Text = "Disconnected";
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 10000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // comboBox_Dates
            // 
            this.comboBox_Dates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Dates.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_Dates.FormattingEnabled = true;
            this.comboBox_Dates.Location = new System.Drawing.Point(490, 6);
            this.comboBox_Dates.Name = "comboBox_Dates";
            this.comboBox_Dates.Size = new System.Drawing.Size(207, 33);
            this.comboBox_Dates.TabIndex = 3;
            this.comboBox_Dates.SelectedIndexChanged += new System.EventHandler(this.comboBox_Dates_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.comboBox_Dates);
            this.Controls.Add(this.label_ConnectionDisplay);
            this.Controls.Add(this.label_ConnectedIdentifier);
            this.Controls.Add(this.StripChart);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.StripChart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label_ConnectedIdentifier;
        private System.Windows.Forms.Label label_ConnectionDisplay;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.ComboBox comboBox_Dates;
        private System.Windows.Forms.DataVisualization.Charting.Chart StripChart;
    }
}

