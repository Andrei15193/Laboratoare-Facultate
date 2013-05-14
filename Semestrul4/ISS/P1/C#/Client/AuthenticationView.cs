using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISSApp
{
    public partial class AuthenticationView : Form
    {
        public AuthenticationView()
        {
            InitializeComponent();
        }

        private void OnAuthenticate(object sender, EventArgs e)
        {
            int number;
            DialogResult dialogResult = DialogResult.No;
            try
            {
                number = Convert.ToInt32(this.codePasswordTextbox.Text);
                if (codes.Contains(number))
                    dialogResult = DialogResult.OK;
            }
            catch (FormatException)
            {
                if (password.Contains(this.codePasswordTextbox.Text))
                    dialogResult = DialogResult.OK;
            }
            this.DialogResult = dialogResult;
        }

        private static int[] codes = { 1412, 1234, 0000 };
        private static string[] password = { "password" };
    }
}
