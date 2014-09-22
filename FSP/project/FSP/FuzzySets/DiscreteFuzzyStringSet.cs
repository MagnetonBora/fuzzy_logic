using System.Collections.Generic;

namespace FuzzySets
{
    class DiscreteFuzzyStringSet : ContinuousFuzzySet<string, double>
    {
        public DiscreteFuzzyStringSet() : base() { }

        public DiscreteFuzzyStringSet(ICollection<KeyValuePair<string, double>> collection)
            : base(collection) { }

        public override string ToString()
        {
            return base.ToString();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
