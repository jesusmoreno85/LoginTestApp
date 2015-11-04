using System;
using LoginTestApp.Business.Contracts.Models;

namespace LoginTestApp.Business.Contracts.Strategies
{
    public interface IPasswordRecoveryStrategy
    {
        Action<User> GetRecoveryStrategy(string recoveryOption, out string errorMessage);
    }
}
