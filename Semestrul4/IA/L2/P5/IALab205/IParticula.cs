using System.Collections.Generic;

namespace IALab205
{
    public interface IParticula
    {
        void Deplaseaza(double factorDeInvatareCognitiv, double factorDeInvatareSocial);

        int Fitness { get; }

        double[] Viteze { get; }

        double VitezaMaxima { get; }

        double VitezaMinima { get; }

        ICollection<IParticula> Vecini { get; }
    }
}
