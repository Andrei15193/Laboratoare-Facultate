using System;
namespace FamilyExpenses.Model
{
	public class Shop
	{
		public Shop(string name, ShopType type, Address address)
		{
			if (name == null)
				throw new ArgumentNullException("name");
			if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
				throw new ArgumentException("Cannot be empty or whitespace only!", "name");
			if (address == null)
				throw new ArgumentNullException("address");
			_name = name;
			_address = address;
			Type = type;
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
				if (value == null)
					throw new ArgumentNullException("Name");
				if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
					throw new ArgumentException("Cannot be empty or whitespace only!", "Name");
				_name = value;
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
				if (value == null)
					throw new ArgumentNullException("Address");
				_address = value;
			}
		}
		public override string ToString()
		{
			return _name + " on " + _address.Street + ", " + _address.City;
		}

		private string _name;
		private Address _address;
	}
}