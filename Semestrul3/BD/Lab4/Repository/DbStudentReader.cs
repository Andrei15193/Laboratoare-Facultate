using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BDLab4.Repository
{
    /// <summary>
    /// Reads a Student from a data reader.
    /// </summary>
    class DbStudentReader : Controller.StudentReader
    {
        /// <summary>
        /// Creates a new StudentReader with the given data reader. Use Read() and CurrentStudent to read
        /// sequentielly from the given data reader.
        /// <para>
        /// NOTE! The provided data reader must contain at least the fields (with their exact name) of a
        /// Student instance.
        /// </para>
        /// </summary>
        /// <param name="dataReader">is the data reader returned by an IDbCommand.ExecuteQuery(CommandBehavior.CloseConnection).</param>
        public DbStudentReader(IDataReader dataReader)
        {
            this.dataReader = dataReader;
            this.currentStudent = null;
        }

        /// <summary>
        /// Moves on the next position in the sequence and reads a new student from the data reader. On success
        /// the method returns true, otherwise it returns false.
        /// <para>
        /// NOTE! A Read() call must be performed before attempting to read any students!
        /// </para>
        /// <para>
        /// NOTE! When a Read() call fails it automatically closes the connection to the repository. If you do not wish
        /// to read until the end of the stream perform a call to Close().
        /// </para>
        /// </summary>
        /// <returns>returns a bool value that tells whether a section has been read or not.</returns>
        /// <exception cref="StudentException">is thrown if at least one field is invalid. Throwing an exception does not close the connection!</exception>
        public bool Read()
        {
            DateTime dateOfBirth;
            if (!this.dataReader.IsClosed)
                if (this.dataReader.Read())
                {
                    try
                    {
                        dateOfBirth = Convert.ToDateTime(this.dataReader["DateOfBirth"]);
                    }
                    catch (InvalidCastException){
                        dateOfBirth = DateTime.Now.AddDays(-1);
                    }
                    try
                    {
                        this.currentStudent = new Domain.Student(Convert.ToString(this.dataReader["Name"]),
                                                                 Convert.ToString(this.dataReader["SerialNumber"]),
                                                                 Convert.ToString(this.dataReader["Group"]),
                                                                 dateOfBirth,
                                                                 Convert.ToInt16(this.dataReader["SectionCode"]));
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new Domain.StudentException("Could not read data from source! " + exception.Message);
                    }
                    return true;
                }
                else
                {
                    this.dataReader.Close();
                    return false;
                }
            else
                return false;
        }

        /// <summary>
        /// Closes the connection to the repository if it's not closed.
        /// <para>
        /// NOTE! If you iterated through all students using Read() until it returned false, you do not need to worry
        /// about closing the connection because Read() does that for you in this case.
        /// </para>
        /// </summary>
        public void Close()
        {
            if (!this.dataReader.IsClosed)
                this.dataReader.Close();
        }

        /// <summary>
        /// Gets the current Student in the sequence. A Read() call must be performed otherwise null is returned.
        /// </summary>
        public Domain.Student GetCurrentStudent()
        {
            return this.currentStudent;
        }

        private IDataReader dataReader;
        private Domain.Student currentStudent;
    }
}
