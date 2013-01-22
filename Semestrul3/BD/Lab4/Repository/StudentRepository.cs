using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BDLab4.Repository
{
    /// <summary>
    /// Represents the interface of a StudentRepository. Any repository that is destined to handle Student storage has
    /// to implement the methods specified here and respect the pre/post conditions!
    /// <para>
    /// NOTE! In a student repository all students can be uniquely found by their serial number! 
    /// </para>
    /// </summary>
    interface StudentRepository
    {
        /// <summary>
        /// Stores a student in the repository. Students must be distinguishable by their serialNumber.
        /// Two students cannot have the same serialNumber in any repository!
        /// </summary>
        /// <param name="student">is the student that will be stored.</param>
        /// <exception cref="RepositoryException">is thrown if the serialNumber of the given student already exists in the repository.</exception>
        void AddStudent(Domain.Student student);

        /// <summary>
        /// Updates a student in the repository. The search is made by the SerialNumber of the given Student instance.
        /// </summary>
        /// <param name="student">is the student that will be updated.</param>
        void UpdateStudent(Domain.Student student);

        /// <summary>
        /// Deletes a student specified by his serialNumber.
        /// </summary>
        /// <param name="serialNumber">is the unique indentifier for any student in the repository.</param>
        void DeleteStudent(string serialNumber);

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
        Controller.StudentReader GetStudentsFromSection(int sectionCode, Controller.OrderField orderByField, Controller.OrderSense orderBySense);
    }
}
