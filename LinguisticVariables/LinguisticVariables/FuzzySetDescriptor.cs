using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FuzzySets;

namespace FuzzySets
{
    class FuzzySetDescriptor
    {
        public static double EuclidianDistance(ContinuousFuzzySet<double, double> a,
            ContinuousFuzzySet<double, double> b)
        {
            if (a.Keys.Count != b.Keys.Count)
                throw new ArgumentException("Sets have different size");
            IEnumerator<double> ita = a.Values.GetEnumerator();
            IEnumerator<double> itb = b.Values.GetEnumerator();
            double s = 0;
            while (ita.MoveNext() && itb.MoveNext())
            {
                double val1 = ita.Current;
                double val2 = itb.Current;
                s += Math.Abs((val2 - val1) * (val2 - val1));
            }
            return Math.Sqrt(s);
        }

        public static double HammingDistance(ContinuousFuzzySet<double, double> a,
            ContinuousFuzzySet<double, double> b)
        {
            if (a.Keys.Count != b.Keys.Count)
                throw new ArgumentException("Sets have different size");
            IEnumerator<double> ita = a.Values.GetEnumerator();
            IEnumerator<double> itb = b.Values.GetEnumerator();
            double s = 0;
            while (ita.MoveNext() && itb.MoveNext())
            {
                double val1 = ita.Current;
                double val2 = itb.Current;
                s += Math.Abs(val2 - val1);
            }
            return s;
        }

        public static double Entropy(ContinuousFuzzySet<double, double> set)
        {
            double s = 0;
            IEnumerator<double> it = set.Values.GetEnumerator();
            while (it.MoveNext())
            {
                double val = it.Current;
                if (!val.Equals(0) && !val.Equals(1))
                    s += -val * Math.Log(val) - (1 - val) * Math.Log(1 - val);
            }
            return s;
        }

        protected FuzzySetDescriptor() { }
    }
}
