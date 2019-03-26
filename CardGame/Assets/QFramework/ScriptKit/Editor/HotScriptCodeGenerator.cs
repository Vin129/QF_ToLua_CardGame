namespace QFramework{
	using UnityEngine;
	using UnityEditor;
    using System.Text.RegularExpressions;

    public class HotScriptCodeGenerator{
		private const int mLua = 1;
		private static ScriptKitCodeBind mScriptCodeBind = (int type,GameObject uiPrefab,string filePath)=>{addScriptComponent(type,uiPrefab,filePath);};

		[MenuItem("Assets/@Script Kit - Create HotScriptCode")]
		public static void CreateHotScriptCode(){
			Debug.Log("<color=#EE6A50> >>>>>>>Create HotScriptCode  </color>");
			UICodeGenerator.CreateHotScriptCode(ScriptBaseSetting.ScriptType,templates:getTemplates(), mCodeBind: mScriptCodeBind);
			
		}

		private static IBaseTemplate[] getTemplates()
		{
			if(ScriptBaseSetting.ScriptType == mLua){
				return new IBaseTemplate[3]{new LuaUIPanelCodeTemplate() as IBaseTemplate,new LuaPanelComponentsCodeTemplate() as IBaseTemplate,new LuaPanelTemplate() as IBaseTemplate};;
			}
			return null;
		}

		private static void addScriptComponent(int type,GameObject uiPrefab,string filePath){
			if(type == mLua){
				var lc = uiPrefab.GetComponent<LuaComponent>();
				var uiLuaPanel =  uiPrefab.GetComponent<UILuaPanel>();
				if(lc.IsNull()){
					lc = uiPrefab.AddComponent<LuaComponent>();
					var newPath = filePath;
					var resultString = Regex.Split(newPath, "/Lua/", RegexOptions.IgnoreCase);
					newPath = resultString[1];
					newPath = newPath.Replace(".lua", "");
					newPath = newPath.Replace("/", ".");
					lc.LuaPath = newPath;
					lc.LuaFilePath = filePath;
				}	
			}
			Debug.Log("<color=#EE6A50> >>>>>>>Success HotScriptCode </color>");
		} 
	}
}
