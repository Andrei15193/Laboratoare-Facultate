using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IALab205
{
    public interface ICromozom
    {
        ICromozom Incruciseaza(ICromozom cromozom);

        ICromozom Muteaza(TipMutatie tipMutatie);

        int Fitness { get; }
    }
}
