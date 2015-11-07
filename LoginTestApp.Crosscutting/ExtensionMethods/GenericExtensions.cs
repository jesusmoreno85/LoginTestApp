using System.Linq;

// ReSharper disable once CheckNamespace
namespace System
{
    public static class GenericExtensions
    {
        public static bool Exists<T>(this T value, params T[] collectionValues)
        {
            return collectionValues.Contains(value);
        }
    }
}