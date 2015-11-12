using FluentValidation.Results;

// ReSharper disable once CheckNamespace
namespace System.Web.Mvc
{
    public static class ModelStateDictionaryExtensions
    {
        public static void AddModelErrors(this ModelStateDictionary dictionary, ValidationResult validationResult)
        {
            foreach (var validationFailure in validationResult.Errors)
            {
                dictionary.AddModelError(validationFailure.PropertyName, validationFailure.ErrorMessage);
            }
        }
    }
}
