namespace LoginTestApp.Crosscutting.EMail.Templates.Models
{
    public class ResetLinkModel 
    {
        public string CustomerName { get; set; }

        public string ApplicationName { get; set; }

        public string Url { get; set; }
    }
}
