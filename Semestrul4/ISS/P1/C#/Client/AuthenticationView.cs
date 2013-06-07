using System;
using System.Windows.Forms;

namespace ISSApp
{
    public partial class AuthenticationView : Form
    {
        public AuthenticationView()
        {
            InitializeComponent();
        }

        public string Code
        {
            get
            {
                return codePasswordTextbox.Text;
            }
        }

        private void OnAuthenticate(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
