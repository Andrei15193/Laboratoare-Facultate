using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ISSApp
{
    public class Library : IDisposable
    {
        public Library(string connectionString)
        {
            sqlConnection = new SqlConnection(connectionString);
            bookTable = new DataTable();
            bookTable.Columns.Add("Title", typeof(string));
            bookTable.Columns.Add("Author", typeof(string));
            bookTable.Columns.Add("ISBN", typeof(string));
            bookTable.Columns.Add("Count", typeof(int));
        }

        public void AddBook(string title, string author, string isbn, int count)
        {
            lock (bookTable)
            {
                SqlCommand sqlCommand = new SqlCommand() { Connection = sqlConnection };
                sqlCommand.CommandText = "instert into Books(title, author, isbn, count) values(@title, @author, @isbn, @count)";
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.Add(new SqlParameter("@title", title));
                sqlCommand.Parameters.Add(new SqlParameter("@author", author));
                sqlCommand.Parameters.Add(new SqlParameter("@isbn", isbn));
                sqlCommand.Parameters.Add(new SqlParameter("@count", count));
                sqlCommand.Connection.Open();
                SqlTransaction transaction = sqlCommand.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
                sqlCommand.ExecuteNonQuery();
                transaction.Commit();
                sqlCommand.Connection.Close();
            }
        }

        public void UpdateBook(string title, string author, string isbn, int count)
        {
            lock (bookTable)
            {
                SqlCommand sqlCommand = new SqlCommand() { Connection = sqlConnection };
                sqlCommand.CommandText = "update Books set title = @title, author = @author, count = @count where isbn = @isbn";
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.Add(new SqlParameter("@title", title));
                sqlCommand.Parameters.Add(new SqlParameter("@author", author));
                sqlCommand.Parameters.Add(new SqlParameter("@isbn", isbn));
                sqlCommand.Parameters.Add(new SqlParameter("@count", count));
                sqlCommand.Connection.Open();
                SqlTransaction transaction = sqlCommand.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
                sqlCommand.ExecuteNonQuery();
                transaction.Commit();
                sqlCommand.Connection.Close();
            }
        }

        public void DeleteBook(string isbn)
        {
            lock (bookTable)
            {
                SqlCommand sqlCommand = new SqlCommand() { Connection = sqlConnection };
                sqlCommand.CommandText = "delete from Books where isbn = @isbn";
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.Add(new SqlParameter("@isbn", isbn));
                sqlCommand.Connection.Open();
                SqlTransaction transaction = sqlCommand.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
                sqlCommand.ExecuteNonQuery();
                transaction.Commit();
                sqlCommand.Connection.Close();
            }
        }

        public void BorrowBook(string code, string isbn, int count)
        {
            lock (bookTable)
            {
                SqlCommand sqlCommand = new SqlCommand() { Connection = sqlConnection };
                sqlCommand.CommandText = "insert into BorrowedBooks(book, userCode, count, borrowDate, receiptDate) values (@isbn, @user, @count, @borrowDate, null)";
                sqlCommand.Parameters.Add(new SqlParameter("@isbn", isbn));
                sqlCommand.Parameters.Add(new SqlParameter("@user", code));
                sqlCommand.Parameters.Add(new SqlParameter("@count", count));
                sqlCommand.Parameters.Add(new SqlParameter("@borrowDate", DateTime.Now));
                sqlCommand.Connection.Open();
                SqlTransaction transaction = sqlCommand.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
                sqlCommand.ExecuteNonQuery();
                transaction.Commit();
                sqlCommand.Connection.Close();
            }
        }

        public void RetrieveBook(string code, string isbn)
        {
            lock (bookTable)
            {
                SqlCommand sqlCommand = new SqlCommand() { Connection = sqlConnection };
                sqlCommand.CommandText = "update BorrowedBooks set receiptDate = @receiptDate where book = @isbn and userCode = @user";
                sqlCommand.Parameters.Add(new SqlParameter("@receiptDate", DateTime.Now));
                sqlCommand.Parameters.Add(new SqlParameter("@isbn", isbn));
                sqlCommand.Parameters.Add(new SqlParameter("@user", code));
                sqlCommand.Connection.Open();
                SqlTransaction transaction = sqlCommand.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
                sqlCommand.ExecuteNonQuery();
                transaction.Commit();
                sqlCommand.Connection.Close();
            }
        }

        public void Start()
        {
            backgroundWorker = Task.Factory.StartNew(BackgroundWorker);
        }

        public DataView BooksTableView
        {
            get
            {
                return bookTable.DefaultView;
            }
        }

        ~Library()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            if (isDisposed)
                throw new ObjectDisposedException("The object has been disposed!");
            else
                Dispose(true);
        }

        public void Authenticate(string code)
        {
            SqlCommand sqlCommand = new SqlCommand("select code, firstName, lastName, type from Users where code = @code", sqlConnection);
            sqlCommand.Parameters.Add(new SqlParameter("@code", code));
            sqlCommand.Connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.Read())
            {
                switch (((UserType)int.Parse(reader["type"].ToString())))
                {
                    case UserType.Librarian:
                        User = new User(reader["firstName"].ToString(), reader["lastName"].ToString(), reader["code"].ToString(), UserType.Librarian);
                        break;
                    case UserType.Consumer:
                        User = new User(reader["firstName"].ToString(), reader["lastName"].ToString(), reader["code"].ToString(), UserType.Consumer);
                        break;
                }
            }
            sqlCommand.Connection.Close();
        }

        public User User { get; private set; }

        private void BackgroundWorker()
        {
            Monitor.Enter(runsLock);
            while (runs)
            {
                Monitor.Exit(runsLock);
                lock (bookTable)
                {
                    bookTable.Clear();
                    SqlTransaction transaction = sqlConnection.BeginTransaction(IsolationLevel.Serializable);
                    sqlConnection.Open();
                    SqlDataReader reader = new SqlCommand(@"select title, author, isbn, case when (rez1.count is not null) then Books.count + rez1.count else Books.count end as count
                                                            from Books left join(
                                                                    select book, sum(count) as count
                                                                        from BorrowedBooks
                                                                        where receiptDate is null
                                                                        group by book
                                                                ) as rez1
                                                                on isbn = book", sqlConnection).ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                        bookTable.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2], reader[3]);
                    transaction.Commit();
                    reader.Close();
                }
                Thread.Sleep(timeToWait);
                Monitor.Enter(runsLock);
            }
            Monitor.Exit(runsLock);
        }

        private void Dispose(bool isBeingDisposed)
        {
            if (isBeingDisposed)
            {
                lock (runsLock)
                    runs = false;
                backgroundWorker.Wait();
                backgroundWorker.Dispose();
                isDisposed = true;
                GC.SuppressFinalize(this);
            }
            backgroundWorker = null;
            runs = false;
            isDisposed = true;
            runsLock = false;
        }

        private DataTable bookTable;
        private SqlConnection sqlConnection;
        private object runsLock;
        private bool runs;
        private bool isDisposed;
        private Task backgroundWorker;
        private static int timeToWait = 3000;
    }
}
