using System;
using UDPMessenger.BusinessEntities;

namespace UDPMessenger.BusinessComponents
{
    public class MessageEventArgs : EventArgs
    {
        public MessageEventArgs(User sender, string message, MessageScope messageScope)
        {
            User = sender;
            Message = message;
            Scope = messageScope;
        }

        public User User { get; private set; }

        public string Message { get; private set; }

        public MessageScope Scope { get; private set; }
    }
}
