using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RCLab4
{
    static class Program
    {
        private static ConcurrentBag<Task> proxies = new ConcurrentBag<Task>();
        private static ConcurrentDictionary<IPEndPoint, _ProxyInfo> proxyEndPoints = new ConcurrentDictionary<IPEndPoint, _ProxyInfo>();
        private static int bufferSize = 10000;

        static void Main(string[] args)
        {
            if (args.Length > 0)
                if (File.Exists(args[0]))
                    _Run(GetProxyInfos(args[0]));
                else
                    Console.WriteLine("The specified config file does not exist!");
            else
                Console.WriteLine("No config file provided! Please specify the config file as the first command line argument.");
        }

        private static IEnumerable<_ProxyInfo> GetProxyInfos(string configFile)
        {
            StreamReader configReader = null;
            ICollection<_ProxyInfo> proxyInfos = new LinkedList<_ProxyInfo>();
            try
            {
                char[] separators = new char[] { ',' };
                configReader = File.OpenText(configFile);
                while (!configReader.EndOfStream)
                {
                    int incommingPort, outgoingPort;
                    string[] rawProxyInfo = configReader.ReadLine().Split(separators, 3);
                    if (int.TryParse(rawProxyInfo[0], out incommingPort) && int.TryParse(rawProxyInfo[2], out outgoingPort))
                        proxyInfos.Add(new _ProxyInfo(incommingPort, rawProxyInfo[1], outgoingPort));
                }
            }
            catch (IOException)
            {
            }
            finally
            {
                if (configReader != null)
                    configReader.Close();
            }
            return proxyInfos;
        }

        private static void _Run(IEnumerable<_ProxyInfo> proxyInfos)
        {
            foreach (_ProxyInfo proxyInfo in proxyInfos)
                proxies.Add(Task.Factory.StartNew(DoProxyJob, proxyInfo));
            Thread.Sleep(-1);
        }

        private static void DoProxyJob(object state)
        {
            _ProxyInfo proxyInfo = state as _ProxyInfo;
            if (proxyInfo != null)
            {
                try
                {
                    using (Socket proxySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                    {
                        IPEndPoint proxyEndPoint = new IPEndPoint(IPAddress.Any, proxyInfo.IncomingPort);
                        if (!Bind(proxySocket, proxyEndPoint))
                            WriteMessage("Cannot bind to 127.0.0.1:{0}!", proxyInfo.IncomingPort);
                        else
                        {
                            WriteMessage("Bound to 127.0.0.1:{0}!", proxyInfo.IncomingPort);
                            proxySocket.Listen(5);
                            if (proxyEndPoints.TryAdd(proxyEndPoint, proxyInfo))
                                StartProxyJob(proxySocket, proxyEndPoint);
                        }
                    }
                }
                catch (SocketException)
                {
                    WriteMessage("Could not create socket for {0}:{1}!", proxyInfo.OutgoingAddress, proxyInfo.OutgoingPort);
                }
            }
        }

        private static void StartProxyJob(Socket proxySocket, IPEndPoint proxyEndPoint)
        {
            Console.WriteLine("Starting job...");
            _ProxyInfo proxyInfo;
            ConcurrentBag<Task> clientTasks = new ConcurrentBag<Task>();
            CancellationTokenSource clientCancellation = new CancellationTokenSource();
            while (proxyEndPoints.TryGetValue(proxyEndPoint, out proxyInfo))
                if (proxySocket.Poll(1000, SelectMode.SelectRead))
                    Task.Factory.StartNew(DoClientJob, new _ClientJobState(proxySocket.Accept(), proxyInfo), clientCancellation.Token);
            clientCancellation.Cancel();
            Console.WriteLine("Ending job...");
        }

        private static void DoClientJob(object state)
        {
            _ClientJobState clientState = state as _ClientJobState;
            using (Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                int i = 0;
                IPAddress[] hostAddresses = Dns.GetHostAddresses(clientState.ProxyInfo.OutgoingAddress);
                while (i < hostAddresses.Length && !serverSocket.Connected)
                    serverSocket.Connect(new IPEndPoint(hostAddresses[i++], clientState.ProxyInfo.OutgoingPort));
                if (serverSocket.Connected)
                    StartClientJob(serverSocket, clientState.ClientSocket);
                clientState.ClientSocket.Dispose();
            }
        }

        private static void StartClientJob(Socket destination, Socket source)
        {
            Console.WriteLine("Starting client job...");
            Parallel.Invoke(
                () =>
                {
                    try
                    {
                        byte[] buffer = new byte[bufferSize];
                        while (source.Connected || source.Poll(5000, SelectMode.SelectRead))
                            destination.Send(buffer, source.Receive(buffer), SocketFlags.None);
                    }
                    catch (SocketException exception)
                    {
                        Debug.WriteLine(exception);
                    }
                },
                () =>
                {
                    try
                    {
                        byte[] buffer = new byte[bufferSize];
                        while (destination.Connected || destination.Poll(5000, SelectMode.SelectRead))
                            source.Send(buffer, destination.Receive(buffer), SocketFlags.None);
                    }
                    catch (SocketException exception)
                    {
                        Debug.WriteLine(exception);
                    }
                });
            Console.WriteLine("Ending client job...");
        }

        private static bool Bind(Socket proxySocket, IPEndPoint endPoint)
        {
            try
            {
                proxySocket.Bind(endPoint);
                return proxySocket.IsBound;
            }
            catch (SocketException)
            {
                return false;
            }
        }

        private static void WritePointer()
        {
            Console.WriteLine("  > ");
        }

        private static void WriteMessage(bool withPointer, string format, params object[] objects)
        {
            Console.WriteLine();
            Console.WriteLine(format, objects);
            if (withPointer)
                WritePointer();
        }

        private static void WriteMessage(string format, params object[] objects)
        {
            WriteMessage(false, format, objects);
        }

        private class _ProxyInfo
        {
            public _ProxyInfo(int incomingPort, string outgoingAddress, int outgoingPort)
            {
                IncomingPort = incomingPort;
                OutgoingAddress = outgoingAddress;
                OutgoingPort = outgoingPort;
            }

            public override string ToString()
            {
                return string.Format(" > 127.0.0.1:{0} | < {1}:{2}", IncomingPort, OutgoingAddress, OutgoingPort);
            }

            public int IncomingPort { get; private set; }

            public int OutgoingPort { get; private set; }

            public string OutgoingAddress { get; private set; }
        }

        private class _ClientJobState
        {
            public _ClientJobState(Socket clientSocket, _ProxyInfo proxyInfo)
            {
                ClientSocket = clientSocket;
                ProxyInfo = proxyInfo;
            }

            public Socket ClientSocket;

            public _ProxyInfo ProxyInfo;
        }
    }
}
