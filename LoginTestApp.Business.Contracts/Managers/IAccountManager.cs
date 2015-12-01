using System;
using LoginTestApp.Business.Contracts.BusinessOperation;
using LoginTestApp.Business.Contracts.Models;

namespace LoginTestApp.Business.Contracts.Managers
{
    /// <summary>
    /// Handles several Account operations
    /// </summary>
	public interface IAccountManager
    {
        /// <summary>
        /// Determines if the given values correcponds to a user
        /// </summary>
        /// <param name="alias">The user's alias</param>
        /// <param name="password">The user's password</param>
		bool IsValidLogin(string alias, string password);

        /// <summary>
        /// Looks for a user with a given alias
        /// </summary>
        /// <param name="alias">The user's alias</param>
        /// <param name="isActive">Indicates an active state to check. If null then this parameter gets ignored</param>
        /// <returns>Null if the user wasn't found</returns>
        BusinessOperationResult<User> FindUserByAlias(string alias, bool? isActive);

        /// <summary>
        /// Password recovery request
        /// </summary>
        /// <param name="alias">The user's alias</param>
        /// <param name="recoveryOption">The recovery strategy to use.</param>
        /// <returns>True if there was no issues with the request execution</returns>
        BusinessOperationResult<bool> PasswordRecovery(string alias, string recoveryOption);

        /// <summary>
        /// Validates a password recovery request
        /// </summary>
        /// <param name="guidId">Te unique identifier for the request</param>
        /// <returns>True if the link is found and hasn't been expired or consumed.</returns>
        BusinessOperationResult<bool> ValidatePasswordRecoveryRequest(Guid guidId);

        /// <summary>
        /// Creates a new user account
        /// </summary>
        /// <param name="user">The user account data</param>
        /// <returns>True if there was no issues with the request execution</returns>
        BusinessOperationResult<bool> CreateNew(User user);

        /// <summary>
        /// Updates a new user account
        /// </summary>
        /// <param name="user">The user account data</param>
        /// <returns>True if there was no issues with the request execution</returns>
	    BusinessOperationResult<bool> Update(User user);
    }
}