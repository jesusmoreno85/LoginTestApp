using System;
using System.Net.Mail;
using LoginTestApp.Business.Contracts.Models;
using LoginTestApp.Business.Contracts.Strategies;
using LoginTestApp.Crosscutting.Contracts;
using LoginTestApp.Crosscutting.Contracts.Email;
using LoginTestApp.Crosscutting.Email.Templates.Models;
using LoginTestApp.Crosscutting.Email.Templates.Views;
using LoginTestApp.Repository.Contracts;

namespace LoginTestApp.Business.Strategies
{
    /// <summary>
    /// Represents a password recovery strategy by Reset Link
    /// </summary>
    public class PasswordRecoveryByResetLink : IPasswordRecoveryStrategy
    {
        private readonly IAccountContext accountContext;
        private readonly IEmailSender eMailSender;
        private readonly ISystemContext systemContext;
        private readonly IConfigurationProvider configProvider;
        
        public PasswordRecoveryByResetLink(IAccountContext accountContext, IEmailSender eMailSender, ISystemContext systemContext, IConfigurationProvider configProvider)
        {
            this.accountContext = accountContext;
            this.eMailSender = eMailSender;
            this.systemContext = systemContext;
            this.configProvider = configProvider;
        }

        public string RecoveryOption { get; set; }

        public void PerformRecovery(User user)
        {
            Guid linkGuidId = Guid.NewGuid();
            string urlTemplate = configProvider.GetSectionKeyValue("PasswordRecovery", "ResetLinkUrlTemplate");
            string resetLinkUrl = string.Format(urlTemplate, linkGuidId);

            SaveDynamicLink(linkGuidId, RecoveryOption, resetLinkUrl);

            var template = new ResetLinkTemplate
            {
                Model = new ResetLinkModel
                {
                    ApplicationName = systemContext.AppFullName,
                    CustomerName = user.FullName,
                    Url = resetLinkUrl
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

        private void SaveDynamicLink(Guid guidId, string recoveryType, string url)
        {
            int keepAliveFor = configProvider.GetSectionKeyValue<int>("PasswordRecovery", "ResetLinkKeepAliveFor");

            var dynamicLink = new DynamicLink
            {
                GuidId = guidId,
                Type = "PasswordRecovery",
                SubType = recoveryType,
                Url = url,
                ExpiresOn = systemContext.DateTimeNow.AddMinutes(keepAliveFor),
                IsConsumed = false
            };

            accountContext.DynamicLinks.Create(dynamicLink);
            accountContext.SaveChanges();
        }
    }
}
