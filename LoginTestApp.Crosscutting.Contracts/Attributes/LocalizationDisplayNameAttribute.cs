using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace LoginTestApp.Crosscutting.Contracts.Attributes
{
    /// <summary>
    /// Localization display name attribute
    /// </summary>
    /// <remarks></remarks>
    [AttributeUsage(AttributeTargets.All)]
    public class LocalizationDisplayNameAttribute : DisplayNameAttribute
    {
        private readonly DisplayAttribute display;

        public LocalizationDisplayNameAttribute(string displayName)
            : base(displayName)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizationDisplayNameAttribute"/> class.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        /// <param name="resourceType">Type of the resource.</param>
        /// <remarks></remarks>
        public LocalizationDisplayNameAttribute(string resourceName, Type resourceType)
        {
            display = new DisplayAttribute
            {
                ResourceType = resourceType,
                Name = resourceName
            };
        }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        /// <returns>The display name.</returns>
        /// <remarks></remarks>
        public override string DisplayName
        {
            get
            {
                if (display != null)
                {
                    return display.GetName();
                }

                return this.DisplayName;
            }
        }
            
            


        public static LocalizationDisplayNameAttribute GetAttribute(Type type)
        {
            var attr = (LocalizationDisplayNameAttribute)type.GetCustomAttributes(typeof(LocalizationDisplayNameAttribute), true).SingleOrDefault();
            return attr;
        }

        /// <summary>
        /// Gets the display name for an enum value
        /// </summary>
        /// <param name="enumValue">The enum value</param>
        /// <returns></returns>
        public static string GetDisplayName(Enum enumValue)
        {
            FieldInfo field = enumValue.GetType().GetField(enumValue.ToString());
            return GetDisplayName(field);
        }

        public static string GetDisplayName(MemberInfo member)
        {
            var attr = (LocalizationDisplayNameAttribute)member.GetCustomAttributes(typeof(LocalizationDisplayNameAttribute), true).SingleOrDefault();
            return attr?.DisplayName;
        }

        /// <summary>
        /// Get all the display names for a given enum type
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static IDictionary<Enum, string> GetDisplayNames(Type enumType)
        {
            var fields = enumType.GetFields(BindingFlags.Static | BindingFlags.GetField | BindingFlags.Public);
            var values = Enum.GetValues(enumType).OfType<Enum>();
            var items =
                from value in values
                from field in fields
                let descriptionAttribute = field
                    .GetCustomAttributes(typeof(LocalizationDisplayNameAttribute), true)
                    .OfType<LocalizationDisplayNameAttribute>()
                    .FirstOrDefault()
                let displayName = (descriptionAttribute != null)
                    ? descriptionAttribute.DisplayName
                    : value.ToString()
                where value.ToString() == field.Name
                select new { Id = value, Name = displayName };
            return items.ToDictionary(k => k.Id, v => v.Name);
        }

        public static IDictionary<TEnumType, string> GetDisplayNames<TEnumType>()
        {
            return (IDictionary<TEnumType, string>)GetDisplayNames(typeof(TEnumType));
        }

    }
}