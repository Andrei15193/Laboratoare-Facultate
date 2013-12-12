using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace AlgoritmicaGrafelor.Laborator5.Subarbori.Tests
{
	[TestClass]
	public class PrimTests
	{
		static private readonly IReadOnlyCollection<Tuple<int, int, double>> _onePeekGraph =
			new Tuple<int, int, double>[0];
		static private readonly IReadOnlyCollection<Tuple<int, int, double>> _twoPeekGraph =
			new[] { Tuple.Create(0, 1, 1d) };
		static private readonly IReadOnlyCollection<Tuple<int, int, double>> _threePeekGraph =
			new[] { Tuple.Create(0, 1, 1d),
					Tuple.Create(0, 2, 2d),
					Tuple.Create(1, 2, 1d) };
		static private readonly IReadOnlyCollection<Tuple<int, int, double>> _fourPeekGraph =
			new[] { Tuple.Create(1, 2, 4d),
					Tuple.Create(0, 2, 2d),
					Tuple.Create(0, 3, 3d),
					Tuple.Create(3, 2, 1d) };
		[TestMethod]
		public void TestForOnePeekGraph()
		{
			IReadOnlyCollection<Tuple<int, int, double>> result = SubarborePartial.Prim(_onePeekGraph);
			Assert.IsNotNull(result);
			Assert.AreEqual(0, result.Count);
		}
		[TestMethod]
		public void TestForTwoPeekGraph()
		{
			IReadOnlyCollection<Tuple<int, int, double>> result = SubarborePartial.Prim(_twoPeekGraph);
			Assert.IsNotNull(result);
			Assert.AreEqual(1, result.Count);
			Assert.IsTrue(result.SequenceEqual(_twoPeekGraph));
		}
		[TestMethod]
		public void TestForThreePeekGraph()
		{
			IReadOnlyCollection<Tuple<int, int, double>> result = SubarborePartial.Prim(_threePeekGraph);
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count);
			Assert.IsTrue(result.OrderBy(tuple => tuple.Item2)
								.OrderBy(tuple => tuple.Item1)
								.SequenceEqual(new[]
								{
									Tuple.Create(0, 1, 1d),
									Tuple.Create(1, 2, 1d)
								}));
		}
		[TestMethod]
		public void TestForFourPeekGraph()
		{
			IReadOnlyCollection<Tuple<int, int, double>> result = SubarborePartial.Prim(_fourPeekGraph);
			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Count);
			Assert.IsTrue(result.OrderBy(tuple => tuple.Item2)
								.OrderBy(tuple => tuple.Item1)
								.SequenceEqual(new[]
								{
									Tuple.Create(0, 2, 2d),
									Tuple.Create(1, 2, 4d),
									Tuple.Create(3, 2, 1d) 
								}));
		}
	}
}