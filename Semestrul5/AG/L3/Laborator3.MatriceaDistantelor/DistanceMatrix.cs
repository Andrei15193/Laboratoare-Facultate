using AlgoritmicaGrafelor.Laborator2.DrumuriMinime;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AlgoritmicaGrafelor.Laborator3.MatriceaDistantelor
{
    public static class DistanceMatrix
    {
        static public double[,] MinimumRoadsDistance(IReadOnlyList<IReadOnlyList<int>> predecessorsLists)
        {
            if (predecessorsLists != null)
            {
                double[,] distanceMatrix = new double[predecessorsLists.Count, predecessorsLists.Count];
                List<IReadOnlyDictionary<int, double>> predecessorsListsWithValuesOfOne = new List<IReadOnlyDictionary<int, double>>();

                foreach (IReadOnlyList<int> predecessorsList in predecessorsLists)
                    predecessorsListsWithValuesOfOne.Add(_AddValueForEach(predecessorsList, 1d));

                for (int startPeek = 0; startPeek < predecessorsLists.Count; startPeek++)
                    for (int endPeek = 0; endPeek < predecessorsLists.Count; endPeek++)
                        if (startPeek == endPeek)
                            distanceMatrix[startPeek, endPeek] = 0;
                        else
                            distanceMatrix[startPeek, endPeek] = MinRoads.Yen(predecessorsListsWithValuesOfOne, startPeek, endPeek);

                return distanceMatrix;
            }
            else
                throw new ArgumentNullException("predecessorsLists");
        }

        static private IReadOnlyDictionary<int, double> _AddValueForEach(IReadOnlyList<int> list, double value)
        {
            if (list != null)
            {
                IDictionary<int, double> result = new Dictionary<int, double>();

                foreach (int item in list)
                    result.Add(item, value);

                return new ReadOnlyDictionary<int, double>(result);
            }
            else
                throw new ArgumentNullException("list");
        }
    }
}
