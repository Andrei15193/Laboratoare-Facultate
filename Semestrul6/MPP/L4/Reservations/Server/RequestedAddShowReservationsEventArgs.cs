using System;
using System.Collections.Generic;
using Reservations.Model;
namespace Reservations.Server
{
	internal class RequestedAddShowReservationsEventArgs
		: EventArgs
	{
		internal RequestedAddShowReservationsEventArgs(string personName, string personTelephoneNumber, IReadOnlyList<ShowReservation> showReservations)
		{
			if (personName == null)
				throw new ArgumentNullException("personName");
			if (string.IsNullOrEmpty(personName) || string.IsNullOrWhiteSpace(personName))
				throw new ArgumentException("Cannot be empty or whitespace!", "personName");

			if (personTelephoneNumber == null)
				throw new ArgumentNullException("personTelephoneNumber");
			if (string.IsNullOrEmpty(personTelephoneNumber) || string.IsNullOrWhiteSpace(personTelephoneNumber))
				throw new ArgumentException("Cannot be empty or whitespace!", "personTelephoneNumber");

			if (showReservations == null)
				throw new ArgumentNullException("showReservations");

			_personName = personName;
			_personTelephoneNumber = personTelephoneNumber;
			_showReservations = showReservations;
		}
		
		internal string PersonName
		{
			get
			{
				return _personName;
			}
		}
		internal string PersonTelephoneNumber
		{
			get
			{
				return _personTelephoneNumber;
			}
		}
		internal IReadOnlyList<ShowReservation> ShowReservations
		{
			get
			{
				return _showReservations;
			}
		}

		private readonly string _personName;
		private readonly string _personTelephoneNumber;
		private readonly IReadOnlyList<ShowReservation> _showReservations;
	}
}