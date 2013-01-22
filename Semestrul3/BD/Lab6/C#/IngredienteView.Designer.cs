namespace BDLab6
{
    partial class IngredienteView
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ingredienteNeadaugateListBox = new System.Windows.Forms.ListBox();
            this.ingredienteAdaugateListBox = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cantitateNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.ingredienteNeadaugateContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.adaugaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ingredienteAdaugateContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.stergeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.cantitateNumericUpDown)).BeginInit();
            this.ingredienteNeadaugateContextMenu.SuspendLayout();
            this.ingredienteAdaugateContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ingrediente nefolosite:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(138, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Ingrediente folosite:";
            // 
            // ingredienteNeadaugateListBox
            // 
            this.ingredienteNeadaugateListBox.ContextMenuStrip = this.ingredienteNeadaugateContextMenu;
            this.ingredienteNeadaugateListBox.FormattingEnabled = true;
            this.ingredienteNeadaugateListBox.Location = new System.Drawing.Point(12, 25);
            this.ingredienteNeadaugateListBox.Name = "ingredienteNeadaugateListBox";
            this.ingredienteNeadaugateListBox.Size = new System.Drawing.Size(120, 199);
            this.ingredienteNeadaugateListBox.TabIndex = 2;
            // 
            // ingredienteAdaugateListBox
            // 
            this.ingredienteAdaugateListBox.ContextMenuStrip = this.ingredienteAdaugateContextMenu;
            this.ingredienteAdaugateListBox.FormattingEnabled = true;
            this.ingredienteAdaugateListBox.Location = new System.Drawing.Point(138, 25);
            this.ingredienteAdaugateListBox.Name = "ingredienteAdaugateListBox";
            this.ingredienteAdaugateListBox.Size = new System.Drawing.Size(134, 160);
            this.ingredienteAdaugateListBox.TabIndex = 3;
            this.ingredienteAdaugateListBox.SelectedIndexChanged += new System.EventHandler(this.IngredientSelected);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(138, 188);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Cantitate";
            // 
            // cantitateNumericUpDown
            // 
            this.cantitateNumericUpDown.Location = new System.Drawing.Point(141, 204);
            this.cantitateNumericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.cantitateNumericUpDown.Name = "cantitateNumericUpDown";
            this.cantitateNumericUpDown.Size = new System.Drawing.Size(131, 20);
            this.cantitateNumericUpDown.TabIndex = 6;
            this.cantitateNumericUpDown.ValueChanged += new System.EventHandler(this.QuantityChanged);
            // 
            // ingredienteNeadaugateContextMenu
            // 
            this.ingredienteNeadaugateContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.adaugaToolStripMenuItem});
            this.ingredienteNeadaugateContextMenu.Name = "ingredienteNeadaugateContextMenu";
            this.ingredienteNeadaugateContextMenu.Size = new System.Drawing.Size(116, 26);
            // 
            // adaugaToolStripMenuItem
            // 
            this.adaugaToolStripMenuItem.Name = "adaugaToolStripMenuItem";
            this.adaugaToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.adaugaToolStripMenuItem.Text = "Adauga";
            this.adaugaToolStripMenuItem.Click += new System.EventHandler(this.AdaugaIngredient);
            // 
            // ingredienteAdaugateContextMenu
            // 
            this.ingredienteAdaugateContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stergeToolStripMenuItem});
            this.ingredienteAdaugateContextMenu.Name = "ingredienteAdaugateContextMenu";
            this.ingredienteAdaugateContextMenu.Size = new System.Drawing.Size(108, 26);
            // 
            // stergeToolStripMenuItem
            // 
            this.stergeToolStripMenuItem.Name = "stergeToolStripMenuItem";
            this.stergeToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.stergeToolStripMenuItem.Text = "Sterge";
            this.stergeToolStripMenuItem.Click += new System.EventHandler(this.StergeIngredient);
            // 
            // IngredienteView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 240);
            this.Controls.Add(this.cantitateNumericUpDown);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ingredienteAdaugateListBox);
            this.Controls.Add(this.ingredienteNeadaugateListBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "IngredienteView";
            this.Text = "IngredienteView";
            ((System.ComponentModel.ISupportInitialize)(this.cantitateNumericUpDown)).EndInit();
            this.ingredienteNeadaugateContextMenu.ResumeLayout(false);
            this.ingredienteAdaugateContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox ingredienteNeadaugateListBox;
        private System.Windows.Forms.ListBox ingredienteAdaugateListBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown cantitateNumericUpDown;
        private System.Windows.Forms.ContextMenuStrip ingredienteNeadaugateContextMenu;
        private System.Windows.Forms.ToolStripMenuItem adaugaToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip ingredienteAdaugateContextMenu;
        private System.Windows.Forms.ToolStripMenuItem stergeToolStripMenuItem;
    }
}