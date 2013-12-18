using System;
namespace FamilyExpenses.Model
{
	public class Person
	{
		public Person(string name, Currency preferedCurrency)
		{
			if (name == null)
				throw new ArgumentNullException("name");
			if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
				throw new ArgumentException("Cannot be empty or white space only!", "name");
			_name = name.Trim();
			PreferedCurrency = preferedCurrency;
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
				if (value == null)
					throw new ArgumentNullException("Name");
				if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
					throw new ArgumentException("Cannot be empty or white space only!", "Name");
				_name = value.Trim();
			}
		}

		private string _name;
	}
}