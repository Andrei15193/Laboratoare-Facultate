using System.Collections.Generic;
using System.Linq;
using AlgoritmicaGrafelor.Laborator4.Multimi;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlgoritmicaGrafelor.Laborator4.Tests
{
    [TestClass]
    public class MultimiInteriorStabileTests
    {
        static private readonly IReadOnlyList<IReadOnlyList<int>> _onePeekGraph = new[]
            {
                new List<int>{ }
            };
        static private readonly IReadOnlyList<IReadOnlyList<int>> _twoPeekGraph = new[]
            {
                new List<int>{ 1 },
                new List<int>{ }
            };
        static private readonly IReadOnlyList<IReadOnlyList<int>> _fourPeekGraph = new[]
            {
                new List<int>{ 1, 2 },
                new List<int>{ 3 },
                new List<int>{ 3 },
                new List<int>{ }
            };
        static private readonly IReadOnlyList<IReadOnlyList<int>> _fivePeekGraphWithACycle = new[]
            {
                new List<int>{ 1 },
                new List<int>{ 2, 4 },
                new List<int>{ 3 },
                new List<int>{ 1 },
                new List<int>{ }
            };

        [TestMethod]
        public void TestForOnePeekGraph()
        {
            IReadOnlyList<IReadOnlyList<int>> multimiInteriorStabile = MultimiInteriorStabile.Toate(_onePeekGraph);

            Assert.AreEqual(2, multimiInteriorStabile.Count);
            Assert.IsTrue(multimiInteriorStabile[0].Count == 1 || multimiInteriorStabile[1].Count == 0);
            if (multimiInteriorStabile[0].Count == 1)
            {
                Assert.AreEqual(0, multimiInteriorStabile[0][0]);
                Assert.AreEqual(0, multimiInteriorStabile[1].Count);
            }
            else
            {
                Assert.AreEqual(0, multimiInteriorStabile[1][0]);
                Assert.AreEqual(0, multimiInteriorStabile[0].Count);
            }
        }

        [TestMethod]
        public void TestForTwoPeekGraph()
        {
            IReadOnlyList<IReadOnlyList<int>> multimiInteriorStabile = MultimiInteriorStabile.Toate(_twoPeekGraph)
                                                                                             .OrderBy(multime => multime.Count)
                                                                                             .ToList();

            Assert.AreEqual(3, multimiInteriorStabile.Count);
            Assert.AreEqual(0, multimiInteriorStabile.Where(multime => multime.Count == 1)
                                                     .Select(multime => multime.First())
                                                     .Except(new[] { 0, 1 })
                                                     .Count());
        }

        [TestMethod]
        public void TestForFourPeekGraph()
        {
            IReadOnlyList<IReadOnlyList<int>> multimiInteriorStabile = MultimiInteriorStabile.Toate(_fourPeekGraph)
                                                                                             .OrderBy(multime => multime.Count)
                                                                                             .ToList();

            Assert.AreEqual(7, multimiInteriorStabile.Count);
            Assert.AreEqual(0, multimiInteriorStabile.Where(multime => multime.Count == 1)
                                                     .Select(multime => multime.First())
                                                     .Except(new[] { 0, 1, 2, 3 })
                                                     .Count());

            var misCuDouaElement = multimiInteriorStabile.Where(multime => multime.Count == 2)
                                                         .Select(multime => multime.OrderBy(element => element))
                                                         .OrderBy(multime => string.Join(", ", multime.Select(element => element.ToString())))
                                                         .ToList();
            Assert.AreEqual(2, misCuDouaElement.Count);
            Assert.IsTrue(new[] { 0, 3 }.SequenceEqual(misCuDouaElement[0]));
            Assert.IsTrue(new[] { 1, 2 }.SequenceEqual(misCuDouaElement[1]));
        }

        [TestMethod]
        public void TestForFivePeekGraphWithACycle()
        {
            IReadOnlyList<IReadOnlyList<int>> multimiInteriorStabile = MultimiInteriorStabile.Toate(_fivePeekGraphWithACycle)
                                                                                             .OrderBy(multime => multime.Count)
                                                                                             .ToList();

            Assert.AreEqual(13, multimiInteriorStabile.Count);
            Assert.AreEqual(0, multimiInteriorStabile.Where(multime => multime.Count == 1)
                                                     .Select(multime => multime.First())
                                                     .Except(new[] { 0, 1, 2, 3, 4 })
                                                     .Count());

            var misCuDouaElement = multimiInteriorStabile.Where(multime => multime.Count == 2)
                                                         .Select(multime => multime.OrderBy(element => element))
                                                         .OrderBy(multime => string.Join(", ", multime.Select(element => element.ToString())))
                                                         .ToList();
            Assert.AreEqual(5, misCuDouaElement.Count);
            Assert.IsTrue(new[] { 0, 2 }.SequenceEqual(misCuDouaElement[0]));
            Assert.IsTrue(new[] { 0, 3 }.SequenceEqual(misCuDouaElement[1]));
            Assert.IsTrue(new[] { 0, 4 }.SequenceEqual(misCuDouaElement[2]));
            Assert.IsTrue(new[] { 2, 4 }.SequenceEqual(misCuDouaElement[3]));
            Assert.IsTrue(new[] { 3, 4 }.SequenceEqual(misCuDouaElement[4]));
            var misCuTreiElement = multimiInteriorStabile.Where(multime => multime.Count == 3)
                                                         .Select(multime => multime.OrderBy(element => element))
                                                         .OrderBy(multime => string.Join(", ", multime.Select(element => element.ToString())))
                                                         .ToList();

            Assert.AreEqual(2, misCuTreiElement.Count);
            Assert.IsTrue(new[] { 0, 2, 4 }.SequenceEqual(misCuTreiElement[0]));
            Assert.IsTrue(new[] { 0, 3, 4 }.SequenceEqual(misCuTreiElement[1]));
        }
    }
}
