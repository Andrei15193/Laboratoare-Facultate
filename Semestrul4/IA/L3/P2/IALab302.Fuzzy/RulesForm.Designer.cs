namespace IALab302.Fuzzy
{
    partial class RulesForm
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
            this._rulesDataGridView = new System.Windows.Forms.DataGridView();
            this._okButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._rulesDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // _rulesDataGridView
            // 
            this._rulesDataGridView.AllowUserToAddRows = false;
            this._rulesDataGridView.AllowUserToDeleteRows = false;
            this._rulesDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._rulesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._rulesDataGridView.Location = new System.Drawing.Point(12, 12);
            this._rulesDataGridView.MultiSelect = false;
            this._rulesDataGridView.Name = "_rulesDataGridView";
            this._rulesDataGridView.ReadOnly = true;
            this._rulesDataGridView.RowHeadersVisible = false;
            this._rulesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._rulesDataGridView.Size = new System.Drawing.Size(361, 324);
            this._rulesDataGridView.TabIndex = 0;
            // 
            // _okButton
            // 
            this._okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._okButton.Location = new System.Drawing.Point(298, 342);
            this._okButton.Name = "_okButton";
            this._okButton.Size = new System.Drawing.Size(75, 23);
            this._okButton.TabIndex = 1;
            this._okButton.Text = "Ok";
            this._okButton.UseVisualStyleBackColor = true;
            // 
            // RulesForm
            // 
            this.AcceptButton = this._okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 377);
            this.Controls.Add(this._okButton);
            this.Controls.Add(this._rulesDataGridView);
            this.Name = "RulesForm";
            this.Text = "RulesForm";
            ((System.ComponentModel.ISupportInitialize)(this._rulesDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView _rulesDataGridView;
        private System.Windows.Forms.Button _okButton;
    }
}