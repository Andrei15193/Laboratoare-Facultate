using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDLab6
{
    public class Categorie
    {
        public Categorie(String nume, int cod)
        {
            this.nume = nume;
            this.cod = cod;
        }

        public int Cod
        {
            get
            {
                return this.cod;
            }
        }

        public String Nume 
        {
            get
            {
                return this.nume;
            }
        }

        public override string ToString()
        {
            return this.nume;
        }

        private int cod;
        private String nume;
    }
}
