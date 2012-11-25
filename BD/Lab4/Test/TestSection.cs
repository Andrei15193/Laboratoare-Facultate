using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace BDLab4.Test
{
    [TestFixture]
    class TestSection
    {
        [TestCase]
        public void TestCreate()
        {
            Domain.Section section = null;
            Assert.DoesNotThrow(delegate { section = new Domain.Section(1, "Algebra"); });
            Assert.IsNotNull(section);
        }
    }
}
