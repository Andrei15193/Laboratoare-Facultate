namespace BDLab6
{
    partial class Preparate
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
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.editeazaPreparatMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.actualizeazaPreparatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stergePreparatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.afiseazaIngredienteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preparateListBox = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.retetaLabel = new System.Windows.Forms.Label();
            this.adaugaPreparatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.adaugaPreparatToolStripMenuItem,
            this.editeazaPreparatMenu,
            this.afiseazaIngredienteMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(524, 24);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "menuStrip1";
            // 
            // editeazaPreparatMenu
            // 
            this.editeazaPreparatMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.actualizeazaPreparatToolStripMenuItem,
            this.stergePreparatToolStripMenuItem});
            this.editeazaPreparatMenu.Enabled = false;
            this.editeazaPreparatMenu.Name = "editeazaPreparatMenu";
            this.editeazaPreparatMenu.Size = new System.Drawing.Size(62, 20);
            this.editeazaPreparatMenu.Text = "Editeaza";
            // 
            // actualizeazaPreparatToolStripMenuItem
            // 
            this.actualizeazaPreparatToolStripMenuItem.Name = "actualizeazaPreparatToolStripMenuItem";
            this.actualizeazaPreparatToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.actualizeazaPreparatToolStripMenuItem.Text = "Actualizeaza preparat";
            this.actualizeazaPreparatToolStripMenuItem.Click += new System.EventHandler(this.ActualizeazaPreparat);
            // 
            // stergePreparatToolStripMenuItem
            // 
            this.stergePreparatToolStripMenuItem.Name = "stergePreparatToolStripMenuItem";
            this.stergePreparatToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.stergePreparatToolStripMenuItem.Text = "Sterge preparat";
            this.stergePreparatToolStripMenuItem.Click += new System.EventHandler(this.StergePreparat);
            // 
            // afiseazaIngredienteMenuItem
            // 
            this.afiseazaIngredienteMenuItem.Enabled = false;
            this.afiseazaIngredienteMenuItem.Name = "afiseazaIngredienteMenuItem";
            this.afiseazaIngredienteMenuItem.Size = new System.Drawing.Size(125, 20);
            this.afiseazaIngredienteMenuItem.Text = "Afiseaza ingrediente";
            this.afiseazaIngredienteMenuItem.Click += new System.EventHandler(this.AfiseazaIngrediente);
            // 
            // preparateListBox
            // 
            this.preparateListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.preparateListBox.FormattingEnabled = true;
            this.preparateListBox.Location = new System.Drawing.Point(12, 27);
            this.preparateListBox.Name = "preparateListBox";
            this.preparateListBox.Size = new System.Drawing.Size(186, 212);
            this.preparateListBox.TabIndex = 1;
            this.preparateListBox.SelectedIndexChanged += new System.EventHandler(this.PrepraratSelectat);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.retetaLabel);
            this.groupBox1.Location = new System.Drawing.Point(204, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(308, 212);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Reteta";
            // 
            // retetaLabel
            // 
            this.retetaLabel.Location = new System.Drawing.Point(6, 16);
            this.retetaLabel.Name = "retetaLabel";
            this.retetaLabel.Size = new System.Drawing.Size(296, 193);
            this.retetaLabel.TabIndex = 0;
            // 
            // adaugaPreparatToolStripMenuItem
            // 
            this.adaugaPreparatToolStripMenuItem.Name = "adaugaPreparatToolStripMenuItem";
            this.adaugaPreparatToolStripMenuItem.Size = new System.Drawing.Size(107, 20);
            this.adaugaPreparatToolStripMenuItem.Text = "Adauga preparat";
            this.adaugaPreparatToolStripMenuItem.Click += new System.EventHandler(this.AdaugaPreparat);
            // 
            // Preparate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 255);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.preparateListBox);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.Name = "Preparate";
            this.Text = "Preparate";
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem editeazaPreparatMenu;
        private System.Windows.Forms.ToolStripMenuItem actualizeazaPreparatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stergePreparatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem afiseazaIngredienteMenuItem;
        private System.Windows.Forms.ListBox preparateListBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label retetaLabel;
        private System.Windows.Forms.ToolStripMenuItem adaugaPreparatToolStripMenuItem;
    }
}