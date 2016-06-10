using JanusCore.Support;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JanusCore.Extensions
{
    public static class CollectionExtensions
    {

        public static string FormatForQueryString(this IEnumerable collection)
        {
            var items = (from object item in collection where item != null select item.ToString()).ToList();

            return String.Join(Constants.ENUMERABLE_DELIMITER, items);
        }

        public static string FormatForQueryString(this IDictionary dictionary)
        {
            var keyValuePairs = new List<string>();

            foreach (DictionaryEntry kvp in dictionary)
                keyValuePairs.Add(kvp.Key + ":" + kvp.Value);

            return String.Join(Constants.ENUMERABLE_DELIMITER, keyValuePairs);
        }

        public static TValue TryGetValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue = default(TValue))
        {
            TValue value;

            return dictionary.TryGetValue(key, out value) ? value : defaultValue;
        }

        public static bool TryRemove<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            if (!dictionary.ContainsKey(key))
                return false;

            dictionary.Remove(key);

            return true;
        }

        public static void AddRange<T>(this ICollection<T> to, IEnumerable<T> from)
        {
            if (from != null && to != null)
                from.ForEach(to.Add);
        }

        public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> to, IDictionary<TKey, TValue> from)
        {
            if (from != null && to != null)
                from.Where(kvp => !to.ContainsKey(kvp.Key)).ForEach(to.Add);
        }

        public static void RemoveRange<T>(this ICollection<T> from, Func<T, bool> predicate)
        {
            from.RemoveRange(from.Where(predicate));
        }

        public static void RemoveRange<T>(this ICollection<T> from, IEnumerable<T> toRemove)
        {
            if (from != null && toRemove != null)
            {
                // Need to get a shallow copy here and for each over that otherwise the enum that you're removing
                // will be modified, and therefore throw an exception
                List<T> shallowCopy = toRemove.ToList();

                shallowCopy.ForEach(i => from.Remove(i));
            }
        }
    }
}
