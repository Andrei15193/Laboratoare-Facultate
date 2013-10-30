using System;
using System.Collections.Generic;
using AlgoritmicaGrafelor.Laborator2.DrumuriMinime;

namespace AlgoritmicaGrafelor.Laborator2
{
    internal static class Program
    {
        static private void Main(string[] args)
        {
            List<IReadOnlyDictionary<int, double>> graph = new List<IReadOnlyDictionary<int,double>>();

            Dictionary<int, double> peeks = new Dictionary<int,double>();
            peeks.Add(1, 1);
            peeks.Add(2, 1);
            graph.Add(peeks);
            peeks = new Dictionary<int, double>();
            peeks.Add(3, 1);
            graph.Add(peeks);
            peeks = new Dictionary<int, double>();
            peeks.Add(3, 2);
            graph.Add(peeks);
            peeks = new Dictionary<int, double>();
            graph.Add(peeks);

            Console.WriteLine(MinRoads.Yen(graph, 3, 0));
        }
    }
}
