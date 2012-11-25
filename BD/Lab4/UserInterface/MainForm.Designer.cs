namespace BDLab4.UserInterface
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
            this.studentsListBox = new System.Windows.Forms.ListBox();
            this.studentGroupsListBox = new System.Windows.Forms.ListBox();
            this.sectionsComboBox = new System.Windows.Forms.ComboBox();
            this.addStudentButton = new System.Windows.Forms.Button();
            this.updateStudentButton = new System.Windows.Forms.Button();
            this.deleteStudentButton = new System.Windows.Forms.Button();
            this.clearStudentFieldsButton = new System.Windows.Forms.Button();
            this.displayStudentsPannel = new System.Windows.Forms.Panel();
            this.studentGroupLabel = new System.Windows.Forms.Label();
            this.studentNameLabel = new System.Windows.Forms.Label();
            this.orderByGroupBox = new System.Windows.Forms.GroupBox();
            this.orderBySense = new System.Windows.Forms.GroupBox();
            this.orderDescendingRadioButton = new System.Windows.Forms.RadioButton();
            this.orderAscendingRadioButton = new System.Windows.Forms.RadioButton();
            this.orderByField = new System.Windows.Forms.GroupBox();
            this.orderByGroupRadioButton = new System.Windows.Forms.RadioButton();
            this.orderByNameRadioButton = new System.Windows.Forms.RadioButton();
            this.studentData = new System.Windows.Forms.SplitContainer();
            this.sectionsLabel = new System.Windows.Forms.Label();
            this.studentDataDateOfBirthLabel = new System.Windows.Forms.Label();
            this.studentDataGroupLabel = new System.Windows.Forms.Label();
            this.studentDataSerialNumberLabel = new System.Windows.Forms.Label();
            this.studentDataNameLabel = new System.Windows.Forms.Label();
            this.studentDataDateOfBirthDateTime = new System.Windows.Forms.DateTimePicker();
            this.studentDataGroupTextBox = new System.Windows.Forms.TextBox();
            this.studentDataSerialNumberTextBox = new System.Windows.Forms.TextBox();
            this.studentDataNameTextBox = new System.Windows.Forms.TextBox();
            this.displayStudentsPannel.SuspendLayout();
            this.orderByGroupBox.SuspendLayout();
            this.orderBySense.SuspendLayout();
            this.orderByField.SuspendLayout();
            this.studentData.Panel1.SuspendLayout();
            this.studentData.Panel2.SuspendLayout();
            this.studentData.SuspendLayout();
            this.SuspendLayout();
            // 
            // studentsListBox
            // 
            this.studentsListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.studentsListBox.FormattingEnabled = true;
            this.studentsListBox.Location = new System.Drawing.Point(0, 14);
            this.studentsListBox.Name = "studentsListBox";
            this.studentsListBox.Size = new System.Drawing.Size(156, 197);
            this.studentsListBox.TabIndex = 0;
            this.studentsListBox.Click += new System.EventHandler(this.ClickedStudentNameInListBox);
            // 
            // studentGroupsListBox
            // 
            this.studentGroupsListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.studentGroupsListBox.FormattingEnabled = true;
            this.studentGroupsListBox.Location = new System.Drawing.Point(155, 14);
            this.studentGroupsListBox.Name = "studentGroupsListBox";
            this.studentGroupsListBox.Size = new System.Drawing.Size(75, 197);
            this.studentGroupsListBox.TabIndex = 1;
            this.studentGroupsListBox.Click += new System.EventHandler(this.ClickedStudentGroupInListBox);
            // 
            // sectionsComboBox
            // 
            this.sectionsComboBox.FormattingEnabled = true;
            this.sectionsComboBox.Location = new System.Drawing.Point(-1, 0);
            this.sectionsComboBox.Name = "sectionsComboBox";
            this.sectionsComboBox.Size = new System.Drawing.Size(146, 21);
            this.sectionsComboBox.TabIndex = 2;
            this.sectionsComboBox.SelectedIndexChanged += new System.EventHandler(this.SectionComboBoxIndexChanged);
            // 
            // addStudentButton
            // 
            this.addStudentButton.Enabled = false;
            this.addStudentButton.Location = new System.Drawing.Point(247, 171);
            this.addStudentButton.Name = "addStudentButton";
            this.addStudentButton.Size = new System.Drawing.Size(112, 23);
            this.addStudentButton.TabIndex = 3;
            this.addStudentButton.Text = "Adauga student";
            this.addStudentButton.UseVisualStyleBackColor = true;
            this.addStudentButton.Click += new System.EventHandler(this.AddStudent);
            // 
            // updateStudentButton
            // 
            this.updateStudentButton.Enabled = false;
            this.updateStudentButton.Location = new System.Drawing.Point(366, 171);
            this.updateStudentButton.Name = "updateStudentButton";
            this.updateStudentButton.Size = new System.Drawing.Size(112, 23);
            this.updateStudentButton.TabIndex = 4;
            this.updateStudentButton.Text = "Modifica student";
            this.updateStudentButton.UseVisualStyleBackColor = true;
            this.updateStudentButton.Click += new System.EventHandler(this.UpdateStudent);
            // 
            // deleteStudentButton
            // 
            this.deleteStudentButton.Enabled = false;
            this.deleteStudentButton.Location = new System.Drawing.Point(247, 200);
            this.deleteStudentButton.Name = "deleteStudentButton";
            this.deleteStudentButton.Size = new System.Drawing.Size(112, 23);
            this.deleteStudentButton.TabIndex = 5;
            this.deleteStudentButton.Text = "Sterge student";
            this.deleteStudentButton.UseVisualStyleBackColor = true;
            this.deleteStudentButton.Click += new System.EventHandler(this.DeleteStudent);
            // 
            // clearStudentFieldsButton
            // 
            this.clearStudentFieldsButton.Enabled = false;
            this.clearStudentFieldsButton.Location = new System.Drawing.Point(366, 200);
            this.clearStudentFieldsButton.Name = "clearStudentFieldsButton";
            this.clearStudentFieldsButton.Size = new System.Drawing.Size(112, 23);
            this.clearStudentFieldsButton.TabIndex = 6;
            this.clearStudentFieldsButton.Text = "Curata campuri";
            this.clearStudentFieldsButton.UseVisualStyleBackColor = true;
            this.clearStudentFieldsButton.Click += new System.EventHandler(this.ClearFields);
            // 
            // displayStudentsPannel
            // 
            this.displayStudentsPannel.Controls.Add(this.studentGroupLabel);
            this.displayStudentsPannel.Controls.Add(this.studentNameLabel);
            this.displayStudentsPannel.Controls.Add(this.studentsListBox);
            this.displayStudentsPannel.Controls.Add(this.studentGroupsListBox);
            this.displayStudentsPannel.Location = new System.Drawing.Point(12, 12);
            this.displayStudentsPannel.Name = "displayStudentsPannel";
            this.displayStudentsPannel.Size = new System.Drawing.Size(230, 211);
            this.displayStudentsPannel.TabIndex = 7;
            // 
            // studentGroupLabel
            // 
            this.studentGroupLabel.BackColor = System.Drawing.SystemColors.Control;
            this.studentGroupLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.studentGroupLabel.Location = new System.Drawing.Point(155, 0);
            this.studentGroupLabel.Name = "studentGroupLabel";
            this.studentGroupLabel.Size = new System.Drawing.Size(75, 15);
            this.studentGroupLabel.TabIndex = 8;
            this.studentGroupLabel.Text = "Grupa";
            // 
            // studentNameLabel
            // 
            this.studentNameLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.studentNameLabel.Location = new System.Drawing.Point(0, 0);
            this.studentNameLabel.Name = "studentNameLabel";
            this.studentNameLabel.Size = new System.Drawing.Size(156, 15);
            this.studentNameLabel.TabIndex = 2;
            this.studentNameLabel.Text = "Nume student";
            // 
            // orderByGroupBox
            // 
            this.orderByGroupBox.Controls.Add(this.orderBySense);
            this.orderByGroupBox.Controls.Add(this.orderByField);
            this.orderByGroupBox.Location = new System.Drawing.Point(12, 230);
            this.orderByGroupBox.Name = "orderByGroupBox";
            this.orderByGroupBox.Size = new System.Drawing.Size(466, 100);
            this.orderByGroupBox.TabIndex = 8;
            this.orderByGroupBox.TabStop = false;
            this.orderByGroupBox.Text = "Ordonarea inregistrarilor";
            // 
            // orderBySense
            // 
            this.orderBySense.Controls.Add(this.orderDescendingRadioButton);
            this.orderBySense.Controls.Add(this.orderAscendingRadioButton);
            this.orderBySense.Location = new System.Drawing.Point(236, 19);
            this.orderBySense.Name = "orderBySense";
            this.orderBySense.Size = new System.Drawing.Size(224, 70);
            this.orderBySense.TabIndex = 1;
            this.orderBySense.TabStop = false;
            this.orderBySense.Text = "Sensul ordonarii";
            // 
            // orderDescendingRadioButton
            // 
            this.orderDescendingRadioButton.AutoSize = true;
            this.orderDescendingRadioButton.Location = new System.Drawing.Point(6, 44);
            this.orderDescendingRadioButton.Name = "orderDescendingRadioButton";
            this.orderDescendingRadioButton.Size = new System.Drawing.Size(88, 17);
            this.orderDescendingRadioButton.TabIndex = 1;
            this.orderDescendingRadioButton.Text = "Descrescator";
            this.orderDescendingRadioButton.UseVisualStyleBackColor = true;
            this.orderDescendingRadioButton.CheckedChanged += new System.EventHandler(this.CheckedOrderDescending);
            // 
            // orderAscendingRadioButton
            // 
            this.orderAscendingRadioButton.AutoSize = true;
            this.orderAscendingRadioButton.Checked = true;
            this.orderAscendingRadioButton.Location = new System.Drawing.Point(7, 20);
            this.orderAscendingRadioButton.Name = "orderAscendingRadioButton";
            this.orderAscendingRadioButton.Size = new System.Drawing.Size(70, 17);
            this.orderAscendingRadioButton.TabIndex = 0;
            this.orderAscendingRadioButton.TabStop = true;
            this.orderAscendingRadioButton.Text = "Crescator";
            this.orderAscendingRadioButton.UseVisualStyleBackColor = true;
            this.orderAscendingRadioButton.CheckedChanged += new System.EventHandler(this.CheckedOrderAscending);
            // 
            // orderByField
            // 
            this.orderByField.Controls.Add(this.orderByGroupRadioButton);
            this.orderByField.Controls.Add(this.orderByNameRadioButton);
            this.orderByField.Location = new System.Drawing.Point(6, 19);
            this.orderByField.Name = "orderByField";
            this.orderByField.Size = new System.Drawing.Size(224, 70);
            this.orderByField.TabIndex = 0;
            this.orderByField.TabStop = false;
            this.orderByField.Text = "Dupa camp";
            // 
            // orderByGroupRadioButton
            // 
            this.orderByGroupRadioButton.AutoSize = true;
            this.orderByGroupRadioButton.Location = new System.Drawing.Point(7, 44);
            this.orderByGroupRadioButton.Name = "orderByGroupRadioButton";
            this.orderByGroupRadioButton.Size = new System.Drawing.Size(54, 17);
            this.orderByGroupRadioButton.TabIndex = 1;
            this.orderByGroupRadioButton.Text = "Grupa";
            this.orderByGroupRadioButton.UseVisualStyleBackColor = true;
            this.orderByGroupRadioButton.CheckedChanged += new System.EventHandler(this.CheckedOrderByStudentGroup);
            // 
            // orderByNameRadioButton
            // 
            this.orderByNameRadioButton.AutoSize = true;
            this.orderByNameRadioButton.Checked = true;
            this.orderByNameRadioButton.Location = new System.Drawing.Point(7, 20);
            this.orderByNameRadioButton.Name = "orderByNameRadioButton";
            this.orderByNameRadioButton.Size = new System.Drawing.Size(91, 17);
            this.orderByNameRadioButton.TabIndex = 0;
            this.orderByNameRadioButton.TabStop = true;
            this.orderByNameRadioButton.Text = "Nume student";
            this.orderByNameRadioButton.UseVisualStyleBackColor = true;
            this.orderByNameRadioButton.CheckedChanged += new System.EventHandler(this.CheckedOrderByStudentName);
            // 
            // studentData
            // 
            this.studentData.Location = new System.Drawing.Point(247, 12);
            this.studentData.Name = "studentData";
            // 
            // studentData.Panel1
            // 
            this.studentData.Panel1.Controls.Add(this.sectionsLabel);
            this.studentData.Panel1.Controls.Add(this.studentDataDateOfBirthLabel);
            this.studentData.Panel1.Controls.Add(this.studentDataGroupLabel);
            this.studentData.Panel1.Controls.Add(this.studentDataSerialNumberLabel);
            this.studentData.Panel1.Controls.Add(this.studentDataNameLabel);
            // 
            // studentData.Panel2
            // 
            this.studentData.Panel2.Controls.Add(this.studentDataDateOfBirthDateTime);
            this.studentData.Panel2.Controls.Add(this.studentDataGroupTextBox);
            this.studentData.Panel2.Controls.Add(this.studentDataSerialNumberTextBox);
            this.studentData.Panel2.Controls.Add(this.studentDataNameTextBox);
            this.studentData.Panel2.Controls.Add(this.sectionsComboBox);
            this.studentData.Size = new System.Drawing.Size(231, 141);
            this.studentData.SplitterDistance = 85;
            this.studentData.SplitterWidth = 1;
            this.studentData.TabIndex = 9;
            // 
            // sectionsLabel
            // 
            this.sectionsLabel.AutoSize = true;
            this.sectionsLabel.Location = new System.Drawing.Point(1, 3);
            this.sectionsLabel.Name = "sectionsLabel";
            this.sectionsLabel.Size = new System.Drawing.Size(37, 13);
            this.sectionsLabel.TabIndex = 4;
            this.sectionsLabel.Text = "Sectia";
            // 
            // studentDataDateOfBirthLabel
            // 
            this.studentDataDateOfBirthLabel.AutoSize = true;
            this.studentDataDateOfBirthLabel.Location = new System.Drawing.Point(1, 108);
            this.studentDataDateOfBirthLabel.Name = "studentDataDateOfBirthLabel";
            this.studentDataDateOfBirthLabel.Size = new System.Drawing.Size(69, 13);
            this.studentDataDateOfBirthLabel.TabIndex = 3;
            this.studentDataDateOfBirthLabel.Text = "Data nasterii:";
            // 
            // studentDataGroupLabel
            // 
            this.studentDataGroupLabel.AutoSize = true;
            this.studentDataGroupLabel.Location = new System.Drawing.Point(1, 83);
            this.studentDataGroupLabel.Name = "studentDataGroupLabel";
            this.studentDataGroupLabel.Size = new System.Drawing.Size(39, 13);
            this.studentDataGroupLabel.TabIndex = 2;
            this.studentDataGroupLabel.Text = "Grupa:";
            // 
            // studentDataSerialNumberLabel
            // 
            this.studentDataSerialNumberLabel.AutoSize = true;
            this.studentDataSerialNumberLabel.Location = new System.Drawing.Point(1, 56);
            this.studentDataSerialNumberLabel.Name = "studentDataSerialNumberLabel";
            this.studentDataSerialNumberLabel.Size = new System.Drawing.Size(80, 13);
            this.studentDataSerialNumberLabel.TabIndex = 1;
            this.studentDataSerialNumberLabel.Text = "Numar matricol:";
            // 
            // studentDataNameLabel
            // 
            this.studentDataNameLabel.AutoSize = true;
            this.studentDataNameLabel.Location = new System.Drawing.Point(1, 29);
            this.studentDataNameLabel.Name = "studentDataNameLabel";
            this.studentDataNameLabel.Size = new System.Drawing.Size(76, 13);
            this.studentDataNameLabel.TabIndex = 0;
            this.studentDataNameLabel.Text = "Nume student:";
            // 
            // studentDataDateOfBirthDateTime
            // 
            this.studentDataDateOfBirthDateTime.Enabled = false;
            this.studentDataDateOfBirthDateTime.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.studentDataDateOfBirthDateTime.Location = new System.Drawing.Point(-1, 105);
            this.studentDataDateOfBirthDateTime.Name = "studentDataDateOfBirthDateTime";
            this.studentDataDateOfBirthDateTime.Size = new System.Drawing.Size(146, 20);
            this.studentDataDateOfBirthDateTime.TabIndex = 10;
            // 
            // studentDataGroupTextBox
            // 
            this.studentDataGroupTextBox.Enabled = false;
            this.studentDataGroupTextBox.Location = new System.Drawing.Point(-1, 79);
            this.studentDataGroupTextBox.Name = "studentDataGroupTextBox";
            this.studentDataGroupTextBox.Size = new System.Drawing.Size(146, 20);
            this.studentDataGroupTextBox.TabIndex = 2;
            // 
            // studentDataSerialNumberTextBox
            // 
            this.studentDataSerialNumberTextBox.Enabled = false;
            this.studentDataSerialNumberTextBox.Location = new System.Drawing.Point(-1, 53);
            this.studentDataSerialNumberTextBox.Name = "studentDataSerialNumberTextBox";
            this.studentDataSerialNumberTextBox.Size = new System.Drawing.Size(146, 20);
            this.studentDataSerialNumberTextBox.TabIndex = 1;
            // 
            // studentDataNameTextBox
            // 
            this.studentDataNameTextBox.Enabled = false;
            this.studentDataNameTextBox.Location = new System.Drawing.Point(-1, 27);
            this.studentDataNameTextBox.Name = "studentDataNameTextBox";
            this.studentDataNameTextBox.Size = new System.Drawing.Size(146, 20);
            this.studentDataNameTextBox.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 342);
            this.Controls.Add(this.studentData);
            this.Controls.Add(this.orderByGroupBox);
            this.Controls.Add(this.displayStudentsPannel);
            this.Controls.Add(this.clearStudentFieldsButton);
            this.Controls.Add(this.deleteStudentButton);
            this.Controls.Add(this.updateStudentButton);
            this.Controls.Add(this.addStudentButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Student Management";
            this.Load += new System.EventHandler(this.LoadSections);
            this.displayStudentsPannel.ResumeLayout(false);
            this.orderByGroupBox.ResumeLayout(false);
            this.orderBySense.ResumeLayout(false);
            this.orderBySense.PerformLayout();
            this.orderByField.ResumeLayout(false);
            this.orderByField.PerformLayout();
            this.studentData.Panel1.ResumeLayout(false);
            this.studentData.Panel1.PerformLayout();
            this.studentData.Panel2.ResumeLayout(false);
            this.studentData.Panel2.PerformLayout();
            this.studentData.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox studentsListBox;
        private System.Windows.Forms.ListBox studentGroupsListBox;
        private System.Windows.Forms.ComboBox sectionsComboBox;
        private System.Windows.Forms.Button addStudentButton;
        private System.Windows.Forms.Button updateStudentButton;
        private System.Windows.Forms.Button deleteStudentButton;
        private System.Windows.Forms.Button clearStudentFieldsButton;
        private System.Windows.Forms.Panel displayStudentsPannel;
        private System.Windows.Forms.Label studentNameLabel;
        private System.Windows.Forms.Label studentGroupLabel;
        private System.Windows.Forms.GroupBox orderByGroupBox;
        private System.Windows.Forms.GroupBox orderByField;
        private System.Windows.Forms.RadioButton orderByGroupRadioButton;
        private System.Windows.Forms.RadioButton orderByNameRadioButton;
        private System.Windows.Forms.GroupBox orderBySense;
        private System.Windows.Forms.RadioButton orderDescendingRadioButton;
        private System.Windows.Forms.RadioButton orderAscendingRadioButton;
        private System.Windows.Forms.SplitContainer studentData;
        private System.Windows.Forms.Label studentDataNameLabel;
        private System.Windows.Forms.TextBox studentDataNameTextBox;
        private System.Windows.Forms.DateTimePicker studentDataDateOfBirthDateTime;
        private System.Windows.Forms.TextBox studentDataGroupTextBox;
        private System.Windows.Forms.TextBox studentDataSerialNumberTextBox;
        private System.Windows.Forms.Label studentDataDateOfBirthLabel;
        private System.Windows.Forms.Label studentDataGroupLabel;
        private System.Windows.Forms.Label studentDataSerialNumberLabel;
        private System.Windows.Forms.Label sectionsLabel;


    }
}

