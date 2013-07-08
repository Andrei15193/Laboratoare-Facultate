using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace IALab205
{
    public class Problema5ParticleSwarmOptimization : AbstractParticleSwarmOptimization
    {
        public Problema5ParticleSwarmOptimization(Graf graf)
        {
            _graf = graf;
        }

        protected override IEnumerable<IParticula> GenereazaPopulatie(int dimensiune, double factorDeInertie)
        {
            Random random = Globale.Random;
            IEnumerable<IParticula> particule = _GenereazaParticule(dimensiune, factorDeInertie);

            foreach (Particula particula in particule)
            {
                int numarMaximDeVecini = Math.Min(dimensiune - 1, 20);
                IEnumerable<IParticula> veciniCandidati = particule.Except(new IParticula[] { particula });
                for (int i = 0; i < random.Next(1, numarMaximDeVecini); i++)
                {
                    IParticula vecin = veciniCandidati.ElementAt(random.Next(dimensiune - 1));
                    particula.Vecini.Add(vecin);
                    vecin.Vecini.Add(particula);
                }
            }
            return particule;
        }

        private IEnumerable<IParticula> _GenereazaParticule(int dimensiune, double factorDeInertie)
        {
            LinkedList<IParticula> particule = new LinkedList<IParticula>();
            for (int i = 0; i < dimensiune; i++)
                particule.AddLast(new Particula(_graf, factorDeInertie, -4, 4));
            return particule;
        }

        private readonly Graf _graf;
    }
}
