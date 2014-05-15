using System;
namespace Reservations.Model
{
	public class ShowReservation
	{
		public ShowReservation(Show show, HallLocation hallLocation, string hallPlacement)
		{
			if (show == null)
				throw new ArgumentNullException("show");

			if (hallPlacement == null)
				throw new ArgumentNullException("hallPlacement");
			if (string.IsNullOrEmpty(hallPlacement) || string.IsNullOrWhiteSpace(hallPlacement))
				throw new ArgumentException("Cannot be empty or whitespace!", "hallPlacement");

			_show = show;
			_hallLocation = hallLocation;
			_hallPlacement = hallPlacement.Trim();
		}

		public Show Show
		{
			get
			{
				return _show;
			}
		}
		public HallLocation HallLocation
		{
			get
			{
				return _hallLocation;
			}
		}
		public string HallPlacement
		{
			get
			{
				return _hallPlacement;
			}
		}

		private readonly Show _show;
		private readonly HallLocation _hallLocation;
		private readonly string _hallPlacement;
	}
}