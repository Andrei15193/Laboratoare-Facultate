using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BDLab4.UserInterface
{
    partial class MainForm
    {
        /// <summary>
        /// Ran when the form loads. It populates the section listbox with all sections found in the data base.
        /// </summary>
        private void LoadSections(object sender, EventArgs e)
        {
            Controller.SectionReader sections = null;
            try
            {
                sections = this.applicationController.GetSections();
                while (sections.Read())
                    sectionsComboBox.Items.Add(sections.GetCurrentSection());
            }
            catch (Domain.SectionException exception)
            {
                System.Windows.Forms.MessageBox.Show(exception.Message, "Eroare!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                if (sections != null)
                    sections.Close();
            }
        }

        /// <summary>
        /// Ran when the section list box has a new section selected.
        /// </summary>
        private void SectionComboBoxIndexChanged(object sender, EventArgs e)
        {
            SetControlsForAddingStudents();
            PopulateListsWithStudentData();
        }

        /// <summary>
        /// Ran when the order by student name field radio button is checked or unchecked.
        /// </summary>
        private void CheckedOrderByStudentName(object sender, EventArgs e)
        {
            if (this.orderByNameRadioButton.Checked)
            {
                this.orderByFieldValue = Controller.OrderField.Name;
                SetControlsForAddingStudents();
                PopulateListsWithStudentData();
            }
        }

        /// <summary>
        /// Ran when the order by student group field radio button is checked or unchecked.
        /// </summary>
        private void CheckedOrderByStudentGroup(object sender, EventArgs e)
        {
            if (this.orderByGroupRadioButton.Checked)
            {
                this.orderByFieldValue = Controller.OrderField.Group;
                SetControlsForAddingStudents();
                PopulateListsWithStudentData();
            }
        }

        /// <summary>
        /// Ran when the order by sense is checked or unchecked to ascending.
        /// </summary>
        private void CheckedOrderAscending(object sender, EventArgs e)
        {
            if (this.orderAscendingRadioButton.Checked)
            {
                this.orderBySenseValue = Controller.OrderSense.Ascending;
                SetControlsForAddingStudents();
                PopulateListsWithStudentData();
            }
        }

        /// <summary>
        /// Ran when the order by sense is checked or unchecked to descending.
        /// </summary>
        private void CheckedOrderDescending(object sender, EventArgs e)
        {
            if (this.orderDescendingRadioButton.Checked)
            {
                this.orderBySenseValue = Controller.OrderSense.Descending;
                SetControlsForAddingStudents();
                PopulateListsWithStudentData();
            }
        }

        /// <summary>
        /// Ran when a student has been selected in the table-like list boxes (selected via name list box).
        /// </summary>
        private void ClickedStudentNameInListBox(object sender, EventArgs e)
        {
            Domain.Student student = this.studentsListBox.SelectedItem as Domain.Student;
            this.studentGroupsListBox.SelectedIndex = this.studentsListBox.SelectedIndex;
            if (student != null)
            {
                EnableControlsForUpdatingStudents();
                studentDataNameTextBox.Text = student.Name;
                studentDataGroupTextBox.Text = student.Group;
                studentDataSerialNumberTextBox.Text = student.SerialNumber;
                studentDataDateOfBirthDateTime.Value = student.DateOfBirth;
            }
        }

        /// <summary>
        /// Ran when a student has been selected in the table-like list boxes (selected via group list box).
        /// </summary>
        private void ClickedStudentGroupInListBox(object sender, EventArgs e)
        {
            this.studentsListBox.SelectedIndex = this.studentGroupsListBox.SelectedIndex;
            this.ClickedStudentNameInListBox(sender, e);
        }

        /// <summary>
        /// Ran when the user clicks the add student button.
        /// </summary>
        private void AddStudent(object sender, EventArgs e)
        {
            try
            {
                this.applicationController.AddStudent((this.sectionsComboBox.SelectedItem as Domain.Section).Code,
                                                      this.studentDataNameTextBox.Text,
                                                      this.studentDataSerialNumberTextBox.Text,
                                                      this.studentDataGroupTextBox.Text,
                                                      this.studentDataDateOfBirthDateTime.Value);
                SetControlsForAddingStudents();
                PopulateListsWithStudentData();
            }
            catch (Domain.StudentException exception)
            {
                System.Windows.Forms.MessageBox.Show(exception.Message, "Eroare!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Ran when the user clicks update student button.
        /// </summary>
        private void UpdateStudent(object sender, EventArgs e)
        {
            try
            {
                this.applicationController.UpdateStudent((this.sectionsComboBox.SelectedItem as Domain.Section).Code,
                                                         this.studentDataNameTextBox.Text,
                                                         this.studentDataSerialNumberTextBox.Text,
                                                         this.studentDataGroupTextBox.Text,
                                                         this.studentDataDateOfBirthDateTime.Value);
                SetControlsForAddingStudents();
                PopulateListsWithStudentData();
            }
            catch (Domain.StudentException exception)
            {
                System.Windows.Forms.MessageBox.Show(exception.Message, "Eroare!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Ran when the user clicks the delete student button.
        /// </summary>
        private void DeleteStudent(object sender, EventArgs e)
        {
            try
            {
                this.applicationController.DeleteStudent(this.studentDataSerialNumberTextBox.Text);
                SetControlsForAddingStudents();
                PopulateListsWithStudentData();
            }
            catch (Domain.StudentException exception)
            {
                System.Windows.Forms.MessageBox.Show(exception.Message, "Eroare!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Ran when the user clicks clear fields button.
        /// </summary>
        private void ClearFields(object sender, EventArgs e)
        {
            SetControlsForAddingStudents();
        }
    }
}