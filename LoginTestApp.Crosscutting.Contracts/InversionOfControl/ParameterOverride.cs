
namespace LoginTestApp.Crosscutting.Contracts.InversionOfControl
{
    /// <summary>
    /// Represents a parameter to be overriden by name and value
    /// </summary>
    public class ParameterOverride
    {
        public ParameterOverride(string parameterName, object parameterValue)
        {
            ParameterName = parameterName;
            ParameterValue = parameterValue;
        }

        public string ParameterName { get; set; }

        public object ParameterValue { get; set; }
    }
}
