using AlgoritmicaGrafelor.Laborator3.MatriceaDistantelor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AlgoritmicaGrafelor.Laborator2.DrumuriMinime.Tests
{
    [TestClass]
    public class DistanceMatrixTests
    {
        [TestClass]
        public class MinimumRoadsDistance
        {
            static private readonly IReadOnlyList<IReadOnlyList<int>> _onePeekGraph = new[]
            {
                new List<int>{ }
            };
            static private readonly IReadOnlyList<IReadOnlyList<int>> _twoPeekGraph = new[]
            {
                new List<int>{ },
                new List<int>{ 0 }
            };
            static private readonly IReadOnlyList<IReadOnlyList<int>> _fourPeekGraphWithTwoRoadsEqualInLengthForSameExtremities = new[]
            {
                new List<int>{ },
                new List<int>{ 0 },
                new List<int>{ 0 },
                new List<int>{ 1, 2 }
            };
            static private readonly IReadOnlyList<IReadOnlyList<int>> _fivePeekGraphWithACycle = new[]
            {
                new List<int>{ },
                new List<int>{ 0, 3 },
                new List<int>{ 1 },
                new List<int>{ 2 },
                new List<int>{ 1 }
            };
            static private readonly IReadOnlyList<IReadOnlyList<int>> _fivePeekGraphWithARoadLongerThanTheOtheForSameExtremities = new[]
            {
                new List<int>{ },
                new List<int>{ 0 },
                new List<int>{ 1 },
                new List<int>{ 2, 4 },
                new List<int>{ 0 }
            };

            [TestMethod]
            public void TestForOnePeekGraph()
            {
                double[,] expectedResultMatrix = new[,]{
                    { 0d } };
                double[,] actualResultMatrix
                    = DistanceMatrix.MinimumRoadsDistance(_onePeekGraph);

                _AreEqual(expectedResultMatrix, actualResultMatrix, 1, 1);
            }

            [TestMethod]
            public void TestForTwoPeekGraphWithOneRoadFromZeroToOne()
            {
                double[,] expectedResultMatrix = new[,] {
                    { 0,                       1 },
                    { double.PositiveInfinity, 0 } };
                double[,] actualResultMatrix
                   = DistanceMatrix.MinimumRoadsDistance(_twoPeekGraph);

                _AreEqual(expectedResultMatrix, actualResultMatrix, 2, 2);
            }

            [TestMethod]
            public void TestForFourPeekGraphHavingTwoRoadsOfSameLength()
            {
                double[,] expectedResultMatrix = new[,] {
                    { 0,                       1,                       1,                       2 },
                    { double.PositiveInfinity, 0,                       double.PositiveInfinity, 1 },
                    { double.PositiveInfinity, double.PositiveInfinity, 0,                       1 },
                    { double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, 0 } };
                double[,] actualResultMatrix
                    = DistanceMatrix.MinimumRoadsDistance(_fourPeekGraphWithTwoRoadsEqualInLengthForSameExtremities);

                _AreEqual(expectedResultMatrix, actualResultMatrix, 3, 3);
            }

            [TestMethod]
            public void TestForFivePeekGrapthWithCycle()
            {
                double[,] expectedResultMatrix = new[,] {
                    { 0,                       1,                       2,                       3,                       2 },
                    { double.PositiveInfinity, 0,                       1,                       2,                       1 },
                    { double.PositiveInfinity, 2,                       0,                       1,                       3 },
                    { double.PositiveInfinity, 1,                       2,                       0,                       2 },
                    { double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, 0 } };
                double[,] actualResultMatrix
                    = DistanceMatrix.MinimumRoadsDistance(_fivePeekGraphWithACycle);

                _AreEqual(expectedResultMatrix, actualResultMatrix, 4, 4);

            }

            [TestMethod]
            public void TestForFivePeekGraphContainingTwoRoadsOfDifferentLengthsForSameExtremities()
            {
                double[,] expectedResultMatrix = new[,] {
                    { 0,                       1,                       2,                       2, 1 },
                    { double.PositiveInfinity, 0,                       1,                       2, double.PositiveInfinity },
                    { double.PositiveInfinity, double.PositiveInfinity, 0,                       1, double.PositiveInfinity },
                    { double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, 0, double.PositiveInfinity },
                    { double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, 1, 0 } };
                double[,] actualResultMatrix
                    = DistanceMatrix.MinimumRoadsDistance(_fivePeekGraphWithARoadLongerThanTheOtheForSameExtremities);

                _AreEqual(expectedResultMatrix, actualResultMatrix, 5, 5);
            }

            private void _AreEqual(double[,] expectedResultMatrix, double[,] actualResultMatrix, int lineCount, int columnCount)
            {
                for (int line = 0; line < lineCount; line++)
                    for (int column = 0; column < columnCount; column++)
                        if (expectedResultMatrix[line, column] != actualResultMatrix[line, column])
                            Assert.Fail("Not equal!");
            }
        }
    }
}
