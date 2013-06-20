using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace GTBDDLab4
{
    class Transaction
    {
        public Transaction(string connectionString)
        {
            _connection.ConnectionString = connectionString;
            _command = new SqlCommand() { Connection = _connection, CommandType = CommandType.StoredProcedure };
        }

        public void Start(TransactionType type)
        {
            SqlParameter typeParameter = new SqlParameter("@type", SqlDbType.Char);
            SqlParameter transactionIdParameter = new SqlParameter("@id", SqlDbType.Int) { Direction = ParameterDirection.Output };
            _command.CommandText = "StartTransaction";
            _command.Parameters.Clear();
            _command.Parameters.Add(typeParameter);
            _command.Parameters.Add(transactionIdParameter);
            if (type == TransactionType.Optimistic)
                typeParameter.Value = 'O';
            else
                typeParameter.Value = 'P';
            try
            {
                _connection.Open();
                _command.ExecuteNonQuery();
                _transactionId = Convert.ToInt32(transactionIdParameter.Value);
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                    _connection.Close();
            }
        }

        public void ExecuteNonQuery(string command, string table)
        {
            _command.CommandText = "SubmitOperation";
            _command.Parameters.Clear();
            _command.Parameters.Add(new SqlParameter("@transactionId", SqlDbType.Int) { Value = _transactionId });
            _command.Parameters.Add(new SqlParameter("@operation", SqlDbType.VarChar, 1000) { Value = command });
            _command.Parameters.Add(new SqlParameter("@table", SqlDbType.VarChar, 100) { Value = table });
            try
            {
                _connection.Open();
                _command.ExecuteNonQuery();
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                    _connection.Close();
            }
        }

        public IDataReader ExecuteReader(string command, string table)
        {
            _command.CommandText = "SubmitOperation";
            _command.Parameters.Clear();
            _command.Parameters.Add(new SqlParameter("@transactionId", SqlDbType.Int) { Value = _transactionId });
            _command.Parameters.Add(new SqlParameter("@operation", SqlDbType.VarChar, 1000) { Value = command });
            _command.Parameters.Add(new SqlParameter("@table", SqlDbType.VarChar, 100) { Value = table });
            _connection.Open();
            return _command.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public void Commit()
        {
            _command.CommandText = "CommitTransaction";
            _command.Parameters.Clear();
            _command.Parameters.Add(new SqlParameter("@transactionId", SqlDbType.Int) { Value = _transactionId });
            try
            {
                _connection.Open();
                _command.ExecuteNonQuery();
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                    _connection.Close();
                _transactionId = 0;
            }
        }

        public void Rollback()
        {
            _command.CommandText = "RollbackTransaction";
            _command.Parameters.Clear();
            _command.Parameters.Add(new SqlParameter("@transactionId", SqlDbType.Int) { Value = _transactionId });
            try
            {
                _connection.Open();
                _command.ExecuteNonQuery();
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                    _connection.Close();
                _transactionId = 0;
            }
        }

        public bool IsStarted
        {
            get
            {
                return _transactionId != 0;
            }
        }

        private int _transactionId = 0;
        private readonly SqlConnection _connection = new SqlConnection();
        private readonly SqlCommand _command;
    }
}
