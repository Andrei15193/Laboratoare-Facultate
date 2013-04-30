using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UDPMessenger.BusinessEntities;

namespace UDPMessenger.BusinessComponents
{
    partial class Messenger : IDisposable
    {
        public Messenger(bool isLogging = false)
        {
            IsLogging = isLogging;
            BroadcastAddresses = from broadcastIp in _GetBroadcastAddreses()
                                 select new IPEndPoint(new IPAddress(broadcastIp), Port);
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _wasThisDisposed = false;
            _isRunning = false;
            _runsReaderWriterLock = new ReaderWriterLock();
            _onlineUsers = new Dictionary<IPEndPoint, User>();
            _signalThread = new Thread(_SignalThreadMethod);
            _signalThread.IsBackground = false;
            _signalThread.Name = "Signal thread";
            _readerThread = new Thread(_ReaderThreadMethod);
            _readerThread.IsBackground = false;
            _readerThread.Name = "Reader thread";
            if (IsLogging)
                _StartLog();
        }

        public void Start()
        {
            if (Port < 1024)
                throw new ApplicationException("The port cannot be bellow 1024");
            else if (Username == null || Username == string.Empty)
                throw new ApplicationException("The name must contain at least one character");
            else
            {
                _isRunning = true;
                _socket.Bind(new IPEndPoint(IPAddress.Any, Port));
                if (!_socket.IsBound)
                    throw new ApplicationException("Could not bind to port " + Port);
                else
                {
                    _signalThread.Start();
                    _readerThread.Start();
                }
            }
        }

        public void Dispose()
        {
            _Dispose(true);
        }

        public void SendPublicMessage(string message)
        {
            StringBuilder messageBuilder = new StringBuilder("MESSAGE");
            messageBuilder.Append(':');
            messageBuilder.Append(message);
            messageBuilder.Append('\0');
            foreach (IPEndPoint broadcastAddress in BroadcastAddresses)
                _Send(messageBuilder.ToString(), broadcastAddress, MessageScope.Public);
        }

        public void SendPrivateMessage(string message, User user)
        {
            IPEndPoint userEndPoint;
            if (_TryGetUserEndpoint(user, out userEndPoint))
            {
                StringBuilder messageBuilder = new StringBuilder("PRIVATE");
                messageBuilder.Append(':');
                messageBuilder.Append(message);
                messageBuilder.Append('\0');
                _Send(messageBuilder.ToString(), userEndPoint, MessageScope.Private, user);
            }
        }

        public bool IsLogging { get; private set; }

        public int Port { get; set; }

        public string Username { get; set; }

        public IEnumerable<IPEndPoint> BroadcastAddresses { get; private set; }

        public event EventHandler<MessageEventArgs> MessageSent;

        public event EventHandler<MessageEventArgs> MessageReceived;

        public event EventHandler<UserEventArgs> UserCameOnline;

        public event EventHandler<UserEventArgs> UserWentOffline;

        ~Messenger()
        {
            _Dispose(false);
        }

        private static void _StartLog()
        {
            StreamWriter logFileStream = null;
            try
            {
                logFileStream = File.AppendText("log.txt");
                logFileStream.WriteLine("{0:dddd, d MMMM, yyyy. HH:mm:ss :FFFFFFF} started logging", DateTime.Now);
                logFileStream.WriteLine(_separator);
                logFileStream.WriteLine();
            }
            finally
            {
                if (logFileStream != null)
                    logFileStream.Close();
            }
        }

        private static void _Log(string[] receivedMessage, IPEndPoint senderEndPoint)
        {
            StreamWriter logFileStream = null;
            try
            {
                logFileStream = File.AppendText("log.txt");
                logFileStream.WriteLine("{0} @ {1:dddd, d MMMM, yyyy. HH:mm:ss :FFFFFFF}", string.Join(".", senderEndPoint.Address.GetAddressBytes().Select((ipByte) => ipByte.ToString())), DateTime.Now);
                logFileStream.WriteLine("    {0}:{1}", receivedMessage[0], receivedMessage[1]);
                logFileStream.WriteLine();
            }
            finally
            {
                if (logFileStream != null)
                    logFileStream.Close();
            }
        }

        private void _EndLog()
        {
            StreamWriter logFileStream = null;
            try
            {
                logFileStream = File.AppendText("log.txt");
                logFileStream.WriteLine("{0:dddd, d MMMM, yyyy. HH:mm:ss :FFFFFFF} finished logging", DateTime.Now);
                logFileStream.WriteLine(_separator);
                logFileStream.WriteLine();
            }
            finally
            {
                if (logFileStream != null)
                    logFileStream.Close();
            }
        }

        private static IEnumerable<byte[]> _GetBroadcastAddreses()
        {
            return (from broadcastAddress in
                        from networkInterface in NetworkInterface.GetAllNetworkInterfaces()
                        let unicastAddresses = networkInterface.GetIPProperties().UnicastAddresses
                        from unicastAddress in unicastAddresses
                        where
                            unicastAddress.IPv4Mask != null
                            &&
                            unicastAddress.Address != null
                            &&
                            unicastAddress.Address.AddressFamily == AddressFamily.InterNetwork
                        select _GetBroadcastIPv4Address(unicastAddress.Address.GetAddressBytes(), unicastAddress.IPv4Mask.GetAddressBytes())
                    where broadcastAddress[0] != 127
                    select broadcastAddress).Distinct(new _IPv4BroadcastEqualityComparer());
        }

        private static byte[] _GetBroadcastIPv4Address(byte[] ip, byte[] netmask)
        {
            byte[] broadcast = new byte[ip.Length];
            for (int i = 0; i < ip.Length; i++)
                broadcast[i] = (byte)(~netmask[i] | ip[i]);
            return broadcast;
        }

        private static byte[] _GetMessageBytes(string message)
        {
            return Encoding.Convert(Encoding.Unicode, Encoding.ASCII, Encoding.Unicode.GetBytes(message));
        }

        private void _Dispose(bool isDisposed)
        {
            if (!_wasThisDisposed)
            {
                if (isDisposed)
                {
                    _runsReaderWriterLock.AcquireWriterLock(-1);
                    bool isJoinRequired = _isRunning;
                    _isRunning = false;
                    _runsReaderWriterLock.ReleaseWriterLock();
                    if (isJoinRequired)
                    {
                        _signalThread.Join();
                        _readerThread.Join();
                    }
                    _socket.Dispose();
                }
                BroadcastAddresses = null;
                _socket = null;
                Username = null;
                _onlineUsers = null;
                _signalThread = null;
                _readerThread = null;
                _runsReaderWriterLock = null;
                _wasThisDisposed = true;
                if (IsLogging)
                    _EndLog();
            }
        }

        private void _ReaderThreadMethod(object obj)
        {
            try
            {
                _runsReaderWriterLock.AcquireReaderLock(-1);
                while (_isRunning)
                {
                    _runsReaderWriterLock.ReleaseReaderLock();
                    _ReceiveMessages(1000);
                    _runsReaderWriterLock.AcquireReaderLock(-1);
                }
                _runsReaderWriterLock.ReleaseReaderLock();
            }
            catch (Exception)
            {
            }
            finally
            {
                _runsReaderWriterLock.ReleaseLock();
            }
        }

        private void _SignalThreadMethod(object obj)
        {
            try
            {
                _runsReaderWriterLock.AcquireReaderLock(-1);
                while (_isRunning)
                {
                    _runsReaderWriterLock.ReleaseReaderLock();
                    _SendJoinMessage();
                    Thread.Sleep(1000);
                    _ClearInactiveUsers();
                    _runsReaderWriterLock.AcquireReaderLock(-1);
                }
                _SendLeaveMessage();
                _runsReaderWriterLock.ReleaseReaderLock();
            }
            catch (Exception)
            {
            }
            finally
            {
                _runsReaderWriterLock.ReleaseLock();
            }
        }

        private void _ClearInactiveUsers()
        {
            lock (_onlineUsers)
            {
                foreach (KeyValuePair<IPEndPoint, User> onlineUser in _onlineUsers)
                    onlineUser.Value.OnlineTimeout--;
                foreach (IPEndPoint userIpEndPoint in (from onlineUser in _onlineUsers
                                                       where onlineUser.Value.OnlineTimeout == 0
                                                       select onlineUser.Key).ToList())
                    _RemoveUser(userIpEndPoint);
            }
        }

        private void _SendJoinMessage()
        {
            StringBuilder messageBuilder = new StringBuilder("JOIN:");
            messageBuilder.Append(Username);
            messageBuilder.Append('\0');
            foreach (IPEndPoint broadcastAddress in BroadcastAddresses)
                _Send(messageBuilder.ToString(), broadcastAddress, MessageScope.Public);
        }

        private void _SendLeaveMessage()
        {
            StringBuilder messageBuilder = new StringBuilder("LEAVE:");
            messageBuilder.Append(Username);
            messageBuilder.Append('\0');
            foreach (IPEndPoint broadcastAddress in BroadcastAddresses)
                _Send(messageBuilder.ToString(), broadcastAddress, MessageScope.Public);
        }

        private bool _TryGetUserEndpoint(User user, out IPEndPoint userEndPoint)
        {
            bool canContinue;
            IEnumerator<KeyValuePair<IPEndPoint, User>> onlineUsersEnumerator = _onlineUsers.GetEnumerator();
            userEndPoint = null;
            do
                canContinue = onlineUsersEnumerator.MoveNext();
            while (canContinue && !onlineUsersEnumerator.Current.Value.Equals(user));
            if (canContinue)
                userEndPoint = onlineUsersEnumerator.Current.Key;
            onlineUsersEnumerator.Dispose();
            return canContinue;
        }

        private void _Send(string message, IPEndPoint _ipEndPoint, MessageScope messageScope, User receiver = null)
        {
            lock (_socket)
                _socket.SendTo(_GetMessageBytes(message), _ipEndPoint);
            if (MessageSent != null)
                MessageSent(this, new MessageEventArgs(receiver, message.Split(new char[] { ':' }, 2)[1], messageScope));
        }

        private void _ReceiveMessages(int timeout)
        {
            foreach (KeyValuePair<IPEndPoint, IEnumerable<string[]>> messages in _ReceiveMessagesFromSocket(1000))
                foreach (string[] message in messages.Value)
                    _InterpretMessage(messages.Key, message);
        }

        private void _InterpretMessage(IPEndPoint senderEndPoint, string[] receivedMessage)
        {
            User user;
            if (IsLogging)
                _Log(receivedMessage, senderEndPoint);
            switch (receivedMessage[0])
            {
                case "JOIN":
                    _AddUser(receivedMessage[1], senderEndPoint);
                    break;
                case "LEAVE":
                    _RemoveUser(senderEndPoint);
                    break;
                case "MESSAGE":
                    lock (_onlineUsers)
                        if (_onlineUsers.TryGetValue(senderEndPoint, out user) && MessageReceived != null)
                            MessageReceived(this, new MessageEventArgs(user, receivedMessage[1], MessageScope.Public));
                    break;
                case "PRIVATE":
                    lock (_onlineUsers)
                        if (_onlineUsers.TryGetValue(senderEndPoint, out user) && MessageReceived != null)
                            MessageReceived(this, new MessageEventArgs(user, receivedMessage[1], MessageScope.Private));
                    break;
            }
        }

        private void _RemoveUser(IPEndPoint senderEndPoint)
        {
            User user;
            lock (_onlineUsers)
                if (_onlineUsers.TryGetValue(senderEndPoint, out user))
                {
                    _onlineUsers.Remove(senderEndPoint);
                    if (UserWentOffline != null)
                        UserWentOffline(this, new UserEventArgs(user));
                }
        }

        private void _AddUser(string userName, IPEndPoint senderEndPoint)
        {
            User user;
            lock (_onlineUsers)
                if (!_onlineUsers.TryGetValue(senderEndPoint, out user))
                {
                    user = new User(userName, senderEndPoint.Address.GetAddressBytes());
                    _onlineUsers.Add(senderEndPoint, user);
                    if (UserCameOnline != null)
                        UserCameOnline(this, new UserEventArgs(user));
                }
                else
                    user.OnlineTimeout = 2;
        }

        private IDictionary<IPEndPoint, IEnumerable<string[]>> _ReceiveMessagesFromSocket(int timeout)
        {
            byte[] buffer = new byte[512];
            IDictionary<IPEndPoint, StringBuilder> messages = new Dictionary<IPEndPoint, StringBuilder>();
            if (this._socket.Poll(timeout, SelectMode.SelectRead))
            {
                EndPoint senderEndPoint = new IPEndPoint(IPAddress.Any, 0);
                do
                {
                    string readMessage = new string(Encoding.ASCII.GetChars(buffer, 0, this._socket.ReceiveFrom(buffer, ref senderEndPoint)));
                    StringBuilder messagesBuilder;
                    IPEndPoint senderIPEndPoint = senderEndPoint as IPEndPoint;
                    if (senderIPEndPoint != null)
                        if (messages.TryGetValue(senderIPEndPoint, out messagesBuilder))
                            messagesBuilder.Append(readMessage);
                        else
                            messages.Add(senderIPEndPoint, new StringBuilder(readMessage));
                } while (this._socket.Poll(0, SelectMode.SelectRead));
            }
            return _ParseMessages(messages);
        }

        private IDictionary<IPEndPoint, IEnumerable<string[]>> _ParseMessages(IDictionary<IPEndPoint, StringBuilder> receivedStreams)
        {
            IDictionary<IPEndPoint, IEnumerable<string[]>> parsedMessages = new Dictionary<IPEndPoint, IEnumerable<string[]>>();
            foreach (KeyValuePair<IPEndPoint, StringBuilder> receivedStream in receivedStreams)
            {
                parsedMessages.Add(receivedStream.Key, from unformattedMessage
                                                           in receivedStream.Value.ToString().Split('\0')
                                                       where unformattedMessage.Length != 0
                                                       select unformattedMessage.Split(':'));
            }
            return parsedMessages;
        }

        private static string _separator = new string('-', 80);
        private bool _wasThisDisposed;
        private bool _isRunning;
        private ReaderWriterLock _runsReaderWriterLock;
        private Socket _socket;
        private Dictionary<IPEndPoint, User> _onlineUsers;
        private Thread _signalThread;
        private Thread _readerThread;
    }
}
