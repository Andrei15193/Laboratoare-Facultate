using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace RCLab2
{
    static class Server
    {
        public static void Main(string[] args)
        {
            int port;
            if (args.Length > 0)
                if (int.TryParse(args[0], out port))
                    StartServer(port);
                else
                    Console.WriteLine("Invalid port! Must be a number!");
            else
                Console.WriteLine("Usage: server <port>");
        }

        private static void StartServer(int port)
        {
            Socket socket;
            try
            {
                socket = MakeServer(port);
                try
                {
                    Console.WriteLine("Server started");
                    RunServer(socket);
                    Console.WriteLine("Server closed");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error while running server ");
                    Console.WriteLine(e.ToString());
                }
                finally
                {
                    socket.Dispose();
                }
            }
            catch (SocketException)
            {
                Console.WriteLine("Invalid socket or bind error!");
            }
        }

        private static void RunServer(Socket socket)
        {
            short number;
            byte[] read = new byte[sizeof(short)];
            ClientData clientData;
            EndPoint client = new IPEndPoint(IPAddress.Any, 0);
            IDictionary<EndPoint, ClientData> clients = new Dictionary<EndPoint, ClientData>();
            do
                if (socket.ReceiveFrom(read, read.Length, SocketFlags.None, ref client) == read.Length)
                {
                    number = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(read, 0));
                    Console.WriteLine(ParseRead(number, client));
                    if (clients.ContainsKey(client))
                    {
                        clientData = clients[client];
                        clientData.Add(number);
                        if (!clientData.NeedsToRead)
                        {
                            Console.WriteLine("Sending response to " + ParseClient(client));
                            SendToClient(socket, clientData.Extract(), client);
                            clients.Remove(client);
                        }
                    }
                    else
                        clients.Add(new KeyValuePair<EndPoint, ClientData>(client, new ClientData(number)));
                }
                else
                    Console.WriteLine("Did not receive everything from " + ParseClient(client));
            while (true);
        }

        private static string ParseClient(EndPoint client)
        {
            StringBuilder builder = new StringBuilder();
            if (client is IPEndPoint)
            {
                IPEndPoint clientEndPoint = client as IPEndPoint;
                builder.Append(clientEndPoint.Address);
                builder.Append(":");
                builder.Append(clientEndPoint.Port);
            }
            else
            {
                DnsEndPoint clientEndPoint = client as DnsEndPoint;
                builder.Append(clientEndPoint.Host);
                builder.Append(":");
                builder.Append(clientEndPoint.Port);
            }
            return builder.ToString();
        }

        private static string ParseRead(short number, EndPoint client)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Received ");
            builder.Append(number);
            builder.Append(" from ");
            builder.Append(ParseClient(client));
            return builder.ToString();
        }

        private static void SendToClient(Socket socket, IEnumerable<short> listToBeSent, EndPoint client)
        {
            byte[] terminator = new byte[sizeof(short)];
            for (int i = 0; i < terminator.Length; i++)
                terminator[i] = 0;
            foreach (short item in listToBeSent)
                socket.SendTo(BitConverter.GetBytes(IPAddress.HostToNetworkOrder(item)), sizeof(short), SocketFlags.None, client);
            socket.SendTo(terminator, terminator.Length, SocketFlags.None, client);
        }

        private static Socket MakeServer(int port)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.Bind(new IPEndPoint(IPAddress.Any, port));
            return socket;
        }
    }
}
