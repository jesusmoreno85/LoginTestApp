using System.Collections.Generic;
using System.Linq;

namespace LoginTestApp.Business.Contracts.BusinessOperation
{
    /// <summary>
    /// Represents a business validation result
    /// </summary>
    public class BusinessValidationResult
    {
        public BusinessValidationResult()
        {
            Messages = new List<BusinessMessage>();
        }

        public bool IsValid
        {
            get { return Messages.All(x => x.Level != BusinessMessageLevel.Error); }
        }

        public List<BusinessMessage> Messages { get; set; }
    }
}
