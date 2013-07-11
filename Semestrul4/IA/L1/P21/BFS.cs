using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace IALab1
{
    public class BFS : UninformedSearchMethod
    {
        public uint SearchForFrobeniusNumber(uint[] values)
        {
            uint j = 0;
            uint[] coefs = new uint[values.Length];
            Queue<uint[]> nodes = new Queue<uint[]>();
            for (uint i = 0; i < values.Length; i++)
                coefs[i] = 0;
            return (uint)values.Length;
        }
    }
}
