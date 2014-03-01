using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BDLab4.Domain
{
    /// <summary>
    /// Represents a Student. A student has a name, serialNumber, group, date of birth and section code.
    /// </summary>
    class Student: ICloneable
    {
        /// <summary>
        /// Creates a new Student with the specified values for each field.
        /// </summary>
        /// <param name="name">is the name of the Student.</param>
        /// <param name="serialNumber">is the serial number of the Student (usually used to identify one unique student in a container).</param>
        /// <param name="group">is the group the student belongs to.</param>
        /// <param name="dateOfBirth">is the date of birth of the student.</param>
        /// <param name="sectionCode">is the section code (usually used to identify one unique section in a container).</param>
        /// <exception cref="StudentException">is thrown if at least one of the string fields has an empty string.</exception>
        /// <exception cref="ArgumentNullException">is thrown if at least one of the string fields is null.</exception>
        public Student(string name, string serialNumber, string group, DateTime dateOfBirth, int sectionCode)
        {
            this.SectionCode = sectionCode;
            this.Name = string.Copy(name.Trim());
            this.Group = string.Copy(group.Trim());
            this.serialNumber = string.Copy(serialNumber.Trim());
            this.dateOfBirth = dateOfBirth;
            this.Validate();
        }

        /// <summary>
        /// Validates the current instance. It is best that validate Student instances when modifying data to avoid
        /// logical errors later. Properties do not automatically validate new data, however the constructor does.
        /// </summary>
        /// <exception cref="StudentException">is thrown if at least one field is invalid.</exception>
        public void Validate()
        {
            string err = "", stringedSection = this.SectionCode.ToString();
            Regex nameRegex = new Regex("^([a-zA-Z]+[ -]?)*[^ -]$");
            Regex numericRegex = new Regex("^[0-9]+$");
            if (this.Name.Length == 0)
                err += "Nu exista nume specificat! ";
            else
                if (!nameRegex.Match(this.Name).Success)
                    err += "Numele specificat este invalid! Un nume poate sa contina doar litere, spatii si cratime! Un nume incepe si se termina cu litere! Intre doua nume poate exista o singura cratima sau un singur spatiu! ";
            if (this.SerialNumber.Length == 0)
                err += "Nu exista un numar matricol specificat! ";
            else
                if (!numericRegex.Match(this.SerialNumber).Success)
                    err += "Numarul matricol specificat este invalid! Doar charactere numerice sunt acceptate! ";
            if (this.Group.Length == 0)
                err += "Nu exista o grupa specificata! ";
            else
                if (!numericRegex.Match(this.Group).Success)
                    err += "Grupa specificata este invalida! Doar charactere numerice sunt acceptate! ";
            if (DateTime.Now.CompareTo(this.DateOfBirth) < 0)
                err += "Data nasterii nu poate fi mai tarzie decat data curenta! ";

            if (err.Length != 0)
                throw new Domain.StudentException(err);
        }

        /// <summary>
        /// Creates a clone of the calling instance.
        /// </summary>
        /// <returns>Returns a new Student that has all values copied bit by bit.</returns>
        public object Clone()
        {
            return new Student(this.Name, this.SerialNumber, this.Group, this.DateOfBirth, this.SectionCode);
        }

        /// <summary>
        /// Gets or sets the section code of the current instance.
        /// </summary>
        public int SectionCode { get; set; }

        /// <summary>
        /// Gets or sets the student name of the current instance.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the group of the current instance.
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// Gets the serial number of the current instance.
        /// </summary>
        public string SerialNumber
        {
            get
            {
                return this.serialNumber;
            }
        }

        /// <summary>
        /// Gets the date of birth of the current instance.
        /// </summary>
        public DateTime DateOfBirth
        {
            get
            {
                return this.dateOfBirth;
            }
        }

        /// <summary>
        /// Returns the string representation of the current instance. In this case it's the student name.
        /// </summary>
        /// <returns>a string that is equal in value and reference with the current student name.</returns>
        public override string ToString()
        {
            return this.Name;
        }

        private string serialNumber;
        private DateTime dateOfBirth;
    }
}
