using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;

namespace IALab406
{
    [Serializable]
    public class GeneticAlgorithm
    {
        public void GeneratePopulation(int density)
        {
            _population = new LinkedList<RobotChromosome>();
            for (int i = 0; i < density; i++)
                _population.Add(new RobotChromosome(24, 10));
        }

        public void ResetEntryDataIndex()
        {
            _entryDataIndex = 0;
        }

        public Tuple<RobotChromosome, double> Learn(int cycles, StreamReader dataReader)
        {
            Tuple<RobotChromosome, double> best = null;
            Random random = Globals.Instance.Random;
            for (int i = 0; i < _entryDataIndex; i++)
                dataReader.ReadLine();
            for (int i = 0; i < cycles; i++)
            {
                Debug.WriteLine("Cycle {0}:", i);
                string[] data = dataReader.ReadLine().Split(',');
                double[] input = new double[24];
                for (int j = 0; j < 24; j++)
                    input[j] = double.Parse(data[j]);
                var sortedPopulationByFitness = _population.Select((chromosome) => new
                {
                    Chromosome = chromosome,
                    Fitness = Math.Abs(_ParseToInt(data[24]) - Math.Abs(Math.Round(chromosome.Apply(input)) % 4))
                }).OrderBy((chromosome) => chromosome.Fitness);
                var first = sortedPopulationByFitness.ElementAt(random.Next(Math.Min(_population.Count, 20)));
                var second = sortedPopulationByFitness.ElementAt(random.Next(Math.Min(_population.Count, 20)));
                RobotChromosome newBorn = first.Chromosome.CrossOver(second.Chromosome).Mutate();
                var candidate = new
                {
                    Chromosome = newBorn,
                    Fitness = Math.Abs(_ParseToInt(data[24]) - Math.Abs(Math.Round(newBorn.Apply(input)) % 4))
                };
                var last = sortedPopulationByFitness.Last();
                if (last.Fitness <= candidate.Fitness)
                {
                    Debug.WriteLine("|ROUND( {0} )| % 4, Fitness: {1}", candidate.Chromosome.ToString(), candidate.Fitness);
                    Debug.WriteLine("is no good chromosome!");
                    Debug.WriteLine("population is unchanged!");
                }
                else
                {
                    _population = new HashSet<RobotChromosome>(_population.Take(_population.Count - 1).Concat(new RobotChromosome[] { candidate.Chromosome }));
                    Debug.WriteLine("|ROUND( {0} )| % 4, Fitness: {1}", candidate.Chromosome.ToString(), candidate.Fitness);
                    Debug.WriteLine("has replaced");
                    Debug.WriteLine("|ROUND( {0} )| % 4, Fitness: {1}", last.Chromosome.ToString(), last.Fitness);
                }
                best = new Tuple<RobotChromosome, double>(sortedPopulationByFitness.First().Chromosome, sortedPopulationByFitness.First().Fitness);
            }
            return best;
        }

        public int PopulationDensity
        {
            get
            {
                if (_population == null)
                    return -1;
                else
                    return _population.Count;
            }
        }

        private double _ParseToInt(string label)
        {
            switch (label)
            {
                case "Slight-Left-Turn":
                    return 0;
                case "Move-Forward":
                    return 1;
                case "Slight-Right-Turn":
                    return 2;
                case "Sharp-Right-Turn":
                    return 3;
                default:
                    return 4;
            }
        }

        private int _entryDataIndex = 0;
        private ICollection<RobotChromosome> _population;
    }
}
