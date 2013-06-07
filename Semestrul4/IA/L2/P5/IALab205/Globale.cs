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
                if (random == null)
                     random = new Random(DateTime.Now.Millisecond);
                return random;
            }
        }

        private static Random random = null;
    }
}
