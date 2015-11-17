using System;
using FluentValidation.Results;
using LoginTestApp.Business.Contracts.BusinessOperation;
using LoginTestApp.Business.Contracts.Managers;
using LoginTestApp.Business.Contracts.Models;
using LoginTestApp.Business.Contracts.ModelValidators;
using LoginTestApp.Business.Contracts.Strategies;
using LoginTestApp.Business.Properties;
using LoginTestApp.Crosscutting.Contracts;
using LoginTestApp.Repository.Contracts;

namespace LoginTestApp.Business.Managers
{
	public class AccountManager : IAccountManager
	{
		private readonly IAccountContext accountContext;
		private readonly ICryptoProvider cryptoProvider;
		private readonly IPasswordRecoveryStrategy recoveryResolver;
		private readonly ISystemContext systemContext;
	    private readonly IUserValidator userValidator;

        public AccountManager(
            IAccountContext accountContext, 
            ICryptoProvider cryptoProvider, 
            IPasswordRecoveryStrategy recoveryResolver, 
            ISystemContext systemContext, 
            IUserValidator userValidator)
		{
			this.accountContext = accountContext;
			this.cryptoProvider = cryptoProvider;
			this.recoveryResolver = recoveryResolver;
			this.systemContext = systemContext;
            this.userValidator = userValidator;
		}

        public bool IsValidLogin(string alias, string password)
		{
			var user = FindUserByAlias(alias).OperationResult;

			var isValid = user != null
					  && cryptoProvider.Decrypt(user.Password) == password;

			return isValid;
		}

		public BusinessOperationResult<bool> PasswordRecovery(string alias, string recoveryOption)
		{
		    var result = FindUserByAlias(alias);

		    if (!result.IsError)
		    {
                //Everything is fine we can go with the Recovery Strategy
                var recoveryStrategy = recoveryResolver.GetRecoveryStrategy(recoveryOption);
                recoveryStrategy?.Invoke(result.OperationResult);
            }

		    return result.CastToBooleanResult();
		}

	    public BusinessOperationResult<bool> ValidatePasswordRecoveryRequest(Guid guidId)
		{
            var result = new BusinessOperationResult<bool>();

            DynamicLink link = accountContext.DynamicLinks.GetByGuidId(guidId);

			if (link == null)
			{
                result.AddError(BusinessMessageSource.BusinessRule, Resources.DynamicLinkNotFound);
			}
			else if(systemContext.DateTimeNow > link.ExpiresOn)
			{
                result.AddError(BusinessMessageSource.BusinessRule, Resources.DynamicLinkExpired);
			}
			else
			{
			    result.OperationResult = true;
			}

			return result;
		}

	    public BusinessOperationResult<User> FindUserByAlias(string alias, bool? isActive = null)
		{
            var result = new BusinessOperationResult<User>();

            if (string.IsNullOrWhiteSpace(alias))
            {
                result.AddError(BusinessMessageSource.ModelProperty, Resources.InvalidValue);
            }
            else
            {
                User foundUser = accountContext.Users.FindUserByAlias(alias, isActive);

                if (foundUser == null)
                {
                    result.AddError(BusinessMessageSource.BusinessRule, Resources.UserNotFound);
                }

                result.OperationResult = foundUser;
            }
            
            return result; 
		}

	    public BusinessOperationResult<bool> CreateNew(User user)
	    {
            ValidationResult results;

            if (userValidator.IsValidForCreate(user, out results))
	        {
                accountContext.Users.Create(user);
                accountContext.SaveChanges();

	            return new BusinessOperationResult<bool>(true);
	        }

            return BusinessOperationResult.CreateNew(results); 
	    }
	}
}