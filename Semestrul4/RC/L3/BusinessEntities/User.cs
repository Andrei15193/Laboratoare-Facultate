using System.Text;

namespace UDPMessenger.BusinessEntities
{
    public class User
    {
        public static bool operator ==(User left, User right)
        {
            if (left as object == null)
                return right as object == null;
            else
                return left.Equals(right);
        }

        public static bool operator ==(User left, object right)
        {
            if (left as object == null)
                return right == null;
            else
                return left.Equals(right);
        }

        public static bool operator ==(object left, User right)
        {
            return right == left;
        }

        public static bool operator !=(User left, User right)
        {
            return !(left == right);
        }

        public static bool operator !=(object left, User right)
        {
            return !(left == right);
        }

        public static bool operator !=(User left, object right)
        {
            return !(left == right);
        }

        public User(string name, byte[] ipAddress)
        {
            OnlineTimeout = 2;
            Name = name;
            StringBuilder stringRepresentationBuilder = new StringBuilder();
            for (int i = 0; i < ipAddress.Length - 1; i++)
            {
                stringRepresentationBuilder.Append(ipAddress[i]);
                stringRepresentationBuilder.Append('.');
            }
            stringRepresentationBuilder.Append(ipAddress[ipAddress.Length - 1]);
            IPAddress = stringRepresentationBuilder.ToString();
            stringRepresentationBuilder.Insert(0, " - ");
            stringRepresentationBuilder.Insert(0, Name);
            _stringRepresentation = stringRepresentationBuilder.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj != null)
                return obj is User && IPAddress == (obj as User).IPAddress;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return IPAddress.GetHashCode();
        }

        public override string ToString()
        {
            return _stringRepresentation;
        }

        public string Name { get; private set; }

        public string IPAddress { get; private set; }

        public int OnlineTimeout { get; set; }

        private string _stringRepresentation;
    }
}
