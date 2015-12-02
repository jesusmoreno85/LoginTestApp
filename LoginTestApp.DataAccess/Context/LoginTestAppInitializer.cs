using System;
using System.Data.Entity;
using LoginTestApp.Crosscutting.Contracts;
using LoginTestApp.DataAccess.Properties;
using Constants = LoginTestApp.DataAccess.Contracts.Constants;

namespace LoginTestApp.DataAccess.Context
{
	public class LoginTestAppInitializer : CreateDatabaseIfNotExists<LoginTestAppContext>
	{
		protected override void Seed(LoginTestAppContext context)
		{
            //Initialize the database core state
		    context.Database.ExecuteSqlCommand(Resources.CoreSetup_Sql);

			//ExecuteModelBaseScripts(context,
			//	"Users");

			context.SaveChanges();
		}

        /// <summary>
        /// Configures some common constraints over the tables 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="tableNames"></param>
		private static void ExecuteModelBaseScripts(DbContext dbContext, params string[] tableNames)
		{
			foreach (var tableName in tableNames)
			{
				if (string.IsNullOrEmpty(tableName))
				{
					throw new ArgumentNullException(nameof(tableNames), @"tableNames collections containts a null or empty value");
				}

				dbContext.Database.ExecuteSqlCommand(GetModelBaseScript(tableName));
			}
		}

		private static string GetModelBaseScript(string tableName)
		{
			return Resources.ModelBase_Sql.Replace(Constants.ModelBase.SqlScriptsTableNameExpression, tableName);
		}
	}
}