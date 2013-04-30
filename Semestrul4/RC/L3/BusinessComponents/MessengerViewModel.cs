using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using UDPMessenger.BusinessEntities;

namespace UDPMessenger.BusinessComponents
{
    class MessengerViewModel : INotifyPropertyChanged
    {
        public MessengerViewModel(Messenger messenger)
        {
            Messages = "";
            OnlineUsers = new BindingList<User>();
            _messenger = messenger;
            _messenger.MessageReceived += _MessageReceived;
            _messenger.UserCameOnline += _AddUser;
            _messenger.UserWentOffline += _RemoveUser;
        }

        public void SendMessage(string message)
        {
            _messenger.SendPublicMessage(message);
        }

        public void SendMessage(string message, User user)
        {
            StringBuilder messageStringBuilder = new StringBuilder();
            _messenger.SendPrivateMessage(message, user);
            messageStringBuilder.AppendLine(string.Format("{0:dddd, d MMM, HH:mm} ", DateTime.Now));
            messageStringBuilder.Append("PM to ");
            messageStringBuilder.Append(user.Name);
            messageStringBuilder.Append(": ");
            messageStringBuilder.AppendLine(message);
            messageStringBuilder.AppendLine();
            lock (_messagesStringBuilder)
                _messagesStringBuilder.Append(messageStringBuilder.ToString());
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("Messages"));
        }

        public string Messages
        {
            get
            {
                lock (_messagesStringBuilder)
                    return _messagesStringBuilder.ToString();
            }
            private set
            {
                _messagesStringBuilder.Clear();
                _messagesStringBuilder.Append(value);
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Messages"));
            }
        }

        public BindingList<User> OnlineUsers { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void _RemoveUser(object sender, UserEventArgs e)
        {
            OnlineUsers.Remove(e.User);
        }

        private void _AddUser(object sender, UserEventArgs e)
        {
            OnlineUsers.Add(e.User);
        }

        private void _MessageReceived(object sender, MessageEventArgs e)
        {
            StringBuilder messageStringBuilder = new StringBuilder();
            messageStringBuilder.AppendLine(string.Format("{0:dddd, d MMM, HH:mm} ", DateTime.Now));
            if (e.Scope == MessageScope.Private)
                messageStringBuilder.Append("PM from ");
            messageStringBuilder.Append(e.User.Name);
            messageStringBuilder.Append(": ");
            messageStringBuilder.AppendLine(e.Message);
            messageStringBuilder.AppendLine();
            lock (_messagesStringBuilder)
                _messagesStringBuilder.Append(messageStringBuilder.ToString());
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("Messages"));
        }

        private Messenger _messenger;
        private StringBuilder _messagesStringBuilder = new StringBuilder();
    }
}
