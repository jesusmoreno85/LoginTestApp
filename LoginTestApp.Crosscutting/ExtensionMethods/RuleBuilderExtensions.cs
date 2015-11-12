using FluentValidation;

namespace LoginTestApp.Crosscutting.ExtensionMethods
{
    /// <summary>
    /// Extends some classes of the FluentValidation API
    /// </summary>
    public static class RuleBuilderExtensions
    {
        /// <summary>
        /// Lets to compose a NotEmpty/MaxLength constraint
        /// </summary>
        public static IRuleBuilderOptions<T, string> NotEmpty<T>(this IRuleBuilder<T, string> ruleBuilder, int maxLength)
        {
            return ruleBuilder.NotEmpty().Length(1, maxLength);
        }
    }
}
