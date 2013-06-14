using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RCLab5
{
    public partial class NodeSettingsForm : Form
    {
        public NodeSettingsForm(ViewModel viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;
            graphics = CreateGraphics();
            nodeAddressPanel.DataBindings.Add("BackColor", nodeAddressTextBox, "BackColor");
            networkClassPanel.DataBindings.Add("BackColor", netowrkClassTextBox, "BackColor");
            textColor = nodeAddressTextBox.ForeColor;
            nodeAddressTextBox.TextChanged += valideazaAdresa;
            nodeAddressTextBox.TextChanged += checkOkButton;
            netowrkClassTextBox.TextChanged += valideazaClasaDeRetea;
            netowrkClassTextBox.TextChanged += checkOkButton;
            Paint += paintEvent;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (esteAdresaValida && esteClasaDeReteaValida)
                DialogResult = DialogResult.OK;
        }

        private void valideazaClasaDeRetea(object sender, EventArgs e)
        {
            viewModel.NetworkClass = netowrkClassTextBox.Text;
            esteClasaDeReteaValida = viewModel.ValidateNetworkClass();
            if (esteClasaDeReteaValida)
                netowrkClassTextBox.ForeColor = textColor;
            else
                netowrkClassTextBox.ForeColor = Color.Red;
            DrawNetworkClassBorder();
        }

        private void valideazaAdresa(object sender, EventArgs e)
        {
            viewModel.NodeAddress = nodeAddressTextBox.Text;
            esteAdresaValida = viewModel.ValidateNodeAddress();
            if (esteAdresaValida)
                nodeAddressTextBox.ForeColor = textColor;
            else
                nodeAddressTextBox.ForeColor = Color.Red;
            DrawNodeAddressBorder();
        }

        private void paintEvent(object sender, PaintEventArgs e)
        {
            DeseneazaRame();
        }

        private void DeseneazaRame()
        {
            DrawNodeAddressBorder();
            DrawNetworkClassBorder();
        }

        private void DrawNodeAddressBorder()
        {
            graphics.DrawRectangle(new Pen(esteAdresaValida ? textColor : Color.Red, 1), nodeAddressPanel.Left - 1, nodeAddressPanel.Top - 1, nodeAddressPanel.Width + 1, nodeAddressPanel.Height + 1);
        }

        private void DrawNetworkClassBorder()
        {
            graphics.DrawRectangle(new Pen(esteClasaDeReteaValida ? textColor : Color.Red, 1), networkClassPanel.Left - 1, networkClassPanel.Top - 1, networkClassPanel.Width + 1, networkClassPanel.Height + 1);
        }

        private void checkOkButton(object sender, EventArgs e)
        {
            okButton.Enabled = (esteAdresaValida && esteClasaDeReteaValida);
        }

        private bool esteAdresaValida;
        private bool esteClasaDeReteaValida;
        private readonly Color textColor;
        private readonly Graphics graphics;
        private readonly ViewModel viewModel;
    }
}
