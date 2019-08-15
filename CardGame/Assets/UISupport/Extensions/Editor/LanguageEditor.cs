using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using UnityEditor;
using System.Text.RegularExpressions;

public class LanguageEditor{
	public static string FilePath = Application.dataPath + "/Develop/Lua/Logic/MainTown";
	public static string SavePath = Application.dataPath + "/Localization/";

	public static string KeyFileName = "KeyForLanguageCfg.txt";

	[MenuItem("Tool/LanguageBuild")]
	public static void LanguageBuild()
	{
		List<string> textList = new List<string>();
		List<string> changeList = new List<string>();
		string path = string.Empty;
		string filePath = FilePath;

		Dictionary<string,string> FullNameDic = new Dictionary<string,string>();
		FindAllFilesFullNameWithSuffix(filePath,ref FullNameDic,".lua");
		foreach (var data in FullNameDic)
		{
			CheckTargetFile(data.Key,data.Value,ref textList,ref changeList);
		}

		List<string> allTextList = new List<string>();
		List<string> oldTextList = new List<string>();
		List<string> newTextList = new List<string>();
		var newPath = SavePath;
		if (!Directory.Exists(newPath))
		{
			Directory.CreateDirectory(newPath);
		}
		newPath = newPath + "/" + KeyFileName;
		if(File.Exists(newPath)){
			var sr = new StreamReader(newPath);
			while(!sr.EndOfStream){
				var lineConent = sr.ReadLine();
				oldTextList.Add(lineConent);
			}
			sr.Close();
		}


		textList.ForEach(
			(v)=>{
				if(!allTextList.Contains(v) && Regex.IsMatch(v, @"[\u4e00-\u9fa5]") && v.Length > 0 )
					allTextList.Add(v);
				if(!oldTextList.Contains(v) && Regex.IsMatch(v, @"[\u4e00-\u9fa5]") && v.Length > 0)
					newTextList.Add(v);
			}
		);
		changeList.ForEach(
			(v)=>{
				if(!allTextList.Contains(v) && Regex.IsMatch(v, @"[\u4e00-\u9fa5]") && v.Length > 0 )
					allTextList.Add(v);
				if(!oldTextList.Contains(v) && Regex.IsMatch(v, @"[\u4e00-\u9fa5]") && v.Length > 0)
					newTextList.Add(v);
			}
		);
		StaticLocalizationGenerator.GenerateTextDirectory(StaticLocalizationGenerator.PrefabPath);
		foreach (var text in StaticLocalizationGenerator.TextContent)
		{	
			if(!allTextList.Contains(text.Value) && Regex.IsMatch(text.Value, @"[\u4e00-\u9fa5]") && text.Value.Length > 0 )
					allTextList.Add(text.Value);
			if(!oldTextList.Contains(text.Value) && Regex.IsMatch(text.Value, @"[\u4e00-\u9fa5]")  && text.Value.Length > 0)
					newTextList.Add(text.Value);
		}
		
		var sw = new StreamWriter(newPath, false, new UTF8Encoding(false));
		var strBuilder = new StringBuilder();

		oldTextList.ForEach((line)=>{
			if(allTextList.Contains(line))
				strBuilder.AppendLine(line);
			else
				strBuilder.AppendLine();
		});

		newTextList.ForEach((line)=>{
			strBuilder.AppendLine(line);
		});
		sw.Write(strBuilder);
		sw.Flush();
		sw.Close();
		AssetDatabase.Refresh();
		EditorUtility.ClearProgressBar();

	}
	public static void CheckTargetFile(string path,string fileName,ref List<string> textList,ref List<string> changeList){
		StreamReader sr = new StreamReader(path);
		var strBuilder = new StringBuilder();
		float nowLine = 0;
		float maxLenght = (float)sr.BaseStream.Length;
		EditorUtility.DisplayProgressBar("", "Checking " + fileName, 0);
		bool needWrite = false;
		while(!sr.EndOfStream){
			nowLine++;
			bool needAdd = false;
			EditorUtility.DisplayProgressBar("", "Checking " + fileName, (float) (nowLine) / maxLenght);
			var lineConent = sr.ReadLine();
			var tempConent = lineConent;
			if(lineConent.Contains("UIUtil:SetLabelText") || lineConent.Contains("self:write")){
				var textStartIndex = lineConent.IndexOf("\"");
				var textEndIndex = lineConent.LastIndexOf("\"");
				var textLenght = textEndIndex - textStartIndex;
				if(textLenght > 0){
					var line = lineConent.Substring(textStartIndex + 1,textEndIndex - textStartIndex - 1);
					if(!lineConent.Contains("string.format") && !lineConent.Contains("..") && !lineConent.Contains("LuaHelper.FindChild") && !lineConent.Contains("UIUtil:LanFormat"))
					{
						textList.Add(line);
					}
					else if(tempConent.Contains("UIUtil:LanFormat")){
						var sIndex = tempConent.IndexOf("UIUtil:LanFormat(\"");
						var eIndex = tempConent.LastIndexOf(")");
						if(eIndex - sIndex > 0)
						{
							var c = tempConent.Substring(sIndex,eIndex - sIndex);
							c = c.Replace("UIUtil:LanFormat(\"","");
							var newLine = c.Substring(0,c.IndexOf("\""));
							textList.Add(newLine);
						}
					}
				}
			}
			if(lineConent.Contains("UIUtil:SetLabelText") || lineConent.Contains("self:write")){
				var newlineConent = lineConent;
				var startIndex = lineConent.IndexOf(',') + 1;
				var endIndex = lineConent.LastIndexOf(')');
				var lenght = endIndex - startIndex;
				if(lenght > 0){
					var line = lineConent.Substring(startIndex,lenght);
					var formatKey = string.Empty;
					if(lineConent.Contains("..") && !lineConent.Contains("color=#\"") && !lineConent.Contains("LuaHelper.FindChild") && !lineConent.Contains("UIUtil:LanFormat"))
					{
						var templine = line.Replace("..","@");
						var newStrings = templine.Split('@');
						var reBuildStr = "UIUtil:LanFormat(\"";
						formatKey = string.Empty;
						if(newStrings.Length > 0)
						{
							List<string> formatValueList = new List<string>();
							for(int i = 0;i < newStrings.Length;i++){
								newStrings[i] = newStrings[i].TrimStart();
								newStrings[i] = newStrings[i].TrimEnd();
								if(newStrings[i].StartsWith("\"") && newStrings[i].EndsWith("\"")){
									newStrings[i] = newStrings[i].Replace("\"",string.Empty); 	
								}
								else
								{
									formatValueList.Add(newStrings[i]);
									newStrings[i] = "%s";
								}
								formatKey += newStrings[i];
								reBuildStr += newStrings[i];
							}
							reBuildStr += "\",";
							for(int k = 0;k < formatValueList.Count;k++)
							{
								if(k==formatValueList.Count - 1)
									reBuildStr += formatValueList[k] + ")";
								else
									reBuildStr += formatValueList[k] + ",";
							}
							newlineConent = newlineConent.Replace(line,reBuildStr);
							if(newlineConent.Contains(".."))
								needAdd = false;
							else
								needAdd = true;
						}
						if(needAdd){
							needWrite = true;
							if(!changeList.Contains(formatKey))
								changeList.Add(formatKey);
							lineConent = newlineConent;
						}
					}
				}
			}
			strBuilder.AppendLine(lineConent);
		}
		sr.Close();
		if(needWrite){
			StreamWriter sw = new StreamWriter(path, false,new UTF8Encoding(false));
			sw.Write(strBuilder);
			sw.Flush();
			sw.Close();
		}
	}



	public static void FindAllFilesFullNameWithSuffix(string path,ref Dictionary<string,string> FullNameDic,string suffix = null)
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
            FindAllFilesFullNameWithSuffix(subDirectory[i].FullName,ref FullNameDic,suffix);
        }
    }
}
