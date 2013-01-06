using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BDLab6
{
    public partial class Preparat : Form
    {
        public Preparat(string acceptButtonText)
        {
            InitializeComponent();
            this.acceptButton.Text = acceptButtonText;
        }

        public Preparat(string acceptButtonText, string numePreparat, decimal pretPreparat, decimal durataPreparare)
        {
            InitializeComponent();
            this.acceptButton.Text = acceptButtonText;
            this.numePreparatTextBox.Text = numePreparat;
            this.pretNumericUpDown.Value = pretPreparat;
            this.durataNumericUpDown.Value = durataPreparare;
        }

        public string NumePreparat
        {
            get
            {
                return this.numePreparatTextBox.Text;
            }
        }

        public decimal PretPreparat
        {
            get
            {
                return this.pretNumericUpDown.Value;
            }
        }

        public decimal DurataPreparare
        {
            get
            {
                return this.durataNumericUpDown.Value;
            }
        }
    }
}
