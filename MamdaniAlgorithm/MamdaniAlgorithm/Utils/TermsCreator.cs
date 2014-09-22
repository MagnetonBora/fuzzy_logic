using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinguisticVariables;
using FuzzySets;

namespace FuzzyConclusion
{
    sealed class TermsCreator
    {
        public static List<LinguisticVariable> GetTerms()
        {
            List<LinguisticVariable> list 
                = new List<LinguisticVariable>();
            list.Add(CreateForce());
            list.Add(CreateDexterity());
            list.Add(CreateWisdom());
            list.Add(CreateIntuition());
            list.Add(CreateLuck());
            list.Add(CreateCrit());
            return list;
        }

        private static LinguisticVariable CreateForce()
        {
            double a = 0;
            double b = 10;
            double h = 0.1;
            double x = 0;

            List<Term> terms = new List<Term>();

            List<KeyValuePair<double, double>> list
                = new List<KeyValuePair<double, double>>();

            list.Clear();
            for (x = a; x <= b; x += h)
            {
                double key = x;
                double value = FuzzyHelper.TriangleFunction(Math.Round(x, 10), -2, 0, 2);
                list.Add(new KeyValuePair<double, double>(Math.Round(key, 10), value));
            }

            terms.Add(new Term("Dweeb", new ContinuousFuzzySet<double, double>(list)));

            list.Clear();
            for (x = a; x <= b; x += h)
            {
                double key = x;
                double value = FuzzyHelper.TriangleFunction(Math.Round(x, 10), 0, 2, 4);
                list.Add(new KeyValuePair<double, double>(Math.Round(key, 10), value));
            }

            terms.Add(new Term("Week", new ContinuousFuzzySet<double, double>(list)));

            list.Clear();
            for (x = a; x <= b; x += h)
            {
                double key = x;
                double value = FuzzyHelper.TriangleFunction(Math.Round(x, 10), 2, 4, 6);
                list.Add(new KeyValuePair<double, double>(Math.Round(key, 10), value));
            }

            terms.Add(new Term("Normal", new ContinuousFuzzySet<double, double>(list)));

            list.Clear();
            for (x = a; x <= b; x += h)
            {
                double key = x;
                double value = FuzzyHelper.TriangleFunction(Math.Round(x, 10), 4, 6, 8);
                list.Add(new KeyValuePair<double, double>(Math.Round(key, 10), value));
            }

            terms.Add(new Term("Athlete", new ContinuousFuzzySet<double, double>(list)));

            list.Clear();
            for (x = a; x <= b; x += h)
            {
                double key = x;
                double value = FuzzyHelper.TriangleFunction(Math.Round(x, 10), 6, 8, 10);
                list.Add(new KeyValuePair<double, double>(Math.Round(key, 10), value));
            }

            terms.Add(new Term("Hercules", new ContinuousFuzzySet<double, double>(list)));

            list.Clear();
            for (x = a; x <= b; x += h)
            {
                double key = x;
                double value = FuzzyHelper.TriangleFunction(Math.Round(x, 10), 8, 10, 12);
                list.Add(new KeyValuePair<double, double>(Math.Round(key, 10), value));
            }

            terms.Add(new Term("MrUniverse", new ContinuousFuzzySet<double, double>(list)));

            return new LinguisticVariable("Force", terms, null);
        }

        private static LinguisticVariable CreateDexterity()
        {
            double a = 0;
            double b = 10;
            double h = 0.1;
            double x = 0;

            List<Term> terms = new List<Term>();

            List<KeyValuePair<double, double>> list
                = new List<KeyValuePair<double, double>>();

            list.Clear();
            for (x = a; x <= b; x += h)
            {
                double key = x;
                double value = FuzzyHelper.TriangleFunction(Math.Round(x, 10), -2.5, 0, 2.5);
                list.Add(new KeyValuePair<double, double>(Math.Round(key, 10), value));
            }

            terms.Add(new Term("Clumsy", new ContinuousFuzzySet<double, double>(list)));

            list.Clear();
            for (x = a; x <= b; x += h)
            {
                double key = x;
                double value = FuzzyHelper.TriangleFunction(Math.Round(x, 10), 0, 2.5, 5);
                list.Add(new KeyValuePair<double, double>(Math.Round(key, 10), value));
            }

            terms.Add(new Term("Normal", new ContinuousFuzzySet<double, double>(list)));

            list.Clear();
            for (x = a; x <= b; x += h)
            {
                double key = x;
                double value = FuzzyHelper.TriangleFunction(Math.Round(x, 10), 2.5, 5, 7.5);
                list.Add(new KeyValuePair<double, double>(Math.Round(key, 10), value));
            }

            terms.Add(new Term("Dexterous", new ContinuousFuzzySet<double, double>(list)));

            list.Clear();
            for (x = a; x <= b; x += h)
            {
                double key = x;
                double value = FuzzyHelper.TriangleFunction(Math.Round(x, 10), 5, 7.5, 10);
                list.Add(new KeyValuePair<double, double>(Math.Round(key, 10), value));
            }

            terms.Add(new Term("Gymnast", new ContinuousFuzzySet<double, double>(list)));

            list.Clear();
            for (x = a; x <= b; x += h)
            {
                double key = x;
                double value = FuzzyHelper.TriangleFunction(Math.Round(x, 10), 7.5, 10, 12.5);
                list.Add(new KeyValuePair<double, double>(Math.Round(key, 10), value));
            }

            terms.Add(new Term("Ninja", new ContinuousFuzzySet<double, double>(list)));

            return new LinguisticVariable("Dexterity", terms, null);
        }

        private static LinguisticVariable CreateWisdom()
        {
            double a = 0;
            double b = 10;
            double h = 0.1;
            double x = 0;

            List<Term> terms = new List<Term>();

            List<KeyValuePair<double, double>> list
                = new List<KeyValuePair<double, double>>();

            list.Clear();
            for (x = a; x <= b; x += h)
            {
                double key = x;
                double value = FuzzyHelper.TriangleFunction(Math.Round(x, 10), -2.5, 0, 2.5);
                list.Add(new KeyValuePair<double, double>(Math.Round(key, 10), value));
            }

            terms.Add(new Term("Low", new ContinuousFuzzySet<double, double>(list)));

            list.Clear();
            for (x = a; x <= b; x += h)
            {
                double key = x;
                double value = FuzzyHelper.TriangleFunction(Math.Round(x, 10), 0, 2.5, 5);
                list.Add(new KeyValuePair<double, double>(Math.Round(key, 10), value));
            }

            terms.Add(new Term("Medium", new ContinuousFuzzySet<double, double>(list)));

            list.Clear();
            for (x = a; x <= b; x += h)
            {
                double key = x;
                double value = FuzzyHelper.TriangleFunction(Math.Round(x, 10), 2.5, 5, 7.5);
                list.Add(new KeyValuePair<double, double>(Math.Round(key, 10), value));
            }

            terms.Add(new Term("High", new ContinuousFuzzySet<double, double>(list)));

            list.Clear();
            for (x = a; x <= b; x += h)
            {
                double key = x;
                double value = FuzzyHelper.TriangleFunction(Math.Round(x, 10), 5, 7.5, 10);
                list.Add(new KeyValuePair<double, double>(Math.Round(key, 10), value));
            }

            terms.Add(new Term("Master", new ContinuousFuzzySet<double, double>(list)));

            list.Clear();
            for (x = a; x <= b; x += h)
            {
                double key = x;
                double value = FuzzyHelper.TriangleFunction(Math.Round(x, 10), 7.5, 10, 12.5);
                list.Add(new KeyValuePair<double, double>(Math.Round(key, 10), value));
            }

            terms.Add(new Term("Professor", new ContinuousFuzzySet<double, double>(list)));

            return new LinguisticVariable("Wisdom", terms, null);
        }

        private static LinguisticVariable CreateIntuition()
        {
            double a = 0;
            double b = 10;
            double h = 0.1;
            double x = 0;

            List<Term> terms = new List<Term>();

            List<KeyValuePair<double, double>> list
                = new List<KeyValuePair<double, double>>();

            list.Clear();
            for (x = a; x <= b; x += h)
            {
                double key = x;
                double value = FuzzyHelper.TriangleFunction(Math.Round(x, 10), -3.3, 0, 3.3);
                list.Add(new KeyValuePair<double, double>(Math.Round(key, 10), value));
            }

            terms.Add(new Term("Low", new ContinuousFuzzySet<double, double>(list)));

            list.Clear();
            for (x = a; x <= b; x += h)
            {
                double key = x;
                double value = FuzzyHelper.TriangleFunction(Math.Round(x, 10), 0, 3.3, 6.7);
                list.Add(new KeyValuePair<double, double>(Math.Round(key, 10), value));
            }

            terms.Add(new Term("Medium", new ContinuousFuzzySet<double, double>(list)));

            list.Clear();
            for (x = a; x <= b; x += h)
            {
                double key = x;
                double value = FuzzyHelper.TriangleFunction(Math.Round(x, 10), 3.3, 6.7, 10);
                list.Add(new KeyValuePair<double, double>(Math.Round(key, 10), value));
            }

            terms.Add(new Term("High", new ContinuousFuzzySet<double, double>(list)));

            list.Clear();
            for (x = a; x <= b; x += h)
            {
                double key = x;
                double value = FuzzyHelper.TriangleFunction(Math.Round(x, 10), 6.7, 10, 13.3);
                list.Add(new KeyValuePair<double, double>(Math.Round(key, 10), value));
            }

            terms.Add(new Term("Extrasens", new ContinuousFuzzySet<double, double>(list)));

            return new LinguisticVariable("Intuition", terms, null);
        }

        private static LinguisticVariable CreateLuck()
        {
            double a = 0;
            double b = 10;
            double h = 0.1;
            double x = 0;

            List<Term> terms = new List<Term>();

            List<KeyValuePair<double, double>> list
                = new List<KeyValuePair<double, double>>();

            list.Clear();
            for (x = a; x <= b; x += h)
            {
                double key = x;
                double value = FuzzyHelper.TriangleFunction(Math.Round(x, 10), -3.3, 0, 3.3);
                list.Add(new KeyValuePair<double, double>(Math.Round(key, 10), value));
            }

            terms.Add(new Term("Loser", new ContinuousFuzzySet<double, double>(list)));

            list.Clear();
            for (x = a; x <= b; x += h)
            {
                double key = x;
                double value = FuzzyHelper.TriangleFunction(Math.Round(x, 10), 0, 3.3, 6.7);
                list.Add(new KeyValuePair<double, double>(Math.Round(key, 10), value));
            }

            terms.Add(new Term("Normal", new ContinuousFuzzySet<double, double>(list)));

            list.Clear();
            for (x = a; x <= b; x += h)
            {
                double key = x;
                double value = FuzzyHelper.TriangleFunction(Math.Round(x, 10), 3.3, 6.7, 10);
                list.Add(new KeyValuePair<double, double>(Math.Round(key, 10), value));
            }

            terms.Add(new Term("Lucky", new ContinuousFuzzySet<double, double>(list)));

            list.Clear();
            for (x = a; x <= b; x += h)
            {
                double key = x;
                double value = FuzzyHelper.TriangleFunction(Math.Round(x, 10), 6.7, 10, 13.3);
                list.Add(new KeyValuePair<double, double>(Math.Round(key, 10), value));
            }

            terms.Add(new Term("FavoriteOfDesteny", new ContinuousFuzzySet<double, double>(list)));

            return new LinguisticVariable("Luck", terms, null);
        }

        private static LinguisticVariable CreateCrit()
        {
            double a = 0;
            double b = 1;
            double h = 0.1;
            double x = 0;

            List<Term> terms = new List<Term>();

            List<KeyValuePair<double, double>> list
                = new List<KeyValuePair<double, double>>();

            list.Clear();
            for (x = a; x <= b; x += h)
            {
                double key = x;
                double value = FuzzyHelper.TriangleFunction(Math.Round(x, 10), -0.3, 0, 0.3);
                list.Add(new KeyValuePair<double, double>(Math.Round(key, 10), value));
            }

            terms.Add(new Term("Low", new ContinuousFuzzySet<double, double>(list)));

            list.Clear();
            for (x = a; x <= b; x += h)
            {
                double key = x;
                double value = FuzzyHelper.TriangleFunction(Math.Round(x, 10), 0, 0.3, 0.7);
                list.Add(new KeyValuePair<double, double>(Math.Round(key, 10), value));
            }

            terms.Add(new Term("Medium", new ContinuousFuzzySet<double, double>(list)));

            list.Clear();
            for (x = a; x <= b; x += h)
            {
                double key = x;
                double value = FuzzyHelper.TriangleFunction(Math.Round(x, 10), 0.3, 0.7, 1);
                list.Add(new KeyValuePair<double, double>(Math.Round(key, 10), value));
            }

            terms.Add(new Term("High", new ContinuousFuzzySet<double, double>(list)));

            list.Clear();
            for (x = a; x <= b; x += h)
            {
                double key = x;
                double value = FuzzyHelper.TriangleFunction(Math.Round(x, 10), 0.7, 1, 1.3);
                list.Add(new KeyValuePair<double, double>(Math.Round(key, 10), value));
            }

            terms.Add(new Term("Very High", new ContinuousFuzzySet<double, double>(list)));

            return new LinguisticVariable("Crit", terms, null);
        }
    }
}
