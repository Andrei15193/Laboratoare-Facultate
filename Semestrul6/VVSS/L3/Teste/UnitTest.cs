using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace L3.Teste
{
	[TestClass]
	public class UnitTest
	{
		[TestMethod, ExpectedException(typeof(ArgumentNullException))]
		public void TestNull()
		{
			P11.MaxDistincte(null);
		}
		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void TestMaiMultDecat100()
		{
			P11.MaxDistincte(new int[101]);
		}
		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void TestElementMaiMareDecat30000()
		{
			int[] dateDeTest = new int[100];
			dateDeTest[15193 % dateDeTest.Length] = 30001;

			P11.MaxDistincte(dateDeTest);
		}
		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void TestElementMaiMicDecat30000()
		{
			int[] dateDeTest = new int[100];
			dateDeTest[93151 % dateDeTest.Length] = -30001;

			P11.MaxDistincte(dateDeTest);
		}

		[TestMethod]
		public void TestSecventaVida()
		{
			Assert.AreEqual(P11.MaxDistincte(new int[0]).Count(), 0);
		}
		[TestMethod]
		public void TestExact100DeElementeEgale()
		{
			Assert.IsTrue(new[] { 0 }.SequenceEqual(P11.MaxDistincte(new int[100])));
		}
		[TestMethod]
		public void TestToateElementeleDistincte()
		{
			int[] dateDeTest = new int[50];
			for (int index = 0; index < dateDeTest.Length; index++)
				dateDeTest[index] = index;

			Assert.IsTrue(dateDeTest.SequenceEqual(P11.MaxDistincte(dateDeTest)));
		}
		[TestMethod]
		public void TestPrimele10ElementeDistincte()
		{
			int[] dateDeTest = new int[50];
			for (int index = 0; index < dateDeTest.Length; index++)
				dateDeTest[index] = index % 10;

			Assert.IsTrue(dateDeTest.Take(10).SequenceEqual(P11.MaxDistincte(dateDeTest)));
		}
		[TestMethod]
		public void TestPrimele10ElementeIdenticeUrmatoareleDistincte()
		{
			int[] dateDeTest = new int[20];
			for (int index = 10; index < dateDeTest.Length; index++)
				dateDeTest[index] = index;

			Assert.IsTrue(dateDeTest.Skip(10).SequenceEqual(P11.MaxDistincte(dateDeTest)));
		}
		[TestMethod]
		public void TestPrimele10ElementeIdenticeUrmatoarele10DistincteUrmatoarele5Identice()
		{
			int[] dateDeTest = new int[25];
			for (int index = 0; index < 10; index++)
				dateDeTest[10 + index] = index;

			Assert.IsTrue(dateDeTest.Skip(10).Take(10).SequenceEqual(P11.MaxDistincte(dateDeTest)));
		}
		[TestMethod]
		public void TestPrimele10ElementeIdenticeUrmatoarele10DistincteUrmatoarele5IdenticeUrmatoarele10Identice()
		{
			int[] dateDeTest = new int[30];
			for (int index = 0; index < 10; index++)
				dateDeTest[10 + index] = index;
			for (int index = 0; index < 5; index++)
				dateDeTest[25 + index] = index;

			Assert.IsTrue(dateDeTest.Skip(10).Take(10).SequenceEqual(P11.MaxDistincte(dateDeTest)));
		}
		[TestMethod]
		public void TestPrimele10ElementeIdenticeUrmatoarele5DistincteUrmatoarele5IdenticeUrmatoarele10Distincte()
		{
			int[] dateDeTest = new int[30];
			for (int index = 0; index < 5; index++)
				dateDeTest[10 + index] = index;
			for (int index = 0; index < 10; index++)
				dateDeTest[20 + index] = index;

			Assert.IsTrue(dateDeTest.Skip(20).Take(10).SequenceEqual(P11.MaxDistincte(dateDeTest)));
		}
	}
}