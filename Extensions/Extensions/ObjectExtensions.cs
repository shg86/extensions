using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Checks if value if between 2 others.
        /// </summary>
        public static bool Between<T>(this T value, T from, T to) where T : IComparable<T>
        {
            return value.CompareTo(from) >= 0 && value.CompareTo(to) <= 0;
        }

        /// <summary>
        /// Determines whether a collection is null or has no elements without having to enumerate the entire collection to get a count.  Uses LINQ.
        /// </summary>
        /// <typeparam name="T">The item type.</typeparam>
        /// <param name="items">The items.</param>
        /// <returns>
        /// <character>true</character> if this list is null or empty; otherwise, <character>false</character>.
        /// </returns>
        public static bool IsNullOrEmpty<T>(this IList<T> items)
        {
            return items == null || !items.Any();
        }

        /// <summary>
        /// Returns the range of elements between the specified start and end indexes.
        /// </summary>
        public static IEnumerable<T> Slice<T>(this IEnumerable<T> collection, int start, int end)
        {
            int index = 0;
            int count = 0;

            if (collection == null)
                throw new ArgumentNullException("collection");

            // Optimise item count for ICollection interfaces.
            if (collection is ICollection<T>)
                count = ((ICollection<T>)collection).Count;
            else if (collection is ICollection)
                count = ((ICollection)collection).Count;
            else
                count = collection.Count();

            // Get start/end indexes, negative numbers start at the end of the collection.
            if (start < 0)
                start += count;

            if (end < 0)
                end += count;

            foreach (var item in collection)
            {
                if (index >= end)
                    yield break;

                if (index >= start)
                    yield return item;

                ++index;
            }
        }

        /// <summary>
        ///   Limit a value to a certain range. When the value is smaller/bigger than the range, snap it to the range border.
        /// </summary>
        /// <typeparam name = "T">The type of the value to limit.</typeparam>
        /// <param name = "source">The source for this extension method.</param>
        /// <param name = "start">The start of the interval, included in the interval.</param>
        /// <param name = "end">The end of the interval, included in the interval.</param>
        public static T Clamp<T>(this T source, T start, T end)
            where T : IComparable
        {
            bool isReversed = start.CompareTo(end) > 0;
            T smallest = isReversed ? end : start;
            T biggest = isReversed ? start : end;

            return source.CompareTo(smallest) < 0
                ? smallest
                : source.CompareTo(biggest) > 0
                    ? biggest
                    : source;
        }
 
        /// <summary>
        /// Repeats an action for a number of times.
        /// </summary>
        public static void Times(this int repeatCount, Action<int> action)
        {
            for (int i = 1; i <= repeatCount; i++)
            {
                action(i);
            }
        }

        /// <summary>
        /// 	Adds a value uniquely to to a collection and returns a value whether the value was added or not.
        /// </summary>
        /// <typeparam name = "T">The generic collection value type</typeparam>
        /// <param name = "collection">The collection.</param>
        /// <param name = "value">The value to be added.</param>
        /// <returns>Indicates whether the value was added or not</returns>
        /// <example>
        /// 	<code>
        /// 		list.AddUnique(1); // returns true;
        /// 		list.AddUnique(1); // returns false the second time;
        /// 	</code>
        /// </example>
        public static bool AddUnique<T>(this ICollection<T> collection, T value)
        {
            var alreadyHas = collection.Contains(value);
            if (!alreadyHas)
            {
                collection.Add(value);
            }
            return alreadyHas;
        }

    }
}
