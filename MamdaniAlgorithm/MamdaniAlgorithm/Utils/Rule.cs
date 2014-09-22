using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FuzzyConclusion
{
    class Rule
    {
        private Tuple<Condition[], Conclusion> rule;

        public Condition[] Conditions
        {
            get { return rule.Item1; }
        }

        public Conclusion Conclusion
        {
            get { return rule.Item2; }
        }

        public Rule()
            : base() { }

        public Rule(Tuple<Condition[], Conclusion> rule)
        {
            this.rule = rule;
        }

        public Rule(Tuple<Condition, Conclusion> rule)
        {
            this.rule = new Tuple<Condition[], Conclusion>(new Condition[1], rule.Item2);
            this.rule.Item1[0] = rule.Item1;
        }

        public override string ToString()
        {
            string str = "IF ";
            for(int i = 0; i < rule.Item1.Length; i++)
            {
                str += rule.Item1[i].ToString() + " ";
                if (i != rule.Item1.Length - 1)
                    str += rule.Item1[i].Operation.ToString() + " ";
            }
            return str + "THEN " + rule.Item2.ToString();
        }
    }
}
