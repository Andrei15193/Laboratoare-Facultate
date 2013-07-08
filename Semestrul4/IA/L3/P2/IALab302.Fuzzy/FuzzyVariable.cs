using System.Collections.Generic;

namespace IALab302.Fuzzy
{
    public class FuzzyVariable
    {
        public FuzzyVariable(string name)
        {
            Name = name;
            Labels = new LinkedList<FuzzyLabel>();
        }

        public string Name { get; private set; }

        public ICollection<FuzzyLabel> Labels { get; private set; }
    }
}
