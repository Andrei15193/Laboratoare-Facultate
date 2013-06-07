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
            this.numarNoduri = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.numarNoduriNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dimensiunePopulatieNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.numarGeneratiiNumericUpDown = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numarNoduriNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dimensiunePopulatieNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numarGeneratiiNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // muchiiTextBox
            // 
            this.muchiiTextBox.Location = new System.Drawing.Point(12, 25);
            this.muchiiTextBox.Name = "muchiiTextBox";
            this.muchiiTextBox.ReadOnly = true;
            this.muchiiTextBox.Size = new System.Drawing.Size(211, 250);
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
            // numarNoduri
            // 
            this.numarNoduri.AutoSize = true;
            this.numarNoduri.Location = new System.Drawing.Point(9, 283);
            this.numarNoduri.Name = "numarNoduri";
            this.numarNoduri.Size = new System.Drawing.Size(76, 13);
            this.numarNoduri.TabIndex = 2;
            this.numarNoduri.Text = "Numar noduri: ";
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(69, 359);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Partitioneaza";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // numarNoduriNumericUpDown
            // 
            this.numarNoduriNumericUpDown.Location = new System.Drawing.Point(126, 281);
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
            this.numarNoduriNumericUpDown.Size = new System.Drawing.Size(97, 20);
            this.numarNoduriNumericUpDown.TabIndex = 4;
            this.numarNoduriNumericUpDown.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numarNoduriNumericUpDown.Leave += new System.EventHandler(this.numericUpDown1_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 309);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Numar generatii:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 335);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Dimensiune populatie:";
            // 
            // dimensiunePopulatieNumericUpDown
            // 
            this.dimensiunePopulatieNumericUpDown.Location = new System.Drawing.Point(126, 333);
            this.dimensiunePopulatieNumericUpDown.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.dimensiunePopulatieNumericUpDown.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.dimensiunePopulatieNumericUpDown.Name = "dimensiunePopulatieNumericUpDown";
            this.dimensiunePopulatieNumericUpDown.Size = new System.Drawing.Size(97, 20);
            this.dimensiunePopulatieNumericUpDown.TabIndex = 7;
            this.dimensiunePopulatieNumericUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numarGeneratiiNumericUpDown
            // 
            this.numarGeneratiiNumericUpDown.Location = new System.Drawing.Point(126, 307);
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
            this.numarGeneratiiNumericUpDown.Size = new System.Drawing.Size(97, 20);
            this.numarGeneratiiNumericUpDown.TabIndex = 8;
            this.numarGeneratiiNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(235, 399);
            this.Controls.Add(this.numarGeneratiiNumericUpDown);
            this.Controls.Add(this.dimensiunePopulatieNumericUpDown);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numarNoduriNumericUpDown);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.numarNoduri);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.muchiiTextBox);
            this.Name = "Form1";
            this.Text = "IA Lab2.05";
            ((System.ComponentModel.ISupportInitialize)(this.numarNoduriNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dimensiunePopulatieNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numarGeneratiiNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox muchiiTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label numarNoduri;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown numarNoduriNumericUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown dimensiunePopulatieNumericUpDown;
        private System.Windows.Forms.NumericUpDown numarGeneratiiNumericUpDown;
    }
}

