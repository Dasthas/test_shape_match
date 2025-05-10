using System.Collections.Generic;
using System.Linq;

namespace Core.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> ShuffleToEnumerable<T>(this IEnumerable<T> source)
        {
            var elements = source.ToArray();
            for (var i = elements.Length - 1; i > 0; i--)
            {
                var swapIndex = UnityEngine.Random.Range(0, i + 1);
                (elements[i], elements[swapIndex]) = (elements[swapIndex], elements[i]);
            }

            foreach (var element in elements)
            {
                yield return element;
            }
        }

        public static T[] ShuffleToArray<T>(this IEnumerable<T> source)
        {
            var elements = source.ToArray();
            for (var i = elements.Length - 1; i > 0; i--)
            {
                var swapIndex = UnityEngine.Random.Range(0, i + 1);
                (elements[i], elements[swapIndex]) = (elements[swapIndex], elements[i]);
            }

            return elements;
        }
    }
}