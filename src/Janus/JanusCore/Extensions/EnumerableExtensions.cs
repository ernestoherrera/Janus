using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JanusCore.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool IsSubsetOf<T>(this IEnumerable<T> subset, IEnumerable<T> superset)
        {
            return subset != null && !subset.Except(superset).Any();
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source.IsNullOrEmpty() || action == null) return;

            foreach (T element in source)
                action(element);
        }

        public static void ForEach<T>(this T[] source, Action<T> action)
        {
            if (source == null || action == null) return;

            foreach (T element in source)
                action(element);
        }

        /// <summary>
        /// Gets the unique set of objects based on the specified key selector.
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="objects"></param>
        /// <param name="getKey">Key selector</param>
        /// <param name="combine">Method to combine objects with the same key</param>
        /// <returns></returns>
        public static IEnumerable<TObject> Distinct<TObject, TKey>(this IEnumerable<TObject> objects, Func<TObject, TKey> getKey, Func<TObject, TObject, TObject> combine = null)
        {
            var distinctObjects = new Dictionary<TKey, TObject>();

            objects.ForEach(o =>
            {
                var key = getKey(o);

                if (distinctObjects.ContainsKey(key))
                {
                    if (combine != null)
                        distinctObjects[key] = combine(distinctObjects[key], o);
                }
                else
                {
                    distinctObjects.Add(key, o);
                }
            });

            return distinctObjects.Values;
        }        

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> value)
        {
            if ((value != null) && (value.Count() > 0))
                return false;

            return true;
        }

        public static void GetNewDeletedItems<T, R>(this IEnumerable<T> originalList, IEnumerable<T> modifiedList, Func<T, R> keySelector, out IEnumerable<T> newList, out IEnumerable<T> deletedList)
        {
            if (originalList == null)
            {
                newList = new List<T>();
                deletedList = new List<T>();
            }
            else
            {
                var newItems = modifiedList.Where(i => !originalList.Select(keySelector).Any(q => q.Equals(keySelector.Invoke(i))));
                var deletedItems = originalList.Where(i => !modifiedList.Select(keySelector).Contains(keySelector.Invoke(i)) && !newItems.Select(keySelector).Contains(keySelector.Invoke(i)));

                newList = newItems;
                deletedList = deletedItems;
            }
        }

        public static int IndexOf<T>(this IEnumerable<T> items, Predicate<T> predicate)
        {
            int index = 0;

            foreach (var item in items)
            {
                if (predicate(item))
                    return index;

                index++;
            }

            return -1;
        }
    }
}
