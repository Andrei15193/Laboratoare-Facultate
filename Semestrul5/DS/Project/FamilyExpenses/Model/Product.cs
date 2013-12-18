using System;
namespace FamilyExpenses.Model
{
	public class Product
	{
		public Product(string name, ProductType type, Producer producer)
		{
			if (name == null)
				throw new ArgumentNullException("name");
			if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
				throw new ArgumentException("Cannot be empty or whitespace only!", "name");
			if (producer == null)
				throw new ArgumentNullException("producer");
			_name = name.Trim();
			_producer = producer;
			Type = type;
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
				if (value == null)
					throw new ArgumentNullException("Name");
				if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
					throw new ArgumentException("Cannot be empty or whitespace only!", "Name");
				_name = value;
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
				if (value == null)
					throw new ArgumentNullException("Producer");
				_producer = value;
			}
		}
		public override string ToString()
		{
			return _name + " by " + _producer.Name;
		}

		private string _name;
		private Producer _producer;
	}
}