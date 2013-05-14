namespace ISSApp
{
    partial class AuthenticationView
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
            this.AuthenticateButton = new System.Windows.Forms.Button();
            this.codePasswordLabel = new System.Windows.Forms.Label();
            this.codePasswordTextbox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // AuthenticateButton
            // 
            this.AuthenticateButton.Location = new System.Drawing.Point(107, 34);
            this.AuthenticateButton.Name = "AuthenticateButton";
            this.AuthenticateButton.Size = new System.Drawing.Size(75, 23);
            this.AuthenticateButton.TabIndex = 0;
            this.AuthenticateButton.Text = "Authenticate button";
            this.AuthenticateButton.UseVisualStyleBackColor = true;
            this.AuthenticateButton.Click += new System.EventHandler(this.OnAuthenticate);
            // 
            // codePasswordLabel
            // 
            this.codePasswordLabel.AutoSize = true;
            this.codePasswordLabel.Location = new System.Drawing.Point(12, 9);
            this.codePasswordLabel.Name = "codePasswordLabel";
            this.codePasswordLabel.Size = new System.Drawing.Size(89, 13);
            this.codePasswordLabel.TabIndex = 1;
            this.codePasswordLabel.Text = "Code/Password: ";
            // 
            // codePasswordTextbox
            // 
            this.codePasswordTextbox.Location = new System.Drawing.Point(107, 6);
            this.codePasswordTextbox.Name = "codePasswordTextbox";
            this.codePasswordTextbox.PasswordChar = '*';
            this.codePasswordTextbox.Size = new System.Drawing.Size(165, 20);
            this.codePasswordTextbox.TabIndex = 3;
            // 
            // AuthenticationView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 69);
            this.Controls.Add(this.codePasswordTextbox);
            this.Controls.Add(this.codePasswordLabel);
            this.Controls.Add(this.AuthenticateButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "AuthenticationView";
            this.Text = "Authentication";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AuthenticateButton;
        private System.Windows.Forms.Label codePasswordLabel;
        private System.Windows.Forms.TextBox codePasswordTextbox;
    }
}