using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace IALab205
{
    public abstract class AbstractParticleSwarmOptimization
    {
        public IParticula CautaRezultat(int dimensiunePopulatie, int numarDeCicluri, double factorDeInertie, double factorDeInvatareCognitiv, double factorDeInvatareSocial, Func<int, int, int> comparareFitness)
        {
            if (comparareFitness != null)
                if (dimensiunePopulatie > 0)
                {
                    Random random = Globale.Random;
                    IEnumerable<IParticula> populatie = GenereazaPopulatie(dimensiunePopulatie, factorDeInertie);
                    for (int i = 0; i < numarDeCicluri; i++)
                    {
                        Debug.WriteLine("Ciclul: {0}", i);
                        Parallel.ForEach(populatie, (particula) =>
                            {
                                particula.Deplaseaza(factorDeInvatareCognitiv, factorDeInvatareSocial);
                            }
                        );
                    }
                    return populatie.OrderBy((cromoz) => cromoz.Fitness, new Comparator<int>(comparareFitness)).First();
                }
                else
                    throw new ArgumentException("Dimensiunea populatiei nu poate fi mai mica sau egala cu 0 (zero)!");
            else
                throw new ArgumentNullException("Valoarea functiei de determinare a fitnessului optim nu poate fi null!");
        }

        protected abstract IEnumerable<IParticula> GenereazaPopulatie(int dimensiune, double factorDeInertie);
    }
}
