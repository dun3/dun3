using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace PerformanceTest.IEnumerableCast
{
    public static class ListExtender
    {
        public static List<K> CastListExtension<T, K>(this List<T> list) where T : K
        {
            List<K> cast = new List<K>(list.Count);
            foreach (T item in list)
            {
                cast.Add(item);
            }
            return cast;
        }

        public static List<K> CastListMethod<T, K>(List<T> list) where T : K
        {
            List<K> cast = new List<K>(list.Count);
            foreach (T item in list)
            {
                cast.Add(item);
            }
            return cast;
        }
    }
}
