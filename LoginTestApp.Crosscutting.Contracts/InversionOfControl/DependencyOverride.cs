using System;

namespace LoginTestApp.Crosscutting.Contracts.InversionOfControl
{
    /// <summary>
    /// Represents an dependency override by name or type
    /// </summary>
    public class DependencyOverride
    {
        public DependencyOverride(string dependencyName, object dependencyValue)
        {
            DependencyName = dependencyName;
            DependencyValue = dependencyValue;
        }

        public DependencyOverride(Type dependencyType, object dependencyValue)
        {
            DependencyType = dependencyType;
            DependencyValue = dependencyValue;
        }

        public string DependencyName { get; }

        public Type DependencyType { get; }

        public object DependencyValue { get; }

        public static DependencyOverride CreateNew<T>(object dependencyValue)
        {
            if(!(dependencyValue is T)) throw new ArgumentException(Properties.Resources.NotValidTypeOnDependencyOverride, nameof(dependencyValue));

            return new DependencyOverride(typeof(T), dependencyValue);
        }
    }
}
