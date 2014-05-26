using System;
namespace Reservations.Model
{
	public class Show
	{
		public Show(string name, DateTime scheduled)
		{
			if (name == null)
				throw new ArgumentNullException("name");
			if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
				throw new ArgumentException("cannot be empty or whitespace!", "name");

			_name = name.Trim();
			_scheduled = scheduled;
		}

		public string Name
		{
			get
			{
				return _name;
			}
		}
		public DateTime Scheduled
		{
			get
			{
				return _scheduled;
			}
		}

		private readonly string _name;
		private readonly DateTime _scheduled;
	}
}