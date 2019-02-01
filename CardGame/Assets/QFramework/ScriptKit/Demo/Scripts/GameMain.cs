using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
public class GameMain : MonoBehaviour {
    public static GameMain Instance;

    public LuaMain LuaMain;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Instance = this;
        LuaMain = LuaMain.getInstance();
    }

    void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
