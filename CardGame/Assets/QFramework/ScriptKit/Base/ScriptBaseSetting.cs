/****************************************************************************
 * Copyright (c) 2019 vin129
 ****************************************************************************/

using System;
using System.IO;
using UnityEngine;

namespace ScriptKit {
#if UNITY_EDITOR
	using UnityEditor;
#endif
	[Serializable]
	public class ScriptBaseSetting
	{
		// TODO:都将支持窗口设置
		public static string VERSIONS = "0.1.1";

		public static string SETTING_DATA_PATH = Application.dataPath + "/ScriptKitData/BaseSetting.json";
#region  EditorKey 
		public static string SCRIPT_PATH_KEY = "SCRIPT_PATH";
#endregion

#if UNITY_EDITOR
		public static string ScriptPathTail = "/Game/UI";
#else 
		public static string ScriptPathTail = "/Game/UI";
#endif
		// public static Dictionary<string,string> ;
		public static int NOW_SCRIPT_TYPE = 1;	
#region ToLua	
		public static int LUA_BASE_TYPE = 1;
		public static string LUA_DIR = Application.dataPath + "/QFramework/Scriptkit/ToLua/Lua";                //lua逻辑代码目录
		public static string TOLUA_DIR = Application.dataPath + "/QFramework/Scriptkit/ToLua/Lua";        //tolua lua文件目录
#endregion
#region SettingSupprot	
		public static string[] ScriptPathHeads = {String.Empty,LUA_DIR};
		public static string NOW_PATH_HEAD = ScriptPathHeads[NOW_SCRIPT_TYPE] ?? String.Empty;
		public static string NOW_PATH_TAIL = ScriptPathTail ?? String.Empty;
#endregion

		public static string GetScriptPath(int scriptType){
			if(ScriptPathHeads[scriptType] == null)
				return ScriptPathTail;
			return ScriptPathHeads[scriptType] + ScriptPathTail;
		}

		public static string GetHotScriptName(){
			switch(NOW_SCRIPT_TYPE){
				case 1:
					return "ToLua";
				break;
				default:
					return "无";
				break;
			}
		}
	}
}
