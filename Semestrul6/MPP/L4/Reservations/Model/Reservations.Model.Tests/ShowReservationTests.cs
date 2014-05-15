using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Reservations.Model.Tests
{
	[TestClass]
	public class ShowReservationTests
	{
		[TestMethod, ExpectedException(typeof(ArgumentNullException))]
		public void TestWithNullShow()
		{
			new ShowReservation(null, HallLocation.Stal, "placement");
		}
		[TestMethod, ExpectedException(typeof(ArgumentNullException))]
		public void TestWithNullHallPosition()
		{
			new ShowReservation(new Show("showName", DateTime.Now), HallLocation.Stal, null);
		}
		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void TestWithEmptyHallPosition()
		{
			new ShowReservation(new Show("showName", DateTime.Now), HallLocation.Stal, string.Empty);
		}
		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void TestWithWhiteSpaceHallPosition()
		{
			new ShowReservation(new Show("showName", DateTime.Now), HallLocation.Stal, " ");
		}

		[TestMethod]
		public void TestWithValidHallPosition()
		{
			Show show = new Show("showName", DateTime.Now);
			ShowReservation showReservation = new ShowReservation(show, HallLocation.Lodge1, "placement");

			Assert.AreSame(showReservation.Show, show);
			Assert.AreEqual(showReservation.HallLocation, HallLocation.Lodge1);
			Assert.AreEqual(showReservation.HallPlacement, "placement");
		}
		[TestMethod]
		public void TestWithValidHallPlacementContaingSpacesAtBeginning()
		{
			Show show = new Show("showName", DateTime.Now);
			ShowReservation showReservation = new ShowReservation(show, HallLocation.Lodge2, "        placement");

			Assert.AreSame(showReservation.Show, show);
			Assert.AreEqual(showReservation.HallLocation, HallLocation.Lodge2);
			Assert.AreEqual(showReservation.HallPlacement, "placement");
		}
		[TestMethod]
		public void TestWithValidHallPlacementContaingSpacesAtEnd()
		{
			Show show = new Show("showName", DateTime.Now);
			ShowReservation showReservation = new ShowReservation(show, HallLocation.Lodge3, "placement        ");

			Assert.AreSame(showReservation.Show, show);
			Assert.AreEqual(showReservation.HallLocation, HallLocation.Lodge3);
			Assert.AreEqual(showReservation.HallPlacement, "placement");
		}
		[TestMethod]
		public void TestWithValidHallPlacementContaingSpacesAtBetweenLetters()
		{
			Show show = new Show("showName", DateTime.Now);
			ShowReservation showReservation = new ShowReservation(show, HallLocation.Lodge4, "placement        placement");

			Assert.AreSame(showReservation.Show, show);
			Assert.AreEqual(showReservation.HallLocation, HallLocation.Lodge4);
			Assert.AreEqual(showReservation.HallPlacement, "placement        placement");
		}
	}
}