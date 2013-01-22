using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace BDLab4.Repository
{
    class SqlDataBaseRepository : Repository.SectionRepository, Repository.StudentRepository
    {
        /// <summary>
        /// Creates a new DataBaseRepository instance. If the connection is trusted then the user and password parameters
        /// are ignored.
        /// <para>
        /// NOTE! In this case there is no need to specify the DataBase provider since it is known to be SQL.
        /// </para>
        /// </summary>
        /// <param name="connectionParams">a set of connection parameters (server, database name etc.).</param>
        /// <param name="trustedConnection">whether the connection is trusted or not (requires user name and password or not).</param>
        public SqlDataBaseRepository(Repository.DataBaseConnectionParameters connectionParams, bool trustedConnection)
        {
            this.dbConnection = new SqlConnection(GetConnectionString(connectionParams, trustedConnection));
            this.dbCommands = new IDbCommand[(int)DbCommands.Size];
            this.InitializeCommands();
        }

        /// <summary>
        /// Stores a student in the repository. Two students cannot have the same serialNumber!
        /// </summary>
        /// <param name="student">is the student that will be stored.</param>
        /// <exception cref="RepositoryException">is thrown if the serialNumber of the given student already exists in the repository.</exception>
        public void AddStudent(Domain.Student student)
        {
            try
            {
                this.dbConnection.Open();
                (this.dbCommands[(int)DbCommands.DbCountStudentsWithSpecifiedSerialNumber].Parameters["@studentSerialNumber"] as SqlParameter).Value = student.SerialNumber;

                if (Convert.ToInt32(this.dbCommands[(int)DbCommands.DbCountStudentsWithSpecifiedSerialNumber].ExecuteScalar()) == 0)
                {
                    (this.dbCommands[(int)DbCommands.DbInsertStudent].Parameters["@studentName"] as SqlParameter).Value = student.Name;
                    (this.dbCommands[(int)DbCommands.DbInsertStudent].Parameters["@studentGroup"] as SqlParameter).Value = student.Group;
                    (this.dbCommands[(int)DbCommands.DbInsertStudent].Parameters["@studentDateOfBirth"] as SqlParameter).Value = student.DateOfBirth;
                    (this.dbCommands[(int)DbCommands.DbInsertStudent].Parameters["@studentSerialnumber"] as SqlParameter).Value = student.SerialNumber;
                    (this.dbCommands[(int)DbCommands.DbInsertStudent].Parameters["@sectionCode"] as SqlParameter).Value = student.SectionCode;
                    this.dbCommands[(int)DbCommands.DbInsertStudent].ExecuteNonQuery();
                    this.dbConnection.Close();
                }
                else
                {
                    this.dbConnection.Close();
                    throw new Repository.RepositoryException("Studentul se afla deja in baza de date! ");
                }
            }
            catch (SqlException)
            {
                if (this.dbConnection.State == ConnectionState.Open)
                    this.dbConnection.Close();
                throw new Repository.RepositoryException("Nu s-a putut stabili o legatura la baza de date! ");
            }
        }

        /// <summary>
        /// Updates a student in the repository. The search is made by the SerialNumber of the given Student instance.
        /// </summary>
        /// <param name="student">is the student that will be updated.</param>
        public void UpdateStudent(Domain.Student student)
        {
            try
            {
                this.dbConnection.Open();
                (this.dbCommands[(int)DbCommands.DbCountStudentsWithSpecifiedSerialNumber].Parameters["@studentSerialNumber"] as SqlParameter).Value = student.SerialNumber;

                if (Convert.ToInt32(this.dbCommands[(int)DbCommands.DbCountStudentsWithSpecifiedSerialNumber].ExecuteScalar()) != 0)
                {
                    (this.dbCommands[(int)DbCommands.DbUpdateStudent].Parameters["@studentName"] as SqlParameter).Value = student.Name;
                    (this.dbCommands[(int)DbCommands.DbUpdateStudent].Parameters["@studentGroup"] as SqlParameter).Value = student.Group;
                    (this.dbCommands[(int)DbCommands.DbUpdateStudent].Parameters["@studentDateOfBirth"] as SqlParameter).Value = student.DateOfBirth;
                    (this.dbCommands[(int)DbCommands.DbUpdateStudent].Parameters["@studentSerialnumber"] as SqlParameter).Value = student.SerialNumber;
                    (this.dbCommands[(int)DbCommands.DbUpdateStudent].Parameters["@sectionCode"] as SqlParameter).Value = student.SectionCode;
                    this.dbCommands[(int)DbCommands.DbUpdateStudent].ExecuteNonQuery();
                    this.dbConnection.Close();
                }
                else
                {
                    this.dbConnection.Close();
                    throw new Repository.RepositoryException("Studentul nu exista in baza de date! Actualizarea nu a putut fi facuta! ");
                }
            }
            catch (SqlException)
            {
                if (this.dbConnection.State == ConnectionState.Open)
                    this.dbConnection.Close();
                throw new Repository.RepositoryException("Nu s-a putut stabili o legatura la baza de date! ");
            }
        }

        /// <summary>
        /// Deletes a student specified by his serialNumber.
        /// </summary>
        /// <param name="serialNumber">is the unique indentifier for any student in the repository.</param>
        public void DeleteStudent(string serialNumber)
        {
            try
            {
                (this.dbCommands[(int)DbCommands.DbCountStudentsWithSpecifiedSerialNumber].Parameters["@studentSerialNumber"] as SqlParameter).Value = serialNumber;

                this.dbConnection.Open();
                if (Convert.ToInt32(this.dbCommands[(int)DbCommands.DbCountStudentsWithSpecifiedSerialNumber].ExecuteScalar()) != 0)
                {
                    (this.dbCommands[(int)DbCommands.DbDeleteStudent].Parameters["@studentSerialnumber"] as SqlParameter).Value = serialNumber;
                    this.dbCommands[(int)DbCommands.DbDeleteStudent].ExecuteNonQuery();
                    this.dbConnection.Close();
                }
                else
                {
                    this.dbConnection.Close();
                    throw new Repository.RepositoryException("Studentul nu exista in baza de date! Stergerea nu a putut fi facuta! ");
                }
            }
            catch (SqlException)
            {
                if (this.dbConnection.State == ConnectionState.Open)
                    this.dbConnection.Close();
                throw new Repository.RepositoryException("Nu s-a putut stabili o legatura la baza de date! ");
            }
        }

        /// <summary>
        /// Gets a StudentReader instance that can be used to sequentially read the students from the section
        /// specified by sectionCode. The returned iterator will give students ordered by the specified field
        /// name and sense.
        /// <para>
        /// NOTE! If you do not wish to read until the end of the stream you need to manually close the connection
        /// by calling Close() method from the returned StudentReader instance!
        /// </para>
        /// </summary>
        /// <param name="sectionCode">is the section code from where students will be selected.</param>
        /// <param name="orderByField">is the field after which students will be ordered.</param>
        /// <param name="orderBySense">is the sense in which students will be ordered.</param>
        /// <returns>Returns a Controller.StudentReader that sequentially reads students from this repository.</returns>
        public Controller.StudentReader GetStudentsFromSection(int sectionCode, Controller.OrderField orderByField, Controller.OrderSense orderBySense)
        {
            string orderBy = " order by ";

            switch (orderByField)
            {
                case Controller.OrderField.Group:
                    orderBy += "grupa ";
                    break;
                default:
                    orderBy += "nume ";
                    break;
            }

            switch (orderBySense)
            {
                case Controller.OrderSense.Descending:
                    orderBy += "desc";
                    break;
                default:
                    orderBy += "asc";
                    break;
            }

            try
            {
                using (SqlCommand selectOrderedStudentsFromSection = new SqlCommand(this.dbCommands[(int)DbCommands.DbSelectStudentsFromSecion].CommandText + orderBy, this.dbConnection as SqlConnection))
                {
                    selectOrderedStudentsFromSection.Parameters.Add("@sectionCode", SqlDbType.Int);
                    selectOrderedStudentsFromSection.Parameters["@sectionCode"].Value = sectionCode;
                    this.dbConnection.Open();
                    return new Repository.DbStudentReader(selectOrderedStudentsFromSection.ExecuteReader(CommandBehavior.CloseConnection));
                }
            }
            catch (SqlException)
            {
                if (this.dbConnection.State == ConnectionState.Open)
                    this.dbConnection.Close();
                throw new Repository.RepositoryException("Nu s-a putut stabili o legatura la baza de date! ");
            }
        }

        /// <summary>
        /// Gets a SectionReader instance that can be used to sequentially read all sections from this repository.
        /// <para>
        /// NOTE! If you do not wish to read until the end of the stream you need to manually close the connection
        /// by calling Close() method from the returned SectionReader instance!
        /// </para>
        /// </summary>
        /// <returns>Returns a Controller.SectionReader that sequentially reads sections from this repository.</returns>
        /// <exception cref="RepositoryException">is thrown when the connection to the data base could not be open.</exception>
        public Controller.SectionReader GetSections()
        {
            try
            {
                this.dbConnection.Open();
                return new Repository.DbSectionReader(this.dbCommands[(int)DbCommands.DbSelectSections].ExecuteReader(CommandBehavior.CloseConnection));
            }
            catch (SqlException)
            {
                if (this.dbConnection.State == ConnectionState.Open)
                    this.dbConnection.Close();
                throw new Repository.RepositoryException("Nu s-a putut stabili o legatura la baza de date! ");
            }
        }

        /// <summary>
        /// Enumerates all available database commands. The enumeration values can be used as indexes for a
        /// collection to avoid using long strings that are harder to use (typos etc.).
        /// <para>
        /// NOTE! All commands are prefixed with Db. Size is the last element and it gives the number of commands.
        /// </para>
        /// <para>
        /// This method is most efficient because the compiler will tell whether a command actually exists and the
        /// intellisense (if enabled) will make it much more easier to find the command you're looking for.
        /// </para>
        /// </summary>
        private enum DbCommands
        {
            DbSelectSections,
            DbSelectStudentsFromSecion,
            DbInsertStudent,
            DbUpdateStudent,
            DbDeleteStudent,
            DbCountStudentsWithSpecifiedSerialNumber,
            Size
        }

        /// <summary>
        /// Initializes all command instances with valid SqlCommands.
        /// </summary>
        private void InitializeCommands()
        {
            this.dbCommands[(int)DbCommands.DbCountStudentsWithSpecifiedSerialNumber] = new SqlCommand("select count(*) from studenti where nrmatricol = @studentSerialNumber", this.dbConnection as SqlConnection);
            this.dbCommands[(int)DbCommands.DbCountStudentsWithSpecifiedSerialNumber].Parameters.Add(new SqlParameter("@studentSerialNumber", System.Data.SqlDbType.VarChar));

            this.dbCommands[(int)DbCommands.DbSelectSections] = new SqlCommand("select cods as Code, denumires as Name from sectii order by Name asc", this.dbConnection as SqlConnection);

            this.dbCommands[(int)DbCommands.DbSelectStudentsFromSecion] = new SqlCommand("select cods as SectionCode, nume as Name, grupa as 'Group', datan as DateOfBirth, nrmatricol as SerialNumber from studenti where cods = @sectionCode", this.dbConnection as SqlConnection);
            this.dbCommands[(int)DbCommands.DbSelectStudentsFromSecion].Parameters.Add(new SqlParameter("@sectionCode", System.Data.SqlDbType.Int));

            this.dbCommands[(int)DbCommands.DbInsertStudent] = new SqlCommand("insert into studenti(cods, nrmatricol, nume, grupa, datan) values (@sectionCode, @studentSerialNumber, @studentName, @studentGroup, @studentDateOfBirth)", this.dbConnection as SqlConnection);
            this.dbCommands[(int)DbCommands.DbInsertStudent].Parameters.Add(new SqlParameter("@sectionCode", System.Data.SqlDbType.Int));
            this.dbCommands[(int)DbCommands.DbInsertStudent].Parameters.Add(new SqlParameter("@studentSerialNumber", System.Data.SqlDbType.VarChar));
            this.dbCommands[(int)DbCommands.DbInsertStudent].Parameters.Add(new SqlParameter("@studentName", System.Data.SqlDbType.VarChar));
            this.dbCommands[(int)DbCommands.DbInsertStudent].Parameters.Add(new SqlParameter("@studentGroup", System.Data.SqlDbType.VarChar));
            this.dbCommands[(int)DbCommands.DbInsertStudent].Parameters.Add(new SqlParameter("@studentDateOfBirth", System.Data.SqlDbType.DateTime));

            this.dbCommands[(int)DbCommands.DbUpdateStudent] = new SqlCommand("update studenti set cods = @sectionCode, nume = @studentName, grupa = @studentGroup, datan = @studentDateOfBirth where nrmatricol = @studentSerialNumber", this.dbConnection as SqlConnection);
            this.dbCommands[(int)DbCommands.DbUpdateStudent].Parameters.Add(new SqlParameter("@sectionCode", System.Data.SqlDbType.Int));
            this.dbCommands[(int)DbCommands.DbUpdateStudent].Parameters.Add(new SqlParameter("@studentName", System.Data.SqlDbType.VarChar));
            this.dbCommands[(int)DbCommands.DbUpdateStudent].Parameters.Add(new SqlParameter("@studentGroup", System.Data.SqlDbType.VarChar));
            this.dbCommands[(int)DbCommands.DbUpdateStudent].Parameters.Add(new SqlParameter("@studentDateOfBirth", System.Data.SqlDbType.DateTime));
            this.dbCommands[(int)DbCommands.DbUpdateStudent].Parameters.Add(new SqlParameter("@studentSerialNumber", System.Data.SqlDbType.VarChar));

            this.dbCommands[(int)DbCommands.DbDeleteStudent] = new SqlCommand("delete from studenti where nrmatricol = @studentSerialNumber", this.dbConnection as SqlConnection);
            this.dbCommands[(int)DbCommands.DbDeleteStudent].Parameters.Add(new SqlParameter("@studentSerialNumber", System.Data.SqlDbType.VarChar));
        }

        /// <summary>
        /// Gets the connection string out of the given connection params.
        /// <para>
        /// NOTE! If trusted connection is set to true then user and password fields of connectionParams are ignored.
        /// </para>
        /// <para>
        /// NOTE! This is a specialized connection string for SQL Connection type, the provider field of connectionParams
        /// is ignored.
        /// </para>
        /// </summary>
        /// <param name="connectionParams">is a set of parameters used to connect to a database.</param>
        /// <param name="trustedConnection">tells whether to use or not the user name and password from the connectionParams instance.</param>
        /// <returns>a valid sql connection string that can be used to connect to a database.</returns>
        /// <exception cref="ArgumentNullException">is thrown if connectionParams is null or is not initialized properly (e.g.: the connection is not trusted and connectionParams holds null for both user name and password).</exception>
        private string GetConnectionString(Repository.DataBaseConnectionParameters connectionParams, bool trustedConnection)
        {
            string connectionString = "Server=" + connectionParams.Server + "; ";
            connectionString += "Database=" + connectionParams.DataBase + "; ";
            if (trustedConnection)
                connectionString += "Trusted_Connection=True;";
            else
            {
                connectionString += "User Id=" + connectionParams.User + "; ";
                connectionString += "Password=" + connectionParams.Password + ";";
            }
            return connectionString;
        }

        private readonly IDbCommand[] dbCommands;
        private readonly IDbConnection dbConnection;
    }
}
