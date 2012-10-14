namespace CSLabBD
{
    partial class NewActorDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewActorDialog));
            this.LabelMessage = new System.Windows.Forms.Label();
            this.LabelActorName = new System.Windows.Forms.Label();
            this.TextBoxActorName = new System.Windows.Forms.TextBox();
            this.ButtonAdd = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LabelMessage
            // 
            this.LabelMessage.Location = new System.Drawing.Point(12, 12);
            this.LabelMessage.Name = "LabelMessage";
            this.LabelMessage.Size = new System.Drawing.Size(249, 14);
            this.LabelMessage.TabIndex = 0;
            this.LabelMessage.Text = "Please enter the name of the actor you wish to add";
            // 
            // LabelActorName
            // 
            this.LabelActorName.AutoSize = true;
            this.LabelActorName.Location = new System.Drawing.Point(12, 32);
            this.LabelActorName.Name = "LabelActorName";
            this.LabelActorName.Size = new System.Drawing.Size(64, 13);
            this.LabelActorName.TabIndex = 1;
            this.LabelActorName.Text = "Actor name:";
            // 
            // TextBoxActorName
            // 
            this.TextBoxActorName.Location = new System.Drawing.Point(82, 29);
            this.TextBoxActorName.Name = "TextBoxActorName";
            this.TextBoxActorName.Size = new System.Drawing.Size(179, 20);
            this.TextBoxActorName.TabIndex = 2;
            // 
            // ButtonAdd
            // 
            this.ButtonAdd.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ButtonAdd.Location = new System.Drawing.Point(15, 67);
            this.ButtonAdd.Name = "ButtonAdd";
            this.ButtonAdd.Size = new System.Drawing.Size(75, 23);
            this.ButtonAdd.TabIndex = 3;
            this.ButtonAdd.Text = "Add actor";
            this.ButtonAdd.UseVisualStyleBackColor = true;
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonCancel.Location = new System.Drawing.Point(186, 67);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.ButtonCancel.TabIndex = 4;
            this.ButtonCancel.Text = "Cancel";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            // 
            // NewActorDialog
            // 
            this.AcceptButton = this.ButtonAdd;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(275, 105);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonAdd);
            this.Controls.Add(this.TextBoxActorName);
            this.Controls.Add(this.LabelActorName);
            this.Controls.Add(this.LabelMessage);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NewActorDialog";
            this.Text = "Add Actor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabelMessage;
        private System.Windows.Forms.Label LabelActorName;
        private System.Windows.Forms.TextBox TextBoxActorName;
        private System.Windows.Forms.Button ButtonAdd;
        private System.Windows.Forms.Button ButtonCancel;
    }
}