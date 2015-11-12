using FluentValidation.Results;

namespace LoginTestApp.Business.Contracts.ModelValidators
{
    /// <summary>
    /// Defines a generic validator for the most common actions
    /// </summary>
    /// <typeparam name="T">Model type to validate</typeparam>
    public interface IValidator<T>
        where T : class, IModel
    {
        /// <summary>
        /// Evaluates if the given instance is valid for Create 
        /// </summary>
        bool IsValidForCreate(T instance, out ValidationResult validationResult);

        /// <summary>
        /// Evaluates if the given instance is valid for Update
        /// </summary>
        bool IsValidForUpdate(T instance, out ValidationResult validationResult);

        /// <summary>
        /// Evaluates if the given instance is valid for Delete
        /// </summary>
        bool IsValidForDelete(T instance, out ValidationResult validationResult);
    }

    /// <summary>
    /// Defines validator implementation
    /// </summary>
    public interface IValidator
    {

    }
}
