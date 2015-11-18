using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using LoginTestApp.Business.Contracts.Models;
using LoginTestApp.Business.Contracts.Strategies;
using LoginTestApp.Crosscutting;
using LoginTestApp.Crosscutting.Contracts;
using LoginTestApp.Crosscutting.Contracts.Email;
using LoginTestApp.Crosscutting.Email.Templates.Views;
using LoginTestApp.Crosscutting.Email.Templates.Models;
using LoginTestApp.Repository.Contracts;

namespace LoginTestApp.Business.Strategies
{
	public class PasswordRecoveryStrategy : IPasswordRecoveryStrategy
	{
		private readonly IAccountContext accountContext;
		private readonly IEmailSender eMailSender;
		private readonly ISystemContext systemContext;
		private readonly ICryptoProvider cryptoProvider;
		private readonly IConfigurationProvider configProvider;

		private const string ResetLink = "ResetLink";
		private const string RecoveryClue = "RecoveryClue";

		private static readonly ConcurrentDictionary<string, MethodInfo> StrategiesDictionary = 
			new ConcurrentDictionary<string, MethodInfo>();

		public PasswordRecoveryStrategy
			(IAccountContext accountContext, IEmailSender eMailSender, ISystemContext systemContext, ICryptoProvider cryptoProvider, IConfigurationProvider configProvider)
		{
			this.accountContext = accountContext;
			this.eMailSender = eMailSender;
			this.systemContext = systemContext;
			this.cryptoProvider = cryptoProvider;
			this.configProvider = configProvider;
		}

		static PasswordRecoveryStrategy()
		{
            //TODO(AngelM): Remove this logic and use the dependency resolver
			if (StrategiesDictionary.Any()) return;

		    foreach (var methodInfo in typeof (PasswordRecoveryStrategy).GetMethodsWithAttribute<AnyDataAttribute>())
		    {
                string key = methodInfo.Value.Data.ToString();

                if (!StrategiesDictionary.ContainsKey(key))
                {
                    StrategiesDictionary.TryAdd(key, methodInfo.Key);
                }

            }
        }

        public Action<User> GetRecoveryStrategy(string recoveryOption)
		{
            MethodInfo methodInfo = StrategiesDictionary[recoveryOption];

			Action<User> targetMethod = user =>
			{
				// ReSharper disable once PossibleNullReferenceException
				methodInfo.Invoke(this, new object[] { user });
			};

			return targetMethod;
		}

		[AnyData(Data = ResetLink)]
		private void PasswordRecoveryByResetLink(User user)
		{
			Guid linkGuidId = Guid.NewGuid();
			string urlTemplate = configProvider.GetSectionKeyValue("PasswordRecovery", "ResetLinkUrlTemplate");
			string resetLinkUrl = string.Format(urlTemplate, linkGuidId);

			SaveDynamicLink(linkGuidId, ResetLink, resetLinkUrl);

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

		[AnyData(Data = RecoveryClue)]
		private void PasswordRecoveryByRecoveryClue(User user)
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