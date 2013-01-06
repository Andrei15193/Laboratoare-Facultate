using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDLab6
{
    class Ingredient
    {
        public Ingredient(int cod, string nume, string unitate_masura)
        {
            this.cod = cod;
            this.nume = nume;
            this.unitate_masura = unitate_masura;
        }

        public int Cod
        {
            get
            {
                return this.cod;
            }
        }

        public string Nume
        {
            get
            {
                return this.nume;
            }
        }

        public string UnitateMasura
        {
            get
            {
                return this.unitate_masura;
            }
        }

        public override string ToString()
        {
            return this.nume;
        }

        private int cod;
        private string nume;
        private string unitate_masura;
    }
}
