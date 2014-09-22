using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinguisticVariables;
using FuzzySets;

namespace FuzzyConclusion
{
    sealed class MamdaniAlgorithm
    {
        private Rule[] rules = null;

        public Rule[] Rules
        {
            set { rules = value; }
            get { return rules; }
        }

        public MamdaniAlgorithm() 
            : base() { }

        public MamdaniAlgorithm(Rule[] rules)
        {
            this.rules = rules;
        }

        public double Process(double[] InputData)
        {
            if (rules == null)
                throw new Exception("Rules are not defined!");
            double[] y = Fuzzification(InputData);
            ContinuousFuzzySet<double, double>[] sets = ActivateConclusion(y);
            ContinuousFuzzySet<double, double> ResultSet = Accumulation(sets);
            double result = Defuzzification(ResultSet);
            return result;
        }

        private double[] Fuzzification(double[] InputData)
        {
            double[] y = new double[rules.Length];
            int i = 0;
            foreach (Rule rule in rules)
            {
                int j = 0;
                double[] b = new double[rule.Conditions.Length];
                foreach (Condition condition in rule.Conditions)
                {
                    int id = condition.TermId;
                    b[j] = condition.Variable.Terms[id].FuzzySet[InputData[j]];
                    j++;
                }
                y[i++] = AggregateConditions(b);
            }
            return y;
        }

        private double AggregateConditions(double[] values)
        {
            double min = values[0];
            for (int i = 0; i < values.Length; i++)
                if (values[0] <= min)
                    min = values[i];
            return min;
        }

        private ContinuousFuzzySet<double, double>[]
            ActivateConclusion(double[] y)
        {
            int i = 0;
            ContinuousFuzzySet<double, double>[] sets = new ContinuousFuzzySet<double, double>[y.Length];
            foreach (Rule rule in rules)
            {
                int j = rule.Conclusion.TermId;
                ContinuousFuzzySet<double, double> S0 = rule.Conclusion.Variable.Terms[j].FuzzySet;
                Dictionary<double, double>.Enumerator it = S0.GetEnumerator();
                List<KeyValuePair<double, double>> list = new List<KeyValuePair<double, double>>();
                while (it.MoveNext())
                {
                    double key = it.Current.Key;
                    double value = Math.Min(y[i], it.Current.Value);
                    list.Add(new KeyValuePair<double, double>(key, value));
                }
                sets[i++] = new ContinuousFuzzySet<double, double>(list);
            }
            return sets;
        }

        private ContinuousFuzzySet<double, double> 
            Accumulation(ContinuousFuzzySet<double, double>[] sets)
        {
            ContinuousFuzzySet<double, double> set = sets[0];
            for (int i = 1; i < sets.Length; i++)
                set = FuzzyOperations.SimpleUnion(set, sets[i]);
            return set;
        }

        private double Defuzzification(ContinuousFuzzySet<double, double> set)
        {
            double s1 = 0;
            double s2 = 0;
            Dictionary<double, double>.Enumerator it = set.GetEnumerator();
            while (it.MoveNext())
            {
                s1 += it.Current.Key * it.Current.Value;
                s2 += it.Current.Value;
            }
            if (s2 == 0)
                return 0.5 * (set.Keys.Max() + set.Keys.Min());
            else
                return s1 / s2;
        }
    }
}
