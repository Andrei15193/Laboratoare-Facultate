using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace AlgoritmicaGrafelor.Laborator1.Transformari.Tests
{
    [TestClass]
    public class TransformsTests
    {
        [TestClass]
        public class ToSuccessorListTests
        {
            [TestMethod]
            public void TestWhenAdjacencyMatrixIsEmpty()
            {
                var SuccessorList = Transforms.ToSuccessorsList(new bool[,] { });

                Assert.AreEqual(0, SuccessorList.Count);
            }

            [TestMethod]
            public void TestWhenAdjacencyMatrixContainsOnlyFalse()
            {
                var SuccessorList = Transforms.ToSuccessorsList(new bool[,] { { false, false }, { false, false } });

                Assert.AreEqual(2, SuccessorList.Count);
                Assert.AreEqual(0, SuccessorList[0].Count);
                Assert.AreEqual(0, SuccessorList[1].Count);
            }

            [TestMethod]
            public void TestWhenAdjacencyMatrixContainsOneTrueOnFirstPosition()
            {
                var SuccessorList = Transforms.ToSuccessorsList(new bool[,] { { true, false }, { false, false } });

                Assert.AreEqual(2, SuccessorList.Count);
                Assert.AreEqual(1, SuccessorList[0].Count);
                Assert.AreEqual(0, SuccessorList[1].Count);
                Assert.AreEqual(0, SuccessorList[0][0]);
            }

            [TestMethod]
            public void TestWhenAdjacencyMatrixContainsContainsOnlyTrue()
            {
                var SuccessorList = Transforms.ToSuccessorsList(new bool[,] { { true, true }, { true, true } });

                Assert.AreEqual(2, SuccessorList.Count);
                Assert.AreEqual(2, SuccessorList[0].Count);
                Assert.AreEqual(0, SuccessorList[0][0]);
                Assert.AreEqual(1, SuccessorList[0][1]);
                Assert.AreEqual(2, SuccessorList[1].Count);
                Assert.AreEqual(0, SuccessorList[1][0]);
                Assert.AreEqual(1, SuccessorList[1][1]);
            }

            [TestMethod]
            public void TestWhenAdjacencyMatrixHasCircularReference()
            {
                var SuccessorList = Transforms.ToSuccessorsList(new bool[,] { { false, true }, { true, false } });

                Assert.AreEqual(2, SuccessorList.Count);
                Assert.AreEqual(1, SuccessorList[0].Count);
                Assert.AreEqual(1, SuccessorList[0][0]);
                Assert.AreEqual(1, SuccessorList[1].Count);
                Assert.AreEqual(0, SuccessorList[1][0]);
            }

            [TestMethod, ExpectedException(typeof(ArgumentNullException))]
            public void TestWhenAdjacencyMatrixIsNull()
            {
                Transforms.ToSuccessorsList(null);
            }

            [TestMethod, ExpectedException(typeof(ArgumentException))]
            public void TestWhenAdjacencyMatrixIsNotSquareMatrix()
            {
                Transforms.ToSuccessorsList(new bool[,] { { false }, { true } });
            }
        }

        [TestClass]
        public class ToAdjacencyMatrixTests
        {
            [TestMethod]
            public void TestWhenSuccessorsListIsEmpty()
            {
                var adjacencyMatrix = Transforms.ToAdjacencyMatrix(new List<IList<int>>());

                Assert.AreEqual(0, adjacencyMatrix.Length);
            }

            [TestMethod]
            public void TestWhenSuccessorsListHas4Nulls()
            {
                var adjacencyMatrix = Transforms.ToAdjacencyMatrix(new List<IList<int>>() { null, null, null, null });

                Assert.AreEqual(16, adjacencyMatrix.Length);
                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                        Assert.IsFalse(adjacencyMatrix[i, j]);
            }
        }
    }
}
