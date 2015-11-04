using System;
using System.Data.Entity;
using LoginTestApp.Crosscutting.Contracts;
using LoginTestApp.DataAccess.Contracts.Entities;
using LoginTestApp.DataAccess.Properties;
using Constants = LoginTestApp.DataAccess.Contracts.Constants;

namespace LoginTestApp.DataAccess.Context
{
	public class LoginTestAppInitializer : CreateDatabaseIfNotExists<LoginTestAppContext>
	{
		private readonly ICryptoProvider cryptoProvider;

		public LoginTestAppInitializer(ICryptoProvider cryptoProvider)
		{
			this.cryptoProvider = cryptoProvider;
		}

		protected override void Seed(LoginTestAppContext context)
		{
			ExecuteModelBaseScripts(context,
				"Users");

			context.Users.Add(new User()
			{
				Id = 1,
				Alias = "admin",
				FullName = "Admin Service Account",
				Password = cryptoProvider.Encrypt("Admin7209"),
				Email = "jesusmoreno85@hotmail.com",
				PhoneNumber = "6671372531",
				PasswordRecoveryClue = cryptoProvider.Encrypt("Password RQ"),
				IsActive = true
			});

			context.SaveChanges();
		}

		private static void ExecuteModelBaseScripts(DbContext dbContext, params string[] tableNames)
		{
			foreach (var tableName in tableNames)
			{
				if (string.IsNullOrEmpty(tableName))
				{
					throw new ArgumentNullException("tableNames", string.Format("tableNames collections containts a null or empty value"));
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