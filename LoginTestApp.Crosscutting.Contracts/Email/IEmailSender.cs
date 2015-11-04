using System.Net.Mail;

namespace LoginTestApp.Crosscutting.Contracts.Email
{
    public interface IEmailSender
    {
        MailAddress From { get; set; }

        MailAddress To { get; set; }

        MailAddressCollection ReplyToList { get; set; }

        string Subject { get; set; }

        string Body { get; set; }

        bool IsBodyHtml { get; set; }

        void Send();
    }
}
