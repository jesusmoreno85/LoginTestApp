
using LoginTestApp.Business.Contracts.BusinessOperation;

namespace LoginTestApp.Business.Contracts
{
    public class ModelStateMessage : BusinessMessage
    {
        public ModelStateMessage() 
            : base(BusinessMessageSource.ModelProperty)
        {

        }

        public string PropertyName { get; set; }

        public object AttemptedValue { get; set; }
    }
}
