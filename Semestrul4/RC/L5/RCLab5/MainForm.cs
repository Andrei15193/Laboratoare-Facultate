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
    public partial class MainForm : Form
    {
        public MainForm(ViewModel viewModel)
        {
            this.viewModel = viewModel;
            InitializeComponent();
            DataBindings.Add("Text", viewModel, "NodeAddress");
            neighbourListBox.DataSource = viewModel.Neighbours;
            routingTableDataGridView.DataSource = viewModel.RoutingTableView;
            viewModel.Start(this);
        }

        private void adaugaVecinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (addNeighbourDialog.ShowDialog() == DialogResult.OK)
            {
                viewModel.AddNeighbour(addNeighbourDialog.Neighbour);
            }
        }

        private readonly ViewModel viewModel;
        private readonly AddNeighbourDialog addNeighbourDialog = new AddNeighbourDialog();
    }
}
