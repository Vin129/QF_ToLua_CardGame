using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewHelper : MonoBehaviour
{
    private NestScrollControler nestControler;

    public enum ParentScrollStyle
    {
        Horizontal,
        Vertical
    }
    public ParentScrollStyle Style = ParentScrollStyle.Horizontal;

    public TabBar tabBar = null;
    public int ChildNum = 0;
    public int StarIndex = 0;
    public float MoveSpeed = 0.5f;
    public float LerpFactor = 0.6f;
    public Button LeftBtn;
    public Button RightBtn;
    public GameObject bg;

    public void SetNestControler(NestScrollControler nest)
    {
        nestControler = nest;
    }

    private void Start()
    {
        if (LeftBtn != null)
        {
            LeftBtn.onClick.AddListener(nestControler.OnClickLeft);
        }
        if (RightBtn != null)
        {
            RightBtn.onClick.AddListener(nestControler.OnClickRight);
        }
    }

    public void HideBtn(float value)
    {
        if (LeftBtn != null)
        {
            LeftBtn.gameObject.SetActive(true);
            if (value == 0 || ChildNum < 2)
                LeftBtn.gameObject.SetActive(false);
        }
        if (RightBtn != null)
        {
            RightBtn.gameObject.SetActive(true);
            if (value == 1 || ChildNum < 2)
                RightBtn.gameObject.SetActive(false);
        }
    }


}
