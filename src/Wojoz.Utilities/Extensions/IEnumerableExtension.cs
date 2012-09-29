using System;
using System.Collections.Generic;
using System.Linq;

namespace Wojoz.Utilities
{
    public static class IEnumerableExtension
    {
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            if (items == null) throw new ArgumentNullException("items");
            if (action == null) throw new ArgumentNullException("action");

            foreach (var item in items)
                action(item);
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> iEnumerable) {
            return iEnumerable == null || !iEnumerable.Any(); 
        }
    }
}
