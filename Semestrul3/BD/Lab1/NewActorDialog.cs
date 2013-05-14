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
