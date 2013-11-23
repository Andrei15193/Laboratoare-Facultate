using System;

namespace FamilyExpenses.Model
{
    public class Product
    {
        public Product(string name, ProductType type, Producer producer)
        {
            if (name != null)
                if (!string.IsNullOrEmpty(name)
                    && !string.IsNullOrWhiteSpace(name))
                    if (producer != null)
                    {
                        _name = name;
                        Type = type;
                    }
                    else
                        throw new ArgumentNullException("producer");
                else
                    throw new ArgumentException("Cannot be empty or whitespace only!", "name");
            else
                throw new ArgumentNullException("name");
        }

        public ProductType Type
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
                        _name = value;
                    else
                        throw new ArgumentException("Cannot be empty or whitespace only!", "Name");
                else
                    throw new ArgumentNullException("Name");
            }
        }

        public Producer Producer
        {
            get
            {
                return _producer;
            }
            set
            {
                if (value != null)
                    _producer = value;
                else
                    throw new ArgumentNullException("Producer");
            }
        }

        private string _name;
        private Producer _producer;
    }
}
