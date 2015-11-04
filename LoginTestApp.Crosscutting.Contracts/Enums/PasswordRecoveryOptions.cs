using LoginTestApp.Crosscutting.Contracts.Attributes;

namespace LoginTestApp.Crosscutting.Contracts.Enums
{
    public enum PasswordRecoveryOptions
    {
        [LocalizationDisplayName("Send a Reset Link")]
        ResetLink,

        [LocalizationDisplayName("Send a Recovery Clue")]
        RecoveryClue
    }
}
