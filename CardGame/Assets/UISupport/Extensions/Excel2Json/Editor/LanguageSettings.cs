using System;

public static class LanguageSettings
{
	public static string INPUT_PATH = "/Develop/Art/Language/";
	public static string OUTPUT_PATH = "/Resources/Localization/";
	public static string OUTPUT_TYPE = ".txt";

	public static LanguageConfig[] tableList = {
		_LC("language_all.xlsx","common",""),
		_LC("language_all_ui.xlsx","ui",""),
		_LC("language_country_data.xlsx","country",""),
		_LC("language_nationality_data.xlsx","nationality",""),
		_LC("language_player_data.xlsx","player","name,desc"),
		_LC("language_prop_data.xlsx","property","name,desc"),
	};

	static LanguageConfig _LC(string tableName, string moduleStr, string keysStr)
	{
		return new LanguageConfig(tableName,moduleStr,keysStr);
	}

	public class LanguageConfig
	{
		//导出表配置
		private string m_tableName;		//表名

		private string m_module;		//模块名
		private string[] m_keys;		//字段名

	
		public LanguageConfig(string tableName, string module, string keysStr){
			m_tableName = tableName;
			m_module = module;

			//if(!string.IsNullOrEmpty(keysStr))
				m_keys = keysStr.Split(',');
		}

		public string GetTableName(){
			return m_tableName;
		}

		public string GetModule(){
			return m_module;
		}

		public string[] GetKeys(){
			return m_keys;
		}
	}
}
