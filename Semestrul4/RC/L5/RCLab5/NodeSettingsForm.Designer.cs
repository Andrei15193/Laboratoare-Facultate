namespace RCLab5
{
    partial class NodeSettingsForm
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
            this.okButton = new System.Windows.Forms.Button();
            this.networkClassLabel = new System.Windows.Forms.Label();
            this.nodeAddressLabel = new System.Windows.Forms.Label();
            this.nodeAddressTextBox = new System.Windows.Forms.TextBox();
            this.netowrkClassTextBox = new System.Windows.Forms.TextBox();
            this.nodeAddressPanel = new System.Windows.Forms.Panel();
            this.networkClassPanel = new System.Windows.Forms.Panel();
            this.nodeAddressPanel.SuspendLayout();
            this.networkClassPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Enabled = false;
            this.okButton.Location = new System.Drawing.Point(96, 64);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // networkClassLabel
            // 
            this.networkClassLabel.AutoSize = true;
            this.networkClassLabel.Location = new System.Drawing.Point(12, 15);
            this.networkClassLabel.Name = "networkClassLabel";
            this.networkClassLabel.Size = new System.Drawing.Size(78, 13);
            this.networkClassLabel.TabIndex = 3;
            this.networkClassLabel.Text = "Clasa de retea:";
            // 
            // nodeAddressLabel
            // 
            this.nodeAddressLabel.AutoSize = true;
            this.nodeAddressLabel.Location = new System.Drawing.Point(12, 41);
            this.nodeAddressLabel.Name = "nodeAddressLabel";
            this.nodeAddressLabel.Size = new System.Drawing.Size(64, 13);
            this.nodeAddressLabel.TabIndex = 4;
            this.nodeAddressLabel.Text = "Adresa nod:";
            // 
            // nodeAddressTextBox
            // 
            this.nodeAddressTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nodeAddressTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.nodeAddressTextBox.Location = new System.Drawing.Point(3, 2);
            this.nodeAddressTextBox.Name = "nodeAddressTextBox";
            this.nodeAddressTextBox.Size = new System.Drawing.Size(120, 13);
            this.nodeAddressTextBox.TabIndex = 2;
            // 
            // netowrkClassTextBox
            // 
            this.netowrkClassTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.netowrkClassTextBox.Location = new System.Drawing.Point(3, 2);
            this.netowrkClassTextBox.Name = "netowrkClassTextBox";
            this.netowrkClassTextBox.Size = new System.Drawing.Size(120, 13);
            this.netowrkClassTextBox.TabIndex = 1;
            // 
            // nodeAddressPanel
            // 
            this.nodeAddressPanel.Controls.Add(this.nodeAddressTextBox);
            this.nodeAddressPanel.Location = new System.Drawing.Point(97, 39);
            this.nodeAddressPanel.Name = "nodeAddressPanel";
            this.nodeAddressPanel.Size = new System.Drawing.Size(128, 18);
            this.nodeAddressPanel.TabIndex = 6;
            // 
            // networkClassPanel
            // 
            this.networkClassPanel.Controls.Add(this.netowrkClassTextBox);
            this.networkClassPanel.Location = new System.Drawing.Point(97, 13);
            this.networkClassPanel.Name = "networkClassPanel";
            this.networkClassPanel.Size = new System.Drawing.Size(128, 18);
            this.networkClassPanel.TabIndex = 5;
            // 
            // NodeSettingsForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ClientSize = new System.Drawing.Size(241, 96);
            this.Controls.Add(this.networkClassPanel);
            this.Controls.Add(this.nodeAddressPanel);
            this.Controls.Add(this.nodeAddressLabel);
            this.Controls.Add(this.networkClassLabel);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "NodeSettingsForm";
            this.Text = "Setari nod";
            this.nodeAddressPanel.ResumeLayout(false);
            this.nodeAddressPanel.PerformLayout();
            this.networkClassPanel.ResumeLayout(false);
            this.networkClassPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label networkClassLabel;
        private System.Windows.Forms.Label nodeAddressLabel;
        private System.Windows.Forms.TextBox nodeAddressTextBox;
        private System.Windows.Forms.TextBox netowrkClassTextBox;
        private System.Windows.Forms.Panel nodeAddressPanel;
        private System.Windows.Forms.Panel networkClassPanel;
    }
}