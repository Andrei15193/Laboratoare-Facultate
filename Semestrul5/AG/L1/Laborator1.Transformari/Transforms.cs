using System;
using System.Collections.Generic;

namespace AlgoritmicaGrafelor.Laborator1.Transformari
{
    public static class Transforms
    {
        /// <exception cref="System.ArgumentNullException">Thrown when adjancency matrix is null.</exception>
        /// <exception cref="System.ArgumentException">Thrown when the adjacency matrix is not a square matrix.</exception>
        static public IList<IList<int>> ToSuccessorList(bool[,] adjacencyMatrix)
        {
            if (adjacencyMatrix != null)
            {
                int numberOfPeaks = (int)Math.Sqrt(adjacencyMatrix.Length);

                if (Math.Pow(numberOfPeaks, 2) == adjacencyMatrix.Length)
                {
                    IList<IList<int>> SuccessorsList = new List<IList<int>>(numberOfPeaks);

                    for (int i = 0; i < numberOfPeaks; i++)
                    {
                        IList<int> SuccessorPeaks = new List<int>();

                        for (int j = 0; j < numberOfPeaks; j++)
                            if (adjacencyMatrix[i, j])
                                SuccessorPeaks.Add(j);

                        SuccessorsList.Add(SuccessorPeaks);
                    }

                    return SuccessorsList;
                }
                else
                    throw new ArgumentException("The adjacencyMatrix is not a square matrix!");
            }
            else
                throw new ArgumentNullException("adjacencyMatrix");
        }

        /// <exception cref="System.ArgumentNullException">Thrown when successorsList is null.</exception>
        /// <exception cref="System.ArgumentException">Thrown when the successorsList contains a peak not present in the graph.</exception>
        static public bool[,] ToAdjacencyMatrix(IList<IList<int>> successorsList)
        {
            if (successorsList != null)
            {
                bool[,] adjacencyMatrix = new bool[successorsList.Count, successorsList.Count];

                for (int i = 0; i < successorsList.Count; i++)
                    if (successorsList[i] != null)
                        foreach (int successor in successorsList[i])
                            if (0 <= successor && successor <= successorsList.Count)
                                adjacencyMatrix[i, successor] = true;
                            else
                                throw new ArgumentException("The successor " + successor + " is not a peak in the graph!");
                return adjacencyMatrix;
            }
            else
                throw new ArgumentNullException("successorList");
        }
    }
}
