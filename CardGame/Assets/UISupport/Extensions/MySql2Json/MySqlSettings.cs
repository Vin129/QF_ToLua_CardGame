using System;

public static class MySqlSettings
{
	public static string HOST = "139.199.205.193";  
	public static string PORT = "3306";  
	public static string ACCOUNT = "fifa";  
	public static string PASSWORD = "91fifa";  
	public static string DATABASE = "common";
	public static string OUTPUT_PATH = "/Resources/Tables/Common/";
	public static string OUTPUT_TYPE = ".json";

	public static TableConfig[] tableList = {
		//_TC("common_player_data","*","status","=","1","player_id"),
        _TC("common_player_data","*","status","=","1","",false),
        _TC("common_player_formation","*","status","=","1","",true),
        _TC("common_player_level","*","status","=","1","",true),
        _TC("common_player_star","*","status","=","1","star_level",false),
        _TC("common_player_position","*","status","=","1","",false),
        _TC("common_player_type","*","status","=","1","",false),
        _TC("common_player_relation_detail","*","status","=","1","relation_id,relation_level",false),
        _TC("common_player_relation_info","*","status","=","1","player_id,id",false),
        _TC("common_formation_position","*","status","=","1","",false),
		_TC("common_player_grow_base","*","status","=","1","type,star",false),
		_TC("common_debris_playerInfo","*","status","=","1","player_id",false),
        _TC("common_debris_equipInfo","*","status","=","1","",false),
        _TC("common_team_club","*","status","=","1","icon",false),
		_TC("common_property","*","status","=","1","",false),
        _TC("common_equipment_info","*","status","=","1","",false),
		_TC("common_equipment_uplevel","*","status","=","1","level",false),
  		_TC("common_equipment_upstar","*","status","=","1","",false),
   		_TC("common_debris_equipInfo","*","status","=","1","",false),
   		_TC("common_username","*","status","=","1","",true),

 };

	static TableConfig _TC(string tableName, string itemsStr, string colsStr,string operationStr, string valuesStr,string keysStr)
	{
		return new TableConfig(tableName,itemsStr,colsStr,operationStr,valuesStr,keysStr,false);
	}

	static TableConfig _TC(string tableName, string itemsStr, string colsStr,string operationStr, string valuesStr,string keysStr,bool isArray)
	{
		return new TableConfig(tableName,itemsStr,colsStr,operationStr,valuesStr,keysStr,isArray);
	}

	public class TableConfig
	{
		//导出表配置
		private string m_tableName;		//表名
		//string itemsStr;		//需要导出字段，如果是所有为 *
		//string colsStr;			//where 条件字段
		//string operationStr;	//对应where 条件字段
		//string valuesStr;		//对应where 条件字段

		private string[] m_items;			//需要导出字段，如果是所有为 *
		private string[] m_cols;			//where 条件字段
		private string[] m_operation;		//对应where 条件字段
		private string[] m_values;		//对应where 条件字段

		//导出json配置字段名，如果不配置使用表第一个字段
		private string[] m_keys;
		private bool m_isArray;

		public TableConfig(string tableName, string itemsStr, string colsStr,string operationStr, string valuesStr,string keysStr,bool isArray){
			m_tableName = tableName;

			m_items = itemsStr.Split(',');
			m_cols = colsStr.Split(',');
			m_operation = operationStr.Split(',');
			m_values = valuesStr.Split(',');
			m_keys = keysStr.Split(',');
			m_isArray = isArray;
		}

		public string GetTableName(){
			return m_tableName;
		}

		public string[] GetItems(){
			return m_items;
		}

		public string[] GetCols(){
			return m_cols;
		}

		public string[] GetOperation(){
			return m_operation;
		}

		public string[] GetValues(){
			return m_values;
		}

		public string[] GetKeys(){
			return m_keys;
		}

		public bool IsArray(){
			return m_isArray;
		}
	}
}