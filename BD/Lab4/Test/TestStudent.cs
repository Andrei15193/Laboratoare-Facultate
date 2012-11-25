using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace BDLab4.Test
{
    [TestFixture]
    class TestStudent
    {
        [TestCase]
        public void TestCreate()
        {
            Domain.Student student = null;
            Assert.DoesNotThrow(delegate { student = new Domain.Student("Andrei", "1234", "221", DateTime.Now, 2); });
            Assert.IsNotNull(student);
        }

        [TestCase]
        public void TestCreateNameField()
        {
            Assert.Throws<Domain.StudentException>(delegate { new Domain.Student("          ", "123", "123", DateTime.Now, 1); });
            Assert.Throws<Domain.StudentException>(delegate { new Domain.Student("-Andrei", "123", "123", DateTime.Now, 1); });
            Assert.Throws<Domain.StudentException>(delegate { new Domain.Student("Andrei-", "123", "123", DateTime.Now, 1); });
            Assert.Throws<Domain.StudentException>(delegate { new Domain.Student("   -Andrei-        ", "123", "123", DateTime.Now, 1); });
            Assert.Throws<Domain.StudentException>(delegate { new Domain.Student("-", "123", "123", DateTime.Now, 1); });
            Assert.Throws<Domain.StudentException>(delegate { new Domain.Student("-Andrei-Fangli", "123", "123", DateTime.Now, 1); });
            Assert.Throws<Domain.StudentException>(delegate { new Domain.Student("Andrei-Fangli-", "123", "123", DateTime.Now, 1); });
            Assert.Throws<Domain.StudentException>(delegate { new Domain.Student("   -Andrei-Fangli-        ", "123", "123", DateTime.Now, 1); });
            Assert.Throws<Domain.StudentException>(delegate { new Domain.Student("-Andrei Fangli", "123", "123", DateTime.Now, 1); });
            Assert.Throws<Domain.StudentException>(delegate { new Domain.Student("Andrei Fangli-", "123", "123", DateTime.Now, 1); });
            Assert.Throws<Domain.StudentException>(delegate { new Domain.Student("   -Andrei Fangli-        ", "123", "123", DateTime.Now, 1); });
            Assert.Throws<Domain.StudentException>(delegate { new Domain.Student("-", "A123", "123", DateTime.Now, 1); });
            Assert.Throws<Domain.StudentException>(delegate { new Domain.Student("Andrei- Fangli", "123", "123", DateTime.Now, 1); });
            Assert.Throws<Domain.StudentException>(delegate { new Domain.Student("Andrei - Fangli", "123", "123", DateTime.Now, 1); });
            Assert.Throws<Domain.StudentException>(delegate { new Domain.Student("Andrei -Fangli", "123", "123", DateTime.Now, 1); });
            Assert.Throws<Domain.StudentException>(delegate { new Domain.Student("Andrei123Fangli", "123", "123", DateTime.Now, 1); });
            Assert.DoesNotThrow(delegate { new Domain.Student("Andrei-Fangli", "123", "123", DateTime.Now, 1); });
            Assert.DoesNotThrow(delegate { new Domain.Student("Andrei Fangli", "123", "123", DateTime.Now, 1); });
            Assert.DoesNotThrow(delegate { new Domain.Student("      Andrei Fangli          ", "123", "123", DateTime.Now, 1); });
            Assert.DoesNotThrow(delegate { new Domain.Student("      Andrei-Fangli          ", "123", "123", DateTime.Now, 1); });
        }

        [TestCase]
        public void TestCreateSerialNumberField()
        {
            Assert.Throws<Domain.StudentException>(delegate { new Domain.Student("Andrei", "-", "123", DateTime.Now, 1); });
            Assert.Throws<Domain.StudentException>(delegate { new Domain.Student("Andrei", "", "123", DateTime.Now, 1); });
            Assert.Throws<Domain.StudentException>(delegate { new Domain.Student("Andrei", "           ", "123", DateTime.Now, 1); });
            Assert.Throws<Domain.StudentException>(delegate { new Domain.Student("Andrei", "       -           ", "123", DateTime.Now, 1); });
            Assert.Throws<Domain.StudentException>(delegate { new Domain.Student("Andrei", "A123$asd", "123", DateTime.Now, 1); });
            Assert.Throws<Domain.StudentException>(delegate { new Domain.Student("Andrei", "A123asd", "123", DateTime.Now, 1); });
            Assert.Throws<Domain.StudentException>(delegate { new Domain.Student("Andrei", "A123            ", "123", DateTime.Now, 1); });
            Assert.Throws<Domain.StudentException>(delegate { new Domain.Student("Andrei", "12          3   ", "123", DateTime.Now, 1); });
            Assert.DoesNotThrow(delegate { new Domain.Student("Andrei", "          123      ", "123", DateTime.Now, 1); });
            Assert.DoesNotThrow(delegate { new Domain.Student("Andrei", "123", "123", DateTime.Now, 1); });
        }

        [TestCase]
        public void TestCreateGroupField()
        {
            Assert.Throws<Domain.StudentException>(delegate { new Domain.Student("Andrei", "123", "     ", DateTime.Now, 1); });
            Assert.Throws<Domain.StudentException>(delegate { new Domain.Student("Andrei", "123", " a  ", DateTime.Now, 1); });
            Assert.Throws<Domain.StudentException>(delegate { new Domain.Student("Andrei", "123", " 12A34 ", DateTime.Now, 1); });
            Assert.Throws<Domain.StudentException>(delegate { new Domain.Student("Andrei", "123", " 12           34 ", DateTime.Now, 1); });
            Assert.DoesNotThrow(delegate { new Domain.Student("Andrei", "123", "123", DateTime.Now, 1); });
            Assert.DoesNotThrow(delegate { new Domain.Student("Andrei", "123", "           019       ", DateTime.Now, 0); });
            Assert.DoesNotThrow(delegate { new Domain.Student("Andrei", "123", "12345", DateTime.Now, 123); });
        }

        [TestCase]
        public void TestCreateDateOfBirthField()
        {
            Assert.Throws<Domain.StudentException>(delegate { new Domain.Student("Andrei", "123", "221", DateTime.Now.AddDays(10), 2); });
            Assert.DoesNotThrow(delegate { new Domain.Student("Andrei", "123", "221", DateTime.Now.AddDays(-2), 2); });
        }

        [TestCase]
        public void TestProperties()
        {
            int section = 2;
            string name = "Andrei", serial = "1234", group = "221";
            DateTime now = DateTime.Now;
            Domain.Student student = new Domain.Student(name, serial, group, now, section);
            Assert.AreEqual(student.Name, name);
            Assert.AreEqual(student.SerialNumber, serial);
            Assert.AreEqual(student.Group, group);
            Assert.AreEqual(student.DateOfBirth, now);
            Assert.AreEqual(student.SectionCode, section);

            Assert.AreNotSame(student.Name, name);
            Assert.AreNotSame(student.SerialNumber, serial);
            Assert.AreNotSame(student.Group, group);
            Assert.AreNotSame(student.DateOfBirth, now);
            Assert.AreNotSame(student.SectionCode, section);
        }

        [TestCase]
        public void TestClone()
        {
            Domain.Student student = new Domain.Student("Andrei", "1234", "221", DateTime.Now, 2);
            Domain.Student studentClone = (Domain.Student)student.Clone();
            Assert.AreEqual(student.Name, studentClone.Name);
            Assert.AreEqual(student.SerialNumber, studentClone.SerialNumber);
            Assert.AreEqual(student.Group, studentClone.Group);
            Assert.AreEqual(student.DateOfBirth, studentClone.DateOfBirth);
            Assert.AreEqual(student.SectionCode, studentClone.SectionCode);

            Assert.AreNotSame(student.Name, studentClone.Name);
            Assert.AreNotSame(student.SerialNumber, studentClone.SerialNumber);
            Assert.AreNotSame(student.Group, studentClone.Group);
            Assert.AreNotSame(student.DateOfBirth, studentClone.DateOfBirth);
            Assert.AreNotSame(student.SectionCode, studentClone.SectionCode);
        }
    }
}
