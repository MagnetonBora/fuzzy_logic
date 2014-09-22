using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using LinguisticVariables;

namespace FuzzyConclusion
{
    sealed class RulesInitializer
    {
        public static List<int[]> ReadRules(string path) 
        {
            StreamReader reader = new StreamReader(path);
            string line = string.Empty;
            List<int[]> rules = new List<int[]>();
            while (!reader.EndOfStream)
            {
                line = reader.ReadLine();
                string[] buf = line.Split(',');
                int[] tuple = new int[buf.Length];
                for (int i = 0; i < buf.Length; i++)
                {
                    tuple[i] = Convert.ToInt16(buf[i]);
                }
                rules.Add(tuple);
                tuple = null;
            }

            reader.Close();
            return rules;
        }

        public static Rule[] GetRules(string path)
        {            
            List<int[]> list = ReadRules(path);
            List<LinguisticVariable> vars = TermsCreator.GetTerms();
            Rule[] rules = new Rule[list.Count];
            int ConcPos = vars.Count - 1;

            for (int i = 0; i < list.Count; i++)
            {
                    Condition[] condition = new Condition[list[i].Length-1];
                for (int j = 0; j < list[i].Length - 1; j++)
                {
                    condition[j] = new Condition(vars[j], list[i][j]);
                }
                    Conclusion conclusion = new Conclusion(vars[ConcPos], list[i][ConcPos]);
                    rules[i] = new Rule(new Tuple<Condition[], Conclusion>(condition, conclusion));
            }

            return rules;
        }
    }
}
