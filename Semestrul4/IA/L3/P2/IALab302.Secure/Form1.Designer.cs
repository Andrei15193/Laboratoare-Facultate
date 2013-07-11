namespace IALab302
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
            this.getEfficiencyButton = new System.Windows.Forms.Button();
            this.validateEfficiencyButton = new System.Windows.Forms.Button();
            this.conclusionsComboBox = new System.Windows.Forms.ComboBox();
            this.resultTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // getEfficiencyButton
            // 
            this.getEfficiencyButton.Location = new System.Drawing.Point(12, 277);
            this.getEfficiencyButton.Name = "getEfficiencyButton";
            this.getEfficiencyButton.Size = new System.Drawing.Size(109, 23);
            this.getEfficiencyButton.TabIndex = 0;
            this.getEfficiencyButton.Text = "Stabileste eficienta";
            this.getEfficiencyButton.UseVisualStyleBackColor = true;
            this.getEfficiencyButton.Click += new System.EventHandler(this.getEfficiencyButton_Click);
            // 
            // validateEfficiencyButton
            // 
            this.validateEfficiencyButton.Location = new System.Drawing.Point(12, 12);
            this.validateEfficiencyButton.Name = "validateEfficiencyButton";
            this.validateEfficiencyButton.Size = new System.Drawing.Size(109, 23);
            this.validateEfficiencyButton.TabIndex = 1;
            this.validateEfficiencyButton.Text = "Valideaza eficienta";
            this.validateEfficiencyButton.UseVisualStyleBackColor = true;
            this.validateEfficiencyButton.Click += new System.EventHandler(this.validateEfficiencyButton_Click);
            // 
            // conclusionsComboBox
            // 
            this.conclusionsComboBox.FormattingEnabled = true;
            this.conclusionsComboBox.Location = new System.Drawing.Point(127, 14);
            this.conclusionsComboBox.Name = "conclusionsComboBox";
            this.conclusionsComboBox.Size = new System.Drawing.Size(244, 21);
            this.conclusionsComboBox.TabIndex = 2;
            // 
            // resultTextBox
            // 
            this.resultTextBox.Location = new System.Drawing.Point(12, 41);
            this.resultTextBox.Name = "resultTextBox";
            this.resultTextBox.ReadOnly = true;
            this.resultTextBox.Size = new System.Drawing.Size(359, 230);
            this.resultTextBox.TabIndex = 3;
            this.resultTextBox.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 312);
            this.Controls.Add(this.resultTextBox);
            this.Controls.Add(this.conclusionsComboBox);
            this.Controls.Add(this.validateEfficiencyButton);
            this.Controls.Add(this.getEfficiencyButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "IA Lab3.02";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button getEfficiencyButton;
        private System.Windows.Forms.Button validateEfficiencyButton;
        private System.Windows.Forms.ComboBox conclusionsComboBox;
        private System.Windows.Forms.RichTextBox resultTextBox;
    }
}

