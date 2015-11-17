namespace LoginTestApp.Business.Contracts.BusinessOperation
{
    /// <summary>
    /// Represents a business message
    /// </summary>
    public class BusinessMessage
    {
        public BusinessMessage(BusinessMessageSource source)
        {
            Source = source;
        }

        public BusinessMessageLevel Level { get; set; }

        public BusinessMessageSource Source { get; set; }

        public string Message { get; set; }
    }
}
