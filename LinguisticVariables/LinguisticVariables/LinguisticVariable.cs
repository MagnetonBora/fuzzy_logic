using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using FuzzySets;

namespace LinguisticVariables
{
    class Term
    {
        private string name;

        private ContinuousFuzzySet<double, double> set;

        public string Name
        {
            set { name = value; }
            get { return name; }
        }

        public ContinuousFuzzySet<double, double> FuzzySet
        {
            set { set = value;}
            get { return set; }
        }

        public Dictionary<double, double>.KeyCollection Universum
        {
            get { return set.Keys; }
        }

        public Term()
            : base() { }

        public Term(string name, ContinuousFuzzySet<double, double> set)
        {
            this.name = name;
            this.set = set;
        }

        public override string ToString()
        {
            return name;
        }
    }

    class Rule
    {
        private string Quantifier;

        public delegate double SemanticRuleDelegate(double x);        

        private SemanticRuleDelegate SemanticRule;

        public Rule() 
            : base() { }

        public Rule(string Quantifier, SemanticRuleDelegate rule)
        {
            this.Quantifier = Quantifier;
            this.SemanticRule = rule;
        }

        public Term Activate(Term t)
        {
            Dictionary<double, double>.Enumerator it = t.FuzzySet.GetEnumerator();
            List<KeyValuePair<double, double>> list
                = new List<KeyValuePair<double, double>>();
            while (it.MoveNext())
            {
                double x = it.Current.Key;
                double y = SemanticRule(t.FuzzySet[x]);
                list.Add(new KeyValuePair<double, double>(x, y));
            }
            ContinuousFuzzySet<double, double> FuzzySet = 
                new ContinuousFuzzySet<double, double>(list);
            return new Term(Quantifier + " " + t.Name, FuzzySet);
        }
    }

    class LinguisticVariable
    {
        // Name of variable
        private string name;

        // Terms
        private List<Term> terms;

        // Rules 
        private List<Rule> rules;

        public string Name
        {
            set { name = value; }
            get { return name; }
        }

        public List<Term> Terms
        {
            set { terms = value; }
            get { return terms; }
        }

        public List<Rule> Rules
        {
            set { rules = value; }
            get { return rules; }                 
        }

        public LinguisticVariable() 
            : base() { }

        public LinguisticVariable(string name, List<Term> terms, List<Rule> rules)
        {
            this.name = name;
            this.rules = rules;
            this.terms = terms;
        }        

    }

    class LinguisticVariableHelper
    {
        public static List<Term> GenerateTerms(LinguisticVariable var)
        {
            List<Term> terms = new List<Term>();
            for (int i = 0; i < var.Terms.Count; i++)
                terms.Add(var.Terms[i]);
            foreach (Rule rule in var.Rules)
                foreach (Term term in var.Terms)
                    terms.Add(rule.Activate(term));
            return terms;
        }
    }
}
