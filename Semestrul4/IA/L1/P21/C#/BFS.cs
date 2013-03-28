using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace IALab1
{
    public class BFS : UninformedSearchMethod
    {
        public uint SearchForFrobeniusNumber(uint[] values, uint maxA)
        {
            uint maxValue = 0;
            LinkedList<uint> numbers = CalculateNumbers(GenerateCoefs(values, maxA), values);
            uint[] resultedNumbers = numbers.ToArray();
            Array.Sort(resultedNumbers);
            for (uint i = 0; i < resultedNumbers.Length - 1; i++)
                if (resultedNumbers[i + 1] - resultedNumbers[i] > 0 && CheckSolution(resultedNumbers[i + 1] -1, values))
                    maxValue = resultedNumbers[i + 1] - 1;
            return maxValue;
        }

        private bool CheckSolution(uint p, uint[] values)
        {
            uint i = 0;
            while (i < values.Length && p % values[i] != 0)
                i++;
            return i == values.Length;
        }

        private LinkedList<uint> CalculateNumbers(Queue<uint[]> queue, uint[] values)
        {
            LinkedList<uint> numbers = new LinkedList<uint>();
            while (queue.Count > 0)
                numbers.AddFirst(CalculateNumbers(queue.Dequeue(), values));
            return numbers;
        }

        private uint CalculateNumbers(uint[] p, uint[] values)
        {
            uint result = 0;
            for (uint i = 0; i < p.Length; i++)
                result += p[i] * values[i];
            return result;
        }

        private Queue<uint[]> GenerateCoefs(uint[] values, uint maxA)
        {
            uint[] currentNode, copy;
            Queue<uint[]> nodes = new Queue<uint[]>();
            nodes.Enqueue(GetZeroArray((uint)values.Length));
            for (uint currentIndex = 0; currentIndex < values.Length; currentIndex++)
                for (double k = 0; k < Math.Pow(maxA, currentIndex); k++)
                {
                    currentNode = nodes.Dequeue();
                    for (uint value = 0; value < maxA; value++)
                    {
                        copy = currentNode.Clone() as uint[];
                        copy[currentIndex] = value;
                        nodes.Enqueue(copy);
                    }
                }
            return nodes;
        }

        private string GetString(uint[] p)
        {
            StringBuilder builder = new StringBuilder();
            foreach (uint item in p)
            {
                builder.Append(item);
            }
            return builder.ToString();
        }

        private uint[] GetZeroArray(uint maxA)
        {
            uint[] array = new uint[maxA];
            for (int i = 0; i < maxA; i++)
                array[i] = 0;
            return array;
        }
    }
}
