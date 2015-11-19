using LoginTestApp.Business.Contracts.BusinessOperation;

namespace LoginTestApp.Business.Contracts.ModelValidators
{
    /// <summary>
    /// Defines a generic validator for the most common actions
    /// </summary>
    /// <typeparam name="T">Model type to validate</typeparam>
    public interface IValidator<T>
        where T : class, IModel
    {
        ///// <summary>
        ///// Evaluates if the given instance is valid for Create 
        ///// </summary>
        //bool IsValidForFind(T instance, out ValidationResult validationResult);

        /// <summary>
        /// Evaluates if the given instance is valid for Create 
        /// </summary>
        BusinessValidationResult ValidateForCreate(T instance);

        /// <summary>
        /// Evaluates if the given instance is valid for Update
        /// </summary>
        BusinessValidationResult ValidateForUpdate(T instance);

        /// <summary>
        /// Evaluates if the given instance is valid for Delete
        /// </summary>
        BusinessValidationResult ValidateForDelete(T instance);
    }

    /// <summary>
    /// Defines validator implementation
    /// </summary>
    public interface IValidator
    {

    }
}
