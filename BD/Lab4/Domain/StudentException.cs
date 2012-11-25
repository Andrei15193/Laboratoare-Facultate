using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BDLab4.Domain
{
    /// <summary>
    /// Represents a specialized Exception class for the Domain entity Student.
    /// </summary>
    class StudentException : Exception
    {
        /// <summary>
        /// Creates a new StudentException instance with the given message.
        /// </summary>
        /// <param name="message">is a message that describes the context in which the exception occurred.</param>
        public StudentException(string message)
            : base(message)
        {
        }
    }
}
