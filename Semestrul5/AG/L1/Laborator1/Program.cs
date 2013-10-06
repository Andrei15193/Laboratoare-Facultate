using AlgoritmicaGrafelor.Laborator1.Transformari;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgoritmicaGrafelor.Laborator1
{
    internal class Program
    {
        static private void Main(string[] args)
        {
            try
            {
                int option;
                do
                {
                    Console.WriteLine("Graph transforms. Which transform would you like to make?");
                    Console.WriteLine("1. adjacency matrix -> successors list");
                    Console.WriteLine("2. successors list -> adjacency matrix");
                    Console.WriteLine("3. exit");

                    option = _ReadOption(1, 3);
                    switch (option)
                    {
                        case 1:
                            _AdjacencyMatrixToSuccessorsListOption();
                            break;
                        case 2:
                            _SuccessorsListToAdjacencyMatrixOption();
                            break;
                    }
                } while (option != 3);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Invalid operation:");
                Console.WriteLine(exception.Message);
            }
        }

        static private void _AdjacencyMatrixToSuccessorsListOption()
        {
            int peakNumber = _ReadInteger("Number of peaks: ");

            while (peakNumber < 0)
            {
                Console.WriteLine("Peak number must be positive!");
                peakNumber = _ReadInteger("Number of peaks: ");
            }

            WriteSuccessorsList(Transforms.ToSuccessorsList(_ReadSquareMatrix(peakNumber)));
        }

        static private bool[,] _ReadSquareMatrix(int peakNumber)
        {
            bool[,] matrix = new bool[peakNumber, peakNumber];

            Console.WriteLine("0 = false, any other number = true.");
            for (int lineIndex = 0; lineIndex < peakNumber; lineIndex++)
                for (int columnIndex = 0; columnIndex < peakNumber; columnIndex++)
                    matrix[lineIndex, columnIndex] = (_ReadInteger(string.Format("matrix[{0}][{1}] = ", lineIndex, columnIndex)) != 0);

            return matrix;
        }

        static private void WriteSuccessorsList(IList<IList<int>> successorsList)
        {
            Console.WriteLine("Successors list: ");
            for (int peakIndex = 0; peakIndex < successorsList.Count; peakIndex++)
                Console.WriteLine("{0}: {1}", peakIndex, string.Join(", ", successorsList[peakIndex].Select(successor => successor.ToString())));
        }

        static private void _SuccessorsListToAdjacencyMatrixOption()
        {
            int peakNumber = _ReadInteger("Number of peaks: ");

            while (peakNumber < 0)
            {
                Console.WriteLine("Peak number must be positive!");
                peakNumber = _ReadInteger("Number of peaks: ");
            }

            WriteAdjacencyMatrix(Transforms.ToAdjacencyMatrix(_ReadSuccessorsList(peakNumber)));
        }

        static private IList<IList<int>> _ReadSuccessorsList(int peakNumber)
        {
            IList<IList<int>> successorsList = new List<IList<int>>(peakNumber);

            for (int peakIndex = 0; peakIndex < peakNumber; peakIndex++)
            {
                int numberOfSuccessors = _ReadInteger(string.Format("Number of successors for {0}: ", peakIndex));
                IList<int> successorList = new List<int>(numberOfSuccessors);

                while (numberOfSuccessors > 0)
                {
                    successorList.Add(_ReadInteger(string.Format("Successor to {0}: ", peakIndex)));
                    numberOfSuccessors--;
                }

                successorsList.Add(successorList);
            }

            return successorsList;
        }

        static private void WriteAdjacencyMatrix(bool[,] adajacencyMatrix)
        {
            Console.WriteLine("Adjacency matrix: ");

            for (int lineIndex = 0; lineIndex < adajacencyMatrix.GetLength(1); lineIndex++)
            {
                for (int columnIndex = 0; columnIndex < adajacencyMatrix.GetLength(1); columnIndex++)
                    Console.Write("{0} ", Convert.ToInt32(adajacencyMatrix[lineIndex, columnIndex]));
                Console.WriteLine();
            }
        }

        static private int _ReadOption(int minimum, int maximum)
        {
            int option = _ReadInteger("Your option: ");

            while (option < minimum || maximum < option)
            {
                Console.WriteLine("Invalid option.");
                option = _ReadInteger("Your option: ");
            }

            return option;
        }

        static private int _ReadInteger(string message)
        {
            int number;

            Console.Write(message);
            while (!int.TryParse(Console.ReadLine(), out number))
            {
                Console.WriteLine("Must enter a number!");
                Console.Write(message);
            }

            return number;
        }
    }
}
