using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IALab205
{
    public partial class Form3 : Form
    {
        public Form3(Cromozom rezultat)
        {
            InitializeComponent();
            solutieTextBox.Text = "Solutia:\r\n\r\nPartitionarea primului graf:\r\n";
            foreach (var muchie in rezultat.MuchiilePrimuluiGraf)
                solutieTextBox.Text += string.Format("{0} - {1}, ", muchie.PrimulNod, muchie.AlDoileaNod);
            solutieTextBox.Text += "\r\n\r\nPartitionarea celui de al doilea graf:\r\n";
            foreach (var muchie in rezultat.MuchiileCeluiDeAlDoileaGraf)
                solutieTextBox.Text += string.Format("{0} - {1}, ", muchie.PrimulNod, muchie.AlDoileaNod);
            solutieTextBox.Text += "\r\n";
            solutieTextBox.Text += string.Format("Fitness: {0}", rezultat.Fitness);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
