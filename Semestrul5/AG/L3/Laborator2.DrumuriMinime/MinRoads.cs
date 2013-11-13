using System;
using System.Collections.Generic;

namespace AlgoritmicaGrafelor.Laborator2.DrumuriMinime
{
    public static class MinRoads
    {
        static public double Yen(IReadOnlyList<IReadOnlyDictionary<int, double>> predecessorsLists, int sourcePeek, int destinationPeek)
        {
            if (predecessorsLists != null)
                if (predecessorsLists.Count > 0)
                    if (0 <= sourcePeek
                        && sourcePeek < predecessorsLists.Count)
                        if (0 <= destinationPeek
                            && destinationPeek < predecessorsLists.Count)
                        {
                            List<int> allPeeksExceptSource = new List<int>();
                            double[] minimumRoadValues = new double[predecessorsLists.Count];

                            for (int peek = 0; peek < minimumRoadValues.Length; peek++)
                                if (peek == sourcePeek)
                                    minimumRoadValues[peek] = 0;
                                else
                                {
                                    allPeeksExceptSource.Add(peek);
                                    if (!predecessorsLists[peek].TryGetValue(sourcePeek, out minimumRoadValues[peek]))
                                        minimumRoadValues[peek] = double.PositiveInfinity;
                                }

                            bool foundASmallerRoadValue;

                            do
                            {
                                foundASmallerRoadValue = false;

                                for (int peekIndex = 0; peekIndex < allPeeksExceptSource.Count; peekIndex++)
                                    foreach (KeyValuePair<int, double> predecessor in predecessorsLists[allPeeksExceptSource[peekIndex]])
                                        if (minimumRoadValues[predecessor.Key] + predecessor.Value < minimumRoadValues[allPeeksExceptSource[peekIndex]])
                                        {
                                            minimumRoadValues[allPeeksExceptSource[peekIndex]] = minimumRoadValues[predecessor.Key] + predecessor.Value;
                                            foundASmallerRoadValue = true;
                                        }

                                for (int peekIndex = allPeeksExceptSource.Count - 1; peekIndex >= 0; peekIndex--)
                                    foreach (KeyValuePair<int, double> predecessor in predecessorsLists[allPeeksExceptSource[peekIndex]])
                                        if (minimumRoadValues[predecessor.Key] + predecessor.Value < minimumRoadValues[allPeeksExceptSource[peekIndex]])
                                        {
                                            minimumRoadValues[allPeeksExceptSource[peekIndex]] = minimumRoadValues[predecessor.Key] + predecessor.Value;
                                            foundASmallerRoadValue = true;
                                        }
                            } while (foundASmallerRoadValue);

                            return minimumRoadValues[destinationPeek];
                        }
                        else
                            throw new ArgumentOutOfRangeException("destinationPeek");
                    else
                        throw new ArgumentOutOfRangeException("sourcePeek");
                else
                    throw new ArgumentException("The graph is empty!");
            else
                throw new ArgumentNullException("predecessorsLists");
        }
    }
}
