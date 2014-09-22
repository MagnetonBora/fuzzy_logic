using System.Collections.Generic;

namespace FuzzySets
{
    class FuzzySetCreator<TKey, TValue>
    {

        public delegate double MemberShipFunction4(double x, double a, double b, double c);

        public delegate double MemberShipFunction3(double x, double a, double b);

        public static ContinuousFuzzySet<TKey, TValue> CreateInstance(ICollection<KeyValuePair<TKey, TValue>> collection)
        {
            return new ContinuousFuzzySet<TKey, TValue>(collection);
        }

        public static DiscreteFuzzyStringSet CreateInstance(List<KeyValuePair<string, double>> list)
        {
            return new DiscreteFuzzyStringSet(list);
        }

        public static DiscreteFuzzyStringSet CreateInstance(MemberShipFunction4 f, double a, double b, double c,
            ICollection<string> collection)
        {
            List<KeyValuePair<string, double>> list = new List<KeyValuePair<string, double>>();
            double id = 0;
            foreach (string s in collection)
            {
                KeyValuePair<string, double> pair = new KeyValuePair<string, double>(s, f(id, a, b, c));
                id += 1;
                list.Add(pair);
            }
            return new DiscreteFuzzyStringSet(list);
        }

        public static DiscreteFuzzyStringSet CreateInstance(MemberShipFunction3 f, double a, double b,
            ICollection<string> collection)
        {
            List<KeyValuePair<string, double>> list = new List<KeyValuePair<string, double>>();
            double id = 0;
            foreach (string s in collection)
            {
                KeyValuePair<string, double> pair = new KeyValuePair<string, double>(s, f(id, a, b));
                id += 1;
                list.Add(pair);
            }
            return new DiscreteFuzzyStringSet(list);
        }

        public static ContinuousFuzzySet<double, double> CreateInstance(MemberShipFunction3 f,
            double left, double right, double step, double a, double b)
        {
            List<KeyValuePair<double, double>> list = new List<KeyValuePair<double, double>>();

            for (double x = left; x <= right; x += step)
            {
                KeyValuePair<double, double> pair = new KeyValuePair<double, double>(x,
                    f(x, a, b));
                list.Add(pair);
            }

            return new ContinuousFuzzySet<double, double>(list);
        }

        public static ContinuousFuzzySet<double, double> CreateInstance(MemberShipFunction4 f,
            double left, double right, double step, double a, double b, double c)
        {
            List<KeyValuePair<double, double>> list = new List<KeyValuePair<double, double>>();

            for (double x = left; x <= right; x += step)
            {
                KeyValuePair<double, double> pair = new KeyValuePair<double, double>(x,
                    f(x, a, b, c));
                list.Add(pair);
            }

            return new ContinuousFuzzySet<double, double>(list);
        }

        protected FuzzySetCreator() { }

    }
}
