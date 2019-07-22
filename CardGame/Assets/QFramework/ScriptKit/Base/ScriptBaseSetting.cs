/****************************************************************************
 * Copyright (c) 2017 magicbell
 * Copyright (c) 2018.3 ~ 7 liangxie
 * 
 * http://qframework.io
 * https://github.com/liangxiegame/QFramework
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
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
#region SettingSupprot	
		public static int LUA_BASE_TYPE = 1;
		public static string LUA_DIR = Application.dataPath + "/QFramework/Scriptkit/ToLua/Lua";                //lua逻辑代码目录
		public static string TOLUA_DIR = Application.dataPath + "/QFramework/Scriptkit/ToLua/Lua";        //tolua lua文件目录
		public static string[] ScriptPathHeads = {String.Empty,LUA_DIR};
		public static string NOW_PATH_HEAD = ScriptPathHeads[NOW_SCRIPT_TYPE] ?? String.Empty;
		public static string NOW_PATH_TAIL = ScriptPathTail ?? String.Empty;
#endregion

		public static string GetScriptPath(int scriptType){
			if(ScriptPathHeads[scriptType] == null)
				return ScriptPathTail;
			return ScriptPathHeads[scriptType] + ScriptPathTail;
		}
	}
}
