using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace RCLab2
{
    static class Client
    {
        static void Main(string[] args)
        {
            int port;
            IPAddress ipAddress;
            if (args.Length >= 0)
                if (IPAddress.TryParse("127.0.0.1", out ipAddress))
                    try
                    {
                        port = int.Parse("12345");
                        RunClient(ipAddress, port);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Invalid port! Must be a number!");
                    }
                else
                    Console.WriteLine("Invalid server IP (must be IPv4)");
            else
                Console.WriteLine("Usage: client <ip> <port>");
        }

        private static void RunClient(IPAddress ipAddress, int port)
        {
            short read, i = 0;
            Socket socket = null;
            EndPoint server = new IPEndPoint(ipAddress, port);
            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                Console.WriteLine("Enter two arrays. The end of an array is signaled with 0.");
                for (byte j = 1; j <= 2; j++, i = 0)
                    do
                    {
                        read = ReadShort(string.Format("array_{0}[{1}]=", j, i++));
                        try
                        {
                            socket.SendTo(BitConverter.GetBytes(IPAddress.HostToNetworkOrder(read)), sizeof(short), SocketFlags.None, server);
                        }
                        catch (SocketException)
                        {
                            Console.WriteLine("Error sending data.");
                        }
                    } while (read != 0);
                PrintResult(ReadResult(socket));
            }
            catch (SocketException)
            {
                Console.WriteLine("Did not receive result from server");
            }
            finally
            {
                Console.ReadKey();
                if (socket != null)
                    socket.Dispose();
            }
        }

        private static void PrintResult(IEnumerable<short> list)
        {
            Console.WriteLine("Server response ({0} numbers):", list.Count());
            foreach (short item in list)
                Console.Write(item + " ");
            Console.WriteLine();
        }

        private static IEnumerable<short> ReadResult(Socket socket)
        {
            byte[] bytesRead = new byte[sizeof(short)];
            short read;
            EndPoint server = new IPEndPoint(IPAddress.Any, 0);
            LinkedList<short> result = new LinkedList<short>();
            if (socket.Poll(5000, SelectMode.SelectRead))
            {
                socket.ReceiveFrom(bytesRead, bytesRead.Length, SocketFlags.None, ref server);
                read = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(bytesRead, 0));
                while (read != 0)
                {
                    result.AddLast(read);
                    socket.ReceiveFrom(bytesRead, bytesRead.Length, SocketFlags.None, ref server);
                    read = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(bytesRead, 0));
                }
            }
            return result;
        }

        private static short ReadShort(string message)
        {
            short result;
            do
                Console.Write(message);
            while (!short.TryParse(Console.ReadLine(), out result));
            return result;
        }
    }
}
