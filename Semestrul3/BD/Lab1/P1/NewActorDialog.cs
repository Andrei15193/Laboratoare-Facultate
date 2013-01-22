using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CSLabBD
{
    public partial class NewActorDialog : Form
    {
        public NewActorDialog()
        {
            InitializeComponent();
        }

        public string ActorName
        {
            get
            {
                return TextBoxActorName.Text;
            }
            set
            {
                TextBoxActorName.Text = value;
            }
        }
    }
}
