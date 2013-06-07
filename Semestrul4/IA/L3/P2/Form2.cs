using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IALab302
{
    public partial class Form2 : Form
    {
        public Form2(string factText, bool areThereMoreFacts = false)
        {
            InitializeComponent();
            factTextBox.Text = factText;
            if (areThereMoreFacts)
                questionLabel.Text = "Sunt urmatoarele fapte adevarate?";
        }

        private void positiveAnswerButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void negativeAnswerButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
