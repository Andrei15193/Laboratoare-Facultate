namespace IALab302
{
    partial class Form2
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
            this.questionLabel = new System.Windows.Forms.Label();
            this.factTextBox = new System.Windows.Forms.RichTextBox();
            this.positiveAnswerButton = new System.Windows.Forms.Button();
            this.negativeAnswerButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // questionLabel
            // 
            this.questionLabel.AutoSize = true;
            this.questionLabel.Location = new System.Drawing.Point(12, 9);
            this.questionLabel.Name = "questionLabel";
            this.questionLabel.Size = new System.Drawing.Size(146, 13);
            this.questionLabel.TabIndex = 0;
            this.questionLabel.Text = "Este urmatorul fapt adevarat?";
            // 
            // factTextBox
            // 
            this.factTextBox.Location = new System.Drawing.Point(15, 25);
            this.factTextBox.Name = "factTextBox";
            this.factTextBox.Size = new System.Drawing.Size(461, 96);
            this.factTextBox.TabIndex = 1;
            this.factTextBox.Text = "";
            // 
            // positiveAnswerButton
            // 
            this.positiveAnswerButton.Location = new System.Drawing.Point(15, 127);
            this.positiveAnswerButton.Name = "positiveAnswerButton";
            this.positiveAnswerButton.Size = new System.Drawing.Size(75, 23);
            this.positiveAnswerButton.TabIndex = 2;
            this.positiveAnswerButton.Text = "Da";
            this.positiveAnswerButton.UseVisualStyleBackColor = true;
            this.positiveAnswerButton.Click += new System.EventHandler(this.positiveAnswerButton_Click);
            // 
            // negativeAnswerButton
            // 
            this.negativeAnswerButton.Location = new System.Drawing.Point(401, 127);
            this.negativeAnswerButton.Name = "negativeAnswerButton";
            this.negativeAnswerButton.Size = new System.Drawing.Size(75, 23);
            this.negativeAnswerButton.TabIndex = 3;
            this.negativeAnswerButton.Text = "Nu";
            this.negativeAnswerButton.UseVisualStyleBackColor = true;
            this.negativeAnswerButton.Click += new System.EventHandler(this.negativeAnswerButton_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 162);
            this.Controls.Add(this.negativeAnswerButton);
            this.Controls.Add(this.positiveAnswerButton);
            this.Controls.Add(this.factTextBox);
            this.Controls.Add(this.questionLabel);
            this.Name = "Form2";
            this.Text = "Confirmare fapt";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label questionLabel;
        private System.Windows.Forms.RichTextBox factTextBox;
        private System.Windows.Forms.Button positiveAnswerButton;
        private System.Windows.Forms.Button negativeAnswerButton;
    }
}