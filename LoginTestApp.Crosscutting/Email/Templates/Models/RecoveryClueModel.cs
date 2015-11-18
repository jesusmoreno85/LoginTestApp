namespace LoginTestApp.Crosscutting.Email.Templates.Models
{
    public class ResetLinkModel 
    {
        public string CustomerName { get; set; }

        public string ApplicationName { get; set; }

        public string Url { get; set; }
    }
}
