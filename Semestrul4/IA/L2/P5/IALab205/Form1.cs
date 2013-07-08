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
                while (adiacentaForm.ShowDialog() != DialogResult.OK)
                {
                }
                for (int j = 0; j < numarNoduriNumericUpDown.Value; j++)
                    adiacenta[i, j] = adiacentaForm.Adiacente[j];
            }
            graf = new Graf(adiacenta);
            muchiiTextBox.Text = string.Join(", ", graf.Muchii.Select((muchie) => string.Format("{0} - {1}", muchie.PrimulNod, muchie.AlDoileaNod)));
            button1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_aeRadioButton.Checked)
                new Form3(new Problema5AlgoritmEvolutiv(graf).CautaRezultat((int)dimensiunePopulatieNumericUpDown.Value, (int)numarGeneratiiNumericUpDown.Value, TipMutatie.Tare, (x, y) => x.CompareTo(y)) as Cromozom).ShowDialog();
            else
                new Form3(new Problema5ParticleSwarmOptimization(graf)
                    .CautaRezultat(
                    (int)dimensiunePopulatieNumericUpDown.Value,
                    (int)numarGeneratiiNumericUpDown.Value,
                    (double)_factorInertieNumericUpDown.Value,
                    (double)_factorCognitivNumericUpDown.Value,
                    (double)_factorSocialNumericUpDown.Value,
                    (x, y) => x.CompareTo(y)) as Particula
                ).ShowDialog();
        }

        private void _aeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            _numarGeneratiiLabel.Text = "Numar generatii:";
            _dimensiunePopulatieLabel.Text = "Dimensiune populatie:";
            _factorCognitivNumericUpDown.Enabled = false;
            _factorInertieNumericUpDown.Enabled = false;
            _factorSocialNumericUpDown.Enabled = false;
        }

        private void _psoRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            _numarGeneratiiLabel.Text = "Numar cicluri:";
            _dimensiunePopulatieLabel.Text = "Numar particule:";
            _factorCognitivNumericUpDown.Enabled = true;
            _factorInertieNumericUpDown.Enabled = true;
            _factorSocialNumericUpDown.Enabled = true;
        }

        private bool[,] adiacenta;
        private Graf graf;
    }
}
