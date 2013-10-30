using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlgoritmicaGrafelor.Laborator2.DrumuriMinime.Tests
{
    [TestClass]
    public class MinRoadsTests
    {
        [TestClass]
        public class YenTests
        {
            static private readonly IReadOnlyList<IReadOnlyDictionary<int, double>> _onePeekGraph = new[]
            {
                new KeyValuePair<int, double>[0].ToDictionary(pair => pair.Key, pair => pair.Value)
            };
            static private readonly IReadOnlyList<IReadOnlyDictionary<int, double>> _twoPeekGraph = new[]
            {
                new KeyValuePair<int, double>[0].ToDictionary(pair => pair.Key, pair => pair.Value),

                new [] { new KeyValuePair<int, double>(0, 1) }.ToDictionary(pair => pair.Key, pair => pair.Value)
            };
            static private readonly IReadOnlyList<IReadOnlyDictionary<int, double>> _fourPeekGraphWithTwoRoutesEqualInLength = new[]
            {   
                new KeyValuePair<int, double>[0].ToDictionary(pair => pair.Key, pair => pair.Value),

                new [] { new KeyValuePair<int, double>(0, 1) }.ToDictionary(pair => pair.Key, pair => pair.Value),
               
                new [] { new KeyValuePair<int, double>(0, 1) }.ToDictionary(pair => pair.Key, pair => pair.Value),
               
                new [] { new KeyValuePair<int, double>(2, 1), new KeyValuePair<int, double>(3, 2) }.ToDictionary(pair => pair.Key, pair => pair.Value),
            };
            static private readonly IReadOnlyList<IReadOnlyDictionary<int, double>> _fivePeekGraphWithACycle = new[]
            {   
                new KeyValuePair<int, double>[0].ToDictionary(pair => pair.Key, pair => pair.Value),

                new [] { new KeyValuePair<int, double>(0, 1), new KeyValuePair<int, double>(3, 1) }.ToDictionary(pair => pair.Key, pair => pair.Value),
               
                new [] { new KeyValuePair<int, double>(1, 1) }.ToDictionary(pair => pair.Key, pair => pair.Value),
               
                new [] { new KeyValuePair<int, double>(2, 1) }.ToDictionary(pair => pair.Key, pair => pair.Value),
               
                new [] { new KeyValuePair<int, double>(1, 1) }.ToDictionary(pair => pair.Key, pair => pair.Value)
            };
            static private readonly IReadOnlyList<IReadOnlyDictionary<int, double>> _fivePeekGraphWithARouteLongerThanTheOtherButCheaper = new[]
            {   
                new KeyValuePair<int, double>[0].ToDictionary(pair => pair.Key, pair => pair.Value),

                new [] { new KeyValuePair<int, double>(0, 2) }.ToDictionary(pair => pair.Key, pair => pair.Value),
               
                new [] { new KeyValuePair<int, double>(1, 1) }.ToDictionary(pair => pair.Key, pair => pair.Value),
               
                new [] { new KeyValuePair<int, double>(2, 1), new KeyValuePair<int, double>(4, 10) }.ToDictionary(pair => pair.Key, pair => pair.Value),
               
                new [] { new KeyValuePair<int, double>(0, 1) }.ToDictionary(pair => pair.Key, pair => pair.Value)
            };

            [TestMethod]
            public void TestWhenTheGraphHasOnlyOnePeek()
            {
                Assert.AreEqual(0, MinRoads.Yen(_onePeekGraph, 0, 0));
            }

            [TestMethod]
            public void TestWhenTheGraphHasTwoPeeksAndTheSourceAndDestinationAreTheSame()
            {
                Assert.AreEqual(0, MinRoads.Yen(_twoPeekGraph, 0, 0));
                Assert.AreEqual(0, MinRoads.Yen(_twoPeekGraph, 1, 1));
            }

            [TestMethod]
            public void TestWhenTheGraphHasTwoPeeksAndThereIsNoRoadFromSourceToDestination()
            {
                Assert.IsTrue(double.IsPositiveInfinity(MinRoads.Yen(_twoPeekGraph, 1, 0)));
            }

            [TestMethod]
            public void TestWhenTheGraphHasTwoPeeksAndThereIsARoadFromSourceToDestination()
            {
                Assert.AreEqual(1, MinRoads.Yen(_twoPeekGraph, 0, 1));
            }

            [TestMethod]
            public void TestWhenTheGrapHasTwoRoutesEqualInLengthButOneIsCheaper()
            {
                Assert.AreEqual(2, MinRoads.Yen(_fourPeekGraphWithTwoRoutesEqualInLength, 0, 3));
            }

            [TestMethod]
            public void TestWhenTheGrapHasACircuit()
            {
                Assert.AreEqual(2, MinRoads.Yen(_fivePeekGraphWithACycle, 0, 4));
            }

            [TestMethod]
            public void TestWhenTheGrapHasARouteLongerThanTheOtherButCheaper()
            {
                Assert.AreEqual(4, MinRoads.Yen(_fivePeekGraphWithARouteLongerThanTheOtherButCheaper, 0, 3));
            }
        }
    }
}
