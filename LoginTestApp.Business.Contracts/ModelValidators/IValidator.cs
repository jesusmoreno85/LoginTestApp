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
        ValidationResult IsValidForCreate(T instance);

        /// <summary>
        /// Evaluates if the given instance is valid for Update
        /// </summary>
        ValidationResult IsValidForUpdate(T instance);

        /// <summary>
        /// Evaluates if the given instance is valid for Delete
        /// </summary>
        ValidationResult IsValidForDelete(T instance);
    }

    /// <summary>
    /// Defines validator implementation
    /// </summary>
    public interface IValidator
    {

    }
}
