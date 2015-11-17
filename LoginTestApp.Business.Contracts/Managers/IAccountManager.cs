using System;
using LoginTestApp.Business.Contracts.BusinessOperation;
using LoginTestApp.Business.Contracts.Models;

namespace LoginTestApp.Business.Contracts.Managers
{
	public interface IAccountManager
    {
		bool IsValidLogin(string alias, string password);

        BusinessOperationResult<User> FindUserByAlias(string alias, bool? isActive = true);

        BusinessOperationResult<bool> PasswordRecovery(string alias, string recoveryOption);

        BusinessOperationResult<bool> ValidatePasswordRecoveryRequest(Guid guidId);

        BusinessOperationResult<bool> CreateNew(User user);
	}
}