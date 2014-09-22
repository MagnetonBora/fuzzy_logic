using System;

namespace FuzzySets
{    
    class FuzzyHelper
    {
        private FuzzyHelper()
        { }

        public static double TriangleFunction(double x, double a, double b, double c)
        {
            if (x <= a)
                return 0;
            else if (x > a && x <= b)
                return (x - a) / (b - a);
            else if (x > b && x <= c)
                return (c - x) / (c - b);
            return 0;
        }

        public static double SFunction(double x, double a, double b)
        {
            if (x <= a) return 0;
            else if (x > a && x <= 0.5 * (a+b)) return 2 * (x - a) * (x - a) / ((b - a) * (b - a));
            else if (x > 0.5 * (a + b) && x <= b) return 1 - 2 * (x - b) * (x - b) / ((b - a) * (b - a));
            else return 1;
        }

        public static double ZFunction(double x, double a, double b)
        {
            return 1 - SFunction(x, a, b);
        }

        public static double SFunction(double x, double a, double b, double c)
        {
            if (x <= a)
                return 0;
            else if (x > a && x <= b)
                return 2 * (x - a) * (x - a) / ((c - a) * (c - a));
            else if (x > b && x <= c)
                return 1 - 2 * (x - c) * (x - c) / ((c - a) * (c - a));
            else
                return 1;
        }

        public static double PFunction(double x, double a, double b, double c)
        {
            if (x <= c)
                return SFunction(x, c - b, c - b/2, c);
            else
                return 1 - SFunction(x, c, c + b/2, c + b);
        }

        public static double PFunction(double x, double a, double c)
        {
            double b = 0.5 * (a + c);
            if (x <= c)
                return SFunction(x, c - b, c);
            else
                return 1 - SFunction(x, c, c + b);
        }

        public static double[] PolyFunction(double[] x)
        {
            double s = 0;
            double[] t = new double[x.Length];

            for (int i = 0; i < x.Length; i++)
            {
                t[i] = x[i];
                s += x[i];
            }

            for (int i = 0; i < t.Length; i++)
                t[i] /= s;

            Array.Sort(t);

            return t;
        }

    }
}
