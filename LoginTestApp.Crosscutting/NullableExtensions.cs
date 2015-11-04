
// ReSharper disable once CheckNamespace
namespace System
{
    public static class NullableExtensions
    {
        /// <summary>
        /// Evaluates a nullable instance to determine if it has value and that value is equals to another given value
        /// </summary>
        /// <param name="nullable">The value to evaluate</param>
        /// <param name="value">The value to be compared in case nullable has value</param>
        /// <typeparam name="T">The concrete underlaying type of Nullable class</typeparam>
        /// <returns>True if it has value and that value is equals to another given value otherwise, false</returns>
        public static bool HasValueEquals<T>(this T? nullable, T value) where T : struct
        {
            bool hasValueEquals = nullable.HasValue && nullable.Value.Equals(value);

            return hasValueEquals;
        }

        /// <summary>
        /// Evaluates a nullable instance to determine if it has no value or that value is equals to another given value
        /// </summary>
        /// <param name="nullable">The value to evaluate</param>
        /// <param name="value">The value to be compared in case nullable has no value</param>
        /// <typeparam name="T">The concrete underlaying type of Nullable class</typeparam>
        /// <returns>True if it has no value or that value is equals to another given value otherwise, false</returns>
        public static bool HasNoValueOrEquals<T>(this T? nullable, T value) where T : struct
        {
            bool hasValueEquals = !nullable.HasValue || nullable.Value.Equals(value);

            return hasValueEquals;
        }
    }
}

