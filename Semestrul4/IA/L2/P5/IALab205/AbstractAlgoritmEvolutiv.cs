using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace IALab205
{
    public abstract class AbstractAlgoritmEvolutiv
    {
        public ICromozom CautaRezultat(int dimensiunePopulatie, int numarDeGenerati, TipMutatie tipMutatie, Func<int, int, int> comparareFitness)
        {
            if (comparareFitness != null)
                if (dimensiunePopulatie > 0)
                {
                    Random random = Globale.Random;
                    ICromozom[] generatieCurenta = CreazaPopulatie(dimensiunePopulatie).ToArray();
                    ICollection<ICromozom> nouaGeneratie = new LinkedList<ICromozom>();
                    for (int generatie = 0; generatie < numarDeGenerati; generatie++)
                    {
                        Debug.WriteLine("Generatia: {0}", generatie);
                        for (int i = 0; i < dimensiunePopulatie; i++)
                            nouaGeneratie.Add(generatieCurenta[random.Next(dimensiunePopulatie)].Incruciseaza(generatieCurenta[random.Next(dimensiunePopulatie)]).Muteaza(tipMutatie));
                        generatieCurenta = nouaGeneratie.ToArray();
                        nouaGeneratie.Clear();
                    }
                    return generatieCurenta.OrderBy((cromoz) => cromoz.Fitness, new Comparator<int>(comparareFitness)).First();
                }
                else
                    throw new ArgumentException("Dimensiunea populatiei nu poate fi mai mica sau egala cu 0 (zero)!");
            else
                throw new ArgumentNullException("Valoarea functiei de determinare a fitnessului optim nu poate fi null!");
        }

        protected abstract IEnumerable<ICromozom> CreazaPopulatie(int dimensiunePopulatie);
    }
}
