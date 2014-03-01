using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BDLab4.Repository
{
    /// <summary>
    /// Retains a set of connections parameters for any DataBaseRepository class.
    /// </summary>
    class DataBaseConnectionParameters
    {
        /// <summary>
        /// Creates a new instance having the specified server address and data base name. The user and password fields
        /// are filled with null. This constructor can be used when the connection is trusted and the default provider
        /// is used.
        /// </summary>
        /// <param name="server">is the server location (IP or domain).</param>
        /// <param name="dataBase">is the database name to connect to.</param>
        /// <exception cref="ArgumentNullException">is thrown if  at least one parameter is null.</exception>
        public DataBaseConnectionParameters(string server, string dataBase)
        {
            this.provider = null;
            this.server = string.Copy(server);
            this.dataBase = string.Copy(dataBase);
            this.user = null;
            this.password = null;
        }

        /// <summary>
        /// Creates a new instance having the specified fields. This constructor can be used when the connection
        /// to the database is not trusted, but the default provider is used.
        /// </summary>
        /// <param name="server">is the server location (IP or domain).</param>
        /// <param name="dataBase">is the database name to connect to.</param>
        /// <param name="user">is the username used to connect to the database.</param>
        /// <param name="password">is the user account password.</param>
        /// <exception cref="ArgumentNullException">is thrown if  at least one parameter is null.</exception>
        public DataBaseConnectionParameters(string server, string dataBase, string user, string password)
        {
            this.provider = null;
            this.server = string.Copy(server);
            this.dataBase = string.Copy(dataBase);
            this.user = string.Copy(user);
            this.password = string.Copy(password);
        }

        /// <summary>
        /// Creates a new instance having the specified provider, server address and data base name. The user and password
        /// fields are filled with null. This constructor can be used when the connection is trusted and there is no need
        /// for a user and a password.
        /// </summary>
        /// <param name="server">is the server location (IP or domain).</param>
        /// <param name="dataBase">is the database name to connect to.</param>
        /// <param name="provider">is a string that tells the database provider (SQL, Oracle etc.).</param>
        /// <exception cref="ArgumentNullException">is thrown if  at least one parameter is null.</exception>
        public DataBaseConnectionParameters(string server, string dataBase, string provider)
        {
            this.provider = string.Copy(provider);
            this.server = string.Copy(server);
            this.dataBase = string.Copy(dataBase);
            this.user = null;
            this.password = null;
        }

        /// <summary>
        /// Creates a new instance having the specified fields. This constructor can be used when the connection
        /// to the database is not trusted and the user with it's password need to be given.
        /// </summary>
        /// <param name="server">is the server location (IP or domain).</param>
        /// <param name="dataBase">is the database name to connect to.</param>
        /// <param name="provider">is a string that tells the database provider (SQL, Oracle etc.).</param>
        /// <param name="user">is the username used to connect to the database.</param>
        /// <param name="password">is the user account password.</param>
        /// <exception cref="ArgumentNullException">is thrown if  at least one parameter is null.</exception>
        public DataBaseConnectionParameters(string server, string dataBase, string provider, string user, string password)
        {
            this.provider = string.Copy(provider);
            this.server = string.Copy(server);
            this.dataBase = string.Copy(dataBase);
            this.user = string.Copy(user);
            this.password = string.Copy(password);
        }

        /// <summary>
        /// Gets the database provider.
        /// </summary>
        public string Provider
        {
            get
            {
                return this.provider;
            }
        }

        /// <summary>
        /// Gets the database server address.
        /// </summary>
        public string Server
        {
            get
            {
                return this.server;
            }
        }

        /// <summary>
        /// Gets the database name.
        /// </summary>
        public string DataBase
        {
            get
            {
                return this.dataBase;
            }
        }

        /// <summary>
        /// Gets the user name.
        /// </summary>
        public string User
        {
            get
            {
                return this.user;
            }
        }

        /// <summary>
        /// Gets the user password.
        /// </summary>
        public string Password
        {
            get
            {
                return this.password;
            }
        }

        private readonly string provider;
        private readonly string server;
        private readonly string dataBase;
        private readonly string user;
        private readonly string password;
    }
}
