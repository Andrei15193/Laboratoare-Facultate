namespace IALab205
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
            this.muchiiTextBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this._numarNoduriLabel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.numarNoduriNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this._numarGeneratiiLabel = new System.Windows.Forms.Label();
            this._dimensiunePopulatieLabel = new System.Windows.Forms.Label();
            this.dimensiunePopulatieNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.numarGeneratiiNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this._aeRadioButton = new System.Windows.Forms.RadioButton();
            this._searchMethodGroupBox = new System.Windows.Forms.GroupBox();
            this._psoRadioButton = new System.Windows.Forms.RadioButton();
            this._factorSocialNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this._factorCognitivNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this._factorInertieNumericUpDown = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numarNoduriNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dimensiunePopulatieNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numarGeneratiiNumericUpDown)).BeginInit();
            this._searchMethodGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._factorSocialNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._factorCognitivNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._factorInertieNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // muchiiTextBox
            // 
            this.muchiiTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.muchiiTextBox.Location = new System.Drawing.Point(12, 25);
            this.muchiiTextBox.Name = "muchiiTextBox";
            this.muchiiTextBox.ReadOnly = true;
            this.muchiiTextBox.Size = new System.Drawing.Size(283, 172);
            this.muchiiTextBox.TabIndex = 0;
            this.muchiiTextBox.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Muchiile grafului:";
            // 
            // _numarNoduriLabel
            // 
            this._numarNoduriLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._numarNoduriLabel.AutoSize = true;
            this._numarNoduriLabel.Location = new System.Drawing.Point(9, 284);
            this._numarNoduriLabel.Name = "_numarNoduriLabel";
            this._numarNoduriLabel.Size = new System.Drawing.Size(76, 13);
            this._numarNoduriLabel.TabIndex = 2;
            this._numarNoduriLabel.Text = "Numar noduri: ";
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(106, 443);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Partitioneaza";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // numarNoduriNumericUpDown
            // 
            this.numarNoduriNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numarNoduriNumericUpDown.Location = new System.Drawing.Point(126, 282);
            this.numarNoduriNumericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numarNoduriNumericUpDown.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numarNoduriNumericUpDown.Name = "numarNoduriNumericUpDown";
            this.numarNoduriNumericUpDown.Size = new System.Drawing.Size(169, 20);
            this.numarNoduriNumericUpDown.TabIndex = 4;
            this.numarNoduriNumericUpDown.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numarNoduriNumericUpDown.Leave += new System.EventHandler(this.numericUpDown1_Leave);
            // 
            // _numarGeneratiiLabel
            // 
            this._numarGeneratiiLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._numarGeneratiiLabel.AutoSize = true;
            this._numarGeneratiiLabel.Location = new System.Drawing.Point(9, 310);
            this._numarGeneratiiLabel.Name = "_numarGeneratiiLabel";
            this._numarGeneratiiLabel.Size = new System.Drawing.Size(84, 13);
            this._numarGeneratiiLabel.TabIndex = 5;
            this._numarGeneratiiLabel.Text = "Numar generatii:";
            // 
            // _dimensiunePopulatieLabel
            // 
            this._dimensiunePopulatieLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._dimensiunePopulatieLabel.AutoSize = true;
            this._dimensiunePopulatieLabel.Location = new System.Drawing.Point(9, 336);
            this._dimensiunePopulatieLabel.Name = "_dimensiunePopulatieLabel";
            this._dimensiunePopulatieLabel.Size = new System.Drawing.Size(111, 13);
            this._dimensiunePopulatieLabel.TabIndex = 6;
            this._dimensiunePopulatieLabel.Text = "Dimensiune populatie:";
            // 
            // dimensiunePopulatieNumericUpDown
            // 
            this.dimensiunePopulatieNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dimensiunePopulatieNumericUpDown.Location = new System.Drawing.Point(126, 334);
            this.dimensiunePopulatieNumericUpDown.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.dimensiunePopulatieNumericUpDown.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.dimensiunePopulatieNumericUpDown.Name = "dimensiunePopulatieNumericUpDown";
            this.dimensiunePopulatieNumericUpDown.Size = new System.Drawing.Size(169, 20);
            this.dimensiunePopulatieNumericUpDown.TabIndex = 7;
            this.dimensiunePopulatieNumericUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numarGeneratiiNumericUpDown
            // 
            this.numarGeneratiiNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numarGeneratiiNumericUpDown.Location = new System.Drawing.Point(126, 308);
            this.numarGeneratiiNumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numarGeneratiiNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numarGeneratiiNumericUpDown.Name = "numarGeneratiiNumericUpDown";
            this.numarGeneratiiNumericUpDown.Size = new System.Drawing.Size(169, 20);
            this.numarGeneratiiNumericUpDown.TabIndex = 8;
            this.numarGeneratiiNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // _aeRadioButton
            // 
            this._aeRadioButton.AutoSize = true;
            this._aeRadioButton.Checked = true;
            this._aeRadioButton.Location = new System.Drawing.Point(6, 26);
            this._aeRadioButton.Name = "_aeRadioButton";
            this._aeRadioButton.Size = new System.Drawing.Size(103, 17);
            this._aeRadioButton.TabIndex = 9;
            this._aeRadioButton.TabStop = true;
            this._aeRadioButton.Text = "Algoritm Evolutiv";
            this._aeRadioButton.UseVisualStyleBackColor = true;
            this._aeRadioButton.CheckedChanged += new System.EventHandler(this._aeRadioButton_CheckedChanged);
            // 
            // _searchMethodGroupBox
            // 
            this._searchMethodGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._searchMethodGroupBox.Controls.Add(this._psoRadioButton);
            this._searchMethodGroupBox.Controls.Add(this._aeRadioButton);
            this._searchMethodGroupBox.Location = new System.Drawing.Point(12, 203);
            this._searchMethodGroupBox.Name = "_searchMethodGroupBox";
            this._searchMethodGroupBox.Size = new System.Drawing.Size(283, 73);
            this._searchMethodGroupBox.TabIndex = 10;
            this._searchMethodGroupBox.TabStop = false;
            this._searchMethodGroupBox.Text = "Metoda de cautare";
            // 
            // _psoRadioButton
            // 
            this._psoRadioButton.AutoSize = true;
            this._psoRadioButton.Location = new System.Drawing.Point(6, 49);
            this._psoRadioButton.Name = "_psoRadioButton";
            this._psoRadioButton.Size = new System.Drawing.Size(155, 17);
            this._psoRadioButton.TabIndex = 10;
            this._psoRadioButton.Text = "Particle Swarm Optimization";
            this._psoRadioButton.UseVisualStyleBackColor = true;
            this._psoRadioButton.CheckedChanged += new System.EventHandler(this._psoRadioButton_CheckedChanged);
            // 
            // _factorSocialNumericUpDown
            // 
            this._factorSocialNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._factorSocialNumericUpDown.DecimalPlaces = 3;
            this._factorSocialNumericUpDown.Enabled = false;
            this._factorSocialNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this._factorSocialNumericUpDown.Location = new System.Drawing.Point(126, 386);
            this._factorSocialNumericUpDown.Name = "_factorSocialNumericUpDown";
            this._factorSocialNumericUpDown.Size = new System.Drawing.Size(169, 20);
            this._factorSocialNumericUpDown.TabIndex = 11;
            // 
            // _factorCognitivNumericUpDown
            // 
            this._factorCognitivNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._factorCognitivNumericUpDown.DecimalPlaces = 3;
            this._factorCognitivNumericUpDown.Enabled = false;
            this._factorCognitivNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this._factorCognitivNumericUpDown.Location = new System.Drawing.Point(126, 360);
            this._factorCognitivNumericUpDown.Name = "_factorCognitivNumericUpDown";
            this._factorCognitivNumericUpDown.Size = new System.Drawing.Size(169, 20);
            this._factorCognitivNumericUpDown.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 362);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Factor cognitiv:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 388);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Factor social:";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 414);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Factor de inertie:";
            // 
            // _factorInertieNumericUpDown
            // 
            this._factorInertieNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._factorInertieNumericUpDown.DecimalPlaces = 3;
            this._factorInertieNumericUpDown.Enabled = false;
            this._factorInertieNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this._factorInertieNumericUpDown.Location = new System.Drawing.Point(126, 412);
            this._factorInertieNumericUpDown.Name = "_factorInertieNumericUpDown";
            this._factorInertieNumericUpDown.Size = new System.Drawing.Size(169, 20);
            this._factorInertieNumericUpDown.TabIndex = 16;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(307, 478);
            this.Controls.Add(this._factorInertieNumericUpDown);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._factorCognitivNumericUpDown);
            this.Controls.Add(this._factorSocialNumericUpDown);
            this.Controls.Add(this._searchMethodGroupBox);
            this.Controls.Add(this.numarGeneratiiNumericUpDown);
            this.Controls.Add(this.dimensiunePopulatieNumericUpDown);
            this.Controls.Add(this._dimensiunePopulatieLabel);
            this.Controls.Add(this._numarGeneratiiLabel);
            this.Controls.Add(this.numarNoduriNumericUpDown);
            this.Controls.Add(this.button1);
            this.Controls.Add(this._numarNoduriLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.muchiiTextBox);
            this.MinimumSize = new System.Drawing.Size(323, 517);
            this.Name = "Form1";
            this.Text = "IA Lab2.05";
            ((System.ComponentModel.ISupportInitialize)(this.numarNoduriNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dimensiunePopulatieNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numarGeneratiiNumericUpDown)).EndInit();
            this._searchMethodGroupBox.ResumeLayout(false);
            this._searchMethodGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._factorSocialNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._factorCognitivNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._factorInertieNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox muchiiTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label _numarNoduriLabel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown numarNoduriNumericUpDown;
        private System.Windows.Forms.Label _numarGeneratiiLabel;
        private System.Windows.Forms.Label _dimensiunePopulatieLabel;
        private System.Windows.Forms.NumericUpDown dimensiunePopulatieNumericUpDown;
        private System.Windows.Forms.NumericUpDown numarGeneratiiNumericUpDown;
        private System.Windows.Forms.RadioButton _aeRadioButton;
        private System.Windows.Forms.GroupBox _searchMethodGroupBox;
        private System.Windows.Forms.RadioButton _psoRadioButton;
        private System.Windows.Forms.NumericUpDown _factorSocialNumericUpDown;
        private System.Windows.Forms.NumericUpDown _factorCognitivNumericUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown _factorInertieNumericUpDown;
    }
}

