using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BDLab4.Controller
{
    /// <summary>
    /// Represents the minimal requirements for an interator-like instance that sequentially returns Students
    /// from a repository. Instances of this type can be used to get a number of students from a repository and
    /// perform application logic or just display the data.
    /// </summary>
    interface StudentReader
    {
        /// <summary>
        /// Gets the current Student in the sequence. A Read() call must be performed otherwise null is returned.
        /// </summary>
        Domain.Student GetCurrentStudent();

        /// <summary>
        /// Moves on the next position in the sequence and reads a new student from the data reader. On success the
        /// method returns true, otherwise it returns false.
        /// <para>
        /// NOTE! A Read() call must be performed before attempting to read any students!
        /// </para>
        /// <para>
        /// NOTE! When a Read() call fails it automatically closes the connection to the repository. If you do not wish
        /// to read until the end of the stream perform a call to Close().
        /// </para>
        /// </summary>
        /// <returns>returns a bool value that tells whether a section has been read or not.</returns>
        /// <exception cref="StudentException">is thrown if at least one field is invalid.</exception>
        bool Read();

        /// <summary>
        /// Closes the reader. When you are done iterating through the sequence of students call this method.
        /// </summary>
        void Close();
    }
}
