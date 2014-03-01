using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BDLab4.Repository
{
    /// <summary>
    /// Represents a specialized exception for repositories.
    /// </summary>
    class RepositoryException : Exception
    {
        /// <summary>
        /// Creates a new instance of RepositoryException
        /// </summary>
        /// <param name="message">is a message that describes the context in which the exception occurred.</param>
        public RepositoryException(string message)
            : base(message)
        {
        }
    }
}
