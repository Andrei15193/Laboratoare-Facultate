using System;
namespace FamilyExpenses.Model
{
	public class Producer
	{
		public Producer(string name, string country)
		{
			if (name == null)
				throw new ArgumentNullException("name");
			if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
				throw new ArgumentException("Cannot be empty or whitespace only!", "name");
			if (country == null)
				throw new ArgumentNullException("country");
			if (string.IsNullOrEmpty(country) || string.IsNullOrWhiteSpace(country))
				throw new ArgumentException("Cannot be empty or whitespace only!", "country");
			_name = name.Trim();
			_country = country.Trim();
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
		public string Country
		{
			get
			{
				return _country;
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("Country");
				if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
					throw new ArgumentException("Cannot be empty or whitespace only!", "Country");
				_country = value;
			}
		}

		private string _name;
		private string _country;
	}
}