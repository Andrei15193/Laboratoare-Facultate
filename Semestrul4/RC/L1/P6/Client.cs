using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace RCLab1Client
{
    static class Program
    {
        public static void Main(string[] args)
        {
            int port;
            IPAddress serverAddress;
            if (args.Length >= 2)
                if (IPAddress.TryParse(args[0], out serverAddress))
                    if (int.TryParse(args[1], out port))
                        RunClient(serverAddress, port);
                    else
                        Console.WriteLine("Invalid port! Must be a number!");
                else
                    Console.WriteLine("Invalid server IP! Must be IPv4!");
            else
                Console.WriteLine("Usage: client <server-ip> <server-port>");
        }

        private static void RunClient(IPAddress serverAddress, int port)
        {
            string[] strings;
            Socket socket = null;
            IPEndPoint server = new IPEndPoint(serverAddress, port);
            try
            {
                strings = ReadStrings(ReadUShort("Enter the number of strings: "));
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(serverAddress, port);
                if (socket.Connected)
                {
                    SendStrings(strings, socket);
                    Console.WriteLine("Waiting to receive data from server");
                    Console.WriteLine("Response: {0}", ReadResult(socket));
                }
                else
                    Console.WriteLine("Failed to connect to " + server.Address + ":" + server.Port);
            }
            catch (SocketException)
            {
                Console.WriteLine("Invalid socket!");
            }
            finally
            {
                if (socket != null)
                    socket.Close();
            }
        }

        private static string ReadResult(Socket socket)
        {
            string read;
            byte[] buffer = new byte[100];
            StringBuilder builder = new StringBuilder();
            socket.Receive(buffer, sizeof(int), SocketFlags.None);
            switch (IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 0)))
            {
                case 0:
                    read = new string(Encoding.ASCII.GetChars(buffer, 0, socket.Receive(buffer)));
                    while (!read.Contains('\0'))
                    {
                        builder.Append(read);
                        read = new string(Encoding.ASCII.GetChars(buffer, 0, socket.Receive(buffer)));
                    }
                    builder.Append(read.Substring(0, read.IndexOf('\0')));
                    return builder.ToString();
                case -1:
                    return "Timedout while sending data";
                default:
                    return "Unknown error";
            }
        }

        private static void SendStrings(string[] strings, Socket socket)
        {
            socket.Send(BitConverter.GetBytes((ushort)IPAddress.HostToNetworkOrder((short)strings.Length)));
            foreach (string item in strings)
                // Had trouble disabling Nagle's algorithm to be able to send ~3 bytes of data.
                // Always sending 100 byte arrays for the server to read (it reads 100 bytes even if there are more messages making them magically disappear).
                socket.Send(Encoding.Convert(Encoding.Unicode, Encoding.ASCII, Encoding.Unicode.GetBytes(item + '\0' + new string(' ', 99 - item.Length))));
        }

        private static string[] ReadStrings(ushort n)
        {
            string[] strings = new string[n];
            for (ushort i = 0; i < n; i++)
            {
                Console.Write("string_{0} = ", i + 1);
                strings[i] = Console.ReadLine();
            }
            return strings;
        }

        private static ushort ReadUShort(string message)
        {
            ushort value;
            do
                Console.Write(message);
            while (!ushort.TryParse(Console.ReadLine(), out value));
            return value;
        }
    }
}
