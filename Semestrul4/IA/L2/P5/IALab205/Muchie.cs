using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IALab205
{
    public class Muchie
    {
        public Muchie(int primul, int alDoilea)
        {
            PrimulNod = primul;
            AlDoileaNod = alDoilea;
        }

        public int PrimulNod { get; private set; }

        public int AlDoileaNod { get; private set; }
    }
}
