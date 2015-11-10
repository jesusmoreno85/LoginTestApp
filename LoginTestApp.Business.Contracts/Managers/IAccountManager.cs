using System;
using LoginTestApp.Business.Contracts.Models;

namespace LoginTestApp.Business.Contracts.Managers
{
	public interface IAccountManager
	{
		bool IsValidLogin(string alias, string password);

		User FindUserByAlias(string alias, bool isActive = true);

		void PasswordRecovery(string alias, string recoveryOption, out string errorMessage);

		bool ValidatePasswordRecoveryRequest(Guid guidId, out string errorMessage);

	    void CreateNew(User user);
	}
}