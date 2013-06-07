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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void numericUpDown1_Leave(object sender, EventArgs e)
        {
            adiacenta = new bool[(int)numarNoduriNumericUpDown.Value, (int)numarNoduriNumericUpDown.Value];
            for (int i = 0; i < numarNoduriNumericUpDown.Value; i++)
            {
                Form2 adiacentaForm = new Form2(i, (int)numarNoduriNumericUpDown.Value);
                adiacentaForm.ShowDialog();
                for (int j = 0; j < numarNoduriNumericUpDown.Value; j++)
                    adiacenta[i, j] = adiacentaForm.Adiacente[j];
            }
            graf = new Graf(adiacenta);
            muchiiTextBox.Text = string.Join(", ", graf.Muchii.Select((muchie) => string.Format("{0} - {1}", muchie.PrimulNod, muchie.AlDoileaNod)));
            button1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AbstractAlgoritmEvolutiv algoritmEvolutiv = new Problema5AlgoritmEvolutiv(graf);
            Cromozom rezultat = algoritmEvolutiv.CautaRezultat((int)dimensiunePopulatieNumericUpDown.Value, (int)numarGeneratiiNumericUpDown.Value, TipMutatie.Tare, (x, y) => x.CompareTo(y)) as Cromozom;
            new Form3(rezultat).ShowDialog();
        }

        private bool[,] adiacenta;
        private Graf graf;
    }
}
