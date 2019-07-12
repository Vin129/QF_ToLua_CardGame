using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
/// <summary>
/// 转盘
/// 通过自己设置动画曲线来控制0~1直接的过渡
/// 设置TurnItem来达到一些效果（改变前后关系，中间效果，拖拽点击事件，关联Lua事件） 
/// 增加了与ScrollRect的联动（特制，不适合通用，根据需求调节参数t2cfactor、t2cB、t2cf）
/// TODO：记不清参数代表啥意思了，最好可以动态更新数量
/// </summary>
public class Turnplate : MonoBehaviour
{
    public bool isLoop = true; //控制是否循环
    public int startIndex = 0; //非循环模式有用
    public AnimationCurve XpositionCurve;
    public AnimationCurve YpositionCurve;
    public AnimationCurve ScaleCurve;
    public AnimationCurve DepthCurve;
    public ScrollRect SR = null;
    public ScrollViewControl ScrollControl = null;
    public bool isControl = true;//ScrollView是否联动控制

    public float totalWidth = 200f;
    public float totalHeight = 100f;
    public float t2cfactor = 1.25f; //转盘控制上部banner的系数
    public float t2cB = 0.125f;
    public float t2cf = 0.8f;
    private float dFactor = 0.2f;
    private int startCenterIndex = 0;
    // Lerp duration
    public float lerpDuration = 0.2f;
    private float mCurrentDuration = 0.0f;
    private int mCenterIndex = 0;
    public bool enableLerpTween = true;
    private bool canChangeItem = true;

    private TurnItem curCenterItem;
    private TurnItem preCenterItem;

    // originHorizontalValue Lerp to horizontalTargetValue
    private float originHorizontalValue = 0.1f;
    public float curHorizontalValue = 0.5f;//水平系数


    // targets enhance item in scroll view
    public List<TurnItem> listTurnItems;
    // sort to get right index
    private List<TurnItem> listSortedItems = new List<TurnItem>();

    public delegate void ItemClick(int type);
    public ItemClick onItemClick = null;

    public TabBar tabBar = null;
    public Button btnLeft = null;
    public Button btnRight = null;

    private int changeIndex = -1;
    public Action<int> OnItemChanged;
    void Awake()
    {

    }

    void Start()
    {
        Reset();
        if (btnLeft != null)
        {
            btnLeft.onClick.AddListener(() =>
            {
                OnBtnLeftClick();
            });
        }
        if (btnRight != null)
        {
            btnRight.onClick.AddListener(() =>
            {
                OnBtnRightClick();
            });
        }
    }

    public void SetItems(GameObject[] x, int itemWidth)
    {
        listTurnItems.Clear();
        for (int i = 0; i < x.Length; i++)
        {
            var ti = x[i].GetComponent<TurnItem>();
            if (ti != null)
            {
                listTurnItems.Add(ti);
            }
        }
        if (listTurnItems.Count == 0)
            return;
        totalWidth = itemWidth * x.Length;
        Reset();
    }

    public void Reset()
    {
        enableLerpTween = false;
        if (tabBar != null)
        {
            tabBar.resetTabBar(listTurnItems.Count);
        }
        if (ScrollControl)
        {
            ScrollControl.SetScrollView(this);
        }
        canChangeItem = true;
        int count = listTurnItems.Count;
        dFactor = (Mathf.RoundToInt((1f / count) * 10000f)) * 0.0001f;
        mCenterIndex = count / 2;
        if (count % 2 != 0)
            mCenterIndex = (count - 1) / 2;
        int index = 0;
        for (int i = count - 1; i >= 0; i--)
        {
            listTurnItems[i].CurveOffSetIndex = i;
            listTurnItems[i].CenterOffSet = dFactor * (mCenterIndex - index);
            listTurnItems[i].SetTurnplate(this);
            GameObject obj = listTurnItems[i].gameObject;
            TurnItemEvent script = obj.GetComponent<TurnItemEvent>();
            if (script != null)
                script.SetScrollView(this, listTurnItems[i]);
            index++;
        }
        if (!isLoop)
        {
            startCenterIndex = startIndex;
            changeIndex = startIndex;
        }
        else
        {
            changeIndex = mCenterIndex;
            startCenterIndex = mCenterIndex;
        }
        listSortedItems = new List<TurnItem>(listTurnItems.ToArray());
        curCenterItem = listTurnItems[startCenterIndex];
        curHorizontalValue = 0.5f - curCenterItem.CenterOffSet;
        LerpTweenToTarget(0f, curHorizontalValue, false);
        ChangePage(); 
    }

    void Update()
    {
        if (enableLerpTween)
            TweenViewToTarget();
    }

    private void TweenViewToTarget()
    {
        mCurrentDuration += Time.deltaTime;
        if (mCurrentDuration > lerpDuration)
            mCurrentDuration = lerpDuration;

        float percent = mCurrentDuration / lerpDuration;
        if (Mathf.Abs(Mathf.Abs(originHorizontalValue) - Mathf.Abs(curHorizontalValue)) > 0.7f)
        {
            if (Mathf.Abs(originHorizontalValue - Mathf.Round(originHorizontalValue)) < 0.1f)
            {
                curHorizontalValue = Mathf.Round(originHorizontalValue);
            }
        }
        float value = Mathf.Lerp(originHorizontalValue, curHorizontalValue, percent);
        UpdateTurnplate(value);
        if (mCurrentDuration >= lerpDuration)
        {
            canChangeItem = true;
            enableLerpTween = false;
        }
    }

    //起是位置to目标位置 
    private void LerpTweenToTarget(float originValue, float targetValue, bool needTween = false)
    {
        if (!needTween)
        {
            //手动拖动
            originHorizontalValue = targetValue;
            UpdateTurnplate(targetValue);
        }
        else
        {
            //点击滑动
            originHorizontalValue = originValue;
            curHorizontalValue = targetValue;
            mCurrentDuration = 0.0f;
        }
        enableLerpTween = needTween;
    }

    //更新转盘位置
    public void UpdateTurnplate(float fValue)
    {
        for (int i = 0; i < listTurnItems.Count; i++)
        {
            TurnItem itemScript = listTurnItems[i];
            float xValue = GetXPosValue(fValue, itemScript.CenterOffSet);
            float yValue = GetYPosValue(fValue, itemScript.CenterOffSet);
            float scaleValue = GetScaleValue(fValue, itemScript.CenterOffSet);
            float depthCurveValue = DepthCurve.Evaluate(fValue + itemScript.CenterOffSet) * 10;
            xValue = -totalWidth / 2 + xValue;
            itemScript.UpdateScrollViewItems(xValue, yValue, scaleValue, depthCurveValue);
        }
        // v 来控制ScrollView的Bar值
        float v = 0;
        if (fValue < 0)
        {
            v = Mathf.Abs((fValue - (int)fValue));
            v = 1 - v;
        }
        else
        {
            v = Mathf.Abs((fValue - (int)fValue));
        }
        if (isControl)
        {
            v = Mathf.Round(v * 100) / 100;

            //v = (1.25f * v - 0.125f);
            v = (t2cfactor * v - t2cB);
            //v = (1 - v)*0.8f;
            v = (1 - v) * t2cf;

            if (v < 0)
            {
                v = 1 + v;
                if (SR)
                {
                    SR.horizontalScrollbar.value = v;
                }
            }
            else
            {
                //Debug.Log(v);
                if (SR)
                {
                    SR.horizontalScrollbar.value = v;
                }
            }
        }
        SetItemsDepth();
    }
    // X曲线当前位置
    private float GetXPosValue(float sliderValue, float added)
    {
        float evaluateValue = XpositionCurve.Evaluate(sliderValue + added) * totalWidth;
        return evaluateValue;
    }
    //Y曲线当前位置
    private float GetYPosValue(float sliderValue, float added)
    {
        float evaluateValue = YpositionCurve.Evaluate(sliderValue + added) * totalHeight;
        return evaluateValue;
    }
    //Scale曲线当前大小
    private float GetScaleValue(float sliderValue, float added)
    {
        float evaluateValue = ScaleCurve.Evaluate(sliderValue + added);
        return evaluateValue;
    }


    // 如果有按钮
    public void OnBtnRightClick()
    {
        if (!canChangeItem)
            return;
        int targetIndex = curCenterItem.CurveOffSetIndex + 1;
        if (!isLoop && targetIndex > listTurnItems.Count - 1)
            return;
        if (targetIndex > listTurnItems.Count - 1)
            targetIndex = 0;
        SetHorizontalTargetItemIndex(listTurnItems[targetIndex]);
        if (changeIndex != targetIndex && OnItemChanged != null)
        {
            changeIndex = targetIndex;
            ChangePage();
        }
    }

    public void OnBtnLeftClick()
    {
        if (!canChangeItem)
            return;
        int targetIndex = curCenterItem.CurveOffSetIndex - 1;
        if (!isLoop && targetIndex < 0)
            return;
        if (targetIndex < 0)
            targetIndex = listTurnItems.Count - 1;
        SetHorizontalTargetItemIndex(listTurnItems[targetIndex]);
        if (changeIndex != targetIndex && OnItemChanged != null)
        {
            changeIndex = targetIndex;
            ChangePage();
        }
    }

    //点击移动至中心
    public void SetHorizontalTargetItemIndex(TurnItem selectItem)
    {

        if (!canChangeItem)
            return;

        if (curCenterItem == selectItem)
            return;

        canChangeItem = false;
        preCenterItem = curCenterItem;
        curCenterItem = selectItem;

        // 判断在左在右
        float centerXValue = XpositionCurve.Evaluate(0.5f) * totalWidth - totalWidth * 0.5f;
        bool isRight = false;
        if (selectItem.transform.localPosition.x > centerXValue)
            isRight = true;

        // 计算距离中心位置
        int moveIndexCount = GetMoveCurveFactorCount(preCenterItem, selectItem);
        float dvalue = 0.0f;
        if (isRight)
        {
            dvalue = -dFactor * moveIndexCount;
        }
        else
        {
            dvalue = dFactor * moveIndexCount;
        }

        float originValue = curHorizontalValue;
        LerpTweenToTarget(originValue, curHorizontalValue + dvalue, true);
    }

    private int GetMoveCurveFactorCount(TurnItem preCenterItem, TurnItem newCenterItem)
    {
        int factorCount = 0;
        if (!isLoop)
            factorCount = Mathf.Abs(newCenterItem.CurveOffSetIndex) - Mathf.Abs(preCenterItem.CurveOffSetIndex);
        else
            factorCount = Mathf.Abs(newCenterItem.RealIndex) - Mathf.Abs(preCenterItem.RealIndex);
        return Mathf.Abs(factorCount);
    }

    public float factor = 0.001f;

    // 拖动
    public void OnDragMove(Vector2 delta)
    {
        if (Mathf.Abs(delta.x) > 0.0f)
        {
            if (!isLoop)
            {
                float speedLimit = 40;
                if (delta.x > speedLimit)
                    delta.x = speedLimit;
                if (delta.x < -speedLimit)
                    delta.x = -speedLimit;
                if ((curCenterItem.CurveOffSetIndex == 0 && delta.x > 0) || (curCenterItem.CurveOffSetIndex == listTurnItems.Count - 1 && delta.x < 0))
                    return;
            }
            curHorizontalValue += delta.x * factor * 1.5f;
            LerpTweenToTarget(0.0f, curHorizontalValue, false);
        }
    }

    public void OnDragEnd()
    {
        float target = 0f;
        int closestIndex = 0;
        float value = (curHorizontalValue - (int)curHorizontalValue);
        float min = float.MaxValue;
        float tmp = 0.5f * (curHorizontalValue < 0 ? -1 : 1);
        for (int i = 0; i < listTurnItems.Count; i++)
        {
            float dis = Mathf.Abs(Mathf.Abs(value) - Mathf.Abs((tmp - listTurnItems[i].CenterOffSet)));
            if (dis < min)
            {
                closestIndex = i;
                min = dis;
            }
        }
        originHorizontalValue = curHorizontalValue;
        if ((tmp - curCenterItem.CenterOffSet) == 0 && (int)curHorizontalValue != Mathf.Round(curHorizontalValue))
            target = Mathf.Round(curHorizontalValue);
        else
            target = ((int)curHorizontalValue + (tmp - curCenterItem.CenterOffSet));
        LerpTweenToTarget(originHorizontalValue, target, true);
        canChangeItem = false;
        if (!isLoop)
        {
            if (curCenterItem.CurveOffSetIndex == listTurnItems.Count - 1 && closestIndex == 0)
            {
                return;
            }
        }
        if (changeIndex != closestIndex && OnItemChanged != null)
        {
            changeIndex = closestIndex;
            ChangePage();
        }
    }

    static public int Sortdepth(TurnItem a, TurnItem b) { return a.Depth.CompareTo(b.Depth); }
    private void SetItemsDepth()
    {
        List<TurnItem> temp = new List<TurnItem>();
        for (int i = 0; i < listTurnItems.Count; i++)
        {
            temp.Add(listTurnItems[i]);
        }
        //temp = listTurnItems;     //这样赋值，sort temp之后，源数据也脏掉了
        temp.Sort(Sortdepth);
        if (temp.Count % 2 != 0)
        {
            for (int i = 0; i < temp.Count; i++)
            {
                temp[i].transform.SetSiblingIndex(i);
                int index = (int)(Mathf.Round(i / 2));
                temp[i].RealIndex = index;
                if (i < temp.Count - 1)
                {
                    temp[i].IsCenter = false;
                }
                else
                {
                    temp[i].IsCenter = true;
                    preCenterItem = curCenterItem;
                    curCenterItem = temp[i];
                }
            }
        }
        else
        {
            int index = 0;
            float temDepth = 0f;
            temDepth = temp[0].Depth;
            for (int i = 0; i < temp.Count; i++)
            {
                temp[i].transform.SetSiblingIndex(i);
                if (temDepth < temp[i].Depth)
                {
                    index++;
                    temDepth = temp[i].Depth;
                }
                temp[i].RealIndex = index;
                if (i < temp.Count - 1)
                {
                    temp[i].IsCenter = false;
                }
                else
                {
                    temp[i].IsCenter = true;
                    preCenterItem = curCenterItem;
                    curCenterItem = temp[i];
                }
            }
        }
    }

    private void ChangePage()
    {
        if (OnItemChanged != null)
            OnItemChanged(changeIndex);
        if (tabBar != null)
        {
            tabBar.setTabIndex(changeIndex);
        }
        if (btnLeft != null)
        {
            btnLeft.gameObject.SetActive(true);
            if (changeIndex == 0)
                btnLeft.gameObject.SetActive(false);
        }
        if (btnRight != null)
        {
            btnRight.gameObject.SetActive(true);
            if (changeIndex == listTurnItems.Count - 1)
                btnRight.gameObject.SetActive(false);
        }
    }


    public void JumpToItem(TurnItem selectItem)
    {
        if (!canChangeItem || curCenterItem == null)
            return;
        changeIndex = selectItem.CurveOffSetIndex;
        SetHorizontalTargetItemIndex(selectItem);
        ChangePage();
    }
}
