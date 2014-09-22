using System;
using System.Collections.Generic;

namespace Utils
{
    class ElementsCache
    {
        private static readonly ElementsCache instance = new ElementsCache();

        private static List<Object> list = new List<Object>();

        public static int Count
        {
            get { return list.Count; }
        }      

        protected ElementsCache() { }

        public static ElementsCache Instance
        {
            get { return instance;  }
        }

        public static void Add(Object item)
        {
            list.Add(item);
        }

        public static Object GetAt(int i)
        {
            if (list.Count != 0)
                return list[i];
            else
                return null;
        }

        public static void RemoveAt(int i)
        {
            list.RemoveAt(i);
        }

        public static void Clear()
        {
            list.Clear();
        }
    }
}
