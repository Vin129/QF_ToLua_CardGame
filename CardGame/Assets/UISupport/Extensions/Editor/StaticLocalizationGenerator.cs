using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public static class StaticLocalizationGenerator {

    public static SortedDictionary<string, string> ConfigContent = new SortedDictionary<string, string>();
    public static SortedDictionary<string, string> TextContent = new SortedDictionary<string, string>();

    public static string PrefabPath = Application.dataPath + "/Resources/Prefabs";
    public static string ConfigOutputPath = "/Develop/Lua/Localization/StaticLocalizationConfig.lua";
    public static string PrefabTextPath = "/PrefabText.txt";

    [MenuItem("Custom Editor/Localization/GenerateStaticConfig")]
	public static void GenerateStaticConfig()
    {
        ReadConfigFile();
        GenerateStaticDirectory(PrefabPath);
        GenerateConfigFile();
        ConfigContent.Clear();
    }

    public static void GenerateStaticDirectory(string path)
    {
        DirectoryInfo di = new DirectoryInfo(path);
        DirectoryInfo[] subDirectory = di.GetDirectories();
        FileInfo[] fileArray = di.GetFiles();
        for (int k = 0; k < fileArray.Length; k++)
        {
            Transform trans = AssetDatabase.LoadAssetAtPath<Transform>(fileArray[k].FullName.Substring(fileArray[k].FullName.IndexOf("Assets")));

            if (trans != null)
            {
                Text[] children = trans.GetComponentsInChildren<Text>(true);
                string rootName = trans.name;
                if (!ConfigContent.ContainsKey(rootName) && children.Length > 0)
                {
                    StringBuilder luaTableStr = new StringBuilder();
                    StringBuilder TextContentStr = new StringBuilder();
                    luaTableStr.Append(rootName + " = {\n");
                    for (int i = 0; i < children.Length; i++)
                    {
                        string textPath = children[i].transform.name;
                        Transform parent = children[i].transform.parent;
                        while (parent != null)
                        {
                            textPath = parent.name + "/" + textPath;
                            parent = parent.parent;
                        }
                        int firstIndex = textPath.IndexOf("/");
                        if (firstIndex != -1)
                            textPath = textPath.Substring(firstIndex + 1, textPath.Length - firstIndex - 1);
                        else
                            textPath = "";
                        string text = children[i].text.Replace("\n", "\\n").Replace("\r", "").Replace("\"", "\\\"");
                        bool checkRes = HasChinese(text);
                        if (!"".Equals(text) && checkRes)
                            TextContentStr.Append("\t[\"" + textPath + "\"] = \"" + text + "\",\n");
                    }
                    if (TextContentStr.Length > 0)
                    {
                        luaTableStr.Append(TextContentStr);
                        luaTableStr.Append("}\n");
                        ConfigContent.Add(rootName, luaTableStr.ToString());
                    }
                }
            }
        }
        for (int i = 0; i < subDirectory.Length; i++)
        {
            GenerateStaticDirectory(subDirectory[i].FullName);
        }
    }

    public static void GenerateTextDirectory(string path)
    {
        DirectoryInfo di = new DirectoryInfo(path);
        DirectoryInfo[] subDirectory = di.GetDirectories();
        FileInfo[] fileArray = di.GetFiles();
        for (int k = 0; k < fileArray.Length; k++)
        {
            Transform trans = AssetDatabase.LoadAssetAtPath<Transform>(fileArray[k].FullName.Substring(fileArray[k].FullName.IndexOf("Assets")));
            if (trans != null)
            {
                 EditorUtility.DisplayProgressBar("", "Checking Prefab " + trans.name, 0);
                Text[] children = trans.GetComponentsInChildren<Text>(true);
                for (int i = 0; i < children.Length; i++)
                {
                    if (!TextContent.ContainsKey(children[i].text) && HasChinese(children[i].text))
                    {
                        string text = children[i].text.Replace("\n", "\\n").Replace("\r", "");
                        TextContent.Add(children[i].text, text);
                    }
                }
            }
        }
        for (int i = 0; i < subDirectory.Length; i++)
        {
            GenerateTextDirectory(subDirectory[i].FullName);
        }
    }

    public static void ReadConfigFile()
    {
        FileStream fileStream = File.Open(Application.dataPath + ConfigOutputPath, FileMode.OpenOrCreate);
        StreamReader streamReader = new StreamReader(fileStream);
        string key = null;
        StringBuilder value = new StringBuilder();
        while (!streamReader.EndOfStream)
        {
            string strLine = streamReader.ReadLine();
            if (strLine.Length > 0)
            {
                value.Append(strLine + "\n");
                string firstChar = strLine.Trim().Substring(0, 1);
                if (strLine.Contains("Conment Begin"))
                {
                    key = "BeginComment";
                }
                else if (strLine.Contains("Conment End"))
                {
                    ConfigContent.Add(key, value.ToString());
                    key = null;
                    value = new StringBuilder();
                }
                else if (firstChar.Equals("}"))
                {
                    ConfigContent.Add(key, value.ToString());
                    key = null;
                    value = new StringBuilder();
                }
                else if (!firstChar.Equals("[") && !firstChar.Equals("-"))
                {
                    key = strLine.Substring(0, strLine.IndexOf(" "));
                }
            }
            
        }
        streamReader.Dispose();
        fileStream.Close();
        streamReader.Close();
    }

    public static void GenerateConfigFile()
    {
        
        FileStream fileStream = File.Create(Application.dataPath + ConfigOutputPath);
        StreamWriter streamWriter = new StreamWriter(fileStream);
        SortedDictionary<string, string>.Enumerator enumerator = ConfigContent.GetEnumerator();
        string conment = "";
        ConfigContent.TryGetValue("BeginComment", out conment);
        streamWriter.Write(conment);

        while (enumerator.MoveNext())
        {
            if (!"BeginComment".Equals(enumerator.Current.Key)) 
                streamWriter.Write(enumerator.Current.Value);
        }
        streamWriter.Dispose();
        fileStream.Close();
        streamWriter.Close();
        Debug.Log("静态语言配置生成结束！");
    }

    public static void GenerateTextFile()
    {
        FileStream fileStream = File.Create(Application.dataPath + PrefabTextPath);
        StreamWriter streamWriter = new StreamWriter(fileStream);
        SortedDictionary<string, string>.Enumerator enumerator = TextContent.GetEnumerator();

        while (enumerator.MoveNext())
        {
            streamWriter.Write(enumerator.Current.Value);
        }
        streamWriter.Dispose();
        fileStream.Close();
        streamWriter.Close();
        Debug.Log("文件生成结束！");
    }

    public static bool HasChinese(string str)
    {
        return Regex.IsMatch(str, @"[\u4e00-\u9fa5]");
    }
}
