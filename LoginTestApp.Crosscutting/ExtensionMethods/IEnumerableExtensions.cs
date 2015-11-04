using System;
using System.Collections.Generic;
using System.Linq;

namespace LoginTestApp.Crosscutting.ExtensionMethods
{
    public static class EnumerableExtensions
    {
        public static TSource FirstOrNew<TSource>(this IEnumerable<TSource> source)
        {
            TSource objectValue = source.FirstOrDefault();
            if (objectValue == null)
            {
                objectValue = Activator.CreateInstance<TSource>();
            }

            return objectValue;
        }

        public static bool Meets<TSource>(this TSource source, Func<TSource, bool> predicate)
        {
            return predicate(source);
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> currentEnumerable)
        {
            return !currentEnumerable.IsNotNullOrEmpty();
        }

        public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> currentEnumerable)
        {
            bool isNotNullOrEmpty = currentEnumerable != null && currentEnumerable.Any();

            return isNotNullOrEmpty;
        }
    }
}
