using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UDPMessenger.BusinessComponents;
using UDPMessenger.UserInterface;

namespace UDPMessenger
{
    class UDPMessengerApplication : ApplicationContext
    {
        public UDPMessengerApplication()
        {
            _usernameForm = new UsernameForm();
            _usernameForm.FormClosed += _OpenBroadcastForm;
            _usernameForm.ShowDialog();
        }

        private void _OpenBroadcastForm(object sender, FormClosedEventArgs e)
        {
            _usernameForm.Visible = false;
            if (_usernameForm.DialogResult == DialogResult.OK)
                using (Messenger messenger = new Messenger() { Username = _usernameForm.Username, Port = 12345 })
                {
                    MessengerViewModel messengerViewModel = new MessengerViewModel(messenger);
                    _broadcastForm = new BroadcastForm(messengerViewModel);
                    messenger.Start();
                    _broadcastForm.ShowDialog();
                }
            Application.Exit();
        }

        private UsernameForm _usernameForm;
        private BroadcastForm _broadcastForm;
    }
}
