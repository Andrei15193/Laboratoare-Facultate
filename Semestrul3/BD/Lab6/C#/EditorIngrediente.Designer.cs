namespace BDLab6
{
    partial class EditorIngrediente
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
            this.label1 = new System.Windows.Forms.Label();
            this.ingredienteListBox = new System.Windows.Forms.ListBox();
            this.numeTextBox = new System.Windows.Forms.TextBox();
            this.unitateDeMasuraTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.adaugaButton = new System.Windows.Forms.Button();
            this.modificaButton = new System.Windows.Forms.Button();
            this.stergeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ingrediente";
            // 
            // ingredienteListBox
            // 
            this.ingredienteListBox.FormattingEnabled = true;
            this.ingredienteListBox.Location = new System.Drawing.Point(12, 25);
            this.ingredienteListBox.Name = "ingredienteListBox";
            this.ingredienteListBox.Size = new System.Drawing.Size(237, 121);
            this.ingredienteListBox.TabIndex = 1;
            this.ingredienteListBox.SelectedIndexChanged += new System.EventHandler(this.IngredientSelectat);
            // 
            // numeTextBox
            // 
            this.numeTextBox.Location = new System.Drawing.Point(108, 152);
            this.numeTextBox.Name = "numeTextBox";
            this.numeTextBox.Size = new System.Drawing.Size(141, 20);
            this.numeTextBox.TabIndex = 2;
            // 
            // unitateDeMasuraTextBox
            // 
            this.unitateDeMasuraTextBox.Location = new System.Drawing.Point(108, 177);
            this.unitateDeMasuraTextBox.Name = "unitateDeMasuraTextBox";
            this.unitateDeMasuraTextBox.Size = new System.Drawing.Size(141, 20);
            this.unitateDeMasuraTextBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 155);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Nume:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 180);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Unitate de masura";
            // 
            // adaugaButton
            // 
            this.adaugaButton.Location = new System.Drawing.Point(12, 207);
            this.adaugaButton.Name = "adaugaButton";
            this.adaugaButton.Size = new System.Drawing.Size(75, 23);
            this.adaugaButton.TabIndex = 6;
            this.adaugaButton.Text = "Adauga";
            this.adaugaButton.UseVisualStyleBackColor = true;
            this.adaugaButton.Click += new System.EventHandler(this.AdaugaIngredient);
            // 
            // modificaButton
            // 
            this.modificaButton.Enabled = false;
            this.modificaButton.Location = new System.Drawing.Point(93, 207);
            this.modificaButton.Name = "modificaButton";
            this.modificaButton.Size = new System.Drawing.Size(75, 23);
            this.modificaButton.TabIndex = 7;
            this.modificaButton.Text = "Modifica";
            this.modificaButton.UseVisualStyleBackColor = true;
            this.modificaButton.Click += new System.EventHandler(this.ModificaIngredient);
            // 
            // stergeButton
            // 
            this.stergeButton.Enabled = false;
            this.stergeButton.Location = new System.Drawing.Point(174, 207);
            this.stergeButton.Name = "stergeButton";
            this.stergeButton.Size = new System.Drawing.Size(75, 23);
            this.stergeButton.TabIndex = 8;
            this.stergeButton.Text = "Sterge";
            this.stergeButton.UseVisualStyleBackColor = true;
            this.stergeButton.Click += new System.EventHandler(this.StergeIngredient);
            // 
            // EditorIngrediente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(263, 245);
            this.Controls.Add(this.stergeButton);
            this.Controls.Add(this.modificaButton);
            this.Controls.Add(this.adaugaButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.unitateDeMasuraTextBox);
            this.Controls.Add(this.numeTextBox);
            this.Controls.Add(this.ingredienteListBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "EditorIngrediente";
            this.Text = "EditorIngrediente";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox ingredienteListBox;
        private System.Windows.Forms.TextBox numeTextBox;
        private System.Windows.Forms.TextBox unitateDeMasuraTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button adaugaButton;
        private System.Windows.Forms.Button modificaButton;
        private System.Windows.Forms.Button stergeButton;
    }
}