using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    public static class Extensions
    {
        public static IEnumerable<T> InsertAt<T>(this IEnumerable<T> items, int position, T newItem)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }
            if (position < 0)
            {
                throw new ArgumentOutOfRangeException("position");
            }
            return InsertAtIterator(items, position, newItem);
        }

        private static IEnumerable<T> InsertAtIterator<T>(IEnumerable<T> items, int position, T newItem)
        {
            int index = 0;

            foreach (T item in items)
            {
                if (index == position)
                {
                    yield return newItem;
                }
                yield return item;
                index++;
            }

            if (index == position)
            {
                yield return newItem;
            }

            if (index < position)
            {
                throw new ArgumentOutOfRangeException("position");
            }
        }
    }
}
