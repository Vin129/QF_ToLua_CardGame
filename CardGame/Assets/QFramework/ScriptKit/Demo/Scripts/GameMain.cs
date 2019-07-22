using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using ScriptKit;
public class GameMain : MonoBehaviour {
    public static GameMain Instance;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Instance = this;
        ResMgr.Init();
        BaseOutlet.Instance.Init();
    }

    void Start () {
        //UIMgr.OpenPanel("Resources/MainView");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
