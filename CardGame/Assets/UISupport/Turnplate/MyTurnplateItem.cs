using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MyTurnplateItem : TurnItem {
    //public int type = 0;
    private Image rawImage = null;
    private UICallback uiCB;
    public GameObject light = null;
    public GameObject gray = null;
    public bool isActive = true;
    
    private CanvasGroup canvasGroup;

    protected override void OnAwake()
    {
        rawImage = GetComponent<Image>();
        uiCB = GetComponent<UICallback>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    protected override void OnCenter()
    {
        if (uiCB.DropCallback != null)
            uiCB.DropCallback.Call(type);
        if (!gray && rawImage)
        {
            rawImage.color = Color.white;
        }

        if (light)
        {
            light.SetActive(isActive);
        }

        if (gray)
        {
            gray.SetActive(!isActive);
        }

        if (canvasGroup)
        {
            canvasGroup.alpha = 1;
        }
    }
    protected override void NotCenter()
    {
        if (!gray && rawImage)
        {
            rawImage.color = Color.gray;
        }
        if (light)
        {
            light.SetActive(!isActive);
        }

        if (gray)
        {
            gray.SetActive(isActive);
        }

        if (canvasGroup)
        {
            canvasGroup.alpha = 0.6f;
        }
    }

}
