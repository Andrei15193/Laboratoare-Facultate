using System;
using UDPMessenger.BusinessEntities;

namespace UDPMessenger.BusinessComponents
{
    public class UserEventArgs : EventArgs
    {
        public UserEventArgs(User user)
        {
            User = user;
        }

        public User User { get; private set; }
    }
}
