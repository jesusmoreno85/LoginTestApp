using System;
using LoginTestApp.Business.Contracts.Managers;
using LoginTestApp.Business.Contracts.Models;
using LoginTestApp.Business.Contracts.Strategies;
using LoginTestApp.Business.Properties;
using LoginTestApp.Crosscutting.Contracts;
using LoginTestApp.Repository.Contracts;

namespace LoginTestApp.Business.Managers
{
	public class AccountManager : IAccountManager
	{
		private readonly IRepositoryManager repositoryManager;
		private readonly ICryptoProvider cryptoProvider;
		private readonly IPasswordRecoveryStrategy recoveryResolver;
		private readonly ISystemContext systemContext;

		public AccountManager(IRepositoryManager repositoryManager, ICryptoProvider cryptoProvider, IPasswordRecoveryStrategy recoveryResolver, ISystemContext systemContext)
		{
			this.repositoryManager = repositoryManager;
			this.cryptoProvider = cryptoProvider;
			this.recoveryResolver = recoveryResolver;
			this.systemContext = systemContext;
		}

		public bool IsValidLogin(string alias, string password)
		{
			var user = FindUserByAlias(alias);

			var isValid = user != null
					  && cryptoProvider.Decrypt(user.Password) == password;

			return isValid;
		}

		public void PasswordRecovery(string alias, string recoveryOption, out string errorMessage)
		{
			var user = FindUserByAlias(alias);

			if (user == null)
			{
				errorMessage = Resources.UserAliasNotFound;
				return;
			}

			var recoveryStrategy = recoveryResolver.GetRecoveryStrategy(recoveryOption, out errorMessage);
			if (recoveryStrategy != null)
			{
				recoveryStrategy.Invoke(user);
			}
		}

		public bool ValidatePasswordRecoveryRequest(Guid guidId, out string errorMessage)
		{
			DynamicLink link = repositoryManager.DynamicLinks.GetByGuidId(guidId);

			if (link == null)
			{
				errorMessage = Resources.DynamicLinkNotFound;
				return false;
			}
			
			if(systemContext.DateTimeNow > link.ExpiresOn)
			{
				errorMessage = Resources.DynamicLinkExpired;
				return false;
			}

			errorMessage = null;
			return true;
		}

		public User FindUserByAlias(string alias, bool isActive = true)
		{
			return repositoryManager.Users.FindUserByAlias(alias, isActive);
		}
	}
}