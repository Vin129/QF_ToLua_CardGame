using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

/// <summary>
/// 解决嵌套使用ScrollRect时的Drag冲突问题。请将该脚本放置到内层ScrollRect上(外层的ScrollRect的Drag事件会被内层的拦截)
/// </summary>
public class NestedScrollRect : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	/// <summary>
	/// 外层被拦截需要正常拖动的ScrollRect，可不指定，默认在父对象中找
	/// </summary>
	public ScrollRect anotherScrollRect;
	public NestScrollControler nestScrollControler;
	/// <summary>
	/// 当前的ScrollRect（本脚本所放置的物体上）的拖动方向默认为上下拖动，否则为左右拖动型
	/// </summary>
	public bool thisIsUpAndDown = true;

	private ScrollRect thisScrollRect;

	void Awake ()
	{
		thisScrollRect = GetComponent<ScrollRect> ();
		if (anotherScrollRect == null && nestScrollControler == null)
			anotherScrollRect = GetComponentsInParent<ScrollRect> ()[1];
	}

	public void OnBeginDrag (PointerEventData eventData)
	{
		if(anotherScrollRect)
			anotherScrollRect.OnBeginDrag (eventData);
		if(nestScrollControler)
			nestScrollControler.OnBeginDrag (eventData);
	}

	public void OnDrag (PointerEventData eventData)
	{
		if(anotherScrollRect)
			anotherScrollRect.OnDrag (eventData);
		if(nestScrollControler)
			nestScrollControler.OnDrag (eventData);
		float angle = Vector2.Angle (eventData.delta, Vector2.up);
		//判断拖动方向，防止水平与垂直方向同时响应导致的拖动时整个界面都会动
		if (angle > 45f && angle < 135f)
		{
			thisScrollRect.enabled = !thisIsUpAndDown;
			if(anotherScrollRect)
				anotherScrollRect.enabled = thisIsUpAndDown;
			if(nestScrollControler)
				nestScrollControler.enabled = thisIsUpAndDown;
		}
		else
		{
			if(anotherScrollRect)
				anotherScrollRect.enabled = !thisIsUpAndDown;
			if(nestScrollControler)
				nestScrollControler.enabled = !thisIsUpAndDown;
			thisScrollRect.enabled = thisIsUpAndDown;
		}
	}

	public void OnEndDrag (PointerEventData eventData)
	{
		if(anotherScrollRect){
			anotherScrollRect.OnEndDrag (eventData);
			anotherScrollRect.enabled = true;
		}
		if(nestScrollControler){
			nestScrollControler.OnEndDrag (eventData);
			nestScrollControler.enabled = true;
		}
		thisScrollRect.enabled = true;
	}
}