using System;

namespace LoginTestApp.Crosscutting.Contracts.InversionOfControl
{
    /// <summary>
    /// Represents an inversion of control dependency override
    /// </summary>
    public class DependencyOverride
    {
        public DependencyOverride(object dependencyValue)
        {
            DependencyType = dependencyValue.GetType();
            DependencyValue = dependencyValue;
        }

        public DependencyOverride(Type dependencyType, object dependencyValue)
        {
            DependencyType = dependencyType;
            DependencyValue = dependencyValue;
        }

        public Type DependencyType { get; set; }

        public object DependencyValue { get; set; }
    }
}
