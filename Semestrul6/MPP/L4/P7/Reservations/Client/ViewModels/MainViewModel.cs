using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Reservations.Client.UserInterface;
using Reservations.Model;
using Reservations.Model.Network;
using Reservations.UserInterface;
using Reservations.ViewModels.Commands;
namespace Reservations.ViewModels
{
	internal class MainViewModel
		: INotifyPropertyChanged, IDisposable
	{
		internal MainViewModel()
		{
			try
			{
				_serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				_serverSocket.Connect("localhost", 15193);
				_serverNetworkStream = new NetworkStream(_serverSocket);
				_serverStreamWriter = new StreamWriter(_serverNetworkStream)
				{
					AutoFlush = true
				};
				_readTask = Task.Factory.StartNew(_ReadFromServer);
			}
			catch (SocketException)
			{
				_serverNetworkStream = null;
			}

			_addShowCommand = new DelegateCommand(delegate
				{
					AddShowWindow addShowWindow = new AddShowWindow();

					if (addShowWindow.ShowDialog().GetValueOrDefault())
					{
						Show showToAdd = addShowWindow.NewShow;

						if (showToAdd != null)
							_serverStreamWriter.Write(_showCommandNetworkSerializer.Serialize(new Command<Show>("show_add", showToAdd)));
					}
				}, true);
			_deleteShowCommand = new DelegateCommand(parameter => _serverStreamWriter.Write(_showCommandNetworkSerializer.Serialize(new Command<Show>("show_remove", (Show)parameter))),
													 false);

			_reserveCommand = new DelegateCommand(delegate
				{
					ReservationDetailsWindow reservationDetailsWindow = new ReservationDetailsWindow();

					if (reservationDetailsWindow.ShowDialog().GetValueOrDefault())
					{
						Show selectedShow;
						lock (_showsLock)
							selectedShow = _shows.First(show => show.Scheduled.Date == DateTime.Now.Date);

						_serverStreamWriter.Write(_showReservationCommandNetworkSerializer.Serialize(new Command<ShowReservation>("reservation_add_" + reservationDetailsWindow.PersonPhoneNumber + "_" + reservationDetailsWindow.PersonName,
																																  _showReservationViewModels.SelectMany(reservationsPerPlacement => reservationsPerPlacement.Value.Select(reservationViewModel => new KeyValuePair<HallLocation, ShowReservationViewModel>(reservationsPerPlacement.Key, reservationViewModel.Value)))
																																							.Where(reservation => reservation.Value.IsReservationRequested)
																																							.Select(showReservationViewModel => new ShowReservation(selectedShow, showReservationViewModel.Key, showReservationViewModel.Value.HallLocationPlacement)))));

						foreach (ShowReservationViewModel showReservationViewModel in _showReservationViewModels.Values.SelectMany(showReservationViewModels => showReservationViewModels.Values))
							showReservationViewModel.IsReservationRequested = false;
					}
				}, false);
			_manageShowsCommand = new DelegateCommand(delegate
				{
					LoginWindow loginWindow = new LoginWindow();

					if (loginWindow.ShowDialog().GetValueOrDefault())
						if (string.Equals(loginWindow.Username, "Admin", StringComparison.Ordinal)
							&& string.Equals(loginWindow.Password, "12345", StringComparison.Ordinal))
							new ManageShowsWindow().ShowDialog();
						else
							MessageBox.Show("Numele de utilizator sau parola sunt invalide!", "Eroare!", MessageBoxButton.OK, MessageBoxImage.Error);
				}, true);

			IDictionary<HallLocation, IReadOnlyDictionary<string, ShowReservationViewModel>> reservations = new SortedList<HallLocation, IReadOnlyDictionary<string, ShowReservationViewModel>>();

			reservations.Add(HallLocation.Stal, _CreateStalPlacements());
			reservations.Add(HallLocation.Lodge1, _CreateLodgePlacements());
			reservations.Add(HallLocation.Lodge2, _CreateLodgePlacements());
			reservations.Add(HallLocation.Lodge3, _CreateLodgePlacements());
			reservations.Add(HallLocation.Lodge4, _CreateLodgePlacements());
			reservations.Add(HallLocation.Lodge5, _CreateLodgePlacements());
			reservations.Add(HallLocation.Lodge6, _CreateLodgePlacements());
			reservations.Add(HallLocation.Balcony, _CreateBalconyPlacements());

			_showReservationViewModels = new ReadOnlyDictionary<HallLocation, IReadOnlyDictionary<string, ShowReservationViewModel>>(reservations);
			foreach (ShowReservationViewModel reservationViewModel in _showReservationViewModels.Values.SelectMany(reservation => reservation.Values))
				reservationViewModel.PropertyChanged += (sender, e) =>
				{
					if (string.Equals("IsReservationRequested", e.PropertyName, StringComparison.Ordinal))
						_reserveCommand.CanExecuteCommand = (_shows.Any(show => show.Scheduled.Date == DateTime.Now.Date)
															 && _showReservationViewModels.Values.Any(reservationsPerPlacement => reservationsPerPlacement.Values.Any(reservation => reservation.IsReservationRequested))
															 && _showReservationViewModels.Values.SelectMany(reservationsPerPlacement => reservationsPerPlacement.Values).All(reservation => (reservation.IsReserved == false || reservation.IsReserved != reservation.IsReservationRequested)));
				};
		}

		#region INotifyPropertyChanged Members
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion
		#region IDisposable Members
		public void Dispose()
		{
			_readTask.Dispose();
			_serverSocket.Dispose();
			_serverNetworkStream.Dispose();
		}
		#endregion
		public IEnumerable<ShowReservationViewModel> StalReservations
		{
			get
			{
				return _showReservationViewModels[HallLocation.Stal].Values;
			}
		}
		public IEnumerable<ShowReservationViewModel> Lodge1Reservations
		{
			get
			{
				return _showReservationViewModels[HallLocation.Lodge1].Values;
			}
		}
		public IEnumerable<ShowReservationViewModel> Lodge2Reservations
		{
			get
			{
				return _showReservationViewModels[HallLocation.Lodge2].Values;
			}
		}
		public IEnumerable<ShowReservationViewModel> Lodge3Reservations
		{
			get
			{
				return _showReservationViewModels[HallLocation.Lodge3].Values;
			}
		}
		public IEnumerable<ShowReservationViewModel> Lodge4Reservations
		{
			get
			{
				return _showReservationViewModels[HallLocation.Lodge4].Values;
			}
		}
		public IEnumerable<ShowReservationViewModel> Lodge5Reservations
		{
			get
			{
				return _showReservationViewModels[HallLocation.Lodge5].Values;
			}
		}
		public IEnumerable<ShowReservationViewModel> Lodge6Reservations
		{
			get
			{
				return _showReservationViewModels[HallLocation.Lodge6].Values;
			}
		}
		public IEnumerable<ShowReservationViewModel> BalconyReservations
		{
			get
			{
				return _showReservationViewModels[HallLocation.Balcony].Values;
			}
		}

		public bool IsConnected
		{
			get
			{
				return (_serverNetworkStream != null);
			}
		}
		public Show SelectedShow
		{
			get
			{
				return _selectedShow;
			}
			set
			{
				_selectedShow = value;

				if (_selectedShow != null && _selectedShow.Scheduled.Date > DateTime.Now.Date)
					_deleteShowCommand.CanExecuteCommand = true;
				else
					_deleteShowCommand.CanExecuteCommand = false;

				OnPropertyChanged("SelectedShow");
			}
		}
		public IEnumerable<Show> Shows
		{
			get
			{
				lock (_showsLock)
					return _shows;
			}
			private set
			{
				lock (_showsLock)
				{
					if (value == null)
						throw new ArgumentNullException("Shows");

					_shows = value;
					OnPropertyChanged("Shows");
				}
			}
		}

		public ICommand AddShowCommand
		{
			get
			{
				return _addShowCommand;
			}
		}
		public ICommand DeleteShowCommand
		{
			get
			{
				return _deleteShowCommand;
			}
		}
		public ICommand ReserveCommand
		{
			get
			{
				return _reserveCommand;
			}
		}
		public ICommand ManageShowsCommand
		{
			get
			{
				return _manageShowsCommand;
			}
		}

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

		private IReadOnlyDictionary<string, ShowReservationViewModel> _CreateStalPlacements()
		{
			IDictionary<string, ShowReservationViewModel> stalPlacement = new SortedList<string, ShowReservationViewModel>();
			StringBuilder placementStringBuilder = new StringBuilder();

			for (int row = 1; row <= 10; row++)
				for (int column = 1; column <= 15; column++)
				{
					string placement = placementStringBuilder.Append("Randul ")
															 .AppendFormat("{0,2}", row.ToString())
															 .Append(", pozitia ")
															 .AppendFormat("{0,2}", column.ToString())
															 .ToString();
					stalPlacement.Add(placement, new ShowReservationViewModel(placement));

					placementStringBuilder.Clear();
				}

			return new ReadOnlyDictionary<string, ShowReservationViewModel>(stalPlacement);
		}
		private IReadOnlyDictionary<string, ShowReservationViewModel> _CreateLodgePlacements()
		{
			IDictionary<string, ShowReservationViewModel> stalPlacement = new SortedList<string, ShowReservationViewModel>();
			StringBuilder placementStringBuilder = new StringBuilder();

			for (int position = 1; position <= 15; position++)
			{
				string placement = placementStringBuilder.Append("Pozitia ")
														 .AppendFormat("{0,2}", position.ToString())
														 .ToString();
				stalPlacement.Add(placement, new ShowReservationViewModel(placement));

				placementStringBuilder.Clear();
			}

			return new ReadOnlyDictionary<string, ShowReservationViewModel>(stalPlacement);
		}
		private IReadOnlyDictionary<string, ShowReservationViewModel> _CreateBalconyPlacements()
		{
			IDictionary<string, ShowReservationViewModel> stalPlacement = new SortedList<string, ShowReservationViewModel>();
			StringBuilder placementStringBuilder = new StringBuilder();

			for (int row = 1; row <= 5; row++)
				for (int column = 1; column <= 10; column++)
				{
					string placement = placementStringBuilder.Append("Randul ")
															 .AppendFormat("{0,2}", row.ToString())
															 .Append(", pozitia ")
															 .AppendFormat("{0,2}", column.ToString())
															 .ToString();
					stalPlacement.Add(placement, new ShowReservationViewModel(placement));

					placementStringBuilder.Clear();
				}

			return new ReadOnlyDictionary<string, ShowReservationViewModel>(stalPlacement);
		}

		private void _ReadFromServer()
		{
			try
			{
				StreamReader serverNetworkStreamReader = new StreamReader(_serverNetworkStream);

				do
					_ParseCommand(serverNetworkStreamReader.ReadLine());
				while (_serverSocket.Connected);
			}
			catch
			{
			}
		}
		private void _ParseCommand(string serverCommand)
		{
			if (serverCommand.StartsWith("{shows"))
				Shows = _showCommandNetworkSerializer.Deserialize(serverCommand)
													 .Parameters
													 .Where(show => show != null)
													 .OrderBy(show => show.Scheduled)
													 .ToList();
			else
				if (serverCommand.StartsWith("{reservations"))
					foreach (ShowReservation showReservation in _showReservationCommandNetworkSerializer.Deserialize(serverCommand)
																										.Parameters
																										.Where(showReservation => showReservation != null))
						_showReservationViewModels[showReservation.HallLocation][showReservation.HallPlacement].IsReserved = true;
		}

		private Show _selectedShow;
		private IEnumerable<Show> _shows = new Show[0];
		private readonly object _showsLock = new object();

		private readonly ICommand _addShowCommand;
		private readonly DelegateCommand _deleteShowCommand;
		private readonly ICommand _manageShowsCommand;
		private readonly DelegateCommand _reserveCommand;
		private readonly IReadOnlyDictionary<HallLocation, IReadOnlyDictionary<string, ShowReservationViewModel>> _showReservationViewModels;

		private readonly Task _readTask;
		private readonly Socket _serverSocket;
		private readonly StreamWriter _serverStreamWriter;
		private readonly NetworkStream _serverNetworkStream;

		private static readonly INetworkSerializer<Show> _showNetworkSerializer = new ShowNetworkSerializer();
		private static readonly INetworkSerializer<Command<Show>> _showCommandNetworkSerializer = new CommandNetworkSerializer<Show>(_showNetworkSerializer);
		private static readonly INetworkSerializer<ShowReservation> _showReservationNetworkSerializer = new ShowReservationNetworkSerializer();
		private static readonly INetworkSerializer<Command<ShowReservation>> _showReservationCommandNetworkSerializer = new CommandNetworkSerializer<ShowReservation>(_showReservationNetworkSerializer);
	}
}