using System;

namespace FamilyExpenses.Model
{
    public class Address
    {
        public Address(string street, string city, string county, string country)
        {
            if (street != null)
                if (!string.IsNullOrEmpty(street)
                    && !string.IsNullOrWhiteSpace(street))
                    if (city != null)
                        if (!string.IsNullOrEmpty(city)
                            && !string.IsNullOrWhiteSpace(city))
                            if (county != null)
                                if (!string.IsNullOrEmpty(county)
                                    && !string.IsNullOrWhiteSpace(county))
                                    if (country != null)
                                        if (!string.IsNullOrEmpty(country)
                                            && !string.IsNullOrWhiteSpace(country))
                                        {
                                            _street = street;
                                            _city = city;
                                            _county = county;
                                            _country = country;
                                        }
                                        else
                                            throw new ArgumentException("Cannot be empty or whitespace only!", "country");
                                    else
                                        throw new ArgumentNullException("country");
                                else
                                    throw new ArgumentException("Cannot be empty or whitespace only!", "county");
                            else
                                throw new ArgumentNullException("county");
                        else
                            throw new ArgumentException("Cannot be empty or whitespace only!", "city");
                    else
                        throw new ArgumentNullException("city");
                else
                    throw new ArgumentException("Cannot be empty or whitespace only!", "street");
            else
                throw new ArgumentNullException("street");
        }

        public string Street
        {
            get
            {
                return _street;
            }
            set
            {
                if (value != null)
                    if (!string.IsNullOrEmpty(value)
                        && !string.IsNullOrWhiteSpace(value))
                        _street = value;
                    else
                        throw new ArgumentException("Cannot be empty or whitespace only!", "street");
                else
                    throw new ArgumentNullException("street");
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
                if (value != null)
                    if (!string.IsNullOrEmpty(value)
                        && !string.IsNullOrWhiteSpace(value))
                        _city = value;
                    else
                        throw new ArgumentException("Cannot be empty or whitespace only!", "City");
                else
                    throw new ArgumentNullException("City");
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
                if (value != null)
                    if (!string.IsNullOrEmpty(value)
                        && !string.IsNullOrWhiteSpace(value))
                        _county = value;
                    else
                        throw new ArgumentException("Cannot be empty or whitespace only!", "County");
                else
                    throw new ArgumentNullException("County");
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

        private string _street;
        private string _city;
        private string _county;
        private string _country;
    }
}
