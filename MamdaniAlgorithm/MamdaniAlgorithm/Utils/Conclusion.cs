using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinguisticVariables;

namespace FuzzyConclusion
{
    class Conclusion : Statement
    {
        private double weight = 1;

        public Conclusion()
            : base() { }

        public Conclusion(LinguisticVariable var, int id) : base(var, id) 
        { }

        public Conclusion(LinguisticVariable var, int id, double weight)
            : base(var, id)
        {
            this.weight = weight;
        }

        public double Weight
        {
            set { weight = value; }
            get { return weight; }
        }

        public override string ToString()
        {
            int id = this.TermId;
            return this.Variable.Terms[id].ToString();
        }
    }
}
