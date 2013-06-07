//using System;
//using System.Collections.Generic;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Net;
//using System.Net.Sockets;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Xml.Serialization;

//namespace Server
//{
//    class Program
//    {
//        static ICollection<NetworkStream> clientStreams = new HashSet<NetworkStream>();
//        static ReaderWriterLock clientStreamsLock = new ReaderWriterLock();
//        private static bool runs = true;

//        static void Main(string[] args)
//        {
//            if (args.Length > 0)
//            {
//                Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
//                server.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), int.Parse(args[0])));
//                server.Listen(5);
//                RunServer(server);
//            }
//            else
//                Console.WriteLine("There is no port specified in the command line arguments!");
//        }

//        private static void RunServer(Socket server)
//        {
//            while (runs)
//            {
//                Socket client = server.Accept();
//                client.NoDelay = true;
//                client.LingerState = new LingerOption(true, 10);
//                client.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.NoDelay, true);
//                Console.WriteLine("Client connected!");
//                Task.Factory.StartNew(Client, client);
//                NetworkStream clientStream = new NetworkStream(client);
//            }
//        }

//        private static void Client(object arg)
//        {
//            if (arg is Socket)
//            {
//                Socket client = arg as Socket;
//                NetworkStream clientStream = new NetworkStream(client);
//                clientStreamsLock.AcquireWriterLock(-1);
//                clientStreams.Add(clientStream);
//                clientStreamsLock.ReleaseWriterLock();
//                while (client.Connected && client.Poll(-1, SelectMode.SelectRead))
//                    SatisfyClientRequest(ReadClientData(clientStream), clientStream, "Server=ANDREI-NETBOOK; Database=AndreiLocal; Trusted_Connection=True;");
//                client.Close();
//                clientStream.Close();
//                clientStreamsLock.AcquireWriterLock(-1);
//                clientStreams.Remove(clientStream);
//                clientStreamsLock.ReleaseWriterLock();
//                Console.WriteLine("Client disconnected!");
//            }
//        }

//        private static void SatisfyClientRequest(Protocol.ClientRequest clientRequest, NetworkStream clientStream, string connectionString)
//        {
//            SqlCommand sqlCommand = new SqlCommand() { Connection = new SqlConnection(connectionString) };
//            Console.WriteLine("Client requested operation {0}", clientRequest.Operation);
//            switch (clientRequest.Operation)
//            {
//                case Protocol.OperationType.Add:
//                    AddBook(clientRequest, clientStream, sqlCommand);
//                    break;
//                case Protocol.OperationType.Update:
//                    UpdateBook(clientRequest, clientStream, sqlCommand);
//                    break;
//                case Protocol.OperationType.Delete:
//                    DeleteBook(clientRequest, clientStream, sqlCommand);
//                    break;
//                case Protocol.OperationType.Borrow:
//                    BorrowBook(clientRequest, clientStream, sqlCommand);
//                    break;
//                case Protocol.OperationType.Retrieve:
//                    RetreiveBook(clientRequest, clientStream, sqlCommand);
//                    break;
//                case Protocol.OperationType.Authentication:
//                    Authenticate(clientRequest, clientStream, sqlCommand);
//                    break;
//                default:
//                    break;
//            }
//        }

//        private static void Authenticate(Protocol.ClientRequest clientRequest, NetworkStream clientStream, SqlCommand sqlCommand)
//        {
//            Protocol.User user = null;
//            sqlCommand.CommandText = "select code, firstName, lastName, type from Users where code = @code";
//            sqlCommand.Parameters.Add(new SqlParameter("@code", clientRequest.UserCode));
//            SqlTransaction transaction = sqlCommand.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
//            sqlCommand.Connection.Open();
//            SqlDataReader reader = sqlCommand.ExecuteReader();
//            if (reader.Read())
//            {
//                switch (((Protocol.UserType)int.Parse(reader["type"].ToString())))
//                {
//                    case Protocol.UserType.Librarian:
//                        user = new Protocol.User(reader["firstName"].ToString(), reader["lastName"].ToString(), reader["code"].ToString(), Protocol.UserType.Librarian);
//                        break;
//                    case Protocol.UserType.Consumer:
//                        user = new Protocol.User(reader["firstName"].ToString(), reader["lastName"].ToString(), reader["code"].ToString(), Protocol.UserType.Consumer);
//                        break;
//                }
//            }
//            transaction.Commit();
//            sqlCommand.Connection.Close();
//            SendChanges(clientRequest);
//            SendTo(new Protocol.ServerResponse(null, user, Protocol.OperationType.Authentication), clientStream);
//        }

//        private static void BorrowBook(Protocol.ClientRequest clientRequest, NetworkStream clientStream, SqlCommand sqlCommand)
//        {
//            if (clientRequest.User.Type == Protocol.UserType.Librarian)
//            {
//                sqlCommand.CommandText = "insert into BorrowedBooks(book, userCode, count, borrowDate, receiptDate) values (@isbn, @user, @count, @borrowDate, null)";
//                sqlCommand.Parameters.Add(new SqlParameter("@isbn", clientRequest.Book.Isbn));
//                sqlCommand.Parameters.Add(new SqlParameter("@user", clientRequest.User.Code));
//                sqlCommand.Parameters.Add(new SqlParameter("@count", clientRequest.Book.Count));
//                sqlCommand.Parameters.Add(new SqlParameter("@borrowDate", DateTime.Now));
//                SqlTransaction transaction = sqlCommand.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
//                sqlCommand.Connection.Open();
//                sqlCommand.ExecuteNonQuery();
//                transaction.Commit();
//                sqlCommand.Connection.Close();
//                SendChanges(clientRequest);
//            }
//        }

//        private static void RetreiveBook(Protocol.ClientRequest clientRequest, NetworkStream clientStream, SqlCommand sqlCommand)
//        {
//            if (clientRequest.User.Type == Protocol.UserType.Librarian)
//            {
//                sqlCommand.CommandText = "update BorrowedBooks set receiptDate = @receiptDate where book = @isbn and userCode = @user";
//                sqlCommand.Parameters.Add(new SqlParameter("@receiptDate", DateTime.Now));
//                sqlCommand.Parameters.Add(new SqlParameter("@isbn", clientRequest.Book.Isbn));
//                sqlCommand.Parameters.Add(new SqlParameter("@user", clientRequest.User.Code));
//                SqlTransaction transaction = sqlCommand.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
//                sqlCommand.Connection.Open();
//                sqlCommand.ExecuteNonQuery();
//                transaction.Commit();
//                sqlCommand.Connection.Close();
//                SendChanges(clientRequest);
//            }
//        }

//        private static void DeleteBook(Protocol.ClientRequest clientRequest, NetworkStream clientStream, SqlCommand sqlCommand)
//        {
//            if (clientRequest.User.Type == Protocol.UserType.Librarian)
//            {
//                sqlCommand.CommandText = "delete from Books where isbn = @isbn";
//                sqlCommand.Parameters.Clear();
//                sqlCommand.Parameters.Add(new SqlParameter("@isbn", clientRequest.Book.Isbn));
//                SqlTransaction transaction = sqlCommand.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
//                sqlCommand.Connection.Open();
//                sqlCommand.ExecuteNonQuery();
//                transaction.Commit();
//                sqlCommand.Connection.Close();
//                SendChanges(clientRequest);
//            }
//        }

//        private static void SendChanges(Protocol.ClientRequest clientRequest)
//        {
//            XmlSerializer serializer = new XmlSerializer(typeof(Protocol.ServerResponse));
//            clientStreamsLock.AcquireReaderLock(-1);
//            foreach (NetworkStream clientStream in clientStreams)
//                serializer.Serialize(clientStream, new Protocol.ServerResponse(clientRequest.Book, null, clientRequest.Operation));
//            clientStreamsLock.ReleaseReaderLock();
//        }

//        private static void UpdateBook(Protocol.ClientRequest clientRequest, NetworkStream clientStream, SqlCommand sqlCommand)
//        {
//            if (clientRequest.User.Type == Protocol.UserType.Librarian)
//            {
//                sqlCommand.CommandText = "update Books set title = @title, author = @author, count = @count where isbn = @isbn";
//                sqlCommand.Parameters.Clear();
//                sqlCommand.Parameters.Add(new SqlParameter("@title", clientRequest.Book.Title));
//                sqlCommand.Parameters.Add(new SqlParameter("@author", clientRequest.Book.Author));
//                sqlCommand.Parameters.Add(new SqlParameter("@isbn", clientRequest.Book.Isbn));
//                sqlCommand.Parameters.Add(new SqlParameter("@count", clientRequest.Book.Count));
//                SqlTransaction transaction = sqlCommand.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
//                sqlCommand.Connection.Open();
//                sqlCommand.ExecuteNonQuery();
//                transaction.Commit();
//                sqlCommand.Connection.Close();
//            }
//        }

//        private static void AddBook(Protocol.ClientRequest clientRequest, NetworkStream clientStream, SqlCommand sqlCommand)
//        {
//            if (clientRequest.User.Type == Protocol.UserType.Librarian)
//            {
//                sqlCommand.CommandText = "instert into Books(title, author, isbn, count) values(@title, @author, @isbn, @count)";
//                sqlCommand.Parameters.Clear();
//                sqlCommand.Parameters.Add(new SqlParameter("@title", clientRequest.Book.Title));
//                sqlCommand.Parameters.Add(new SqlParameter("@author", clientRequest.Book.Author));
//                sqlCommand.Parameters.Add(new SqlParameter("@isbn", clientRequest.Book.Isbn));
//                sqlCommand.Parameters.Add(new SqlParameter("@count", clientRequest.Book.Count));
//                SqlTransaction transaction = sqlCommand.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
//                sqlCommand.Connection.Open();
//                sqlCommand.ExecuteNonQuery();
//                transaction.Commit();
//                sqlCommand.Connection.Close();
//            }
//        }

//        private static void SendTo(Protocol.ServerResponse response, NetworkStream clientStream)
//        {
//            new XmlSerializer(typeof(Protocol.ServerResponse)).Serialize(clientStream, response);
//        }

//        private static Protocol.ClientRequest ReadClientData(NetworkStream clientStream)
//        {
//            XmlSerializer serializer = new XmlSerializer(typeof(Protocol.ClientRequest));
//            var x = serializer.Deserialize(clientStream) as Protocol.ClientRequest;
//            Console.WriteLine(x.Operation);
//            return x;
//        }
//    }
//}
