using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using LoginTestApp.Crosscutting.Contracts;
using LoginTestApp.Crosscutting.Contracts.Email;

namespace LoginTestApp.Crosscutting.EMail
{
    public class EmailSender : IEmailSender
    {
	    private readonly string host;
	    private readonly int port;
	    private readonly string userName;
	    private readonly string userPassword;
		private readonly ICryptoProvider cryptoProvider;

		public EmailSender(string host, int port, string userName, string userPassword, ICryptoProvider cryptoProvider)
		{
			this.host = host;
			this.port = port;
			this.userName = userName;
			this.userPassword = userPassword;
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

	        using (SmtpClient client = BuildSmtpClient())
	        {
				client.Send(mailMessage);    
	        }
        }

	    private SmtpClient BuildSmtpClient()
	    {
			var smtpClient = new SmtpClient(host)
			{
				Port = port,
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential(userName, this.cryptoProvider.Decrypt(userPassword)),
				EnableSsl = true,
				DeliveryMethod = SmtpDeliveryMethod.Network
			};

		    return smtpClient;
	    }
    }
}