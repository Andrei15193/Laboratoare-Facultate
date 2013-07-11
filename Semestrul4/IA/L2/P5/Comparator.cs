using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IALab205
{
    public class Comparator<T> : IComparer<T>
    {
        public Comparator(Func<T, T, int> functieDeComparare)
        {
            this._functieDeComparare = functieDeComparare;
        }

        public int Compare(T x, T y)
        {
            return _functieDeComparare(x, y);
        }

        private Func<T, T, int> _functieDeComparare;
    }
}
