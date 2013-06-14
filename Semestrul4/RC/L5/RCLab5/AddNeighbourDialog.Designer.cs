namespace RCLab5
{
    partial class AddNeighbourDialog
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
            this.neighbourLabel = new System.Windows.Forms.Label();
            this.neighbourTextBox = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // neighbourLabel
            // 
            this.neighbourLabel.AutoSize = true;
            this.neighbourLabel.Location = new System.Drawing.Point(12, 9);
            this.neighbourLabel.Name = "neighbourLabel";
            this.neighbourLabel.Size = new System.Drawing.Size(59, 13);
            this.neighbourLabel.TabIndex = 0;
            this.neighbourLabel.Text = "Neighbour:";
            // 
            // neighbourTextBox
            // 
            this.neighbourTextBox.Location = new System.Drawing.Point(77, 6);
            this.neighbourTextBox.Name = "neighbourTextBox";
            this.neighbourTextBox.Size = new System.Drawing.Size(127, 20);
            this.neighbourTextBox.TabIndex = 1;
            this.neighbourTextBox.TextChanged += new System.EventHandler(this.neightbourNameChanged);
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Enabled = false;
            this.okButton.Location = new System.Drawing.Point(77, 32);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "Add";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // AddNeighbourDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(216, 65);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.neighbourTextBox);
            this.Controls.Add(this.neighbourLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AddNeighbourDialog";
            this.Text = "Add Neighbour";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label neighbourLabel;
        private System.Windows.Forms.TextBox neighbourTextBox;
        private System.Windows.Forms.Button okButton;
    }
}