using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinguisticVariables;

namespace FuzzyConclusion
{
    class Statement
    {
        private int id;
        
        private LinguisticVariable var;

        public int TermId
        {
            get { return id; }
        }

        public LinguisticVariable Variable
        {
            set { var = value; }
            get { return var; }
        }

        public Statement() 
            : base() { }

        public Statement(LinguisticVariable var)
        {
            this.var = var;
        }

        public Statement(LinguisticVariable var, int id)
        {
            this.var = var;
            this.id = id;
        }
    }
}
