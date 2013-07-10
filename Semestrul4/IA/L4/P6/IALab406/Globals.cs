using System;

namespace IALab406
{
    public sealed class Globals
    {
        private Globals()
        {
            Random = new Random();
        }

        public static Globals Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Globals();
                return _instance;
            }
        }

        public Random Random { get; private set; }

        private static Globals _instance = null;
    }
}
