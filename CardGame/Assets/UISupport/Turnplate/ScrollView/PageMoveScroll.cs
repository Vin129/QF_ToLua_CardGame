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
        count = mScrollRect.content.GetActiveChildCount();
        onePageValue = 1 / count;
    }

}
