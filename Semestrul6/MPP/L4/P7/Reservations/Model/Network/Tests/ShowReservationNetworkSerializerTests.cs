using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Reservations.Model.Network.Tests
{
	[TestClass]
	public class ShowReservationNetworkSerializerTests
	{
		[TestMethod]
		public void TestShowReservationWithNoEscapingCharactersInHallLocationSerialization()
		{
			Assert.AreEqual("{ShowReservation:{Show:\"test show\",2000-12-14},Stal,\"test location 1\"}\n",
							new ShowReservationNetworkSerializer().Serialize(new ShowReservation(new Show("test show", new DateTime(2000, 12, 14)), HallLocation.Stal, "test location 1")));
		}
		[TestMethod]
		public void TestShowReservationWithEscapingCharactersInHallLocationSerialization()
		{
			Assert.AreEqual("{ShowReservation:{Show:\"test show\",2000-12-14},Stal,\"test location \\\"1\\\"\"}\n",
							new ShowReservationNetworkSerializer().Serialize(new ShowReservation(new Show("test show", new DateTime(2000, 12, 14)), HallLocation.Stal, "test location \"1\"")));
		}

		[TestMethod]
		public void TestShowReservationWithNoEscapingCharactersInNameDeserialization()
		{
			ShowReservation showReservation = new ShowReservationNetworkSerializer().Deserialize("{ShowReservation:{Show:\"test show\",2000-12-14},Stal,\"test location 1\"}\n");

			Assert.IsNotNull(showReservation);
			Assert.AreEqual(showReservation.Show.Name, "test show");
			Assert.AreEqual(showReservation.Show.Scheduled, new DateTime(2000, 12, 14));
			Assert.AreEqual(showReservation.HallLocation, HallLocation.Stal);
			Assert.AreEqual(showReservation.HallPlacement, "test location 1");
		}
		[TestMethod]
		public void TestShowReservationWithEscapingCharactersInNameDeserialization()
		{
			ShowReservation showReservation = new ShowReservationNetworkSerializer().Deserialize("{ShowReservation:{Show:\"test show\",2000-12-14},Stal,\"test location \\\"1\\\"\"}\n");

			Assert.IsNotNull(showReservation);
			Assert.AreEqual(showReservation.Show.Name, "test show");
			Assert.AreEqual(showReservation.Show.Scheduled, new DateTime(2000, 12, 14));
			Assert.AreEqual(showReservation.HallLocation, HallLocation.Stal);
			Assert.AreEqual(showReservation.HallPlacement, "test location \"1\"");
		}

		[TestMethod]
		public void TestShowReservationSerializationDeserializationWithSameShowInstanceHavingNoEscapingCharatersInName()
		{
			ShowReservationNetworkSerializer showReservationNetworkSerializer = new ShowReservationNetworkSerializer();

			ShowReservation showReservation = new ShowReservation(new Show("test show", new DateTime(2000, 12, 14)), HallLocation.Stal, "test location 1");
			ShowReservation deserializedShowReservation = showReservationNetworkSerializer.Deserialize(showReservationNetworkSerializer.Serialize(showReservation));

			Assert.AreNotSame(showReservation, deserializedShowReservation);
			Assert.IsNotNull(deserializedShowReservation);
			Assert.AreEqual(showReservation.Show.Name, deserializedShowReservation.Show.Name);
			Assert.AreEqual(showReservation.Show.Scheduled, deserializedShowReservation.Show.Scheduled);
			Assert.AreEqual(showReservation.HallLocation, deserializedShowReservation.HallLocation);
			Assert.AreEqual(showReservation.HallPlacement, deserializedShowReservation.HallPlacement);
		}
		[TestMethod]
		public void TestShowReservationSerializationDeserializationWithSameShowInstanceHavingEscapingCharatersInName()
		{
			ShowReservationNetworkSerializer showReservationNetworkSerializer = new ShowReservationNetworkSerializer();

			ShowReservation showReservation = new ShowReservation(new Show("test show", new DateTime(2000, 12, 14)), HallLocation.Stal, "test location \"1\"");
			ShowReservation deserializedShowReservation = showReservationNetworkSerializer.Deserialize(showReservationNetworkSerializer.Serialize(showReservation));

			Assert.AreNotSame(showReservation, deserializedShowReservation);
			Assert.IsNotNull(deserializedShowReservation);
			Assert.AreEqual(showReservation.Show.Name, deserializedShowReservation.Show.Name);
			Assert.AreEqual(showReservation.Show.Scheduled, deserializedShowReservation.Show.Scheduled);
			Assert.AreEqual(showReservation.HallLocation, deserializedShowReservation.HallLocation);
			Assert.AreEqual(showReservation.HallPlacement, deserializedShowReservation.HallPlacement);
		}

		[TestMethod]
		public void TestShowDeserializationSerializationWithSameShowInstanceHavingNoEscapingCharatersInName()
		{
			ShowReservationNetworkSerializer showReservationNetworkSerializer = new ShowReservationNetworkSerializer();

			string serialized = "{ShowReservation:{Show:\"test show\",2000-12-14},Stal,\"test location 1\"}\n";
			string reSerialized = showReservationNetworkSerializer.Serialize(showReservationNetworkSerializer.Deserialize(serialized));

			Assert.AreNotSame(serialized, reSerialized);
			Assert.IsNotNull(reSerialized);
			Assert.AreEqual(serialized, reSerialized);
		}
		[TestMethod]
		public void TestShowDeserializationSerializationWithSameShowInstanceHavingEscapingCharatersInName()
		{
			ShowReservationNetworkSerializer showReservationNetworkSerializer = new ShowReservationNetworkSerializer();

			string serialized = "{ShowReservation:{Show:\"test show\",2000-12-14},Stal,\"test location \\\"1\\\"\"}\n";
			string reSerialized = showReservationNetworkSerializer.Serialize(showReservationNetworkSerializer.Deserialize(serialized));

			Assert.AreNotSame(serialized, reSerialized);
			Assert.IsNotNull(reSerialized);
			Assert.AreEqual(serialized, reSerialized);
		}

		[TestMethod, ExpectedException(typeof(ArgumentNullException))]
		public void TestShowSerializationWithNull()
		{
			new ShowReservationNetworkSerializer().Serialize(null);
		}
		[TestMethod, ExpectedException(typeof(ArgumentNullException))]
		public void TestShowDeserializationWithNull()
		{
			new ShowReservationNetworkSerializer().Deserialize(null);
		}
	}
}