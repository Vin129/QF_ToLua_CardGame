using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TurnItemEvent : EventTrigger
{
    public float MoveMul = 1f;
    public bool NeedToClick = true;//是否可被点击
    private Turnplate turnplateScrollView;
    private TurnItem thisTurnItem;
    bool isCanClick = true;
    public void SetScrollView(Turnplate view,TurnItem item)
    {
        turnplateScrollView = view;
        thisTurnItem = item;
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
        isCanClick = false;
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        if (turnplateScrollView != null)
            turnplateScrollView.OnDragMove(eventData.delta *0.6f* MoveMul);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        isCanClick = true;
        base.OnEndDrag(eventData);
        if (turnplateScrollView != null)
            turnplateScrollView.OnDragEnd();
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (NeedToClick) {
            base.OnPointerClick(eventData);
            if (isCanClick) {
                thisTurnItem.OnClickTurnItem();
                if (thisTurnItem.IsCenter && turnplateScrollView.onItemClick != null)
                {
                    turnplateScrollView.onItemClick(thisTurnItem.type);
                }              
            }
        }
    }
}
