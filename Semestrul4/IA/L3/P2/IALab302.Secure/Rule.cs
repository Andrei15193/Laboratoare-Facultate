using System.Collections.Generic;
using System.Linq;

namespace IALab302
{
    public class Rule
    {
        public void Add(Fact fact, bool mustBeValid = true)
        {
            if (!facts.Keys.Contains(fact))
            {
                facts.Add(fact, mustBeValid);
                fact.Add(this);
            }
        }

        public void Add(Conclusion conclusion)
        {
            if (conclusions.Add(conclusion))
                conclusion.Add(this);
        }

        public bool IsValid
        {
            get
            {
                return facts.Count((entry) => entry.Key.IsValid.HasValue && entry.Key.IsValid.Value == entry.Value) == facts.Count;
            }
        }

        public IEnumerable<Fact> Facts
        {
            get
            {
                return facts.Keys;
            }
        }

        public IEnumerable<Conclusion> Conclusions
        {
            get
            {
                return conclusions;
            }
        }

        private IDictionary<Fact, bool> facts = new Dictionary<Fact, bool>();
        private ISet<Conclusion> conclusions = new HashSet<Conclusion>();
    }
}
