using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LinguisticVariables;

namespace FuzzyConclusion
{
    enum FuzzyLogicalOperationTypes
    {
        OR,
        AND,
        XOR
    }

    class Condition : Statement
    {        
        private FuzzyLogicalOperationTypes operation 
            = FuzzyLogicalOperationTypes.AND;

        public FuzzyLogicalOperationTypes Operation
        {
            set { operation = value; }
            get { return operation; }
        }

        public Condition() 
            : base() { }

        public Condition(LinguisticVariable var, int id) : base(var, id) { }

        public Condition(LinguisticVariable var, int id, FuzzyLogicalOperationTypes operation)
            : base(var, id)
        {
            this.operation = operation;
        }

        public override string ToString()
        {
            int id = this.TermId;
            return "<" + this.Variable.Terms[id].ToString() + ">";
        }
    }
}
