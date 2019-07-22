namespace ScriptKit
{
    using UnityEngine;
    using UnityEditor;
    public class ScriptKitEditorWindow:EditorWindow
    {


        private static ScriptKitEditorWindow instance;
        private static Contents contents;

        private GUIStyle guiStyle;

        private Vector2 scrollPos;
        private string scriptPathHead;
        private string scriptPathTail;
        private string scriptPath;
  
        [MenuItem("ScriptKit/Setting")]
        public static void ShowWindow()
        {
            instance = EditorWindow.GetWindow<ScriptKitEditorWindow>();
            instance.titleContent = new GUIContent("ScriptKit" + ScriptBaseSetting.VERSIONS);
            instance.Init();
            instance.Show();
        }

        public void Init(){
            contents = new Contents();
            guiStyle = new GUIStyle();
            scriptPath = EditorPrefs.GetString(ScriptBaseSetting.SCRIPT_PATH_KEY,string.Empty);
            scriptPathHead = ScriptBaseSetting.NOW_PATH_HEAD;
            scriptPathTail = ScriptBaseSetting.NOW_PATH_TAIL;
            if(scriptPath.Equals(string.Empty))
                scriptPath = scriptPathHead + scriptPathTail;
            EditorPrefs.SetString(ScriptBaseSetting.SCRIPT_PATH_KEY,scriptPath);
        }

        public void OnGUI(){
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
			GUILayout.BeginVertical();
            GUILayout.Label("Path:" + scriptPath);
            GUILayout.BeginHorizontal();
			GUILayout.Label(contents.ScriptPathHeadContent);
            GUILayout.TextField(scriptPathHead);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label(contents.ScriptPathTailConten);
            scriptPathTail = GUILayout.TextField(scriptPathTail);
            GUILayout.EndHorizontal();
	        if (GUILayout.Button("Clear Setting"))
			{
				EditorPrefs.DeleteKey(ScriptBaseSetting.SCRIPT_PATH_KEY);
			}
            if (GUILayout.Button("Save Setting"))
			{
                scriptPath = scriptPathHead + scriptPathTail;
				EditorPrefs.SetString(ScriptBaseSetting.SCRIPT_PATH_KEY,scriptPath);
			}

            GUILayout.EndVertical();
            GUILayout.EndScrollView();
        }

        private void OnEnable()
		{
			
        }

        private void OnDisable(){
            scriptPath = scriptPathHead + scriptPathTail;
            EditorPrefs.SetString(ScriptBaseSetting.SCRIPT_PATH_KEY,scriptPath);
        }

         private class Contents
		{
			public readonly GUIContent ScriptPathHeadContent = new GUIContent("ScriptPathHead:",
				"Please don't modify it without authorization");
            public readonly GUIContent ScriptPathTailConten  = new GUIContent("ScriptPathTail:",
            "Path = ScriptPathHead + ScriptPathTail ");
		}
    }
}