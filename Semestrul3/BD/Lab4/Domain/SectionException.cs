using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BDLab4.Domain
{
    /// <summary>
    /// Represents a specialized Exception class for the Domain entity Section.
    /// </summary>
    class SectionException : Exception
    {
        /// <summary>
        /// Creates a new SectionException instance with the given message.
        /// </summary>
        /// <param name="message">is a message that describes the context in which the exception occurred.</param>
        public SectionException(string message)
            : base(message)
        {
        }
    }
}
