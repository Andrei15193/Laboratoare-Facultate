using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Reservations.Model.Tests
{
	[TestClass]
	public class ShowTests
	{
		[TestMethod, ExpectedException(typeof(ArgumentNullException))]
		public void TestWithNullName()
		{
			new Show(null, DateTime.Now);
		}
		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void TestWithEmptyName()
		{
			new Show(string.Empty, DateTime.Now);
		}
		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void TestWithWhiteSpaceName()
		{
			new Show(" ", DateTime.Now);
		}

		[TestMethod]
		public void TestWithValidName()
		{
			DateTime now = DateTime.Now;
			Show show = new Show("showName", now);

			Assert.AreEqual(show.Name, "showName");
			Assert.AreEqual(show.Scheduled, now);
		}
		[TestMethod]
		public void TestWithValidNameContaingSpacesAtBeginning()
		{
			DateTime now = DateTime.Now;
			Show show = new Show("         showName", now);

			Assert.AreEqual(show.Name, "showName");
			Assert.AreEqual(show.Scheduled, now);
		}
		[TestMethod]
		public void TestWithValidNameContaingSpacesAtEnd()
		{
			DateTime now = DateTime.Now;
			Show show = new Show("showName               ", now);

			Assert.AreEqual(show.Name, "showName");
			Assert.AreEqual(show.Scheduled, now);
		}
		[TestMethod]
		public void TestWithValidNameContaingSpacesAtBetweenLetters()
		{
			DateTime now = DateTime.Now;
			Show show = new Show("show             Name", now);

			Assert.AreEqual(show.Name, "show             Name");
			Assert.AreEqual(show.Scheduled, now);
		}
	}
}