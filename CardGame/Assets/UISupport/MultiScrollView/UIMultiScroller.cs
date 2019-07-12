
using UnityEngine;
using System.Collections.Generic;

public class UIMultiScroller : MonoBehaviour
{
    public enum Arrangement { Horizontal, Vertical, }
    public Arrangement _movement = Arrangement.Horizontal;

    #region 这块为固定位置的参数 
    public float TopStillHeight = 0f;
    public float BottomStillHeight = 0f;

    public float LeftStillWidth = 0f;
    public float RightStillWidth = 0f;
    #endregion

    //单行或单列的Item数量
    [Range(1, 20)]
    public int maxPerLine = 5;
    //Item之间的距离
    [Range(0, 100)]
    public int cellPadiding = 5;
    //Item的宽高
    public int cellWidth = 500;
    public int cellHeight = 100;
    //默认加载的行数，一般比可显示行数大2~3行
    [Range(0, 20)]
    public int viewCount = 6;
    public GameObject itemPrefab;
    public RectTransform _content;

    private int _index = -1;
    private List<UIMultiScrollIndex> _itemList;
    private int _dataCount;

    private Queue<UIMultiScrollIndex> _unUsedQueue;  //将未显示出来的Item存入未使用队列里面，等待需要使用的时候直接取出

	public delegate void VoidDelegate (int index, int realIndex, GameObject obj);
	public VoidDelegate onUpdateItem;

    private void Awake()
    {
        //_itemList = new List<UIMultiScrollIndex>();
        //_unUsedQueue = new Queue<UIMultiScrollIndex>();
        //DataCount = 100;
        //OnValueChange(Vector2.zero);
    }

    void Start()
    {
        //RestDataCount(50);
        //_itemList = new List<UIMultiScrollIndex>();
        //_unUsedQueue = new Queue<UIMultiScrollIndex>();
        //DataCount = 100;
        //OnValueChange(Vector2.zero);
    }

    public void OnValueChange(Vector2 pos)
    {
        if (_itemList == null)
            return;
        int index = GetPosIndex();
        if (_index != index && index > -1)
        {
            _index = index;
            for (int i = _itemList.Count; i > 0; i--)
            {
                UIMultiScrollIndex item = _itemList[i - 1];
                if (item.Index < index * maxPerLine || (item.Index >= (index + viewCount) * maxPerLine))
                {
                    //GameObject.Destroy(item.gameObject);
                    _itemList.Remove(item);
                    _unUsedQueue.Enqueue(item);
                    item.gameObject.SetActive(false);
                }
            }
            for (int i = _index * maxPerLine; i < (_index + viewCount) * maxPerLine; i++)
            {
                if (i < 0) continue;
                if (i > _dataCount - 1) continue;
                bool isOk = false;
                foreach (UIMultiScrollIndex item in _itemList)
                {
                    if (item.Index == i) isOk = true;
                }
                if (isOk) continue;
                CreateItem(i);
            }
        }
    }

    /// <summary>
    /// 提供给外部的方法，添加指定位置的Item
    /// </summary>
    public void AddItem(int index)
    {
        if (index > _dataCount)
        {
            Debug.LogError("添加错误:" + index);
            return;
        }
        AddItemIntoPanel(index);
        DataCount += 1;
    }

    /// <summary>
    /// 提供给外部的方法，删除指定位置的Item
    /// </summary>
    public void DelItem(int index)
    {
        Debug.Log("-------");
        //restConent();
        if (index < 0 || index > _dataCount - 1)
        {
            Debug.LogError("删除错误:" + index);
            return;
        }
        DelItemFromPanel(index);
        DataCount -= 1;
    }

    private void AddItemIntoPanel(int index)
    {
        for (int i = 0; i < _itemList.Count; i++)
        {
            UIMultiScrollIndex item = _itemList[i];
            if (item.Index >= index) item.Index += 1;
        }
        CreateItem(index);
    }

    private void DelItemFromPanel(int index)
    {
        int maxIndex = -1;
        int minIndex = int.MaxValue;
        for (int i = _itemList.Count; i > 0; i--)
        {
            UIMultiScrollIndex item = _itemList[i - 1];
            if (item.Index == index)
            {
                GameObject.Destroy(item.gameObject);
                _itemList.Remove(item);
            }
            if (item.Index > maxIndex)
            {
                maxIndex = item.Index;
            }
            if (item.Index < minIndex)
            {
                minIndex = item.Index;
            }
            if (item.Index > index)
            {
                item.Index -= 1;
            }
        }
        if (maxIndex < DataCount - 1)
        {
            CreateItem(maxIndex);
        }
    }

    private void CreateItem(int index)
    {
        UIMultiScrollIndex itemBase;
        if (_unUsedQueue.Count > 0)
        {
            itemBase = _unUsedQueue.Dequeue();
            itemBase.gameObject.SetActive(true);
        }
        else
        {
            itemBase = GameTools.AddChild(_content, itemPrefab).GetComponent<UIMultiScrollIndex>();
			itemBase.RealIndex = index;
        }

        itemBase.Scroller = this;
        itemBase.Index = index;
        _itemList.Add(itemBase);

		if (onUpdateItem != null) 
			onUpdateItem(index,itemBase.RealIndex,itemBase.gameObject);
		else
			Debug.Log ("CreateItem:" + index+","+itemBase.RealIndex);
    }

    private int GetPosIndex()
    {
        switch (_movement)
        {
            case Arrangement.Horizontal:
                var realMovingX = _content.anchoredPosition.x + LeftStillWidth;
                if (realMovingX > 0)
                    realMovingX = 0;
                return Mathf.FloorToInt(realMovingX / -(cellWidth + cellPadiding));
            case Arrangement.Vertical:
                var realMovingY = _content.anchoredPosition.y - TopStillHeight;
                if (realMovingY < 0)
                    realMovingY = 0;
                return Mathf.FloorToInt(realMovingY / (cellHeight + cellPadiding));
        }
        return 0;
    }

    public Vector3 GetPosition(int i)
    {
        switch (_movement)
        {
            case Arrangement.Horizontal:
                return new Vector3(cellWidth * (i / maxPerLine) + LeftStillWidth, -(cellHeight + cellPadiding) * (i % maxPerLine), 0f);
            case Arrangement.Vertical:
                return new Vector3(cellWidth * (i % maxPerLine), -(cellHeight + cellPadiding) * (i / maxPerLine) - TopStillHeight, 0f);
        }
        return Vector3.zero;
    }

    public int DataCount
    {
        get { return _dataCount; }
        set
        {
            _dataCount = value;
            UpdateTotalWidth();
        }
    }

    private void UpdateTotalWidth()
    {
        int lineCount = Mathf.CeilToInt((float)_dataCount / maxPerLine);
        switch (_movement)
        {
            case Arrangement.Horizontal:
                _content.sizeDelta = new Vector2(cellWidth * lineCount + cellPadiding * (lineCount - 1) + LeftStillWidth + RightStillWidth, _content.sizeDelta.y);
                break;
            case Arrangement.Vertical:
                _content.sizeDelta = new Vector2(_content.sizeDelta.x, cellHeight * lineCount + cellPadiding * (lineCount - 1) + TopStillHeight + BottomStillHeight);
                break;
        }
    }

    public void RestDataCount(int count)
    {
        if (count < 0)
        {
            count = -count;
            DataCount = count;
            for (int i = 0; i < _itemList.Count; i++)
            {
                var itemBase = _itemList[i];
                if (onUpdateItem != null)
                    onUpdateItem(itemBase.Index, itemBase.RealIndex, itemBase.gameObject);
            }
        }
        else
        {
            //Rest();
            DeletChild();
            restConent();
            _index = -1;
            _itemList = new List<UIMultiScrollIndex>();
            _unUsedQueue = new Queue<UIMultiScrollIndex>();
            DataCount = count;
            OnValueChange(Vector2.zero);
        }
    }

    void Rest()
    {
        if (_itemList != null)
        {
            for (int i = 0; i < _itemList.Count; i++)
            {
                GameObject.Destroy(_itemList[i].gameObject);
            }
        }
    }

    public void restConent() {
        _content.anchoredPosition = new Vector2(0,0);
    }

    public void DeletChild() {
        for (int i = 0;i< _content.childCount;i++) {
            if(_content.GetChild(i).GetComponent<DontDestoryMark>() == null)
                GameObject.Destroy(_content.GetChild(i).gameObject);
        }
    }
}
