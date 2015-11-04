using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text.RegularExpressions;

namespace LoginTestApp.Repository.ExtensionMethods
{
	public static class ContextExtensions
	{
		public static string GetTableName<T>(this DbContext context) where T : class
		{
			return context.GetObjectContext().GetTableName<T>();
		}

		public static string GetTableName<T>(this ObjectContext context) where T : class
		{
			string sql = context.CreateObjectSet<T>().ToTraceString();
			var regex = new Regex("FROM (?<table>.*) AS");
			Match match = regex.Match(sql);

			string table = match.Groups["table"].Value;
			return table;
		}

		public static IEnumerable<string> GetKeyPropertiesNames<T>(this DbContext context) where T : class
		{
			ObjectSet<T> set = context.GetObjectContext().CreateObjectSet<T>();
			IEnumerable<string> keyNames = set.EntitySet.ElementType
														.KeyMembers
														.Select(k => k.Name);

			return keyNames;
		}

		public static IEnumerable<string> GetKeyPropertiesNames<T>(this ObjectContext context) where T : class
		{
			ObjectSet<T> set = context.CreateObjectSet<T>();
			IEnumerable<string> keyNames = set.EntitySet.ElementType
														.KeyMembers
														.Select(k => k.Name);

			return keyNames;
		}

		public static ObjectContext GetObjectContext(this DbContext context)
		{
			return ((IObjectContextAdapter)context).ObjectContext;
		}
	}
}
