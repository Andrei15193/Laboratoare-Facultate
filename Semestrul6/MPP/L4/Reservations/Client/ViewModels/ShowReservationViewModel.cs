using System.ComponentModel;
namespace Reservations.ViewModels
{
	internal class ShowReservationViewModel
		: INotifyPropertyChanged
	{
		internal ShowReservationViewModel(string hallLocationPlacement)
		{
			_hallLocationPlacement = hallLocationPlacement;
		}

		#region INotifyPropertyChanged Members
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion
		public bool IsReserved
		{
			get
			{
				return _isReserved;
			}
			set
			{
				if (_isReserved != value)
				{
					_isReserved = value;
					OnPropertyChanged("IsReserved");
				}
			}
		}
		public bool IsReservationRequested
		{
			get
			{
				return _isReservationRequested;
			}
			set
			{
				if (_isReservationRequested != value)
				{
					_isReservationRequested = value;
					OnPropertyChanged("IsReservationRequested");
				}
			}
		}
		public string HallLocationPlacement
		{
			get
			{
				return _hallLocationPlacement;
			}
		}

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

		private bool _isReserved;
		private bool _isReservationRequested;
		private readonly string _hallLocationPlacement;
	}
}