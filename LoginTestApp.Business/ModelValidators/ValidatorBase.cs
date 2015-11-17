using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using FluentValidation;
using FluentValidation.Results;
using LoginTestApp.Business.Contracts;
using LoginTestApp.Crosscutting.Contracts.Attributes;

namespace LoginTestApp.Business.ModelValidators
{
    public abstract class ValidatorBase<T> : AbstractValidator<T>, Contracts.ModelValidators.IValidator<T>
        where T : class, IModel
    {
        private static readonly ConcurrentDictionary<Type, Dictionary<string, Action>> cache = new ConcurrentDictionary<Type, Dictionary<string, Action>>();

        protected ValidatorBase()
        {
            var currentType = GetType();

            if (!LoadMappingsFromCache(currentType))
            {
                //The current type hasn't been already loaded 
                LoadMappingsFromTypeDefinition(currentType);
            }
        }

        /// <summary>
        /// It tries to load the mappings from the cache
        /// </summary>
        /// <param name="currentType">Concrete type of the implementation</param>
        /// <returns>True if the type is already in the cache otherwise, false</returns>
        private bool LoadMappingsFromCache(Type currentType)
        {
            // ReSharper disable once RedundantAssignment
            Dictionary<string, Action> mappedMethods;

            if (!cache.TryGetValue(currentType, out mappedMethods)) return false;

            //If the code gets here the current type has been already loaded so, there is no longer need of using reflection
            foreach (var methodMapping in mappedMethods)
            {
                RuleSet(methodMapping.Key, methodMapping.Value);
            }

            return true;
        }

        /// <summary>
        /// Will load the mapped types from the reflected metadata
        /// </summary>
        /// <param name="currentType">Concrete type of the implementation</param>
        private void LoadMappingsFromTypeDefinition(Type currentType)
        {
            var mappedMethods = new Dictionary<string, Action>();

            foreach (var methodInfo in currentType.GetMethodsWithAttribute<RuleSetMapperAttribute>())
            {
                string ruleSetName = methodInfo.Key.Name;
                var targetMethod = currentType.GetMethod(methodInfo.Value.RuleSetTargetMethodName, BindingFlags.NonPublic | BindingFlags.Instance);

                Action dynamicAction = () => targetMethod.Invoke(this, new object[] { });

                mappedMethods.Add(ruleSetName, dynamicAction);

                //It creates the dynamic action for the ruleset name, e.g. IsValidForCreate
                RuleSet(ruleSetName, dynamicAction);
            }

            cache.TryAdd(currentType, mappedMethods);
        }

        //public virtual bool IsValidForFind(T instance, out ValidationResult validationResult)
        //{
        //    throw new NotImplementedException();
        //}

        public virtual bool IsValidForCreate(T instance, out ValidationResult validationResult)
        {
            throw new NotImplementedException();
        }

        public virtual bool IsValidForUpdate(T instance, out ValidationResult validationResult)
        {
            throw new NotImplementedException();
        }

        public virtual bool IsValidForDelete(T instance, out ValidationResult validationResult)
        {
            throw new NotImplementedException();
        }

        protected ValidationResult ValidateMappedRuleSet(T instance, [CallerMemberName] string ruleSetName = null)
        {
            return this.Validate(instance, ruleSet: ruleSetName);
        }
    }
}
