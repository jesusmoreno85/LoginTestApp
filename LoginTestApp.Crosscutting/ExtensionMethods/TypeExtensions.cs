using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LoginTestApp.Crosscutting.ExtensionMethods;

// ReSharper disable once CheckNamespace
namespace System
{
    /// <summary>
    /// Extends the functionality of <see cref="Type"/> 
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Finds methods decorated with an specific attribute 
        /// </summary>
        /// <typeparam name="T">Attribute Type</typeparam>
        public static Dictionary<MethodInfo, T> GetMethodsWithAttribute<T>(this Type contextType)
            where T : Attribute
        {
            return contextType.GetMethodsWithAttribute<T>(x => true);
        }

        /// <summary>
        /// Finds methods decorated with an specific attribute and also that matches the given predicate
        /// </summary>
        /// <typeparam name="T">Attribute Type</typeparam>
        public static Dictionary<MethodInfo, T> GetMethodsWithAttribute<T>(this Type contextType, Func<T, bool> predicate)
            where T : Attribute
        {
            var methods = contextType
                .GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public |
                            BindingFlags.NonPublic)
                .Where(t =>
                    t.GetCustomAttribute<T>()
                        .Meets(x =>
                            x != null
                            && predicate(x)))
                .ToList();

            return methods.ToDictionary(methodInfo => methodInfo, methodInfo => methodInfo.GetCustomAttribute<T>());
        }
    }
}
