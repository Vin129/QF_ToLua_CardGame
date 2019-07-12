using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 嵌套拖拽的传递（目前支持下层ScrollRect，需手动设置）
/// 主要处理嵌套拖拽中的方向拖拽/上下左右手势
/// 拖拽结束后响应
/// </summary>
public class NestedDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    /// <summary>
    /// 类型and角度起始值
    /// </summary>
    enum Direction {
        Left = 245,
        Right = 65,
        Up = 295,
        Down = 115
    }
    /// <summary>
    /// 嵌套拖拽
    /// 只检测横和竖的冲突
    /// 嵌套外出目前只针对ScrollRect
    /// </summary>
    public ScrollRect anotherScrollRect;
    /// <summary>
    /// 拖动方向默认为上下拖动，否则为左右拖动型
    /// </summary>
    public bool thisIsUpAndDown = true;
    private Direction dragDirection;
    private bool isAvoid = false;
    float angle = 0f;
    /// <summary>
    /// 上左 1 ，下右2
    /// </summary>
    public UnityEvent event1;
    public UnityEvent event2;

    
    void Awake()
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (anotherScrollRect != null)
            anotherScrollRect.OnBeginDrag(eventData);
        
        Vector3 v3 = Vector3.Cross(eventData.delta, Vector3.up);
        if (v3.z > 0)
            angle = Vector3.Angle(eventData.delta, Vector3.up);
        else
            angle = 360 - Vector3.Angle(eventData.delta, Vector3.up);


        if (angle >= (float)Direction.Left && angle < (float)Direction.Up)
            dragDirection = Direction.Left;
        else if(angle >= (float)Direction.Up || angle < (float)Direction.Right)
            dragDirection = Direction.Up;
        else if (angle >= (float)Direction.Right && angle < (float)Direction.Down)
            dragDirection = Direction.Right;
        else if (angle >= (float)Direction.Down && angle < (float)Direction.Left)
            dragDirection = Direction.Down;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (anotherScrollRect != null)
            anotherScrollRect.OnDrag(eventData);

        //判断拖动方向，防止水平与垂直方向同时响应导致的拖动时整个界面都会动

        if (dragDirection == Direction.Up || dragDirection == Direction.Down)
        {
            //anotherScrollRect.enabled = thisIsUpAndDown;
            if (thisIsUpAndDown)
                isAvoid = true;
            else
                isAvoid = false;
        }
        else
        {
            //anotherScrollRect.enabled = !thisIsUpAndDown;
            if (thisIsUpAndDown)
                isAvoid = false;
            else
                isAvoid = true;
        }
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (anotherScrollRect != null)
        {
            anotherScrollRect.OnEndDrag(eventData);
            anotherScrollRect.enabled = true;
        }
        if (isAvoid) {
            if (dragDirection == Direction.Left || dragDirection == Direction.Up)
                event1.Invoke();
            else
                event2.Invoke();
        }
    }
}
