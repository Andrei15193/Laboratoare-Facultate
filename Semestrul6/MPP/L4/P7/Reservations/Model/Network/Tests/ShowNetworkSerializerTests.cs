using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Reservations.Model.Network.Tests
{
	[TestClass]
	public class ShowNetworkSerializerTests
	{
		[TestMethod]
		public void TestShowWithNoEscapingCharactersInNameSerialization()
		{
			Assert.AreEqual("{Show:\"test show\",2000-12-14}\n",
							new ShowNetworkSerializer().Serialize(new Show("test show", new DateTime(2000, 12, 14))));
		}
		[TestMethod]
		public void TestShowWithEscapingCharactersInNameSerialization()
		{
			Assert.AreEqual("{Show:\"test \\\"show\\\"\",2000-12-14}\n",
							new ShowNetworkSerializer().Serialize(new Show("test \"show\"", new DateTime(2000, 12, 14))));
		}

		[TestMethod]
		public void TestShowWithNoEscapingCharactersInNameDeserialization()
		{
			Show show = new ShowNetworkSerializer().Deserialize("{Show:\"test show\",2000-12-14}\n");

			Assert.IsNotNull(show);
			Assert.AreEqual(show.Name, "test show");
			Assert.AreEqual(show.Scheduled, new DateTime(2000, 12, 14));
		}
		[TestMethod]
		public void TestShowWithEscapingCharactersInNameDeserialization()
		{
			Show show = new ShowNetworkSerializer().Deserialize("{Show:\"test \\\"show\\\"\",2000-12-14}\n");

			Assert.IsNotNull(show);
			Assert.AreEqual(show.Name, "test \"show\"");
			Assert.AreEqual(show.Scheduled, new DateTime(2000, 12, 14));
		}

		[TestMethod]
		public void TestShowSerializationDeserializationWithSameShowInstanceHavingNoEscapingCharatersInName()
		{
			ShowNetworkSerializer showNetworkSerializer = new ShowNetworkSerializer();

			Show show = new Show("test show", new DateTime(2000, 12, 14));
			Show deserializedShow = showNetworkSerializer.Deserialize(showNetworkSerializer.Serialize(show));

			Assert.AreNotSame(show, deserializedShow);
			Assert.IsNotNull(deserializedShow);
			Assert.AreEqual(show.Name, deserializedShow.Name);
			Assert.AreEqual(show.Scheduled, deserializedShow.Scheduled);
		}
		[TestMethod]
		public void TestShowSerializationDeserializationWithSameShowInstanceHavingEscapingCharatersInName()
		{
			ShowNetworkSerializer showNetworkSerializer = new ShowNetworkSerializer();

			Show show = new Show("test \"show\"", new DateTime(2000, 12, 14));
			Show deserializedShow = showNetworkSerializer.Deserialize(showNetworkSerializer.Serialize(show));

			Assert.AreNotSame(show, deserializedShow);
			Assert.IsNotNull(deserializedShow);
			Assert.AreEqual(show.Name, deserializedShow.Name);
			Assert.AreEqual(show.Scheduled, deserializedShow.Scheduled);
		}

		[TestMethod]
		public void TestShowDeserializationSerializationWithSameShowInstanceHavingNoEscapingCharatersInName()
		{
			ShowNetworkSerializer showNetworkSerializer = new ShowNetworkSerializer();

			string serialized = "{Show:\"test show\",2000-12-14}\n";
			string reSerialized = showNetworkSerializer.Serialize(showNetworkSerializer.Deserialize(serialized));

			Assert.AreNotSame(serialized, reSerialized);
			Assert.IsNotNull(reSerialized);
			Assert.AreEqual(serialized, reSerialized);
		}
		[TestMethod]
		public void TestShowDeserializationSerializationWithSameShowInstanceHavingEscapingCharatersInName()
		{
			ShowNetworkSerializer showNetworkSerializer = new ShowNetworkSerializer();

			string serialized = "{Show:\"test \\\"show\\\"\",2000-12-14}\n";
			string reSerialized = showNetworkSerializer.Serialize(showNetworkSerializer.Deserialize(serialized));

			Assert.AreNotSame(serialized, reSerialized);
			Assert.IsNotNull(reSerialized);
			Assert.AreEqual(serialized, reSerialized);
		}

		[TestMethod, ExpectedException(typeof(ArgumentNullException))]
		public void TestShowSerializationWithNull()
		{
			new ShowNetworkSerializer().Serialize(null);
		}
		[TestMethod, ExpectedException(typeof(ArgumentNullException))]
		public void TestShowDeserializationWithNull()
		{
			new ShowNetworkSerializer().Deserialize(null);
		}
	}
}