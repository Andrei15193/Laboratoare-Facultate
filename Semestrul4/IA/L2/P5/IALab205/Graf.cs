using System;
using System.Collections.Generic;
using System.Linq;

namespace IALab205
{
    public class Graf
    {
        public Graf(bool[,] adiacenta)
        {
            int nrNoduri = (int)Math.Sqrt(adiacenta.Length);
            ICollection<Muchie> muchii = new LinkedList<Muchie>();
            for (int i = 1; i <= nrNoduri; i++)
            {
                noduri.Add(i);
                for (int j = i + 1; j <= nrNoduri; j++)
                    if (adiacenta[i - 1, j - 1])
                        muchii.Add(new Muchie(i, j));
            }
            this.muchii = muchii.ToArray();
        }

        public Muchie[] Muchii
        {
            get
            {
                return muchii;
            }
        }

        public IEnumerable<int> Noduri
        {
            get
            {
                return noduri;
            }
        }

        private Muchie[] muchii;

        private ISet<int> noduri = new HashSet<int>();
    }
}
