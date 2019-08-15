using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using Excel;
using System.Data;
using System.Text;

namespace VExtention{
	public static class IOExtention {

		[MenuItem("Tool/Test")]
		public static void Test(){
			string lua_path = Application.dataPath + "/Develop/TestIO/Test.lua";
			string excel_path = Application.dataPath + "/Develop/Test.xlsx";
			excel_path.ExcelConvert2Lua(lua_path);
			AssetDatabase.Refresh();
		}

		public static void ExcelConvert2Lua(this string path,string file_path){
			LuaLanguageMgrTemplate.Generate(path,file_path);
		}


		public static void ExcelConvert2File(this string path,string file_path){
			var ExcelRowsDic = path.GetExcelRowsDic();
			if(ExcelRowsDic.Count < 1)
				return;	
			foreach (var bigValue in ExcelRowsDic)
			{
				string fileName = bigValue.Key;
				StringBuilder sb = new StringBuilder();
				Dictionary<int,List<object>> rowDictionary = bigValue.Value;
				foreach (var rowValue in rowDictionary)
				{
					List<object> rowList = rowValue.Value;
					rowList.ForEach(
						(v)=>{
							sb.Append("|");
							sb.Append(v);
							sb.Append("|");
						}
					);
					sb.Append("\n");
				}
				if(ExcelRowsDic.Count == 1)
					file_path.WriteFile(sb);
				else
				{
					var t_Path = file_path;
					var suffix = t_Path.Substring(t_Path.LastIndexOf(".",t_Path.Length));
					var filePath = t_Path.Substring(0,t_Path.LastIndexOf("/") + 1) + fileName + suffix;
					Debug.Log(filePath);
					filePath.WriteFile(sb);
				}
			}	
		}





	#region IO && Excel
		//根据excel路径获取excel内容  [页[行[格]]]
		public static Dictionary<string,Dictionary<int,List<object>>> GetExcelRowsDic(this string path)
		{
			Dictionary<string,Dictionary<int,List<object>>> builderDic = new Dictionary<string,Dictionary<int,List<object>>>();
			using (FileStream stream = File.Open(path,FileMode.Open,FileAccess.Read)) 
			{
				using(IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream)) 
				{
					DataSet excel = excelReader.AsDataSet();
					if (excel.Tables.Count < 1)
						return builderDic;
					StringBuilder sb = new StringBuilder();
					for (int count = 0; count < excel.Tables.Count; count++) 
					{
						sb = new StringBuilder();
						DataTable mSheet = excel.Tables[count];
						if (mSheet.Rows.Count < 1)
						{
							builderDic.Add(mSheet.TableName,null);
							continue;
						}
						int rowCount = mSheet.Rows.Count;
						int colCount = mSheet.Columns.Count;
		
						Dictionary<int,List<object>> rowDic = new Dictionary<int,List<object>>();
						//读取数据
						for (int i = 0; i < rowCount; i++)
						{
							List<object> rowList = new List<object>();
							for (int j = 0; j < colCount; j++)
							{
								var content = mSheet.Rows[i][j].ToTypeValue();
								rowList.Add(content);
							}
							rowDic.Add(i,rowList);
						}
						builderDic.Add(mSheet.TableName,rowDic);
					}
				}
			}
			return builderDic;
		} 

		//检查路径是否存在文件夹，不存在则创建
		public static void CreatePathDirectory(this string path){
			var t_path = path;
			string parentPath = t_path.Substring(0,path.LastIndexOf("/"));
			if (!Directory.Exists(parentPath))
			{
				Directory.CreateDirectory(parentPath);
			}
		}

		// public static void CreateDirectory(string path){
		// 	string parentPath = path.Substring(0,path.LastIndexOf("/"));
		// 	if (!Directory.Exists(parentPath))
		// 	{
		// 		Directory.CreateDirectory(parentPath);
		// 		CreateDirectory(parentPath);
		// 	}
		// 	return;
		// }

		//文件写入
		public static void WriteFile(this string path,StringBuilder sb,bool append = false,Encoding encoding = null){
			path.CreatePathDirectory();
			if(encoding == null)
				encoding = new UTF8Encoding(false);
			StreamWriter sw = new StreamWriter(path, append, encoding);
			sw.Write(sb);
			sw.Flush();
			sw.Close();
		}



		public static object ToTypeValue(this object Value)
		{
			try
			{
				if (Value.ToString() == System.Convert.ToInt32(Value).ToString())
					return System.Convert.ToInt32(Value);
				else
					return System.Convert.ToDouble(Value);
			}
			catch
			{
				return Value.ToString();
			}
		}

		public static void FindFilesFullNameWithSuffix(string path,ref Dictionary<string,string> FullNameDic,string suffix = null)
		{
			DirectoryInfo di = new DirectoryInfo(path);
			DirectoryInfo[] subDirectory = di.GetDirectories();
			FileInfo[] fileArray = di.GetFiles();
			for (int k = 0; k < fileArray.Length; k++)
			{
				if(!FullNameDic.ContainsKey(fileArray[k].FullName))
				{
					if(suffix != null)
					{
						if(fileArray[k].FullName.EndsWith(suffix))
							FullNameDic.Add(fileArray[k].FullName,fileArray[k].Name);
					}
					else
					{
						FullNameDic.Add(fileArray[k].FullName,fileArray[k].Name);
					}	
				}
			}
			for (int i = 0; i < subDirectory.Length; i++)
			{
				FindFilesFullNameWithSuffix(subDirectory[i].FullName,ref FullNameDic,suffix);
			}
		}

	#endregion
	}
}
