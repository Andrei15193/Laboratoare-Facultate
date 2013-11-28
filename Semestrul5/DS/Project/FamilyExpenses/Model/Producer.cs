using System;

namespace FamilyExpenses.Model
{
    public class Producer
    {
        public Producer(string name, string country)
        {
            if (name != null)
                if (!string.IsNullOrEmpty(name)
                    && !string.IsNullOrWhiteSpace(name))
                    if (country != null)
                        if (!string.IsNullOrEmpty(country)
                            && !string.IsNullOrWhiteSpace(country))
                        {
                            _name = name.Trim();
                            _country = country.Trim();
                        }
                        else
                            throw new ArgumentException("Cannot be empty or whitespace only!", "country");
                    else
                        throw new ArgumentNullException("country");
                else
                    throw new ArgumentException("Cannot be empty or whitespace only!", "name");
            else
                throw new ArgumentNullException("name");
            ;
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
                        _name = value;
                    else
                        throw new ArgumentException("Cannot be empty or whitespace only!", "Name");
                else
                    throw new ArgumentNullException("Name");
            }
        }

        public string Country
        {
            get
            {
                return _country;
            }
            set
            {
                if (value != null)
                    if (!string.IsNullOrEmpty(value)
                        && !string.IsNullOrWhiteSpace(value))
                        _country = value;
                    else
                        throw new ArgumentException("Cannot be empty or whitespace only!", "Country");
                else
                    throw new ArgumentNullException("Country");
            }
        }

        private string _name;
        private string _country;
    }
}
