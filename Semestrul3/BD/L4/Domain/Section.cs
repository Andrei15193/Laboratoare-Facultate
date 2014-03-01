using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BDLab4.Domain
{
    /// <summary>
    /// Represents a section. A section (or specialization) has a code that usually is unique in a repository and
    /// a name.
    /// </summary>
    class Section
    {
        /// <summary>
        /// Creates a new Section with the specified code and name.
        /// </summary>
        /// <param name="code">is the code of the section (usually unique in a repository).</param>
        /// <param name="name">is the name of the section (used to display to the user for better understanding).</param>
        public Section(int code, string name)
        {
            this.code = code;
            this.Name = name;
        }

        /// <summary>
        /// Gets the section code.
        /// </summary>
        public int Code
        {
            get
            {
                return this.code;
            }
        }

        /// <summary>
        /// Gets or sets the name of the section.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Returns the string representation of the current instance. In this case it's the section name.
        /// </summary>
        /// <returns>a string that is equal in value and reference with the current section name.</returns>
        public override string ToString()
        {
            return this.Name;
        }

        private int code;
    }
}
