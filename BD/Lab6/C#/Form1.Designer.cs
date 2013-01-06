namespace BDLab6
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
            this.categoriiListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.afiseazaPreparate = new System.Windows.Forms.ToolStripMenuItem();
            this.ingredienteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // categoriiListBox
            // 
            this.categoriiListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.categoriiListBox.FormattingEnabled = true;
            this.categoriiListBox.Location = new System.Drawing.Point(15, 40);
            this.categoriiListBox.Name = "categoriiListBox";
            this.categoriiListBox.Size = new System.Drawing.Size(257, 212);
            this.categoriiListBox.TabIndex = 0;
            this.categoriiListBox.SelectedIndexChanged += new System.EventHandler(this.CategorieSelectata);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Categorii";
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.afiseazaPreparate,
            this.ingredienteToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(284, 24);
            this.mainMenu.TabIndex = 2;
            this.mainMenu.Text = "menuStrip1";
            // 
            // afiseazaPreparate
            // 
            this.afiseazaPreparate.Enabled = false;
            this.afiseazaPreparate.Name = "afiseazaPreparate";
            this.afiseazaPreparate.Size = new System.Drawing.Size(115, 20);
            this.afiseazaPreparate.Text = "Afiseaza preparate";
            this.afiseazaPreparate.Click += new System.EventHandler(this.AfiseazaPreparate);
            // 
            // ingredienteToolStripMenuItem
            // 
            this.ingredienteToolStripMenuItem.Name = "ingredienteToolStripMenuItem";
            this.ingredienteToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.ingredienteToolStripMenuItem.Text = "Ingrediente";
            this.ingredienteToolStripMenuItem.Click += new System.EventHandler(this.DeschideEditorIngrediente);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.categoriiListBox);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Retete culinare";
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox categoriiListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem afiseazaPreparate;
        private System.Windows.Forms.ToolStripMenuItem ingredienteToolStripMenuItem;
    }
}

