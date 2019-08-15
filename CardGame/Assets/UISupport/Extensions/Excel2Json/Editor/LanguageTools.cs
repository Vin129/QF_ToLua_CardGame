using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

using LanguageConfig = LanguageSettings.LanguageConfig;

public class LanguageTools : EditorWindow
{
    /// <summary>
    /// 当前编辑器窗口实例
    /// </summary>
    private static LanguageTools instance;

    /// <summary>
    /// Excel文件列表
    /// </summary>
	private static List<LanguageConfig> excelList;

    /// <summary>
    /// 项目根路径	
    /// </summary>
    private static string pathRoot;

    /// <summary>
    /// 滚动窗口初始位置
    /// </summary>
    private static Vector2 scrollPos;

    /// <summary>
    /// 是否保留原始文件
    /// </summary>
    private static bool keepSource = true;

    /// <summary>
    /// 显示当前窗口	
    /// </summary>
    //[MenuItem("Tools/LanguageTools")]
    static void ShowLanguageTools()
    {
        Init();
        //加载Excel文件
        LoadExcel();
        instance.Show();
    }

    void OnGUI()
    {
        DrawOptions();
        DrawExport();
    }

    /// <summary>
    /// 绘制插件界面配置项
    /// </summary>
    private void DrawOptions()
    {
        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("格式类型:", GUILayout.Width(85));
		EditorGUILayout.LabelField("TXT", GUILayout.Width(85));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("编码类型:", GUILayout.Width(85));
		EditorGUILayout.LabelField("UTF-8", GUILayout.Width(85));
        GUILayout.EndHorizontal();

        keepSource = GUILayout.Toggle(keepSource, "保留Excel源文件");
    }

    /// <summary>
    /// 绘制插件界面输出项
    /// </summary>
    private void DrawExport()
    {
        if (excelList == null) return;
        if (excelList.Count < 1)
        {
            EditorGUILayout.LabelField("目前没有Excel文件被选中哦!");
        }
        else
        {
            EditorGUILayout.LabelField("下列项目将被转换为TXT:");
            GUILayout.BeginVertical();
            scrollPos = GUILayout.BeginScrollView(scrollPos, false, true, GUILayout.Height(150));
			foreach (LanguageConfig lc in excelList)
            {
                GUILayout.BeginHorizontal();
				GUILayout.Label(LanguageSettings.INPUT_PATH+ lc.GetTableName ());
                GUILayout.EndHorizontal();
            }
            GUILayout.EndScrollView();
            GUILayout.EndVertical();

            //输出
            if (GUILayout.Button("转换"))
            {
                Convert();
            }
        }
    }

    /// <summary>
    /// 转换Excel文件
    /// </summary>
    private static void Convert()
    {
		//删除旧的文件
		//输出类型
		string outpath = Application.dataPath + LanguageSettings.OUTPUT_PATH;
		if (Directory.Exists (outpath)) {
			DirectoryInfo direction = new DirectoryInfo (outpath);
			FileInfo[] files = direction.GetFiles ("*",SearchOption.AllDirectories);

			for (int i = 0; i < files.Length; i++) {
				if (files [i].Name.EndsWith (".txt")) {
					FileUtil.DeleteFileOrDirectory (files [i].ToString());
				}
			}
		}
			

		foreach (LanguageConfig lc in excelList)
        {
            //获取Excel文件的绝对路径
			string excelPath = pathRoot +LanguageSettings.INPUT_PATH+ lc.GetTableName();
            //构造Excel工具类
            ExcelUtility excel = new ExcelUtility(excelPath);
            //编码类型
			Encoding encoding = Encoding.GetEncoding("utf-8");


			excel.ConvertToTXT(outpath, encoding,lc);

            //判断是否保留源文件
            if (!keepSource)
            {
                FileUtil.DeleteFileOrDirectory(excelPath);
            }

            //刷新本地资源
            AssetDatabase.Refresh();
        }

        //转换完后关闭插件
        //这样做是为了解决窗口
        //再次点击时路径错误的Bug
        instance.Close();

    }

    /// <summary>
    /// 加载Excel
    /// </summary>
    private static void LoadExcel()
    {
		if (excelList == null) excelList = new List<LanguageConfig>();
        excelList.Clear();

		LanguageConfig[] cfgList = LanguageSettings.tableList;

        //判断是否有对象被选中
		if (cfgList.Length == 0)
            return;
        //遍历每一个对象判断不是Excel文件
		foreach (LanguageConfig lc in cfgList)
        {
			string fileName = pathRoot +LanguageSettings.INPUT_PATH+ lc.GetTableName ();
			if (File.Exists (fileName))
				excelList.Add (lc);
        }
    }

    private static void Init()
    {
        //获取当前实例
        instance = EditorWindow.GetWindow<LanguageTools>();
        //初始化
		pathRoot = Application.dataPath;

        scrollPos = new Vector2(instance.position.x, instance.position.y + 75);
    }

    void OnSelectionChange()
    {
        //当选择发生变化时重绘窗体
        Show();
        LoadExcel();
        Repaint();
    }
}
