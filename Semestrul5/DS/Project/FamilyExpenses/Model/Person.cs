using System;

namespace FamilyExpenses.Model
{
    public class Person
    {
        public Person(string name, Currency preferedCurrency)
        {
            if (name != null)
                if (!string.IsNullOrEmpty(name)
                    && !string.IsNullOrWhiteSpace(name))
                {
                    _name = name.Trim();
                    PreferedCurrency = preferedCurrency;
                }
                else
                    throw new ArgumentException("Cannot be empty or white space only!", "name");
            else
                throw new ArgumentNullException("name");
        }

        public Currency PreferedCurrency
        {
            get;
            set;
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value != null)
                    if (!string.IsNullOrEmpty(value)
                        && !string.IsNullOrWhiteSpace(value))
                        _name = value.Trim();
                    else
                        throw new ArgumentException("Cannot be empty or white space only!", "Name");
                else
                    throw new ArgumentNullException("Name");
            }
        }

        private string _name;
    }
}
