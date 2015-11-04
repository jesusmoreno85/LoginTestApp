using System.Collections.Specialized;

namespace LoginTestApp.Crosscutting.Contracts
{
	public interface IConfigurationProvider
	{
		StringDictionary GetSectionValues(string sectionName);
		string GetSectionKeyValue(string sectionName, string key);
		T GetSectionKeyValue<T>(string sectionName, string key);
		string GetSetting(string settingName);
		T GetSetting<T>(string settingName);
	}
}