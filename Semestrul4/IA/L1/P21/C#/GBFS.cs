using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IALab1
{
    public class GBFS : BestFS
    {
        public uint SearchForFrobeniusNumber(uint[] values, uint aMax)
        {
            uint value = (uint)values.Select((e) => e * aMax).Sum((x) => x) - 1;
            Array.Sort(values);
            while (value > 1 && !CheckSolution(value, values))
                value -= values[0];
            return value <= 1 ? 1 : value;
        }

        private bool CheckSolution(uint p, uint[] values)
        {
            uint i = 0;
            while (i < values.Length && p % values[i] != 0)
                i++;
            return i == values.Length;
        }
    }
}
