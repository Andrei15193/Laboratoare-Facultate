using System;
using Reservations.Model;
namespace Reservations.Server
{
	internal class RequestedAddShowEventArgs
		: EventArgs
	{
		internal RequestedAddShowEventArgs(Show newShow)
		{
			if (newShow == null)
				throw new ArgumentNullException("newShow");
			_newShow = newShow;
		}

		internal Show NewShow
		{
			get
			{
				return _newShow;
			}
		}

		private readonly Show _newShow;
	}
}