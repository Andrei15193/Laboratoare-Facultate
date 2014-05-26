using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Reservations.Model;
using Reservations.Model.Network;
using Reservations.Server;
using ReservationsServer.Logging;
namespace ReservationsServer
{
	internal class ShowAttendeeProxy
		: IDisposable
	{
		internal ShowAttendeeProxy(Socket showAttendeeSocket)
		{
			_clientSocket = showAttendeeSocket;

			_clientNetworkStream = new NetworkStream(showAttendeeSocket);
			_clientStreamWriter = new StreamWriter(_clientNetworkStream)
			{
				AutoFlush = true
			};
			_readTask = Task.Factory.StartNew(_ClientStreamReadTask);
			_clientAddress = ((IPEndPoint)_clientSocket.RemoteEndPoint).Address.ToString();
			Logger.WriteLineAsync("Connected", _clientAddress);
		}

		#region IDisposable Members
		public void Dispose()
		{
			_clientSocket.Dispose();
			_clientStreamWriter.Dispose();
			_clientNetworkStream.Dispose();
		}
		#endregion
		internal void SendShows(IEnumerable<Show> shows)
		{
			if (shows == null)
				throw new ArgumentNullException("shows");

			Logger.WriteLineFormatAsync("Sending all shows to: {0}", "Server", _clientAddress);
			_clientStreamWriter.Write(_showCommandNetworkSerializer.Serialize(new Command<Show>("shows", shows)));
			Logger.WriteLineFormatAsync("Sent all shows to: {0}", "Server", _clientAddress);
		}
		internal void SendShowReservations(IEnumerable<ShowReservation> showReservations)
		{
			if (showReservations == null)
				throw new ArgumentNullException("showReservations");

			_clientStreamWriter.Write(_showReservationCommandNetworkSerializer.Serialize(new Command<ShowReservation>("reservations", showReservations)));
		}

		internal event EventHandler<RequestedAddShowEventArgs> RequestedAddShow;
		internal event EventHandler<RequestedRemoveShowEventArgs> RequestedRemoveShow;
		internal event EventHandler<RequestedAddShowReservationsEventArgs> RequestedAddReservations;

		private void _ClientStreamReadTask()
		{
			try
			{
				StreamReader clientNetworkStreamReader = new StreamReader(_clientNetworkStream);

				do
					_ParseCommand(clientNetworkStreamReader.ReadLine());
				while (_clientSocket.Connected);
			}
			catch (IOException)
			{
				Logger.WriteLineAsync("Disconnected", _clientAddress);
			}
			catch (Exception exception)
			{
				Logger.WriteLineAsync(exception.ToString(), _clientAddress);
			}
		}
		private void _ParseCommand(string command)
		{
			Logger.WriteLineAsync(command, _clientAddress);

			if (command.StartsWith("{show_add:"))
				_AddShowCommand(_showCommandNetworkSerializer.Deserialize(command));
			else
				if (command.StartsWith("{show_remove:"))
					_RemoveShowCommand(_showCommandNetworkSerializer.Deserialize(command));
				else
					if (command.StartsWith("{reservation_add_"))
						_AddShowReservationCommand(_showReservationCommandNetworkSerializer.Deserialize(command));
		}
		private void _AddShowCommand(Command<Show> addNewShowCommand)
		{
			if (addNewShowCommand != null)
				foreach (Show newShow in addNewShowCommand.Parameters)
				{
					Logger.WriteLineFormatAsync("Requesting add new show: {0}, {1:dd MMM yyyy}",
												_clientAddress,
												newShow.Name,
												newShow.Scheduled);

					if (RequestedAddShow != null)
						RequestedAddShow(this, new RequestedAddShowEventArgs(newShow));

					Logger.WriteLineFormatAsync("Requested add new show: {0}, {1:dd MMM yyyy}",
												_clientAddress,
												newShow.Name,
												newShow.Scheduled);
				}
		}
		private void _RemoveShowCommand(Command<Show> removeShowCommand)
		{
			if (removeShowCommand != null)
				foreach (Show oldShow in removeShowCommand.Parameters)
				{
					Logger.WriteLineFormatAsync("Requesting remove show: {0}, {1:dd MMM yyyy}",
												_clientAddress,
												oldShow.Name,
												oldShow.Scheduled);

					if (RequestedRemoveShow != null)
						RequestedRemoveShow(this, new RequestedRemoveShowEventArgs(oldShow));

					Logger.WriteLineFormatAsync("Requested remove show: {0}, {1:dd MMM yyyy}",
												_clientAddress,
												oldShow.Name,
												oldShow.Scheduled);
				}
		}
		private void _AddShowReservationCommand(Command<ShowReservation> addNewShowReservationCommand)
		{
			if (addNewShowReservationCommand != null)
			{
				string[] personInfo = addNewShowReservationCommand.Name.Substring(16).Split(new[] { '_' }, 2);
				if (personInfo.Length == 2)
				{
					Logger.WriteLineFormatAsync("{0} {1} requesting reservations: {2}",
												_clientAddress,
												personInfo[1],
												personInfo[0],
												string.Join(", ", from showReservation in addNewShowReservationCommand.Parameters
																  select string.Join(" ", showReservation.HallLocation, showReservation.HallPlacement)));

					if (RequestedAddReservations != null)
						RequestedAddReservations(this, new RequestedAddShowReservationsEventArgs(personInfo[1], personInfo[0], addNewShowReservationCommand.Parameters));

					Logger.WriteLineFormatAsync("{0} {1} requested reservations",
												_clientAddress,
												personInfo[1],
												personInfo[0]);
				}
			}
		}

		private readonly Task _readTask;
		private readonly Socket _clientSocket;
		private readonly StreamWriter _clientStreamWriter;
		private readonly NetworkStream _clientNetworkStream;
		private readonly string _clientAddress;

		private static readonly INetworkSerializer<Show> _showNetworkSerializer = new ShowNetworkSerializer();
		private static readonly INetworkSerializer<Command<Show>> _showCommandNetworkSerializer = new CommandNetworkSerializer<Show>(_showNetworkSerializer);
		private static readonly INetworkSerializer<ShowReservation> _showReservationNetworkSerializer = new ShowReservationNetworkSerializer();
		private static readonly INetworkSerializer<Command<ShowReservation>> _showReservationCommandNetworkSerializer = new CommandNetworkSerializer<ShowReservation>(_showReservationNetworkSerializer);
	}
}