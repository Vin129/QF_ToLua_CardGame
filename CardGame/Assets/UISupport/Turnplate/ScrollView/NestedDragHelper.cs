using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// 对于使用NestedDrag的一些情况进行辅助
/// 自身为ScrollRect的情况，将拖动事件传递给NestedDrag
/// </summary>
public class NestedDragHelper : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    private ScrollRect thisScrollRect;
    public NestedDrag HelpTag;

    void Awake()
    {
        thisScrollRect = GetComponent<ScrollRect>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        HelpTag.OnBeginDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        HelpTag.OnDrag(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        HelpTag.OnEndDrag(eventData);
    }
}
