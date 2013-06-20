namespace GTBDDLab4
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
            this.marksDataGridView = new System.Windows.Forms.DataGridView();
            this.startTransactionButton = new System.Windows.Forms.Button();
            this.commitTransactionButton = new System.Windows.Forms.Button();
            this.rollbackTransactionButton = new System.Windows.Forms.Button();
            this.transactionTypeLabel = new System.Windows.Forms.Label();
            this.transactionTypeComboBox = new System.Windows.Forms.ComboBox();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.noteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.afiseazaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adaugaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actualizeazaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stergeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.marksDataGridView)).BeginInit();
            this.mainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // marksDataGridView
            // 
            this.marksDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.marksDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.marksDataGridView.Location = new System.Drawing.Point(12, 27);
            this.marksDataGridView.Name = "marksDataGridView";
            this.marksDataGridView.RowHeadersVisible = false;
            this.marksDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.marksDataGridView.Size = new System.Drawing.Size(510, 205);
            this.marksDataGridView.TabIndex = 0;
            // 
            // startTransactionButton
            // 
            this.startTransactionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.startTransactionButton.Enabled = false;
            this.startTransactionButton.Location = new System.Drawing.Point(285, 238);
            this.startTransactionButton.Name = "startTransactionButton";
            this.startTransactionButton.Size = new System.Drawing.Size(75, 23);
            this.startTransactionButton.TabIndex = 2;
            this.startTransactionButton.Text = "Incepe";
            this.startTransactionButton.UseVisualStyleBackColor = true;
            this.startTransactionButton.Click += new System.EventHandler(this.startTransactionButton_Click);
            // 
            // commitTransactionButton
            // 
            this.commitTransactionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.commitTransactionButton.Enabled = false;
            this.commitTransactionButton.Location = new System.Drawing.Point(366, 238);
            this.commitTransactionButton.Name = "commitTransactionButton";
            this.commitTransactionButton.Size = new System.Drawing.Size(75, 23);
            this.commitTransactionButton.TabIndex = 3;
            this.commitTransactionButton.Text = "Comite";
            this.commitTransactionButton.UseVisualStyleBackColor = true;
            this.commitTransactionButton.Click += new System.EventHandler(this.commitTransaction_Click);
            // 
            // rollbackTransactionButton
            // 
            this.rollbackTransactionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.rollbackTransactionButton.Enabled = false;
            this.rollbackTransactionButton.Location = new System.Drawing.Point(447, 238);
            this.rollbackTransactionButton.Name = "rollbackTransactionButton";
            this.rollbackTransactionButton.Size = new System.Drawing.Size(75, 23);
            this.rollbackTransactionButton.TabIndex = 4;
            this.rollbackTransactionButton.Text = "Anuleaza";
            this.rollbackTransactionButton.UseVisualStyleBackColor = true;
            this.rollbackTransactionButton.Click += new System.EventHandler(this.rollbackTransaction_Click);
            // 
            // transactionTypeLabel
            // 
            this.transactionTypeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.transactionTypeLabel.AutoSize = true;
            this.transactionTypeLabel.Location = new System.Drawing.Point(9, 243);
            this.transactionTypeLabel.Name = "transactionTypeLabel";
            this.transactionTypeLabel.Size = new System.Drawing.Size(84, 13);
            this.transactionTypeLabel.TabIndex = 5;
            this.transactionTypeLabel.Text = "Tipul tranzactiei:";
            // 
            // transactionTypeComboBox
            // 
            this.transactionTypeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.transactionTypeComboBox.FormattingEnabled = true;
            this.transactionTypeComboBox.Items.AddRange(new object[] {
            "Optimist",
            "Pesimist"});
            this.transactionTypeComboBox.Location = new System.Drawing.Point(99, 240);
            this.transactionTypeComboBox.Name = "transactionTypeComboBox";
            this.transactionTypeComboBox.Size = new System.Drawing.Size(180, 21);
            this.transactionTypeComboBox.TabIndex = 6;
            this.transactionTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.transactionTypeComboBox_SelectedIndexChanged);
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.noteToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(534, 24);
            this.mainMenuStrip.TabIndex = 7;
            this.mainMenuStrip.Text = "Main Menu";
            // 
            // noteToolStripMenuItem
            // 
            this.noteToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.afiseazaToolStripMenuItem,
            this.adaugaToolStripMenuItem,
            this.actualizeazaToolStripMenuItem,
            this.stergeToolStripMenuItem});
            this.noteToolStripMenuItem.Enabled = false;
            this.noteToolStripMenuItem.Name = "noteToolStripMenuItem";
            this.noteToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.noteToolStripMenuItem.Text = "Note";
            // 
            // afiseazaToolStripMenuItem
            // 
            this.afiseazaToolStripMenuItem.Name = "afiseazaToolStripMenuItem";
            this.afiseazaToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.afiseazaToolStripMenuItem.Text = "Afiseaza";
            this.afiseazaToolStripMenuItem.Click += new System.EventHandler(this.afiseazaToolStripMenuItem_Click);
            // 
            // adaugaToolStripMenuItem
            // 
            this.adaugaToolStripMenuItem.Name = "adaugaToolStripMenuItem";
            this.adaugaToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.adaugaToolStripMenuItem.Text = "Adauga";
            this.adaugaToolStripMenuItem.Click += new System.EventHandler(this.adaugaToolStripMenuItem_Click);
            // 
            // actualizeazaToolStripMenuItem
            // 
            this.actualizeazaToolStripMenuItem.Name = "actualizeazaToolStripMenuItem";
            this.actualizeazaToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.actualizeazaToolStripMenuItem.Text = "Actualizeaza";
            this.actualizeazaToolStripMenuItem.Click += new System.EventHandler(this.actualizeazaToolStripMenuItem_Click);
            // 
            // stergeToolStripMenuItem
            // 
            this.stergeToolStripMenuItem.Name = "stergeToolStripMenuItem";
            this.stergeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.stergeToolStripMenuItem.Text = "Sterge";
            this.stergeToolStripMenuItem.Click += new System.EventHandler(this.stergeToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 273);
            this.Controls.Add(this.transactionTypeComboBox);
            this.Controls.Add(this.transactionTypeLabel);
            this.Controls.Add(this.rollbackTransactionButton);
            this.Controls.Add(this.commitTransactionButton);
            this.Controls.Add(this.startTransactionButton);
            this.Controls.Add(this.marksDataGridView);
            this.Controls.Add(this.mainMenuStrip);
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "Form1";
            this.Text = "GTBDDLab4";
            ((System.ComponentModel.ISupportInitialize)(this.marksDataGridView)).EndInit();
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView marksDataGridView;
        private System.Windows.Forms.Button startTransactionButton;
        private System.Windows.Forms.Button commitTransactionButton;
        private System.Windows.Forms.Button rollbackTransactionButton;
        private System.Windows.Forms.Label transactionTypeLabel;
        private System.Windows.Forms.ComboBox transactionTypeComboBox;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem noteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem afiseazaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem adaugaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem actualizeazaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stergeToolStripMenuItem;
    }
}

