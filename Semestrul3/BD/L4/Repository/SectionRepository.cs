using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BDLab4.Repository
{
    /// <summary>
    /// Represents the interface of a SectionRepository. Any repository that is destined to handle Section storage has
    /// to implement the methods specified here and respect the pre/post conditions!
    /// <para>
    /// NOTE! In a section repository all sections can be uniquely found by their sectionCode! 
    /// </para>
    /// </summary>
    interface SectionRepository
    {
        /// <summary>
        /// Gets a SectionReader instance that can be used to sequentially read all sections from this repository.
        /// <para>
        /// NOTE! If you do not wish to read until the end of the stream you need to manually close the connection
        /// by calling Close() method from the returned SectionReader instance!
        /// </para>
        /// </summary>
        /// <returns>Returns a Controller.SectionReader that sequentially reads sections from this repository.</returns>
        Controller.SectionReader GetSections();
    }
}
