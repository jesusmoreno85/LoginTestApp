using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using LoginTestApp.Crosscutting.Contracts;

namespace LoginTestApp.Crosscutting
{
	public class ConfigurationProvider : IConfigurationProvider
	{
		public StringDictionary GetSectionValues(string sectionName)
		{
			var nameValueCollection = ConfigurationManager.GetSection(sectionName) as NameValueCollection;

			if (nameValueCollection == null) return null;

			//Everything is OK, continue with the fill up logic
			var keyValueReturn = new StringDictionary();

			nameValueCollection.AllKeys.ToList().ForEach(key =>
			{
				string value = nameValueCollection[key];
				keyValueReturn.Add(key, value);
			});

			return keyValueReturn;
		}

		public string GetSectionKeyValue(string sectionName, string key)
		{
			var sectionValues = GetSectionValues(sectionName);

			if (sectionValues.Count == 0) return null;

			var keyValue = sectionValues[key];

			return keyValue;
		}

		public T GetSectionKeyValue<T>(string sectionName, string key)
		{
			string keyValue = GetSectionKeyValue(sectionName, key);

			T returnValue = string.IsNullOrEmpty(keyValue)
				? default(T)
				: (T)Convert.ChangeType(keyValue, typeof(T));

			return returnValue;
		}

		public string GetSetting(string settingName)
		{
			return ConfigurationManager.AppSettings[settingName];
		}

		public T GetSetting<T>(string settingName)
		{
			return (T)Convert.ChangeType(ConfigurationManager.AppSettings[settingName], typeof(T));
		}
	}
}
