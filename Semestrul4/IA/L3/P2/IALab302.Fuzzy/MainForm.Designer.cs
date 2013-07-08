namespace IALab302.Fuzzy
{
    partial class MainForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea7 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea8 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea9 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend7 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Legend legend8 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Legend legend9 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Title title7 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.Title title8 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.Title title9 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this._chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this._temperatureNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this._capacityNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this._capacityLabel = new System.Windows.Forms.Label();
            this._temperatureLabel = new System.Windows.Forms.Label();
            this._calculateButton = new System.Windows.Forms.Button();
            this._rulesButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._chart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._temperatureNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._capacityNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // _chart
            // 
            this._chart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea7.AxisX.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea7.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea7.BorderColor = System.Drawing.Color.Gainsboro;
            chartArea7.Name = "temperatureChartArea";
            chartArea8.Name = "capacityChartArea";
            chartArea9.Name = "powerChartArea";
            this._chart.ChartAreas.Add(chartArea7);
            this._chart.ChartAreas.Add(chartArea8);
            this._chart.ChartAreas.Add(chartArea9);
            legend7.DockedToChartArea = "temperatureChartArea";
            legend7.IsDockedInsideChartArea = false;
            legend7.LegendStyle = System.Windows.Forms.DataVisualization.Charting.LegendStyle.Column;
            legend7.Name = "temperatureLegend";
            legend7.TableStyle = System.Windows.Forms.DataVisualization.Charting.LegendTableStyle.Tall;
            legend8.DockedToChartArea = "capacityChartArea";
            legend8.IsDockedInsideChartArea = false;
            legend8.LegendStyle = System.Windows.Forms.DataVisualization.Charting.LegendStyle.Column;
            legend8.Name = "capacityLegend";
            legend8.TableStyle = System.Windows.Forms.DataVisualization.Charting.LegendTableStyle.Tall;
            legend9.DockedToChartArea = "powerChartArea";
            legend9.IsDockedInsideChartArea = false;
            legend9.LegendStyle = System.Windows.Forms.DataVisualization.Charting.LegendStyle.Column;
            legend9.Name = "powerLegend";
            legend9.TableStyle = System.Windows.Forms.DataVisualization.Charting.LegendTableStyle.Tall;
            this._chart.Legends.Add(legend7);
            this._chart.Legends.Add(legend8);
            this._chart.Legends.Add(legend9);
            this._chart.Location = new System.Drawing.Point(12, 12);
            this._chart.Name = "_chart";
            this._chart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            this._chart.Size = new System.Drawing.Size(608, 329);
            this._chart.TabIndex = 0;
            this._chart.Text = "Chart";
            title7.DockedToChartArea = "temperatureChartArea";
            title7.IsDockedInsideChartArea = false;
            title7.Name = "temperatureTitle";
            title7.Text = "Temperatura";
            title8.DockedToChartArea = "capacityChartArea";
            title8.IsDockedInsideChartArea = false;
            title8.Name = "capacityTitle";
            title8.Text = "Capacitate";
            title9.DockedToChartArea = "powerChartArea";
            title9.IsDockedInsideChartArea = false;
            title9.Name = "powerTitle";
            title9.Text = "Putere furnal";
            this._chart.Titles.Add(title7);
            this._chart.Titles.Add(title8);
            this._chart.Titles.Add(title9);
            // 
            // _temperatureNumericUpDown
            // 
            this._temperatureNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._temperatureNumericUpDown.DecimalPlaces = 3;
            this._temperatureNumericUpDown.Location = new System.Drawing.Point(82, 349);
            this._temperatureNumericUpDown.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this._temperatureNumericUpDown.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this._temperatureNumericUpDown.Name = "_temperatureNumericUpDown";
            this._temperatureNumericUpDown.Size = new System.Drawing.Size(153, 20);
            this._temperatureNumericUpDown.TabIndex = 1;
            this._temperatureNumericUpDown.Value = new decimal(new int[] {
            95,
            0,
            0,
            0});
            // 
            // _capacityNumericUpDown
            // 
            this._capacityNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._capacityNumericUpDown.DecimalPlaces = 3;
            this._capacityNumericUpDown.Location = new System.Drawing.Point(305, 349);
            this._capacityNumericUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this._capacityNumericUpDown.Name = "_capacityNumericUpDown";
            this._capacityNumericUpDown.Size = new System.Drawing.Size(153, 20);
            this._capacityNumericUpDown.TabIndex = 2;
            this._capacityNumericUpDown.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // _capacityLabel
            // 
            this._capacityLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._capacityLabel.AutoSize = true;
            this._capacityLabel.Location = new System.Drawing.Point(241, 352);
            this._capacityLabel.Name = "_capacityLabel";
            this._capacityLabel.Size = new System.Drawing.Size(58, 13);
            this._capacityLabel.TabIndex = 3;
            this._capacityLabel.Text = "Capacitate";
            // 
            // _temperatureLabel
            // 
            this._temperatureLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._temperatureLabel.AutoSize = true;
            this._temperatureLabel.Location = new System.Drawing.Point(9, 352);
            this._temperatureLabel.Name = "_temperatureLabel";
            this._temperatureLabel.Size = new System.Drawing.Size(67, 13);
            this._temperatureLabel.TabIndex = 4;
            this._temperatureLabel.Text = "Temperatura";
            // 
            // _calculateButton
            // 
            this._calculateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._calculateButton.Location = new System.Drawing.Point(545, 347);
            this._calculateButton.Name = "_calculateButton";
            this._calculateButton.Size = new System.Drawing.Size(75, 23);
            this._calculateButton.TabIndex = 5;
            this._calculateButton.Text = "Calculeaza";
            this._calculateButton.UseVisualStyleBackColor = true;
            this._calculateButton.Click += new System.EventHandler(this.calculateButton_Click);
            // 
            // _rulesButton
            // 
            this._rulesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._rulesButton.Location = new System.Drawing.Point(464, 347);
            this._rulesButton.Name = "_rulesButton";
            this._rulesButton.Size = new System.Drawing.Size(75, 23);
            this._rulesButton.TabIndex = 6;
            this._rulesButton.Text = "Reguli";
            this._rulesButton.UseVisualStyleBackColor = true;
            this._rulesButton.Click += new System.EventHandler(this._rulesButton_Click);
            // 
            // MainForm
            // 
            this.AcceptButton = this._calculateButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 380);
            this.Controls.Add(this._rulesButton);
            this.Controls.Add(this._calculateButton);
            this.Controls.Add(this._temperatureLabel);
            this.Controls.Add(this._capacityLabel);
            this.Controls.Add(this._capacityNumericUpDown);
            this.Controls.Add(this._temperatureNumericUpDown);
            this.Controls.Add(this._chart);
            this.MinimumSize = new System.Drawing.Size(648, 419);
            this.Name = "MainForm";
            this.Text = "IALab302.Fuzzy";
            ((System.ComponentModel.ISupportInitialize)(this._chart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._temperatureNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._capacityNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart _chart;
        private System.Windows.Forms.NumericUpDown _temperatureNumericUpDown;
        private System.Windows.Forms.NumericUpDown _capacityNumericUpDown;
        private System.Windows.Forms.Label _capacityLabel;
        private System.Windows.Forms.Label _temperatureLabel;
        private System.Windows.Forms.Button _calculateButton;
        private System.Windows.Forms.Button _rulesButton;
    }
}

