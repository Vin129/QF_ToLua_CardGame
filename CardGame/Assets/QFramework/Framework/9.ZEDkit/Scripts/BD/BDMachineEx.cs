using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using BehaviorDesigner.Runtime;

public class BDMachineEx : MonoBehaviour {

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    private void RunBD(string _path) {
        ExternalBehaviorTree externalBehaviorTree = Resources.Load(_path) as ExternalBehaviorTree;
        if (null != externalBehaviorTree)
        {
            GameObject go = new GameObject("lalala");
            BehaviorTree tree = go.AddComponent<BehaviorTree>();
            tree.ExternalBehavior = externalBehaviorTree;
            //tree.SetBehaviorSource(externalBehaviorTree.BehaviorSource);
            //tree.Start();
        }
    }

    public void OnGUI()
    {
#if UNITY_EDITOR
        OnButtonDo("点击运行", new Rect(100f, 100f, 200f, 100f),()=> {
            RunBD("BD/100000");
        });
#endif
    }

#if UNITY_EDITOR
    private void OnButtonDo(string name,Rect rect,UnityAction onCall) {
        if (GUI.Button(rect,new GUIContent(name)))
        {
            if (null != onCall)
                onCall();
        }
    }
#endif
}
