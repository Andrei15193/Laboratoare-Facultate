using System;
namespace FamilyExpenses.Model
{
	public struct Income
	{
		public Income(int sum, DateTime dateReceived, Person person)
		{
			if (person == null)
				throw new ArgumentNullException("person");
			_person = person;
			_sum = sum;
			_dateReceived = dateReceived;
		}

		public int Sum
		{
			get
			{
				return _sum;
			}
		}
		public DateTime DateReceived
		{
			get
			{
				return _dateReceived;
			}
		}
		public Person Person
		{
			get
			{
				return _person;
			}
		}

		private readonly int _sum;
		private readonly DateTime _dateReceived;
		private readonly Person _person;
	}
}