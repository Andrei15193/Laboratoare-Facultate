using System;
namespace FamilyExpenses.Model
{
	public class Address
	{
		public Address(string street, string city, string county, string country)
		{
			if (street == null)
				throw new ArgumentNullException("street");
			if (string.IsNullOrEmpty(street) || string.IsNullOrWhiteSpace(street))
				throw new ArgumentException("Cannot be empty or whitespace only!", "street");
			if (city == null)
				throw new ArgumentNullException("city");
			if (string.IsNullOrEmpty(city) || string.IsNullOrWhiteSpace(city))
				throw new ArgumentException("Cannot be empty or whitespace only!", "city");
			if (county == null)
				throw new ArgumentNullException("county");
			if (string.IsNullOrEmpty(county) || string.IsNullOrWhiteSpace(county))
				throw new ArgumentException("Cannot be empty or whitespace only!", "county");
			if (country == null)
				throw new ArgumentNullException("country");
			if (string.IsNullOrEmpty(country) || string.IsNullOrWhiteSpace(country))
				throw new ArgumentException("Cannot be empty or whitespace only!", "country");
			_street = street;
			_city = city;
			_county = county;
			_country = country;
		}

		public string Street
		{
			get
			{
				return _street;
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("street");
				if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
					throw new ArgumentException("Cannot be empty or whitespace only!", "street");
				_street = value;
			}
		}
		public string City
		{
			get
			{
				return _city;
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("City");
				if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
					throw new ArgumentException("Cannot be empty or whitespace only!", "City");
				_city = value;
			}
		}
		public string County
		{
			get
			{
				return _county;
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("County");
				if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
					throw new ArgumentException("Cannot be empty or whitespace only!", "County");
				_county = value;
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

		private string _street;
		private string _city;
		private string _county;
		private string _country;
	}
}