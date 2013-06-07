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
    public partial class Form2 : Form
    {
        public Form2(int nod, int totalNoduri)
        {
            InitializeComponent();
            Text += (nod + 1).ToString();
            this.totalNoduri = totalNoduri;
        }

        public bool[] Adiacente { get; private set; }

        private void button1_Click(object sender, EventArgs e)
        {
            string text = noduriAdiacenteTextBox.Text + ",";
            Adiacente = new bool[totalNoduri];
            for (int i = 0; i < totalNoduri; i++)
                if (text.Contains((i + 1).ToString() + ","))
                    Adiacente[i] = true;
                else
                    Adiacente[i] = false;
            DialogResult = DialogResult.OK;
        }

        private int totalNoduri;
    }
}
