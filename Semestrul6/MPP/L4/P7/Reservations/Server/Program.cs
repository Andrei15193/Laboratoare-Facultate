using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Xml.Linq;
using Reservations.Model;
using Reservations.Model.Network;
using Reservations.Server;
using ReservationsServer.Logging;
namespace ReservationsServer
{
	internal static class Program
	{
		internal static void Main(string[] args)
		{
			bool run = true;
			try
			{
				Logger.Instance = new ConsoleLogger(DebugLogger.Instance);

				Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				server.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), Constants.TcpPort));
				server.Listen(10);

				while (run)
				{
					Socket clientSocket = server.Accept();
					ShowAttendeeProxy showAttendeeProxy = new ShowAttendeeProxy(clientSocket);

					showAttendeeProxy.RequestedAddShow += _ClientRequestedAddShow;
					showAttendeeProxy.RequestedRemoveShow += _ClientRequestedRemoveShow;
					showAttendeeProxy.RequestedAddReservations += _ClientRequestedAddReservations;

					lock (_fileLock)
					{
						XDocument xDocument = _LoadXmlDocument();
						string today =  DateTime.Now.ToString("yyyy-MM-dd");

						showAttendeeProxy.SendShows(xDocument.Root.Elements("Show").Select(showXElement => new Show(showXElement.Attribute("Name").Value, DateTime.ParseExact(showXElement.Attribute("Scheduled").Value, "yyyy-MM-dd", null))));
						
						XElement showXmlElement = xDocument.Root.Elements("Show").FirstOrDefault(showXElement => string.Equals( showXElement.Attribute("Scheduled").Value, today, StringComparison.Ordinal));
						if (showXmlElement != null)
						{
							Show show = new Show(showXmlElement.Attribute("Name").Value, DateTime.ParseExact(showXmlElement.Attribute("Scheduled").Value, "yyyy-MM-dd", null));
							showAttendeeProxy.SendShowReservations(showXmlElement.Elements("Reservations").SelectMany(reservationXElement => reservationXElement.Elements("Reservation"))
																				 .Select(reservationXElement => new ShowReservation(show, (HallLocation)Enum.Parse(typeof(HallLocation), reservationXElement.Attribute("HallLocation").Value), reservationXElement.Attribute("HallPlacement").Value)));
						}
					}

					_attendeeProxies.Add(showAttendeeProxy);
				}
			}
			catch (Exception exception)
			{
				Logger.WriteLineAsync(exception.ToString(), "Server");
			}
			finally
			{
				foreach (ShowAttendeeProxy attendeeProxy in _attendeeProxies)
					attendeeProxy.Dispose();

				Logger.AwaitLogging();
			}
		}

		private static void _ClientRequestedAddShow(object sender, RequestedAddShowEventArgs e)
		{
			lock (_fileLock)
			{
				XDocument xDocument = _LoadXmlDocument();

				if (xDocument.Root.Elements("Show").All(showXElement => e.NewShow.Scheduled != DateTime.ParseExact(showXElement.Attribute("Scheduled").Value, "yyyy-MM-dd", null)))
				{
					xDocument.Root.Add(new XElement("Show",
													new XAttribute("Name", e.NewShow.Name),
													new XAttribute("Scheduled", e.NewShow.Scheduled.ToString("yyyy-MM-dd"))));

					xDocument.Save(_fileName);
					Logger.WriteLineFormatAsync("Added new show: {0}, {1:dd MMM yyyy}", "Server", e.NewShow.Name, e.NewShow.Scheduled);

					_SendToClients(xDocument.Root.Elements("Show").Select(showXElement => new Show(showXElement.Attribute("Name").Value, DateTime.ParseExact(showXElement.Attribute("Scheduled").Value, "yyyy-MM-dd", null))));
					Logger.WriteLineAsync("Sent show list to clients", "Server");
				}
				else
					Logger.WriteLineFormatAsync("Could not add new show: {0}, {1:dd MMM yyyy} (day already taken)", "Server", e.NewShow.Name, e.NewShow.Scheduled);
			}
		}
		private static void _ClientRequestedRemoveShow(object sender, RequestedRemoveShowEventArgs e)
		{
			lock (_fileLock)
			{
				XDocument xDocument = _LoadXmlDocument();

				string scheduled = e.OldShow.Scheduled.ToString("yyyy-MM-dd");
				XElement showXElement = xDocument.Root.Elements("Show").FirstOrDefault(showXmlElement => showXmlElement.Attribute("Scheduled").Value == scheduled);

				if (showXElement != null)
				{
					showXElement.Remove();
					xDocument.Save(_fileName);
					Logger.WriteLineFormatAsync("Removed show: {0}, {1:dd MMM yyyy}", "Server", e.OldShow.Name, e.OldShow.Scheduled);

					_SendToClients(xDocument.Root.Elements("Show").Select(showXmlElement => new Show(showXmlElement.Attribute("Name").Value, DateTime.ParseExact(showXmlElement.Attribute("Scheduled").Value, "yyyy-MM-dd", null))));
					Logger.WriteLineAsync("Sent show list to clients", "Server");
				}
				else
					Logger.WriteLineFormatAsync("Could not remove show: {0}, {1:dd MMM yyyy} (does not exist)", "Server", e.OldShow.Name, e.OldShow.Scheduled);
			}
		}
		private static void _ClientRequestedAddReservations(object sender, RequestedAddShowReservationsEventArgs e)
		{
			lock (_fileLock)
			{
				XDocument xDocument = _LoadXmlDocument();

				foreach (IGrouping<DateTime, ShowReservation> showReservationsBySchedule in e.ShowReservations.GroupBy(showReservation => showReservation.Show.Scheduled))
				{

					string showScheduled = showReservationsBySchedule.Key.ToString("yyyy-MM-dd");
					XElement showXElement = xDocument.Root.Elements("Show").FirstOrDefault(showXmlElement => string.Equals(showScheduled, showXmlElement.Attribute("Scheduled").Value, StringComparison.Ordinal));

					showXElement.Add(new XElement("Reservations",
									 new XAttribute("PersonaName", e.PersonName),
									 new XAttribute("PersonTelephoneNumber", e.PersonTelephoneNumber),
									 showReservationsBySchedule.Select(showReservation => new XElement("Reservation",
																									   new XAttribute("HallLocation", showReservation.HallLocation.ToString()),
																									   new XAttribute("HallPlacement", showReservation.HallPlacement)))));

					xDocument.Save(_fileName);
					Logger.WriteLineFormatAsync("Added reservations ({0}): {1}",
												e.PersonName,
												"Server",
												string.Join(", ", e.ShowReservations.Select(showReservation => string.Format("{0}, {1}", showReservation.HallLocation.ToString(), showReservation.HallPlacement))));
				}

				_SendShowReservationsToClient(xDocument);
			}
		}

		private static void _SendShowReservationsToClient(XDocument xDocument)
		{
			string todaySchedule = DateTime.Now.ToString("yyyy-MM-dd");
			XElement showXElement = xDocument.Root.Elements("Show").FirstOrDefault(showXmlElement => string.Equals(todaySchedule, showXmlElement.Attribute("Scheduled").Value, StringComparison.Ordinal));

			if (showXElement != null)
			{
				Show show = new Show(showXElement.Attribute("Name").Value, DateTime.Now);
				IList<ShowAttendeeProxy> disconectedShowAttendees = new List<ShowAttendeeProxy>();
				IEnumerable<ShowReservation> todaysShowReservations = showXElement.Elements("Reservations")
																				  .SelectMany(reservationsXmlElement => reservationsXmlElement.Elements("Reservation"))
																				  .Select(reservationXmlElement => new ShowReservation(show,
																																	   (HallLocation)Enum.Parse(typeof(HallLocation), reservationXmlElement.Attribute("HallLocation").Value),
																																	   reservationXmlElement.Attribute("HallPlacement").Value))
																				  .ToList();

				foreach (ShowAttendeeProxy attendeeProxy in _attendeeProxies)
					try
					{
						attendeeProxy.SendShowReservations(todaysShowReservations);
					}
					catch (SocketException)
					{
						disconectedShowAttendees.Add(attendeeProxy);
					}

				foreach (ShowAttendeeProxy disconectedShowAttendee in disconectedShowAttendees)
					_attendeeProxies.Remove(disconectedShowAttendee);
			}
			else
				Logger.WriteLineFormat("No show schedueld for today {0:dd MMM yyyy}", "Server", DateTime.Now);
		}

		private static void _SendToClients(IEnumerable<Show> shows)
		{
			lock (_attendeeProxiesLock)
			{
				IList<ShowAttendeeProxy> disconectedShowAttendees = new List<ShowAttendeeProxy>();

				foreach (ShowAttendeeProxy attendeeProxy in _attendeeProxies)
					try
					{
						attendeeProxy.SendShows(shows);
					}
					catch (Exception)
					{
						disconectedShowAttendees.Add(attendeeProxy);
					}

				foreach (ShowAttendeeProxy disconectedShowAttendee in disconectedShowAttendees)
					_attendeeProxies.Remove(disconectedShowAttendee);
			}
		}
		private static XDocument _LoadXmlDocument()
		{
			if (File.Exists(_fileName))
				return XDocument.Load(_fileName);
			else
				return new XDocument(new XElement("Reservations"));
		}

		private static string _fileName = "shows.xml";
		private static object _fileLock = new object();
		private static object _attendeeProxiesLock = new object();
		private static IList<ShowAttendeeProxy> _attendeeProxies = new List<ShowAttendeeProxy>();

		private static readonly INetworkSerializer<Show> _showNetworkSerializer = new ShowNetworkSerializer();
		private static readonly INetworkSerializer<Command<Show>> _showCommandNetworkSerializer = new CommandNetworkSerializer<Show>(_showNetworkSerializer);
		private static readonly INetworkSerializer<ShowReservation> _showReservationNetworkSerializer = new ShowReservationNetworkSerializer();
		private static readonly INetworkSerializer<Command<ShowReservation>> _showReservationCommandNetworkSerializer = new CommandNetworkSerializer<ShowReservation>(_showReservationNetworkSerializer);
	}
}