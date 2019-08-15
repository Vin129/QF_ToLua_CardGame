using UnityEngine;
using UnityEditor;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;

using MySql.Data.MySqlClient;

using Newtonsoft.Json;
using TableConfig = MySqlSettings.TableConfig;

public class MySqlTools : EditorWindow
{
	private static MySqlTools instance;
	private static string pathRoot;

	private static MySqlUtility mySql;//连接类对象  

	private static List<TableConfig> databaseList;

	private static Vector2 scrollPos;

	[MenuItem("Tools/MySql2Json")]
	static void ShowMySqlTools()
	{
		Init();

		instance.Show ();
	}

	private static string host = "";  
	private static string port = "";  
	private static string acc = "";  
	private static string pwd = "";  
	private static string db = "";

	private bool isConnect = false;
	private bool isConverting = false;
	private bool isFinished = false;

	private static void Init()
	{
		host = MySqlSettings.HOST;
		port = MySqlSettings.PORT;
		acc = MySqlSettings.ACCOUNT;
		pwd = MySqlSettings.PASSWORD;
		db = MySqlSettings.DATABASE;

		//获取当前实例
		instance = EditorWindow.GetWindow<MySqlTools>();
		//初始化
		pathRoot = Application.dataPath;

		scrollPos = new Vector2(instance.position.x, instance.position.y + 75);
	} 

	void OnGUI()
	{
		host = EditorGUILayout.TextField ("host", host);
		port = EditorGUILayout.TextField ("port", port);
		acc = EditorGUILayout.TextField ("acc", acc);
		pwd = EditorGUILayout.TextField ("pwd", pwd);
		db = EditorGUILayout.TextField ("db", db);

		if (isConnect == false) {
			if (GUILayout.Button ("连接数据库")) {
				connectDB ();
			}
		} else {
			if (isFinished == false) {
				if (databaseList == null) return;
				if (databaseList.Count < 1)
				{
					EditorGUILayout.LabelField("没有需要导出的数据表");
				}
				else
				{
					EditorGUILayout.LabelField("下列数据表将被转换为json:");
					GUILayout.BeginVertical();
					scrollPos = GUILayout.BeginScrollView(scrollPos, false, true, GUILayout.Height(150));
					foreach (TableConfig lc in databaseList)
					{
						GUILayout.BeginHorizontal();
						GUILayout.Label(lc.GetTableName ());
						GUILayout.EndHorizontal();
					}
					GUILayout.EndScrollView();
					GUILayout.EndVertical();

					//输出
					if (isConverting == false && GUILayout.Button ("数据库已连接，点击导出数据")) {
						convertDB ();
					}
				}
			} else {
				EditorGUILayout.LabelField("导出数据成功！");
			}

		}
	}

	private void connectDB(){
		if(isConnect == false){
			string sqlString = string.Format("Database={0};Data Source={1};User Id={2};Password={3};port={4}", db, host, acc, pwd, port); 
			mySql = new MySqlUtility(sqlString);
			isConnect = true;

			LoadDB ();
		}
	}

	private void LoadDB()
	{
		if (databaseList == null) databaseList = new List<TableConfig>();
		databaseList.Clear();

		TableConfig[] cfgList = MySqlSettings.tableList;


		if (cfgList.Length == 0)
			return;

		foreach (TableConfig lc in cfgList)
		{
			databaseList.Add (lc);
		}
	}

	private void convertDB(){
		isConverting = true;

		TableConfig[] tableList = MySqlSettings.tableList;
		foreach (TableConfig tc in tableList) {
			string tableName = tc.GetTableName();	
			string[] items = tc.GetItems();
			string[] cols = tc.GetCols();
			string[] operation = tc.GetOperation();
			string[] values = tc.GetValues();
			string[] keys = tc.GetKeys();
			bool isArray = tc.IsArray();

			string JsonPath = pathRoot + MySqlSettings.OUTPUT_PATH+tableName+MySqlSettings.OUTPUT_TYPE;
			FileUtil.DeleteFileOrDirectory(JsonPath);

			DataSet ds = mySql.SelectWhere(tableName,items,cols,operation,values);
			if(ds != null)
			{
				if(isArray){
					convert2List (JsonPath, ds, keys);
				}else{
					convert2Map (JsonPath, ds, keys);
				}
			}
		}
		isFinished = true;
		isConverting = false;
		mySql.Close ();
		mySql = null;

		AssetDatabase.Refresh();

		instance.Close();
	}

	private void convert2Map(string JsonPath, DataSet ds, string[] keys){
		Dictionary<string, object> tableDic = new Dictionary<string, object>();

		DataTable table = ds.Tables[0];
		foreach (DataRow row in table.Rows)
		{
			if (string.IsNullOrEmpty (keys [0])) {
				Dictionary<string, object> rowDic = new Dictionary<string, object>();
				foreach (DataColumn column in table.Columns)
				{
					string colKey = column.ToString();
					rowDic.Add (colKey, row [column]);
				}

				string rowKey = row [0].ToString ();
				//添加到表数据中
				tableDic.Add (rowKey, rowDic);
			} else {
				Dictionary<string, object> tempDic = tableDic;
				for (int i = 0; i < keys.Length; i++) {
					string key = keys [i];
					string tempDicKey = row [key].ToString();
					if (tempDic.ContainsKey (tempDicKey)) {
						tempDic = (Dictionary<string, object>)tempDic [tempDicKey];
					}else {
						Dictionary<string, object> dic = new Dictionary<string, object>();
						tempDic.Add (tempDicKey, dic);
						tempDic = dic;
					}
				}

				foreach (DataColumn column in table.Columns)
				{				
					string colKey = column.ToString();
					tempDic.Add (colKey, row [column]);
				}
			}
		}

		//生成Json字符串
		if(tableDic.Count > 0){
			Encoding encoding = Encoding.GetEncoding("utf-8");
			string json = JsonConvert.SerializeObject(tableDic, Newtonsoft.Json.Formatting.None);
			//写入文件
			using (FileStream fileStream = new FileStream(JsonPath, FileMode.Create, FileAccess.Write))
			{
				using (TextWriter textWriter = new StreamWriter(fileStream, encoding))
				{
					textWriter.Write(json);
				}
			}
		}
	}

	private void convert2List(string JsonPath, DataSet ds, string[] keys){
		//List 模式不支持 keys
		List<object> tableDic = new List<object>();

		DataTable table = ds.Tables[0];
		foreach (DataRow row in table.Rows)
		{
			//if (string.IsNullOrEmpty (keys [0])) {
				Dictionary<string, object> rowDic = new Dictionary<string, object>();
				foreach (DataColumn column in table.Columns)
				{
					string colKey = column.ToString();
					rowDic.Add (colKey, row [column]);
				}

				//string rowKey = row [0].ToString ();
				//添加到表数据中
				tableDic.Add(rowDic);
			//} else {
				/*Dictionary<string, object> tempDic = new Dictionary<string, object>();
				for (int i = 0; i < keys.Length; i++) {
					string key = keys [i];
					string tempDicKey = row [key].ToString();
					if (tempDic.ContainsKey (tempDicKey)) {
						tempDic = (Dictionary<string, object>)tempDic [tempDicKey];
					}else {
						Dictionary<string, object> dic = new Dictionary<string, object>();
						tempDic.Add (tempDicKey, dic);
						tempDic = dic;
					}
				}

				foreach (DataColumn column in table.Columns)
				{				
					string colKey = column.ToString();
					tempDic.Add (colKey, row [column]);
				}*/
			//}
		}

		//生成Json字符串
		if(tableDic.Count > 0){
			Encoding encoding = Encoding.GetEncoding("utf-8");
			string json = JsonConvert.SerializeObject(tableDic, Newtonsoft.Json.Formatting.None);
			//写入文件
			using (FileStream fileStream = new FileStream(JsonPath, FileMode.Create, FileAccess.Write))
			{
				using (TextWriter textWriter = new StreamWriter(fileStream, encoding))
				{
					textWriter.Write(json);
				}
			}
		}
	}
}


