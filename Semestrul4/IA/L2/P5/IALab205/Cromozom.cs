using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IALab205
{
    public class Cromozom : ICromozom
    {
        public Cromozom(Graf graf)
        {
            this.Graf = graf;
            muchii = new BitArray(graf.Muchii.Length);
            Random random = Globale.Random;
            for (int i = 0; i < muchii.Length; i++)
                muchii[i] = (random.Next(10) < 5);
        }

        public ICromozom Incruciseaza(ICromozom cromozom)
        {
            Cromozom fiu = null;
            if (cromozom is Cromozom)
            {
                fiu = new Cromozom(cromozom as Cromozom);
                Random random = Globale.Random;
                int indexCurent = 0, lungimeCurenta = random.Next(fiu.muchii.Length);
                while (indexCurent < fiu.muchii.Length && indexCurent < fiu.muchii.Length - 3)
                {
                    for (int i = indexCurent; i < lungimeCurenta; i++)
                        fiu.muchii[i] = muchii[i];
                    indexCurent += lungimeCurenta;
                    indexCurent += random.Next(fiu.muchii.Length - indexCurent);
                    lungimeCurenta = random.Next(fiu.muchii.Length - indexCurent);
                }
            }
            return fiu;
        }

        public ICromozom Muteaza(TipMutatie tipMutatie)
        {
            if (muchii.Length > 0)
            {
                Random random = Globale.Random;
                int index = random.Next(muchii.Length);
                switch (tipMutatie)
                {
                    case TipMutatie.Tare:
                        muchii[index] = !muchii[index];
                        break;
                    default:
                        muchii[index] = random.Next(10) < 5;
                        break;
                }
            }
            return this;
        }

        public int Fitness
        {
            get
            {
                return _NumaraVecini(true) + _NumaraVecini(false);
            }
        }

        public Graf Graf { get; private set; }

        public IEnumerable<Muchie> MuchiilePrimuluiGraf
        {
            get
            {
                for (int i = 0; i < Graf.Muchii.Length; i++)
                    if (muchii[i])
                        yield return Graf.Muchii[i];
            }
        }

        public IEnumerable<Muchie> MuchiileCeluiDeAlDoileaGraf
        {
            get
            {
                for (int i = 0; i < Graf.Muchii.Length; i++)
                    if (!muchii[i])
                        yield return Graf.Muchii[i];
            }
        }

        private int _NumaraVecini(bool estePrimulGraf)
        {
            int numarDeTriunghiuri = 0;
            foreach (int nod in Graf.Noduri)
                foreach (int vecin in _DeterminaVecini(nod, nod))
                    foreach (int vecinulVecinului in _DeterminaVecini(vecin, vecin, estePrimulGraf))
                        numarDeTriunghiuri += _DeterminaVecini(vecinulVecinului).Count((vecinulVecinuluiVecinului) => vecinulVecinuluiVecinului == nod);
            return numarDeTriunghiuri;
        }

        private Cromozom(Cromozom subiect)
        {
            Graf = subiect.Graf;
            muchii = subiect.muchii.Clone() as BitArray;
        }

        private IEnumerable<int> _DeterminaVecini(int nod, bool esteMuchie = true)
        {
            ICollection<int> vecini = new HashSet<int>();
            for (int i = 0; i < muchii.Length; i++)
                if (muchii[i] == esteMuchie)
                    if (Graf.Muchii[i].PrimulNod == nod)
                        vecini.Add(Graf.Muchii[i].AlDoileaNod);
                    else
                        if (Graf.Muchii[i].AlDoileaNod == nod)
                            vecini.Add(Graf.Muchii[i].PrimulNod);
            return vecini;
        }

        private IEnumerable<int> _DeterminaVecini(int nod, int limitaInferioara, bool esteMuchie = true)
        {
            return _DeterminaVecini(nod, esteMuchie).Where((vecin) => limitaInferioara < vecin);
        }

        private BitArray muchii;
    }
}
