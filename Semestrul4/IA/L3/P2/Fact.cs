using System.Collections.Generic;

namespace IALab302
{
    public class Fact
    {
        public Fact(string text)
        {
            Text = text;
            IsValid = null;
        }

        public void Add(Rule rule)
        {
            if (rules.Add(rule))
                rule.Add(this);
        }

        public IEnumerable<Rule> Rules
        {
            get
            {
                return rules;
            }
        }

        public string Text { get; private set; }

        public bool? IsValid { get; set; }

        private ISet<Rule> rules = new HashSet<Rule>();
    }
}
