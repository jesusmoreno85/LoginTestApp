using LoginTestApp.Business.Contracts.Models;

namespace LoginTestApp.Business.Contracts.Strategies
{
    /// <summary>
    /// Defines a password recovery strategy
    /// </summary>
    public interface IPasswordRecoveryStrategy
    {
        /// <summary>
        /// Executes the recover of the password strategy
        /// </summary>
        /// <param name="user"></param>
        void PerformRecovery(User user);

        /// <summary>
        /// The recovery strategy represented by this code
        /// </summary>
        string RecoveryOption { get; set; }
    }
}
