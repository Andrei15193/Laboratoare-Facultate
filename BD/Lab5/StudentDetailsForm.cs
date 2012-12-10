using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BDLab5
{
    public partial class StudentDetailsForm : Form
    {
        public StudentDetailsForm(string okButtonText)
        {
            InitializeComponent();
            okButton.Text = okButtonText;
        }

        public StudentDetailsForm(string okButtonText, string name, string serialNumber, string group, DateTime? dateOfBirth)
        {
            InitializeComponent();
            serialNumberTextBox.Enabled = false;
            nameTextBox.Text = name;
            serialNumberTextBox.Text = serialNumber;
            groupTextBox.Text = group;
            if (dateOfBirth != null)
                dateOfBirthDatePicker.Value = dateOfBirth.Value;
            okButton.Text = okButtonText;
        }

        public string StudentName
        {
            get
            {
                return this.nameTextBox.Text;
            }
        }

        public string SudentGroup
        {
            get
            {
                return this.groupTextBox.Text;
            }
        }

        public string StudentSerialNumber
        {
            get
            {
                return this.serialNumberTextBox.Text;
            }
        }

        public DateTime StudentDateOfBirth
        {
            get
            {
                return this.dateOfBirthDatePicker.Value;
            }
        }
    }
}
