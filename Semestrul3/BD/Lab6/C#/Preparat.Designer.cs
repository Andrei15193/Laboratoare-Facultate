namespace BDLab6
{
    partial class Preparat
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
            this.numePreparatTextBox = new System.Windows.Forms.TextBox();
            this.durataNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.pretNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.acceptButton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.durataNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pretNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // numePreparatTextBox
            // 
            this.numePreparatTextBox.Location = new System.Drawing.Point(108, 12);
            this.numePreparatTextBox.Name = "numePreparatTextBox";
            this.numePreparatTextBox.Size = new System.Drawing.Size(120, 20);
            this.numePreparatTextBox.TabIndex = 0;
            // 
            // durataNumericUpDown
            // 
            this.durataNumericUpDown.Location = new System.Drawing.Point(108, 64);
            this.durataNumericUpDown.Name = "durataNumericUpDown";
            this.durataNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.durataNumericUpDown.TabIndex = 1;
            // 
            // pretNumericUpDown
            // 
            this.pretNumericUpDown.Location = new System.Drawing.Point(108, 38);
            this.pretNumericUpDown.Name = "pretNumericUpDown";
            this.pretNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.pretNumericUpDown.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Nume preparat:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Pret:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Durata preparare:";
            // 
            // acceptButton
            // 
            this.acceptButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.acceptButton.Location = new System.Drawing.Point(27, 92);
            this.acceptButton.Name = "acceptButton";
            this.acceptButton.Size = new System.Drawing.Size(75, 23);
            this.acceptButton.TabIndex = 6;
            this.acceptButton.Text = "Adauga";
            this.acceptButton.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(138, 92);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "Anuleaza";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // Preparat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(245, 133);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.acceptButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pretNumericUpDown);
            this.Controls.Add(this.durataNumericUpDown);
            this.Controls.Add(this.numePreparatTextBox);
            this.Name = "Preparat";
            this.Text = "Preparat";
            ((System.ComponentModel.ISupportInitialize)(this.durataNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pretNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox numePreparatTextBox;
        private System.Windows.Forms.NumericUpDown durataNumericUpDown;
        private System.Windows.Forms.NumericUpDown pretNumericUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button acceptButton;
        private System.Windows.Forms.Button button2;
    }
}