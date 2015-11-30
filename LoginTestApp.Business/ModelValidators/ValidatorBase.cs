using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using FluentValidation;
using FluentValidation.Results;
using LoginTestApp.Business.Contracts;
using LoginTestApp.Business.Contracts.BusinessOperation;
using LoginTestApp.Crosscutting.Contracts.Attributes;

namespace LoginTestApp.Business.ModelValidators
{
    public abstract class ValidatorBase<T> : AbstractValidator<T>, Contracts.ModelValidators.IValidator<T>
        where T : class, IModel
    {
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once StaticMemberInGenericType
        private static readonly ConcurrentDictionary<Type, Dictionary<string, MethodInfo>> cache = new ConcurrentDictionary<Type, Dictionary<string, MethodInfo>>();

        protected ValidatorBase()
        {
            CreateRuleSets();
        }

        #region Private Methods

        /// <summary>
        /// Creates the rule sets mapped in current concrete type
        /// </summary>
        /// <remarks>It the current type is already loaded in the cache then just the rulesets are configured to the validation engine</remarks>
        private void CreateRuleSets()
        {
            var currentType = GetType();

            // ReSharper disable once RedundantAssignment
            Dictionary<string, MethodInfo> mappedMethods = new Dictionary<string, MethodInfo>();

            if (!cache.TryGetValue(currentType, out mappedMethods))
            {
                //The current type hasn't been already loaded 
                mappedMethods = GetMappingsFromTypeDefinition(currentType);

                cache.TryAdd(currentType, mappedMethods);
            }

            //No mapped rules to configure
            if (!mappedMethods.Any()) return;
            
            foreach (var targetMethod in mappedMethods)
            {
                Action dynamicAction = () => targetMethod.Value.Invoke(this, new object[] { });

                //It creates the dynamic action for the ruleset name, e.g. IsValidForCreate
                RuleSet(targetMethod.Key, dynamicAction);
            }
        }

        /// <summary>
        /// Will load the mapped types from the reflected metadata
        /// </summary>
        /// <param name="currentType">Concrete type of the implementation</param>
        private Dictionary<string, MethodInfo> GetMappingsFromTypeDefinition(Type currentType)
        {
            var mappedMethods = new Dictionary<string, MethodInfo>();

            foreach (var methodInfo in currentType.GetMethodsWithAttribute<RuleSetMapperAttribute>())
            {
                string ruleSetName = methodInfo.Key.Name;
                var targetMethod = currentType.GetMethod(methodInfo.Value.RuleSetTargetMethodName, BindingFlags.NonPublic | BindingFlags.Instance);

                mappedMethods.Add(ruleSetName, targetMethod);
            }

            return mappedMethods;
        }

        #endregion Private Methods

        #region IValidator

        //public virtual bool IsValidForFind(T instance, out ValidationResult validationResult)
        //{
        //    throw new NotImplementedException();
        //}

        public virtual BusinessValidationResult ValidateForCreate(T instance)
        {
            throw new NotImplementedException();
        }

        public virtual BusinessValidationResult ValidateForUpdate(T instance)
        {
            throw new NotImplementedException();
        }

        public virtual BusinessValidationResult ValidateForDelete(T instance)
        {
            throw new NotImplementedException();
        }

        #endregion IValidator

        #region Protected Methods

        /// <summary>
        /// Performs a rule validation set
        /// </summary>
        /// <param name="instance">Instance to validate</param>
        /// <param name="ruleSetName">Ruleset name to perform</param>
        /// <returns>The validation findings</returns>
        protected ValidationResult ValidateMappedRuleSet(T instance, [CallerMemberName] string ruleSetName = null)
        {
            if(string.IsNullOrWhiteSpace(ruleSetName)) throw new ArgumentNullException(nameof(ruleSetName));

            return this.Validate(instance, ruleSet: ruleSetName);
        }

        /// <summary>
        /// Converts a <see cref="ValidationResult"/> into <see cref="BusinessValidationResult"/>
        /// </summary>
        /// <param name="validationResult">The validation results to map</param>
        protected BusinessValidationResult MapToBusinessValidationResult(ValidationResult validationResult)
        {
            BusinessValidationResult result = new BusinessValidationResult();

            foreach (var validationFailure in validationResult.Errors)
            {
                result.Messages.Add(new ModelStateMessage
                {
                    Level = BusinessMessageLevel.Error,
                    PropertyName = validationFailure.PropertyName,
                    AttemptedValue = validationFailure.AttemptedValue,
                    Message = validationFailure.ErrorMessage
                });
            }

            return result;
        }

        #endregion Protected Methods
    }
}
