namespace GTBDDLab3
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
            this.label1 = new System.Windows.Forms.Label();
            this.isolationLevelComboBox = new System.Windows.Forms.ComboBox();
            this.incepeTranzactieButton = new System.Windows.Forms.Button();
            this.comiteTranzactieButton = new System.Windows.Forms.Button();
            this.anulareTranzactieButton = new System.Windows.Forms.Button();
            this.rezervariDataGridView = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.rezervariToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adaugaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rezervaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stergeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.afiseazaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.rezervariDataGridView)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 287);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nivel de izolare:";
            // 
            // isolationLevelComboBox
            // 
            this.isolationLevelComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.isolationLevelComboBox.FormattingEnabled = true;
            this.isolationLevelComboBox.Location = new System.Drawing.Point(100, 284);
            this.isolationLevelComboBox.Name = "isolationLevelComboBox";
            this.isolationLevelComboBox.Size = new System.Drawing.Size(273, 21);
            this.isolationLevelComboBox.TabIndex = 1;
            // 
            // incepeTranzactieButton
            // 
            this.incepeTranzactieButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.incepeTranzactieButton.Location = new System.Drawing.Point(379, 283);
            this.incepeTranzactieButton.Name = "incepeTranzactieButton";
            this.incepeTranzactieButton.Size = new System.Drawing.Size(75, 23);
            this.incepeTranzactieButton.TabIndex = 2;
            this.incepeTranzactieButton.Text = "Incepe";
            this.incepeTranzactieButton.UseVisualStyleBackColor = true;
            this.incepeTranzactieButton.Click += new System.EventHandler(this.incepeTranzactieButton_Click);
            // 
            // comiteTranzactieButton
            // 
            this.comiteTranzactieButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comiteTranzactieButton.Enabled = false;
            this.comiteTranzactieButton.Location = new System.Drawing.Point(460, 283);
            this.comiteTranzactieButton.Name = "comiteTranzactieButton";
            this.comiteTranzactieButton.Size = new System.Drawing.Size(75, 23);
            this.comiteTranzactieButton.TabIndex = 3;
            this.comiteTranzactieButton.Text = "Comite";
            this.comiteTranzactieButton.UseVisualStyleBackColor = true;
            this.comiteTranzactieButton.Click += new System.EventHandler(this.comiteTranzactieButton_Click);
            // 
            // anulareTranzactieButton
            // 
            this.anulareTranzactieButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.anulareTranzactieButton.Enabled = false;
            this.anulareTranzactieButton.Location = new System.Drawing.Point(541, 283);
            this.anulareTranzactieButton.Name = "anulareTranzactieButton";
            this.anulareTranzactieButton.Size = new System.Drawing.Size(75, 23);
            this.anulareTranzactieButton.TabIndex = 4;
            this.anulareTranzactieButton.Text = "Anuleaza";
            this.anulareTranzactieButton.UseVisualStyleBackColor = true;
            this.anulareTranzactieButton.Click += new System.EventHandler(this.anulareTranzactieButton_Click);
            // 
            // rezervariDataGridView
            // 
            this.rezervariDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rezervariDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.rezervariDataGridView.Location = new System.Drawing.Point(12, 27);
            this.rezervariDataGridView.Name = "rezervariDataGridView";
            this.rezervariDataGridView.ReadOnly = true;
            this.rezervariDataGridView.RowHeadersVisible = false;
            this.rezervariDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.rezervariDataGridView.Size = new System.Drawing.Size(604, 250);
            this.rezervariDataGridView.TabIndex = 7;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rezervariToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(628, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // rezervariToolStripMenuItem
            // 
            this.rezervariToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.adaugaToolStripMenuItem,
            this.rezervaToolStripMenuItem,
            this.stergeToolStripMenuItem,
            this.afiseazaToolStripMenuItem});
            this.rezervariToolStripMenuItem.Enabled = false;
            this.rezervariToolStripMenuItem.Name = "rezervariToolStripMenuItem";
            this.rezervariToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.rezervariToolStripMenuItem.Text = "Rezervari";
            // 
            // adaugaToolStripMenuItem
            // 
            this.adaugaToolStripMenuItem.Name = "adaugaToolStripMenuItem";
            this.adaugaToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.adaugaToolStripMenuItem.Text = "Adauga";
            this.adaugaToolStripMenuItem.Click += new System.EventHandler(this.updateRemoteDatabaseEvent);
            // 
            // rezervaToolStripMenuItem
            // 
            this.rezervaToolStripMenuItem.Name = "rezervaToolStripMenuItem";
            this.rezervaToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.rezervaToolStripMenuItem.Text = "Rezerva";
            this.rezervaToolStripMenuItem.Click += new System.EventHandler(this.updateRemoteDatabaseEvent);
            // 
            // stergeToolStripMenuItem
            // 
            this.stergeToolStripMenuItem.Name = "stergeToolStripMenuItem";
            this.stergeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.stergeToolStripMenuItem.Text = "Sterge";
            this.stergeToolStripMenuItem.Click += new System.EventHandler(this.updateRemoteDatabaseEvent);
            // 
            // afiseazaToolStripMenuItem
            // 
            this.afiseazaToolStripMenuItem.Name = "afiseazaToolStripMenuItem";
            this.afiseazaToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.afiseazaToolStripMenuItem.Text = "Afiseaza";
            this.afiseazaToolStripMenuItem.Click += new System.EventHandler(this.afiseazaToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 316);
            this.Controls.Add(this.rezervariDataGridView);
            this.Controls.Add(this.anulareTranzactieButton);
            this.Controls.Add(this.comiteTranzactieButton);
            this.Controls.Add(this.incepeTranzactieButton);
            this.Controls.Add(this.isolationLevelComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "GTBDDLab3";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.rezervariDataGridView)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox isolationLevelComboBox;
        private System.Windows.Forms.Button incepeTranzactieButton;
        private System.Windows.Forms.Button comiteTranzactieButton;
        private System.Windows.Forms.Button anulareTranzactieButton;
        private System.Windows.Forms.DataGridView rezervariDataGridView;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem adaugaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rezervaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stergeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem afiseazaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rezervariToolStripMenuItem;
    }
}

