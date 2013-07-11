using System.Collections.Generic;
using System.Linq;

namespace IALab302
{
    public class SystemExpert
    {
        public SystemExpert(IEnumerable<Conclusion> conclusions)
        {
            ISet<Fact> facts = new HashSet<Fact>();
            Conclusions = conclusions;
            foreach (Conclusion conclusion in conclusions)
                foreach (Fact fact in GetFacts(conclusion))
                    facts.Add(fact);
            Facts = facts;
        }

        public IEnumerable<Fact> GetFacts(Conclusion conclusion)
        {
            if (Conclusions.Contains(conclusion))
            {
                ISet<Fact> facts = new HashSet<Fact>();
                foreach (Rule rule in conclusion.Rules)
                    foreach (Fact fact in GetFacts(rule))
                        facts.Add(fact);
                return facts;
            }
            else
                return new Fact[0];
        }

        public IEnumerable<Fact> GetFacts(Rule rule)
        {
            ISet<Fact> facts = new HashSet<Fact>();
            Queue<Rule> leftRules = new Queue<Rule>();
            leftRules.Enqueue(rule);
            while (leftRules.Count > 0)
            {
                Rule leftRule = leftRules.Dequeue();
                foreach (Fact fact in leftRule.Facts)
                    facts.Add(fact);
            }
            return facts;
        }

        public void Reset()
        {
            foreach (Fact fact in Facts)
                fact.IsValid = null;
        }

        public bool HasConclusion
        {
            get
            {
                return Conclusions.Count((conclusion) => conclusion.Rules.Count((rule) => rule.IsValid == true) > 0) > 0;
            }
        }

        public Conclusion Conclusion
        {
            get
            {
                return Conclusions.Where((conclusion) => conclusion.Rules.Count((rule) => rule.IsValid == true) > 0).First();
            }
        }

        public IEnumerable<Conclusion> Conclusions { get; private set; }

        public IEnumerable<Fact> Facts { get; private set; }
    }
}
