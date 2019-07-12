//-------------------------------
//该Demo由风冻冰痕所写
//http://icemark.cn/blog
//转载请说明出处
//-------------------------------
using UnityEngine;
using LuaInterface;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class UIMultiScrollIndex : MonoBehaviour
{
    //public Text _textIndex;
    //public Text _textOld;
    //public Button _buttonAdd;
    //public Button _buttonDel;
    private UIMultiScroller _scroller;
    private int _index;
	private int _realIndex;

    void Awake()
    {
       //_buttonAdd.onClick.AddListener(OnButtonAddClick);
       //_buttonDel.onClick.AddListener(OnButtonDelClick);
    }

    void Start()
    {
        //_textOld.text = _index.ToString();
    }

    private void OnButtonAddClick()
    {
        //添加一个新的Item
        _scroller.AddItem(_index + 1);
    }

    public void OnButtonDelClick()
    {
        //删除当前的Item
        _scroller.DelItem(_index);
    }

    public int Index
    {
        get { return _index; }
        set
        {
            _index = value;
            transform.localPosition = _scroller.GetPosition(_index);
            //_textIndex.text = _index.ToString();
            //gameObject.name = "item" + (_index < 10 ? "0" + _index : _index.ToString());
            gameObject.name = "item" + (_index+1).ToString();
            UICallback kDropCb = null;
            kDropCb = gameObject.GetComponent<UICallback>();
            if (kDropCb != null) 
                kDropCb.DropCallback.Call(_realIndex+1,_index +1);
        }
    }

	public int RealIndex
	{
		get { return _realIndex; }
		set
		{
			_realIndex = value;
		}
	}

    public UIMultiScroller Scroller
    {
        set { _scroller = value; }
    }
}
