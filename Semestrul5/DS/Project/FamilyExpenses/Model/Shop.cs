using System;

namespace FamilyExpenses.Model
{
    public class Shop
    {
        public Shop(string name, ShopType type, Address address)
        {
            if (name != null)
                if (!string.IsNullOrEmpty(name)
                    && string.IsNullOrWhiteSpace(name))
                    if (address != null)
                    {
                        _name = name;
                        _address = address;
                        Type = type;
                    }
                    else
                        throw new ArgumentException("Cannot be empty or whitespace only!", "name");
                else
                    throw new ArgumentNullException("address");
            else
                throw new ArgumentNullException("name");
        }

        public ShopType Type
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
                        && string.IsNullOrWhiteSpace(value))
                        _name = value;
                    else
                        throw new ArgumentException("Cannot be empty or whitespace only!", "Name");
                else
                    throw new ArgumentNullException("Name");
            }
        }

        public Address Address
        {
            get
            {
                return _address;
            }
            set
            {
                if (value != null)
                    _address = value;
                else
                    throw new ArgumentNullException("Address");
            }
        }

        private string _name;
        private Address _address;
    }
}
