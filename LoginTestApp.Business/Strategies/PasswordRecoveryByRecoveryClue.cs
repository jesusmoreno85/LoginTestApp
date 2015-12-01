using System.Net.Mail;
using LoginTestApp.Business.Contracts.Models;
using LoginTestApp.Business.Contracts.Strategies;
using LoginTestApp.Crosscutting.Contracts;
using LoginTestApp.Crosscutting.Contracts.Email;
using LoginTestApp.Crosscutting.Email.Templates.Models;
using LoginTestApp.Crosscutting.Email.Templates.Views;

namespace LoginTestApp.Business.Strategies
{
    /// <summary>
    /// Represents a password recovery strategy by Recovery Clue
    /// </summary>
    public class PasswordRecoveryByRecoveryClue: IPasswordRecoveryStrategy
    {
        private readonly IEmailSender eMailSender;
        private readonly ISystemContext systemContext;
        private readonly ICryptoProvider cryptoProvider;
        private readonly IConfigurationProvider configProvider;

        public PasswordRecoveryByRecoveryClue(IEmailSender eMailSender, ISystemContext systemContext, ICryptoProvider cryptoProvider, IConfigurationProvider configProvider)
        {
            this.eMailSender = eMailSender;
            this.systemContext = systemContext;
            this.cryptoProvider = cryptoProvider;
            this.configProvider = configProvider;
        }

        public string RecoveryOption { get; set; }

        public void PerformRecovery(User user)
        {
            var template = new RecoveryClueTemplate()
            {
                Model = new RecoveryClueModel
                {
                    ApplicationName = systemContext.AppFullName,
                    CustomerName = user.FullName,
                    RecoveryClue = cryptoProvider.Decrypt(user.PasswordRecoveryClue)
                }
            };

            //Starting eMail submission 
            string senderAddress = configProvider.GetSectionKeyValue("PasswordRecovery", "EmailSenderAddress");
            string senderFullName = configProvider.GetSectionKeyValue("PasswordRecovery", "EmailSenderFullName");

            eMailSender.From = new MailAddress(senderAddress, senderFullName);
            eMailSender.To = new MailAddress(user.Email, user.FullName);
            eMailSender.IsBodyHtml = true;
            eMailSender.Subject = template.Subject;
            eMailSender.Body = template.TransformText();

            eMailSender.Send();
        }
    }
}
