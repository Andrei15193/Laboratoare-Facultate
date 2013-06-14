using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCLab5
{
    class EqualityComparer<T> : IEqualityComparer<T>
    {
        public EqualityComparer(Func<T, T, bool> compareFunction, Func<T, int> hashFunction)
        {
            this.compareFunction = compareFunction;
            this.hashFunction = hashFunction;
        }

        public bool Equals(T x, T y)
        {
            return compareFunction(x, y);
        }

        public int GetHashCode(T obj)
        {
            return hashFunction(obj);
        }

        private Func<T, T, bool> compareFunction;
        private Func<T, int> hashFunction;
    }
}
