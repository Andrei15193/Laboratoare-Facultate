namespace Protocol
{
    public class User
    {
        public User()
        {
        }

        public User(string firstName, string lastName, string code, UserType userType)
        {
            FirstName = firstName;
            LastName = lastName;
            Code = code;
            Type = userType;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Code { get; set; }

        public UserType Type { get; set; }
    }
}
