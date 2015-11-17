using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace LoginTestApp.Business.Contracts.BusinessOperation
{
    /// <summary>
    /// Represents a business operation and it's possible messages
    /// </summary>
    /// <typeparam name="TResult">The concrete result type</typeparam>
    public class BusinessOperationResult<TResult>
    {
        public BusinessOperationResult()
        {
            Messages = new List<BusinessMessage>();
            OperationResult = default(TResult);
        }

        public BusinessOperationResult(List<BusinessMessage> messages, TResult result)
        {
            Messages = messages;
            OperationResult = result;
        }
        
        public BusinessOperationResult(List<BusinessMessage> messages)
        {
            Messages = messages;
            OperationResult = default(TResult);
        }


        public BusinessOperationResult(TResult result)
        {
            OperationResult = result;
            Messages = new List<BusinessMessage>();
        }

        public List<BusinessMessage> Messages { get; set; }

        public TResult OperationResult { get; set; }

        public bool IsError
        {
            get { return Messages.Any(x => x.Level == BusinessMessageLevel.Error); }
        }

        public void AddError(BusinessMessageSource source, string message)
        {
            Messages.Add(new BusinessMessage(source)
            {
                Level = BusinessMessageLevel.Error,
                Source = source,
                Message = message
            });
        }

        public void FillMessages(ValidationResult validationResult)
        {
            Messages.Clear();

            foreach (var validationFailure in validationResult.Errors)
            {
                Messages.Add(new ModelStateMessage
                {
                    Level = BusinessMessageLevel.Error,
                    PropertyName = validationFailure.PropertyName, 
                    AttemptedValue = validationFailure.AttemptedValue, 
                    Message = validationFailure.ErrorMessage
                });
            }
        }

        public BusinessOperationResult<bool> CastToBooleanResult()
        {
            var result = new BusinessOperationResult<bool>(IsError);

            BusinessMessage[] messages = {};
            Messages.CopyTo(messages);

            result.Messages = messages.ToList();

            return result;
        }
    }

    public class BusinessOperationResult
    {
        public static BusinessOperationResult<bool> CreateNew(ValidationResult validationResult)
        {
            var result = new BusinessOperationResult<bool>();
            result.FillMessages(validationResult);

            result.OperationResult = validationResult.IsValid;

            return result;
        }
    }
}
