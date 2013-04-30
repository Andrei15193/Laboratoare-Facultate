namespace UDPMessenger.UserInterface
{
    partial class BroadcastForm
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
            this._messageTextBox = new System.Windows.Forms.TextBox();
            this._sendButton = new System.Windows.Forms.Button();
            this._messagesRichTextBox = new System.Windows.Forms.RichTextBox();
            this._onlineUsersListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _messageTextBox
            // 
            this._messageTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._messageTextBox.Location = new System.Drawing.Point(12, 259);
            this._messageTextBox.Name = "_messageTextBox";
            this._messageTextBox.Size = new System.Drawing.Size(244, 20);
            this._messageTextBox.TabIndex = 0;
            this._messageTextBox.TextChanged += new System.EventHandler(this._messageTextChanged);
            // 
            // _sendButton
            // 
            this._sendButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._sendButton.Enabled = false;
            this._sendButton.Location = new System.Drawing.Point(262, 257);
            this._sendButton.Name = "_sendButton";
            this._sendButton.Size = new System.Drawing.Size(75, 23);
            this._sendButton.TabIndex = 1;
            this._sendButton.Text = "Send";
            this._sendButton.UseVisualStyleBackColor = true;
            this._sendButton.Click += new System.EventHandler(this._SendMessage);
            // 
            // _messagesRichTextBox
            // 
            this._messagesRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._messagesRichTextBox.BackColor = System.Drawing.SystemColors.Window;
            this._messagesRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._messagesRichTextBox.Location = new System.Drawing.Point(12, 28);
            this._messagesRichTextBox.Name = "_messagesRichTextBox";
            this._messagesRichTextBox.ReadOnly = true;
            this._messagesRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this._messagesRichTextBox.Size = new System.Drawing.Size(325, 225);
            this._messagesRichTextBox.TabIndex = 2;
            this._messagesRichTextBox.Text = "";
            // 
            // _onlineUsersListBox
            // 
            this._onlineUsersListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._onlineUsersListBox.FormattingEnabled = true;
            this._onlineUsersListBox.Location = new System.Drawing.Point(343, 28);
            this._onlineUsersListBox.Name = "_onlineUsersListBox";
            this._onlineUsersListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this._onlineUsersListBox.Size = new System.Drawing.Size(136, 251);
            this._onlineUsersListBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(343, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Online users:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Messages";
            // 
            // BroadcastForm
            // 
            this.AcceptButton = this._sendButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 289);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._onlineUsersListBox);
            this.Controls.Add(this._messagesRichTextBox);
            this.Controls.Add(this._sendButton);
            this.Controls.Add(this._messageTextBox);
            this.Name = "BroadcastForm";
            this.Text = "BroadcastForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _messageTextBox;
        private System.Windows.Forms.Button _sendButton;
        private System.Windows.Forms.RichTextBox _messagesRichTextBox;
        private System.Windows.Forms.ListBox _onlineUsersListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}