using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// 挂上该组件的ScrollRect可以以页面形式滑动
/// </summary>
public class PageMoveScroll : MonoBehaviour {
    public bool needScriptToInit = false;

    private ScrollRect mScrollRect;
    private int count;
    private float onePageValue;
    private bool needTween;
    private void Start()
    {
        if (!needScriptToInit)
            Init();
    }

    private void Update()
    {
            
    }

    public void Init() {
        mScrollRect = GetComponent<ScrollRect>();
        int activeCount = 0;
        for (int i = 0;i<mScrollRect.content.childCount;i++) {
            if (mScrollRect.content.GetChild(i).gameObject.activeInHierarchy)
                activeCount++;
        }
        count = activeCount;
        onePageValue = 1 / count;
    }

}
