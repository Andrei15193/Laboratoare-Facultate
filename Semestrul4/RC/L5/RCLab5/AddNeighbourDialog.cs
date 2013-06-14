using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RCLab5
{
    public partial class AddNeighbourDialog : Form
    {
        public AddNeighbourDialog()
        {
            InitializeComponent();
            neighbourTextBox.DataBindings.Add("Text", this, "Neighbour");
        }

        public string Neighbour { get; set; }

        private void neightbourNameChanged(object sender, EventArgs e)
        {
            okButton.Enabled = (neighbourTextBox.Text.Length > 0);
        }
    }
}
