using System;
using System.Collections.Generic;

namespace IALab205
{
    public class Problema5AlgoritmEvolutiv : AbstractAlgoritmEvolutiv
    {
        public Problema5AlgoritmEvolutiv(Graf graf)
        {
            this._graf = graf;
        }

        protected override IEnumerable<ICromozom> CreazaPopulatie(int dimensiunePopulatie)
        {
            ICollection<ICromozom> populatie = new LinkedList<ICromozom>();
            for (int i = 0; i < dimensiunePopulatie; i++)
                populatie.Add(new Cromozom(_graf));
            return populatie;
        }

        private Graf _graf;
    }
}
