using System;

namespace LoginTestApp.Crosscutting.Contracts.Attributes
{
    /// <summary>
    /// Decorates a method that must be mapped to a RuleSet
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class RuleSetMapperAttribute : Attribute
    {
        public string RuleSetTargetMethodName { get; set; }

        public RuleSetMapperAttribute(string ruleSetTargetMethodName)
        {
            RuleSetTargetMethodName = ruleSetTargetMethodName;
        }
    }
}
