using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Reservations.Model.Tests
{
	[TestClass]
	public class CommandTests
	{
		[TestMethod, ExpectedException(typeof(ArgumentNullException))]
		public void TestCommandWithNullName()
		{
			new Command<string>(null);
		}
		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void TestCommandWithEmptyName()
		{
			new Command<string>(string.Empty);
		}
		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void TestCommandWithWhiteSpaceName()
		{
			new Command<string>(" ");
		}
		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void TestCommandWithInvalidNotEmptyWhiteSpaceName()
		{
			new Command<string>("$");
		}

		[TestMethod, ExpectedException(typeof(ArgumentNullException))]
		public void TestWithValidNameNullParameters()
		{
			new Command<string>("a", null);
		}

		[TestMethod]
		public void TestWithValidNameAndNoParameters()
		{
			Command<string>  command = new Command<string>("a");

			Assert.AreEqual(command.Name, "a");
			Assert.AreEqual(command.Parameters.Count, 0);
		}

		[TestMethod]
		public void TestWithValidNameContainingSpacesAtBeginningAndNoParameters()
		{
			Command<string> command = new Command<string>("   a");

			Assert.AreEqual(command.Name, "a");
			Assert.AreEqual(command.Parameters.Count, 0);
		}
		[TestMethod]
		public void TestWithValidNameContainingSpacesAtEndAndNoParameters()
		{
			Command<string> command = new Command<string>("a   ");

			Assert.AreEqual(command.Name, "a");
			Assert.AreEqual(command.Parameters.Count, 0);
		}
		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void TestWithInvalidNameContainingSpacesAtBetweenLettersAndNoParameters()
		{
			Command<string> command = new Command<string>("a   a");
		}

		[TestMethod]
		public void TestWithValidNameAndOneParamater()
		{
			Command<string> command = new Command<string>("a", "parameter1");

			Assert.AreEqual(command.Name, "a");
			Assert.AreEqual(command.Parameters.Count, 1);
			Assert.AreEqual(command.Parameters[0], "parameter1");
		}

		[TestMethod]
		public void TestWithValidNameAndTwoParamater()
		{
			Command<string> command = new Command<string>("a", "parameter1", "parameter2");

			Assert.AreEqual(command.Name, "a");
			Assert.AreEqual(command.Parameters.Count, 2);
			Assert.AreEqual(command.Parameters[0], "parameter1");
			Assert.AreEqual(command.Parameters[1], "parameter2");
		}
	}
}