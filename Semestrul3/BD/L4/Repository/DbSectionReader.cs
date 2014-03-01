using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BDLab4.Repository
{
    /// <summary>
    /// Reads a Section from a data reader.
    /// </summary>
    class DbSectionReader : Controller.SectionReader
    {
        /// <summary>
        /// Creates a new SectionReader with the given data reader. Use Read() and CurrentSection to read
        /// sequentielly from the given data reader.
        /// <para>
        /// NOTE! The provided data reader must contain at least the fields (with their exact name) of a
        /// Section instance.
        /// </para>
        /// </summary>
        /// <param name="dataReader">is the data reader returned by an IDbCommand.ExecuteQuery(CommandBehavior.CloseConnection).</param>
        public DbSectionReader(IDataReader dataReader)
        {
            this.dataReader = dataReader;
            this.currentSection = null;
        }

        /// <summary>
        /// Moves on the next position in the sequence and reads a new section from the data reader. On success the
        /// method returns true, otherwise it returns false.
        /// <para>
        /// NOTE! A Read() call must be performed before attempting to read any sections!
        /// </para>
        /// <para>
        /// NOTE! When a Read() call fails it automatically closes the connection to the repository. If you do not wish
        /// to read until the end of the stream perform a call to Close().
        /// </para>
        /// </summary>
        /// <returns>returns a bool value that tells whether a section has been read or not.</returns>
        /// <exception cref="SectionException">is thrown if at least one field is invalid. Throwing an exception does not close the connection!</exception>
        public bool Read()
        {
            if (!this.dataReader.IsClosed)
                if (this.dataReader.Read())
                {
                    try
                    {
                        this.currentSection = new Domain.Section(Convert.ToInt32(this.dataReader["Code"]),
                                                                 Convert.ToString(this.dataReader["Name"]));
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new Domain.SectionException("Could not read data from source! " + exception.Message);
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
        /// NOTE! If you iterated through all sections using Read() until it returned false, you do not need to worry
        /// about closing the connection because Read() does that for you in this case.
        /// </para>
        /// </summary>
        public void Close()
        {
            if (!this.dataReader.IsClosed)
                this.dataReader.Close();
        }

        /// <summary>
        /// Gets the current section in the sequence. A Read() call must be performed otherwise null is returned.
        /// </summary>
        public Domain.Section GetCurrentSection()
        {
            return this.currentSection;
        }

        private IDataReader dataReader;
        private Domain.Section currentSection;
    }
}
