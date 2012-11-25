using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BDLab4.UserInterface
{
    /// <summary>
    /// Represents the main form for the user interface. This is the first form the is being displayed to screen.
    /// </summary>
    partial class MainForm : Form
    {
        /// <summary>
        /// Creates a new MainForm instance having the given controller.
        /// </summary>
        /// <param name="applicationController">an application controller that will handle application business.</param>
        public MainForm(Controller.ApplicationController applicationController)
        {
            this.applicationController = applicationController;
            this.orderByFieldValue = Controller.OrderField.Name;
            this.orderBySenseValue = Controller.OrderSense.Ascending;
            InitializeComponent();
        }

        /// <summary>
        /// Populates the student table-like list boxes with information from the data base.
        /// </summary>
        private void PopulateListsWithStudentData()
        {
            Domain.Student student;
            Controller.StudentReader students = null;
            Domain.Section section = sectionsComboBox.SelectedItem as Domain.Section;
            try
            {
                if (section != null)
                {
                    students = this.applicationController.GetStudentsFromSection(section.Code, this.orderByFieldValue, this.orderBySenseValue);
                    studentsListBox.Items.Clear();
                    studentGroupsListBox.Items.Clear();
                    while (students.Read())
                    {
                        student = students.GetCurrentStudent();
                        studentsListBox.Items.Add(student);
                        studentGroupsListBox.Items.Add(student.Group.ToString());
                    }
                }
            }
            catch (Domain.StudentException exception)
            {
                System.Windows.Forms.MessageBox.Show(exception.Message, "Eroare!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                if (students != null)
                    students.Close();
            }
        }

        /// <summary>
        /// Sets controls (resets textboxes, enables or disables texboxes and buttons) in order for the user to be
        /// able to add a new student.
        /// </summary>
        private void SetControlsForAddingStudents()
        {
            this.studentDataNameTextBox.Text = "";
            this.studentDataGroupTextBox.Text = "";
            this.studentDataSerialNumberTextBox.Text = "";
            this.studentDataDateOfBirthDateTime.Value = DateTime.Now.AddYears(-20);
            this.studentDataNameTextBox.Enabled = true;
            this.studentDataGroupTextBox.Enabled = true;
            this.studentDataDateOfBirthDateTime.Enabled = true;
            this.updateStudentButton.Enabled = false;
            this.deleteStudentButton.Enabled = false;
            this.clearStudentFieldsButton.Enabled = false;
            this.studentDataSerialNumberTextBox.Enabled = true;
            this.addStudentButton.Enabled = true;
        }

        /// <summary>
        /// Enables or disables conrtols (textboxes and buttons) in order for the user to be able to update a student.
        /// </summary>
        private void EnableControlsForUpdatingStudents()
        {
            this.studentDataNameTextBox.Enabled = true;
            this.studentDataGroupTextBox.Enabled = true;
            this.studentDataDateOfBirthDateTime.Enabled = true;
            this.updateStudentButton.Enabled = true;
            this.deleteStudentButton.Enabled = true;
            this.clearStudentFieldsButton.Enabled = true;
            this.studentDataSerialNumberTextBox.Enabled = false;
            this.addStudentButton.Enabled = false;
        }

        private Controller.ApplicationController applicationController;
        private Controller.OrderField orderByFieldValue;
        private Controller.OrderSense orderBySenseValue;
    }
}
