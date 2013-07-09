using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace IALab205
{
    public class Particula : IParticula, ICloneable
    {
        public Particula(Graf graf, double factorDeInertie, double vitezaMinima, double vitezaMaxima)
        {
            Random random = Globale.Random;
            Graf = graf;
            Vecini = new HashSet<IParticula>();
            VitezaMaxima = vitezaMaxima;
            VitezaMinima = VitezaMinima;
            Viteze = new double[graf.Muchii.Length];
            for (int i = 0; i < Viteze.Length; i++)
                Viteze[i] = _NormalizeazaViteza(_GenereazaViteza(random));
            _memorie = new HashSet<Particula>();
            _muchii = new BitArray(graf.Muchii.Length);
            for (int i = 0; i < _muchii.Length; i++)
                _muchii[i] = (random.Next(10) < 5);
            _factorDeInertie = factorDeInertie;
        }

        public void Deplaseaza(double factorDeInvatareCognitiv, double factorDeInvatareSocial)
        {
            _memorie.Add(Clone() as Particula);
            Particula performantaProprie = _memorie.OrderBy((particula) => particula.Fitness).First();
            Particula performantaVecinului = Vecini.OrderBy((particula) => particula.Fitness).First() as Particula;
            for (int i = 0; i < Viteze.Length; i++)
            {
                Viteze[i] = _NormalizeazaViteza(_factorDeInertie * Viteze[i] + factorDeInvatareCognitiv * Globale.Random.NextDouble() * (Convert.ToInt32(performantaProprie._muchii[i]) - Convert.ToInt32(_muchii[i])) + factorDeInvatareSocial * Globale.Random.NextDouble() * (Convert.ToInt32(performantaVecinului._muchii[i]) - Convert.ToInt32(_muchii[i])));
                _muchii[i] = Convert.ToBoolean((Convert.ToInt32(_muchii[i]) + Viteze[i])%2);
            }
        }

        public object Clone()
        {
            return new Particula()
            {
                _memorie = this._memorie,
                _muchii = this._muchii.Clone() as BitArray,
                Vecini = this.Vecini,
                Viteze = this.Viteze.Clone() as double[],
                VitezaMaxima = this.VitezaMaxima,
                VitezaMinima = this.VitezaMinima,
                Graf = this.Graf
            };
        }

        public ICollection<IParticula> Vecini { get; private set; }

        public int Fitness
        {
            get
            {
                int numarDeTriunghiuri = 0;
                foreach (int nod in Graf.Noduri)
                    foreach (int vecin in _DeterminaVecini(nod, nod))
                        foreach (int vecinulVecinului in _DeterminaVecini(vecin, vecin))
                            numarDeTriunghiuri += _DeterminaVecini(vecinulVecinului).Count((vecinulVecinuluiVecinului) => vecinulVecinuluiVecinului == nod);
                foreach (int nod in Graf.Noduri)
                    foreach (int vecin in _DeterminaVecini(nod, nod, false))
                        foreach (int vecinulVecinului in _DeterminaVecini(vecin, vecin, false))
                            numarDeTriunghiuri += _DeterminaVecini(vecinulVecinului, false).Count((vecinulVecinuluiVecinului) => vecinulVecinuluiVecinului == nod);
                return numarDeTriunghiuri;
            }
        }

        public double[] Viteze { get; private set; }

        public double VitezaMaxima { get; private set; }

        public double VitezaMinima { get; private set; }

        public IEnumerable<Muchie> MuchiilePrimuluiGraf
        {
            get
            {
                for (int i = 0; i < Graf.Muchii.Length; i++)
                    if (_muchii[i])
                        yield return Graf.Muchii[i];
            }
        }

        public IEnumerable<Muchie> MuchiileCeluiDeAlDoileaGraf
        {
            get
            {
                for (int i = 0; i < Graf.Muchii.Length; i++)
                    if (!_muchii[i])
                        yield return Graf.Muchii[i];
            }
        }

        public Graf Graf { get; private set; }

        private Particula()
        {
        }

        private double _GenereazaViteza(Random random)
        {
            return random.Next((int)VitezaMinima, (int)VitezaMaxima) + (random.Next(1) * 2 - 1) * random.NextDouble();
        }

        private double _NormalizeazaViteza(double viteza)
        {
            if (viteza < VitezaMinima)
                return VitezaMinima;
            else
                if (viteza > VitezaMaxima)
                    return VitezaMaxima;
                else
                    return viteza;
        }

        private IEnumerable<int> _DeterminaVecini(int nod, bool esteMuchie = true)
        {
            ICollection<int> vecini = new HashSet<int>();
            for (int i = 0; i < _muchii.Length; i++)
                if (_muchii[i] == esteMuchie)
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

        private BitArray _muchii;
        private ICollection<Particula> _memorie;
        private double _factorDeInertie;
    }
}
