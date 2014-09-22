using System;
using System.Collections.Generic;

namespace FuzzySets
{
    class FuzzyOperations
    {

        public static ContinuousFuzzySet<double, double> Multiply(
            ContinuousFuzzySet<double, double> A, ContinuousFuzzySet<double, double> B)
        {
            List<KeyValuePair<double, double>> list =
                new List<KeyValuePair<double, double>>();

            if (A.Values.Count != B.Values.Count)
                throw new ArgumentException("Sets have different size");

            IEnumerator<KeyValuePair<double, double>> ita = A.GetEnumerator();
            IEnumerator<KeyValuePair<double, double>> itb = B.GetEnumerator();

            while (ita.MoveNext() && itb.MoveNext())
            {
                double key = ita.Current.Key;
                double value = ita.Current.Value * itb.Current.Value;
                list.Add(new KeyValuePair<double, double>(key, value));
            }

            return new ContinuousFuzzySet<double, double>(list);
        }

        public static ContinuousFuzzySet<double, double> Sum(
            ContinuousFuzzySet<double, double> A, ContinuousFuzzySet<double, double> B)
        {
            List<KeyValuePair<double, double>> list =
                new List<KeyValuePair<double, double>>();

            if (A.Values.Count != B.Values.Count)
                throw new ArgumentException("Sets have different size");

            IEnumerator<KeyValuePair<double, double>> ita = A.GetEnumerator();
            IEnumerator<KeyValuePair<double, double>> itb = B.GetEnumerator();

            while (ita.MoveNext() && itb.MoveNext())
            {
                double key = ita.Current.Key;
                double value = ita.Current.Value + itb.Current.Value 
                    - ita.Current.Value * itb.Current.Value;
                list.Add(new KeyValuePair<double, double>(key, value));
            }

            return new ContinuousFuzzySet<double, double>(list);
        }

        public static ContinuousFuzzySet<double, double> SimpleIntersection(
            ContinuousFuzzySet<double, double> A, ContinuousFuzzySet<double, double> B) 
        {
            List<KeyValuePair<double, double>> list = 
                new List<KeyValuePair<double, double>>();

            if (A.Values.Count != B.Values.Count)
                throw new ArgumentException("Sets have different size");

            IEnumerator<KeyValuePair<double, double>> ita = A.GetEnumerator();
            IEnumerator<KeyValuePair<double, double>> itb = B.GetEnumerator();
            
            while (ita.MoveNext() && itb.MoveNext())
            {
                double key = ita.Current.Key;
                double value = Math.Min(ita.Current.Value, itb.Current.Value);
                list.Add(new KeyValuePair<double, double>(key, value));
            }

            return new ContinuousFuzzySet<double, double>(list);
        }

        public static ContinuousFuzzySet<double, double> SimpleUnion(
            ContinuousFuzzySet<double, double> A, ContinuousFuzzySet<double, double> B)
        {
            List<KeyValuePair<double, double>> list =
                new List<KeyValuePair<double, double>>();

            if (A.Values.Count != B.Values.Count)
                throw new ArgumentException("Sets have different size");

            IEnumerator<KeyValuePair<double, double>> ita = A.GetEnumerator();
            IEnumerator<KeyValuePair<double, double>> itb = B.GetEnumerator();

            while (ita.MoveNext() && itb.MoveNext())
            {
                double key = ita.Current.Key;
                double value = Math.Max(ita.Current.Value, itb.Current.Value);
                list.Add(new KeyValuePair<double, double>(key, value));
            }

            return new ContinuousFuzzySet<double, double>(list);
        }

        public static ContinuousFuzzySet<double, double> Inversion(
            ContinuousFuzzySet<double, double> A)
        {
            List<KeyValuePair<double, double>> list = 
                new List<KeyValuePair<double, double>>();
            IEnumerator<KeyValuePair<double, double>> it = A.GetEnumerator();
            
            while (it.MoveNext())
            {
                double key = it.Current.Key;
                double value = 1 - it.Current.Value;
                list.Add(new KeyValuePair<double, double>(key, value));
            }

            return new ContinuousFuzzySet<double, double>(list);
        }

        protected FuzzyOperations() { }
    }
}
