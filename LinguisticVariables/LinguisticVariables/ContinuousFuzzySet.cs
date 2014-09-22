using System.Collections;
using System.Collections.Generic;

namespace FuzzySets
{
    class ContinuousFuzzySet<TKey, TValue> : Dictionary<TKey, TValue>
    {

        public ContinuousFuzzySet() : base() { }

        public ContinuousFuzzySet(ICollection<KeyValuePair<TKey, TValue>> collection)
        {
            foreach (KeyValuePair<TKey, TValue> pair in collection)
                this.Add(pair.Key, pair.Value);
        }

        public int Cardinality()
        {
            return this.Count;
        }

        public override string ToString()
        {
            IEnumerator<KeyValuePair<TKey, TValue>> it = this.GetEnumerator();

            string str = string.Empty;

            while (it.MoveNext())
            {
                KeyValuePair<TKey, TValue> pair = it.Current;
                str += "[" + pair.Key + " " + pair.Value + "] ";
            }

            return str;
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
