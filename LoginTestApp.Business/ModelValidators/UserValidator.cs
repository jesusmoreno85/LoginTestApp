using FluentValidation;
using FluentValidation.Results;
using LoginTestApp.Business.Contracts.Models;
using LoginTestApp.Business.Contracts.ModelValidators;
using LoginTestApp.Crosscutting.Contracts.Attributes;
using LoginTestApp.Crosscutting.ExtensionMethods;

namespace LoginTestApp.Business.ModelValidators
{
    /// <summary>
    /// Validates an User instance for different actions
    /// </summary>
    public class UserValidator : ValidatorBase<User>, IUserValidator
    {
        private const int AliasMaxLength = 30;

        #region RuleSets

        private void IsValidForCreateRules()
        {
            RuleFor(x => x.Alias).NotEmpty(AliasMaxLength);

            RuleFor(x => x.Password).NotEmpty(30);

            RuleFor(x => x.PasswordRecoveryClue).NotEmpty(50);

            RuleFor(x => x.Email).NotEmpty(256).EmailAddress();

            RuleFor(x => x.PhoneNumber).NotEmpty(25);

            //There is no need of validation as bool type does not allow nulls
            //RuleFor(x => x.IsActive).NotEmpty();
        }

        #endregion RuleSets

        #region IValidator

        [RuleSetMapper(nameof(IsValidForCreateRules))]
        public override bool IsValidForCreate(User instance, out ValidationResult validationResult)
        {
            validationResult = ValidateMappedRuleSet(instance);
            return validationResult.IsValid;
        }

        #endregion IValidator
    }


}
