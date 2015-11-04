using System.Reflection;
using AutoMapper;

namespace LoginTestApp.Repository.ExtensionMethods
{
	public static class AutoMapperExtensions
	{
		private const BindingFlags Flags = BindingFlags.Public | BindingFlags.Instance;

		public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>
		(this IMappingExpression<TSource, TDestination> expression)
		{
			
			var sourceType = typeof (TSource);
			var destinationProperties = typeof (TDestination).GetProperties(Flags);

			foreach (var property in destinationProperties)
			{
				if (sourceType.GetProperty(property.Name, Flags) == null)
				{
					expression.ForMember(property.Name, opt => opt.Ignore());
				}
			}

			return expression;
		}
	}
}
