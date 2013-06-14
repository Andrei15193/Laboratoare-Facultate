namespace RCLab5
{
    partial class MainForm
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
            this.neighbourListBox = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.adaugaVecinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.routingTableDataGridView = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.routingTableDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(315, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Vecini:";
            // 
            // neighbourListBox
            // 
            this.neighbourListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.neighbourListBox.ContextMenuStrip = this.contextMenuStrip1;
            this.neighbourListBox.FormattingEnabled = true;
            this.neighbourListBox.Location = new System.Drawing.Point(318, 25);
            this.neighbourListBox.Name = "neighbourListBox";
            this.neighbourListBox.Size = new System.Drawing.Size(138, 329);
            this.neighbourListBox.TabIndex = 1;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.adaugaVecinToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.ShowImageMargin = false;
            this.contextMenuStrip1.Size = new System.Drawing.Size(122, 26);
            // 
            // adaugaVecinToolStripMenuItem
            // 
            this.adaugaVecinToolStripMenuItem.Name = "adaugaVecinToolStripMenuItem";
            this.adaugaVecinToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.adaugaVecinToolStripMenuItem.Text = "Adauga vecin";
            this.adaugaVecinToolStripMenuItem.Click += new System.EventHandler(this.adaugaVecinToolStripMenuItem_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tabela de dirijare";
            // 
            // routingTableDataGridView
            // 
            this.routingTableDataGridView.AllowUserToAddRows = false;
            this.routingTableDataGridView.AllowUserToDeleteRows = false;
            this.routingTableDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.routingTableDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.routingTableDataGridView.Location = new System.Drawing.Point(15, 25);
            this.routingTableDataGridView.MultiSelect = false;
            this.routingTableDataGridView.Name = "routingTableDataGridView";
            this.routingTableDataGridView.ReadOnly = true;
            this.routingTableDataGridView.RowHeadersVisible = false;
            this.routingTableDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.routingTableDataGridView.Size = new System.Drawing.Size(297, 329);
            this.routingTableDataGridView.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 366);
            this.Controls.Add(this.routingTableDataGridView);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.neighbourListBox);
            this.Controls.Add(this.label1);
            this.Name = "MainForm";
            this.Text = "RCLab5";
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.routingTableDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox neighbourListBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView routingTableDataGridView;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem adaugaVecinToolStripMenuItem;
    }
}

