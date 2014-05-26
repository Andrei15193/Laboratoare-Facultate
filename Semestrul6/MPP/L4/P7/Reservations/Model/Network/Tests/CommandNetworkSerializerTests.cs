using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Reservations.Model.Network.Tests
{
	[TestClass]
	public class CommandNetworkSerializerTests
	{
		[TestMethod, ExpectedException(typeof(ArgumentNullException))]
		public void TestSerializeWithNullParameter()
		{
			new CommandNetworkSerializer<Show>(new ShowNetworkSerializer()).Serialize(null);
		}
		[TestMethod, ExpectedException(typeof(ArgumentNullException))]
		public void TestDeserializeWithNullParameter()
		{
			new CommandNetworkSerializer<Show>(new ShowNetworkSerializer()).Deserialize(null);
		}

		[TestMethod]
		public void TestCommandWithNoParameterSerialization()
		{
			Assert.AreEqual("{command:}\n", new CommandNetworkSerializer<Show>(new ShowNetworkSerializer()).Serialize(new Command<Show>("command")));
		}
		[TestMethod]
		public void TestCommandWithNoParameterDeserialization()
		{
			Command<Show> command = new CommandNetworkSerializer<Show>(new ShowNetworkSerializer()).Deserialize("{command:}\n");

			Assert.AreEqual("command", command.Name);
			Assert.AreEqual(0, command.Parameters.Count);
		}

		[TestMethod]
		public void TestCommandWithOneParametersSerialization()
		{
			Assert.AreEqual("{command:{Show:\"showName\",2010-04-05}}\n", new CommandNetworkSerializer<Show>(new ShowNetworkSerializer()).Serialize(new Command<Show>("command", new Show("showName", new DateTime(2010, 4, 5)))));
		}
		[TestMethod]
		public void TestCommandWithOneParametersDeserialization()
		{
			Command<Show> command = new CommandNetworkSerializer<Show>(new ShowNetworkSerializer()).Deserialize("{command:{Show:\"showName\",2010-04-05}}\n");

			Assert.AreEqual("command", command.Name);
			Assert.AreEqual(1, command.Parameters.Count);
			Assert.AreEqual("showName", command.Parameters[0].Name);
			Assert.AreEqual(new DateTime(2010, 4, 5), command.Parameters[0].Scheduled);
		}

		[TestMethod]
		public void TestCommandWithTwoParametersSerialization()
		{
			Assert.AreEqual("{command:{Show:\"showName1\",2010-04-05},{Show:\"showName2\",2011-03-06}}\n", new CommandNetworkSerializer<Show>(new ShowNetworkSerializer()).Serialize(new Command<Show>("command", new Show("showName1", new DateTime(2010, 4, 5)), new Show("showName2", new DateTime(2011, 3, 6)))));
		}
		[TestMethod]
		public void TestCommandWithTwoParametersDesSerialization()
		{
			Command<Show> command = new CommandNetworkSerializer<Show>(new ShowNetworkSerializer()).Deserialize("{command:{Show:\"showName1\",2010-04-05},{Show:\"showName2\",2011-03-06}}\n");

			Assert.AreEqual("command", command.Name);
			Assert.AreEqual(2, command.Parameters.Count);
			Assert.AreEqual("showName1", command.Parameters[0].Name);
			Assert.AreEqual(new DateTime(2010, 4, 5), command.Parameters[0].Scheduled);
			Assert.AreEqual("showName2", command.Parameters[1].Name);
			Assert.AreEqual(new DateTime(2011, 3, 6), command.Parameters[1].Scheduled);
		}
	}
}