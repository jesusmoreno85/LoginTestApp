using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using LoginTestApp.Crosscutting.Contracts;
using LoginTestApp.Crosscutting.Contracts.Email;

namespace LoginTestApp.Crosscutting.Email
{
    public class EmailSender : IEmailSender
    {
        private readonly ICryptoProvider cryptoProvider;

		public EmailSender(ICryptoProvider cryptoProvider)
		{
			this.cryptoProvider = cryptoProvider;

			ReplyToList = new MailAddressCollection();
		}

	    public MailAddress From { get; set; }

        public MailAddress To { get; set; }

        public MailAddressCollection ReplyToList { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public bool IsBodyHtml { get; set; }

        public void Send()
        {
            var mailMessage = new MailMessage(From, To)
            {
                Subject = Subject,
                SubjectEncoding = Encoding.UTF8,
                Body = Body,
                BodyEncoding = Encoding.UTF8,
                IsBodyHtml = IsBodyHtml
            };

            ReplyToList.ToList().ForEach(t =>
            {
                mailMessage.ReplyToList.Add(t);
            });

            var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");

            using (var client = new SmtpClient())
            {
                client.UseDefaultCredentials = true;
                client.Credentials = new NetworkCredential(smtpSection.Network.UserName, cryptoProvider.Decrypt(smtpSection.Network.Password));

                client.Send(mailMessage);    
	        }
        }
    }
}