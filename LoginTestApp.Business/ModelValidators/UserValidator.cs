using FluentValidation;
using FluentValidation.Results;
using LoginTestApp.Business.Contracts.BusinessOperation;
using LoginTestApp.Business.Contracts.Models;
using LoginTestApp.Business.Contracts.ModelValidators;
using LoginTestApp.Business.Properties;
using LoginTestApp.Crosscutting.Contracts.Attributes;
using LoginTestApp.Crosscutting.ExtensionMethods;
using LoginTestApp.Repository.Contracts.Repositories;

namespace LoginTestApp.Business.ModelValidators
{
    /// <summary>
    /// Validates an User instance for different actions
    /// </summary>
    public class UserValidator : ValidatorBase<User>, IUserValidator
    {
        private readonly IUsersRepository usersRepository;

        public UserValidator(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        #region RuleSets

        private void IsValidForSaveRules()
        {
            RuleFor(x => x.Alias).NotEmpty(30);

            RuleFor(x => x.Password).NotEmpty(30);

            RuleFor(x => x.PasswordRecoveryClue).NotEmpty(50);

            RuleFor(x => x.Email).NotEmpty(256).EmailAddress();

            RuleFor(x => x.PhoneNumber).NotEmpty(25);

            //There is no need of validation as bool type does not allow nulls
            //RuleFor(x => x.IsActive).NotEmpty();            
        }

        private void IsValidForCreateRules()
        {
            IsValidForSaveRules();
        }

        private void IsValidForUpdateRules()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage(Resources.NotValidIdValueForUpdate);

            IsValidForSaveRules();

            When(x => x.Id > 0, CheckAvailableAlias);

        }

        private void CheckAvailableAlias()
        {
            RuleFor(x => x.Alias)
                .Must((user, alias) => usersRepository.IsAliasAvailable(user.Alias, user.Id))
                .WithMessage(Resources.UserAliasNotAvailable);
        }

        #endregion RuleSets

        #region IValidator

        [RuleSetMapper(nameof(IsValidForCreateRules))]
        public override BusinessValidationResult ValidateForCreate(User instance)
        {
            ValidationResult result = ValidateMappedRuleSet(instance);
            return MapToBusinessValidationResult(result);
        }

        [RuleSetMapper(nameof(IsValidForUpdateRules))]
        public override BusinessValidationResult ValidateForUpdate(User instance)
        {
            ValidationResult result = ValidateMappedRuleSet(instance);
            return MapToBusinessValidationResult(result);
        }

        #endregion IValidator
    }


}
