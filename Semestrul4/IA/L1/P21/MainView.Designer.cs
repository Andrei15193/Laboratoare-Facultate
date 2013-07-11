namespace IALab1
{
    partial class MainView
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
            this.coefficientsLabel = new System.Windows.Forms.Label();
            this.coinValues = new System.Windows.Forms.TextBox();
            this.maximumCoefficientValueLabel = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.coefficientMaximumValueNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.startTimeLabel = new System.Windows.Forms.Label();
            this.startTimeValueLabel = new System.Windows.Forms.Label();
            this.endTimeLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.searchLabel = new System.Windows.Forms.Label();
            this.searchStateLabel = new System.Windows.Forms.Label();
            this.StartButton = new System.Windows.Forms.Button();
            this.searchTypeLabel = new System.Windows.Forms.Label();
            this.searchTypeComboBox = new System.Windows.Forms.ComboBox();
            this.stopButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.coefficientMaximumValueNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // coefficientsLabel
            // 
            this.coefficientsLabel.AutoSize = true;
            this.coefficientsLabel.Location = new System.Drawing.Point(12, 15);
            this.coefficientsLabel.Name = "coefficientsLabel";
            this.coefficientsLabel.Size = new System.Drawing.Size(65, 13);
            this.coefficientsLabel.TabIndex = 0;
            this.coefficientsLabel.Text = "Coin values:";
            // 
            // coinValues
            // 
            this.coinValues.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.coinValues.Location = new System.Drawing.Point(89, 12);
            this.coinValues.Name = "coinValues";
            this.coinValues.Size = new System.Drawing.Size(183, 20);
            this.coinValues.TabIndex = 1;
            // 
            // maximumCoefficientValueLabel
            // 
            this.maximumCoefficientValueLabel.AutoSize = true;
            this.maximumCoefficientValueLabel.Location = new System.Drawing.Point(12, 41);
            this.maximumCoefficientValueLabel.Name = "maximumCoefficientValueLabel";
            this.maximumCoefficientValueLabel.Size = new System.Drawing.Size(71, 13);
            this.maximumCoefficientValueLabel.TabIndex = 2;
            this.maximumCoefficientValueLabel.Text = "Coefficients <";
            // 
            // coefficientMaximumValueNumericUpDown
            // 
            this.coefficientMaximumValueNumericUpDown.Location = new System.Drawing.Point(89, 39);
            this.coefficientMaximumValueNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.coefficientMaximumValueNumericUpDown.Name = "coefficientMaximumValueNumericUpDown";
            this.coefficientMaximumValueNumericUpDown.Size = new System.Drawing.Size(183, 20);
            this.coefficientMaximumValueNumericUpDown.TabIndex = 3;
            this.coefficientMaximumValueNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // startTimeLabel
            // 
            this.startTimeLabel.AutoSize = true;
            this.startTimeLabel.Location = new System.Drawing.Point(12, 95);
            this.startTimeLabel.Name = "startTimeLabel";
            this.startTimeLabel.Size = new System.Drawing.Size(54, 13);
            this.startTimeLabel.TabIndex = 6;
            this.startTimeLabel.Text = "Start time:";
            // 
            // startTimeValueLabel
            // 
            this.startTimeValueLabel.AutoSize = true;
            this.startTimeValueLabel.Location = new System.Drawing.Point(72, 95);
            this.startTimeValueLabel.Name = "startTimeValueLabel";
            this.startTimeValueLabel.Size = new System.Drawing.Size(13, 13);
            this.startTimeValueLabel.TabIndex = 7;
            this.startTimeValueLabel.Text = "0";
            // 
            // endTimeLabel
            // 
            this.endTimeLabel.AutoSize = true;
            this.endTimeLabel.Location = new System.Drawing.Point(12, 113);
            this.endTimeLabel.Name = "endTimeLabel";
            this.endTimeLabel.Size = new System.Drawing.Size(51, 13);
            this.endTimeLabel.TabIndex = 8;
            this.endTimeLabel.Text = "End time:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(72, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "0";
            // 
            // searchLabel
            // 
            this.searchLabel.AutoSize = true;
            this.searchLabel.Location = new System.Drawing.Point(12, 130);
            this.searchLabel.Name = "searchLabel";
            this.searchLabel.Size = new System.Drawing.Size(44, 13);
            this.searchLabel.TabIndex = 10;
            this.searchLabel.Text = "Search:";
            // 
            // searchStateLabel
            // 
            this.searchStateLabel.AutoSize = true;
            this.searchStateLabel.Location = new System.Drawing.Point(72, 130);
            this.searchStateLabel.Name = "searchStateLabel";
            this.searchStateLabel.Size = new System.Drawing.Size(57, 13);
            this.searchStateLabel.TabIndex = 11;
            this.searchStateLabel.Text = "not started";
            // 
            // StartButton
            // 
            this.StartButton.Enabled = false;
            this.StartButton.Location = new System.Drawing.Point(12, 150);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(121, 23);
            this.StartButton.TabIndex = 12;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartSearch);
            // 
            // searchTypeLabel
            // 
            this.searchTypeLabel.AutoSize = true;
            this.searchTypeLabel.Location = new System.Drawing.Point(12, 68);
            this.searchTypeLabel.Name = "searchTypeLabel";
            this.searchTypeLabel.Size = new System.Drawing.Size(67, 13);
            this.searchTypeLabel.TabIndex = 13;
            this.searchTypeLabel.Text = "Search type:";
            // 
            // searchTypeComboBox
            // 
            this.searchTypeComboBox.FormattingEnabled = true;
            this.searchTypeComboBox.Items.AddRange(new object[] {
            "Bread First Search (uninformal)",
            "Greedy (informal)"});
            this.searchTypeComboBox.Location = new System.Drawing.Point(89, 65);
            this.searchTypeComboBox.Name = "searchTypeComboBox";
            this.searchTypeComboBox.Size = new System.Drawing.Size(183, 21);
            this.searchTypeComboBox.TabIndex = 14;
            this.searchTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.NewSearchMethodHasBeenSelected);
            // 
            // stopButton
            // 
            this.stopButton.Enabled = false;
            this.stopButton.Location = new System.Drawing.Point(151, 150);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(121, 23);
            this.stopButton.TabIndex = 15;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += this.CancelSearch;
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 185);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.searchTypeComboBox);
            this.Controls.Add(this.searchTypeLabel);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.searchStateLabel);
            this.Controls.Add(this.searchLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.endTimeLabel);
            this.Controls.Add(this.startTimeValueLabel);
            this.Controls.Add(this.startTimeLabel);
            this.Controls.Add(this.coefficientMaximumValueNumericUpDown);
            this.Controls.Add(this.maximumCoefficientValueLabel);
            this.Controls.Add(this.coinValues);
            this.Controls.Add(this.coefficientsLabel);
            this.MaximumSize = new System.Drawing.Size(1024, 223);
            this.MinimumSize = new System.Drawing.Size(300, 223);
            this.Name = "MainView";
            this.Text = "IA Lab1";
            ((System.ComponentModel.ISupportInitialize)(this.coefficientMaximumValueNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label coefficientsLabel;
        private System.Windows.Forms.TextBox coinValues;
        private System.Windows.Forms.Label maximumCoefficientValueLabel;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.NumericUpDown coefficientMaximumValueNumericUpDown;
        private System.Windows.Forms.Label startTimeLabel;
        private System.Windows.Forms.Label startTimeValueLabel;
        private System.Windows.Forms.Label endTimeLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label searchLabel;
        private System.Windows.Forms.Label searchStateLabel;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Label searchTypeLabel;
        private System.Windows.Forms.ComboBox searchTypeComboBox;
        private System.Windows.Forms.Button stopButton;
    }
}

