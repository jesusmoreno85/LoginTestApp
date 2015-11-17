using System.Collections.Generic;
using System.Linq;
using LoginTestApp.Business.Contracts;
using LoginTestApp.Business.Contracts.BusinessOperation;

// ReSharper disable once CheckNamespace
namespace System.Web.Mvc
{
    public static class ModelStateDictionaryExtensions
    {
        public static void AddModelErrors(this ModelStateDictionary dictionary, IEnumerable<BusinessMessage> messages)
        {
            var modelErrors = messages
                                .Where(x => 
                                    x is ModelStateMessage
                                    && x.Source == BusinessMessageSource.ModelProperty 
                                    && x.Level == BusinessMessageLevel.Error)
                                .Select(x => x as ModelStateMessage);

            foreach (var modelError in modelErrors)
            {
                dictionary.AddModelError(modelError.PropertyName, modelError.Message);
            }
        }
    }
}
