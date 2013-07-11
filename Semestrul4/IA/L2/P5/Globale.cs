using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IALab205
{
    public class Globale
    {
        public static Random Random
        {
            get
            {
                if (_random == null)
                     _random = new Random(DateTime.Now.Millisecond);
                return _random;
            }
        }

        private static Random _random = null;
    }
}
