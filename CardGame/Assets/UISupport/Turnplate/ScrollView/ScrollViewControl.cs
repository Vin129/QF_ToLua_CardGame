using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScrollViewControl : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    public float Movingfactor = 0.25f;
    public float MoveMul = 1;
    private Turnplate turnplateScrollView;
    private bool isDrag = false;
    public void SetScrollView(Turnplate view)
    {
        turnplateScrollView = view;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDrag = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (turnplateScrollView != null)
            turnplateScrollView.OnDragMove(eventData.delta*Movingfactor* MoveMul);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;
        if (turnplateScrollView != null)
            turnplateScrollView.OnDragEnd();
    }

    private void Awake()
    {
        
    }
    //监听按下
    public void OnPointerDown(PointerEventData eventData)
    {
        if(!isDrag)
            PassEvent(eventData, ExecuteEvents.pointerDownHandler);
    }

    //监听抬起
    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isDrag)
            PassEvent(eventData, ExecuteEvents.pointerUpHandler);
    }
    //监听点击
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isDrag)
        {
            PassEvent(eventData, ExecuteEvents.submitHandler);
            PassEvent(eventData, ExecuteEvents.pointerClickHandler);
        }
    }


    //把事件透下去
    public void PassEvent<T>(PointerEventData data, ExecuteEvents.EventFunction<T> function)
        where T : IEventSystemHandler
    {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(data, results);
        GameObject current = data.pointerCurrentRaycast.gameObject;
        for (int i = 0; i < results.Count; i++)
        {   
            if (results[i].gameObject.GetComponent<Button>() != null)
            {
                ExecuteEvents.Execute(results[i].gameObject, data, function);
            }
        }
    }
}
