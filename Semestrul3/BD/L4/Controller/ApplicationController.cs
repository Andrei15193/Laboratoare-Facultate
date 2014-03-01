using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BDLab4.Controller
{
    /// <summary>
    /// Represents the application controller. All application business is done by instances of this class.
    /// </summary>
    class ApplicationController
    {
        /// <summary>
        /// Creates a new ApplicationController instance that uses the specified repositories.
        /// </summary>
        /// <param name="studentRepository">is a repository of students that are uniquelly identified by their serial number.</param>
        /// <param name="sectionRepository">is a repository of sections that are uniquelly identified by their code.</param>
        public ApplicationController(Repository.StudentRepository studentRepository, Repository.SectionRepository sectionRepository)
        {
            this.sectionRepository = sectionRepository;
            this.studentRepository = studentRepository;
        }

        /// <summary>
        /// Validates, creates and stores a Student entity in the repository.
        /// </summary>
        /// <param name="sectionCode">is the section the student belongs to.</param>
        /// <param name="name">is the student name.</param>
        /// <param name="serialNumber">is the student associated serial number, it's unique in a repository.</param>
        /// <param name="group">is the group the student belongs to.</param>
        /// <param name="dateOfBirth">is the date of birth of the student.</param>
        /// <exception cref="StudentException">is thrown if the given student data is invalid or the serialNumber is already present in the repository.</exception>
        public void AddStudent(int sectionCode, string name, string serialNumber, string group, DateTime dateOfBirth)
        {
            try
            {
                this.studentRepository.AddStudent(new Domain.Student(name, serialNumber, group, dateOfBirth, sectionCode));
            }
            catch (Repository.RepositoryException exception)
            {
                throw new Domain.StudentException("S-a intampinat o eroare! " + exception.Message);
            }
        }

        /// <summary>
        /// Validates, creates and updates a Student entity in the repository.
        /// </summary>
        /// <param name="sectionCode">is the section the student belongs to.</param>
        /// <param name="name">is the student name.</param>
        /// <param name="serialNumber">is the student associated serial number, it's unique in a repository.</param>
        /// <param name="group">is the group the student belongs to.</param>
        /// <param name="dateOfBirth">is the date of birth of the student.</param>
        /// <exception cref="StudentException">is thrown if the given student data is invalid or the serialNumber does not exist in the repository.</exception>
        public void UpdateStudent(int sectionCode, string name, string serialNumber, string group, DateTime dateOfBirth)
        {
            try
            {
                this.studentRepository.UpdateStudent(new Domain.Student(name, serialNumber, group, dateOfBirth, sectionCode));
            }
            catch (Repository.RepositoryException exception)
            {
                throw new Domain.StudentException("S-a intampinat o eroare! " + exception.Message);
            }
        }

        /// <summary>
        /// Deletes the student having the specified serialNumber from the student repository.
        /// </summary>
        /// <param name="serialNumber">is the associated serial number of the student that will be deleted.</param>
        /// <exception cref="StudentException">is thrown if the student does not exist in the repository.</exception>
        public void DeleteStudent(string serialNumber)
        {
            try
            {
                this.studentRepository.DeleteStudent(serialNumber);
            }
            catch (Repository.RepositoryException exception)
            {
                throw new Domain.StudentException("S-a intampinat o eroare! " + exception.Message);
            }
        }

        /// <summary>
        /// Returns a StudentReader (an interator) to a sequence of students.
        /// </summary>
        /// <param name="sectionCode">is the section code from where students will be read.</param>
        /// <param name="orderByField">is the field after which students will be ordered.</param>
        /// <param name="orderBySense">is the sense after which students will be ordered.</param>
        /// <returns>a StudentReader to an ordered sequence of students.</returns>
        /// <exception cref="StudentException">is thrown if a StudentReader could not be obtained.</exception>
        public Controller.StudentReader GetStudentsFromSection(int sectionCode, Controller.OrderField orderByField, Controller.OrderSense orderBySense)
        {
            try
            {
                return this.studentRepository.GetStudentsFromSection(sectionCode, orderByField, orderBySense);
            }
            catch (Repository.RepositoryException exception)
            {
                throw new Domain.StudentException("S-a intampinat o eroare! " + exception.Message);
            }
        }

        /// <summary>
        /// Returns a SectionReader (an iterator) to a sequence containing all sections from the sections repository.
        /// </summary>
        /// <returns>a SectionReader to an ascending ordered by name sequence that contains all sections.</returns>
        /// <exception cref="SectionException">is thrown if a SectionReader could not be obtained.</exception>
        public Controller.SectionReader GetSections()
        {
            try
            {
                return this.sectionRepository.GetSections();
            }
            catch (Repository.RepositoryException exception)
            {
                throw new Domain.SectionException("S-a intampinat o eroare! " + exception.Message);
            }
        }

        private Repository.StudentRepository studentRepository;
        private Repository.SectionRepository sectionRepository;
    }
}
