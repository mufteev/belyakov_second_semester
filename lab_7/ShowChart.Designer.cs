namespace lab_7
{
    partial class ShowChart
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.chartRandom = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chartRandom)).BeginInit();
            this.SuspendLayout();
            // 
            // chartRandom
            // 
            this.chartRandom.BackColor = System.Drawing.SystemColors.MenuBar;
            chartArea1.AxisX.Title = "Time";
            chartArea1.AxisY.Title = "Power";
            chartArea1.Name = "ChartArea1";
            this.chartRandom.ChartAreas.Add(chartArea1);
            this.chartRandom.Dock = System.Windows.Forms.DockStyle.Top;
            this.chartRandom.Location = new System.Drawing.Point(0, 0);
            this.chartRandom.Margin = new System.Windows.Forms.Padding(2);
            this.chartRandom.Name = "chartRandom";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series1.Legend = "Legend1";
            series1.MarkerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            series1.MarkerColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            series1.MarkerSize = 7;
            series1.Name = "Series1";
            this.chartRandom.Series.Add(series1);
            this.chartRandom.Size = new System.Drawing.Size(682, 291);
            this.chartRandom.TabIndex = 3;
            this.chartRandom.Text = "chart1";
            title1.Name = "Title1";
            title1.Text = "Pareto";
            this.chartRandom.Titles.Add(title1);
            // 
            // ShowChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 554);
            this.Controls.Add(this.chartRandom);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ShowChart";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.chartRandom)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataVisualization.Charting.Chart chartRandom;
    }
}

