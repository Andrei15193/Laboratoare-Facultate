using System.Collections.Generic;

namespace IALab302
{
    public class Conclusion
    {
        public Conclusion(string text)
        {
            Text = text;
        }

        public void Add(Rule rule)
        {
            if (rules.Add(rule))
                rule.Add(this);
        }

        public override string ToString()
        {
            return Text;
        }

        public IEnumerable<Rule> Rules
        {
            get
            {
                return rules;
            }
        }

        public string Text { get; private set; }

        private ISet<Rule> rules = new HashSet<Rule>();
    }
}
