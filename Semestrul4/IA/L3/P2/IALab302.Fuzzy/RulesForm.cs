using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IALab302.Fuzzy
{
    public partial class RulesForm : Form
    {
        public RulesForm(DataTable rules)
        {
            InitializeComponent();
            _rulesDataGridView.DataSource = rules.DefaultView;
        }
    }
}
