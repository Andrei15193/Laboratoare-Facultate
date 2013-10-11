using System;

namespace RegistProgramareWeb.Laborator1.Infrastructure
{
    [Serializable]
    public class User
    {
        public User(string name, string password)
        {
            if (name != null)
                if (password != null)
                {
                    _name = name.Trim();
                    if (_name.Length > 0)
                        _password = password;
                    else
                        throw new ArgumentException("Name cannot be empty!");
                }
                else
                    throw new ArgumentNullException("password");
            else
                throw new ArgumentNullException("name");
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
        }

        private readonly string _name;
        private readonly string _password;
    }
}
