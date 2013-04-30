using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UDPMessenger.UserInterface
{
    public partial class UsernameForm : Form
    {
        public UsernameForm()
        {
            InitializeComponent();
            Username = "";
            _usernameTextBox.DataBindings.Add("Text", this, "Username");
        }

        public string Username { get; set; }

        private void _usernameTextChanged(object sender, EventArgs e)
        {
            if (_usernameTextBox.Text.Length > 0)
                _connectButton.Enabled = true;
            else
                _connectButton.Enabled = false;
        }
    }
}
