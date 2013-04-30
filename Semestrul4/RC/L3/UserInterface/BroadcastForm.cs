using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UDPMessenger.BusinessComponents;
using UDPMessenger.BusinessEntities;

namespace UDPMessenger.UserInterface
{
    partial class BroadcastForm : Form
    {
        public BroadcastForm(MessengerViewModel messengerViewModel)
        {
            InitializeComponent();
            _messengerViewModel = messengerViewModel;
            messengerViewModel.OnlineUsers.ListChanged += _OnlineUserListChanged;
            DataBindings.Add(new Binding("Messages", messengerViewModel, "Messages"));
        }

        public string Messages
        {
            get
            {
                return _messengerViewModel.Messages;
            }
            set
            {
                _messagesRichTextBox.Invoke(new Action(() =>
                {
                    _messagesRichTextBox.Text = value;
                    _messagesRichTextBox.SelectionStart= value.Length;
                    _messagesRichTextBox.ScrollToCaret();
                }));
            }
        }

        private void _messageTextChanged(object sender, EventArgs e)
        {
            if (_messageTextBox.Text.Length > 0)
                _sendButton.Enabled = true;
            else
                _sendButton.Enabled = false;
        }

        private void _OnlineUserListChanged(object sender, ListChangedEventArgs e)
        {
            switch (e.ListChangedType)
            {
                case ListChangedType.ItemAdded:
                    _onlineUsersListBox.Invoke(new Action(() => { _onlineUsersListBox.Items.Add(_messengerViewModel.OnlineUsers[e.NewIndex]); }));
                    break;
                case ListChangedType.ItemDeleted:
                    _onlineUsersListBox.Invoke(new Action(() => { _onlineUsersListBox.Items.RemoveAt(e.NewIndex); }));
                    break;
                default:
                    break;
            }
        }

        private void _SendMessage(object sender, EventArgs e)
        {
            if (_onlineUsersListBox.SelectedIndex == -1)
                _messengerViewModel.SendMessage(_messageTextBox.Text);
            else
                foreach (object selectedUser in _onlineUsersListBox.SelectedItems)
                    _messengerViewModel.SendMessage(_messageTextBox.Text, selectedUser as User);
            _messageTextBox.Text = "";
        }

        MessengerViewModel _messengerViewModel;
    }
}
