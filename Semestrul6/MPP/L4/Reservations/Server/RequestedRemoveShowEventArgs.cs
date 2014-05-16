using System;
using Reservations.Model;
namespace Reservations.Server
{
	internal class RequestedRemoveShowEventArgs
		: EventArgs
	{
		internal RequestedRemoveShowEventArgs(Show oldShow)
		{
			if (oldShow == null)
				throw new ArgumentNullException("oldShow");
			_oldShow = oldShow;
		}

		internal Show OldShow
		{
			get
			{
				return _oldShow;
			}
		}

		private readonly Show _oldShow;
	}
}
