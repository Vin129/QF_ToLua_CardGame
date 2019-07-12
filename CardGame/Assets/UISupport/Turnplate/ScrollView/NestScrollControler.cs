using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NestScrollControler : ScrollRect
{
    //public enum ParentScrollStyle {
    //    Horizontal,
    //    Vertical
    //}
    //public ParentScrollStyle Style = ParentScrollStyle.Horizontal;

    [SerializeField]
    private ScrollViewHelper svHelper;
    private float[] barIndex;
    private float factor = 0.5f;
    private bool needTween = false;
    private int orgIndex = 1;
    private int targetIndex = 1;
    private float moveSpeed = 0.5f;
    private float lerpFactor = 0.6f;
    private int childCount;
    private bool isHorizontal = true;
    private float lastValue = 0f;
    private bool canDrag = true;
    private GameObject againstTouchBg;
    private TabBar tabBar = null;
    public Action<int> OnPageChanged;
    public float MoveMul = 1f;
    public bool NeedTween
    {
        get
        {
            return needTween;
        }
        set
        {
            if (!needTween && !value)
                return;
            needTween = value;
            againstTouchBg.SetActive(value);
            if (!value)
            {
                updateUI();
            }
        }
    }

    public float LerpFactor
    {
        get
        {
            return lerpFactor;
        }

        set
        {
            lerpFactor = value;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        svHelper = GetComponent<ScrollViewHelper>();
        svHelper.SetNestControler(this);
        if (svHelper.Style == ScrollViewHelper.ParentScrollStyle.Horizontal)
            isHorizontal = true;
        else
            isHorizontal = false;
        SetHelperValue();
    }

    protected override void Start()
    {
        base.Start();
        Init();
    }

    private void Update()
    {
        if (needTween)
            MoveTo();
    }

    public void SetHelperValue() {
        childCount = svHelper.ChildNum;
        moveSpeed = svHelper.MoveSpeed;
        lerpFactor = svHelper.LerpFactor;
        orgIndex = svHelper.StarIndex;
        targetIndex = orgIndex;
        againstTouchBg = svHelper.bg;
        tabBar = svHelper.tabBar;
    }

    public void Init() {
        barIndex = new float[childCount];
        factor = 1f / (childCount - 1);
        for (int i = 0; i < childCount; i++)
        {
            barIndex[i] = i * factor;
        }
        StartCoroutine(SetScrollBarValue(barIndex[orgIndex]));
    }

    private void updateUI()
    {
        if (tabBar)
        {
            tabBar.setTabIndex(orgIndex);
        }
        svHelper.HideBtn(this.horizontalScrollbar.value);
        if (OnPageChanged != null)
            OnPageChanged(targetIndex);
    }

    private void MoveTo() {
        if (!canDrag)
            return;
        if (isHorizontal)
        {
            if (this.horizontalScrollbar.value > barIndex[targetIndex])
            {
                this.horizontalScrollbar.value -= Time.deltaTime * moveSpeed * factor * 3f;
                if (this.horizontalScrollbar.value <= barIndex[targetIndex])
                {
                    this.horizontalScrollbar.value = barIndex[targetIndex];
                    orgIndex = targetIndex;
                    NeedTween = false;
                }
            }
            else if (this.horizontalScrollbar.value < barIndex[targetIndex])
            {
                this.horizontalScrollbar.value += Time.deltaTime * moveSpeed * factor * 3f;
                if (this.horizontalScrollbar.value >= barIndex[targetIndex])
                {
                    this.horizontalScrollbar.value = barIndex[targetIndex];
                    orgIndex = targetIndex;
                    NeedTween = false;
                }
            }
            else {
                orgIndex = targetIndex;
                NeedTween = false;
            }
        }
        else {

        }
    }

    IEnumerator SetScrollBarValue(float value) {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        this.horizontalScrollbar.value = value;
        updateUI();
    }

    private void ScrollMove(Vector2 delta) {

    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (!canDrag)
            return;
        if (isHorizontal)
            lastValue = this.horizontalScrollbar.value;
        else
            lastValue = this.verticalScrollbar.value;
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if (!canDrag)
            return;
        if (isHorizontal)
        {
            this.horizontalScrollbar.value = this.horizontalScrollbar.value - (eventData.delta.x) * lerpFactor* factor * MoveMul * 0.003f;
        }
        else {
            this.verticalScrollbar.value = this.verticalScrollbar.value - (eventData.delta.y) * lerpFactor * factor * MoveMul * 0.003f;
        }
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        if (!canDrag)
            return;
        float tempValue = Math.Abs(lastValue - this.horizontalScrollbar.value);
        if (lastValue > this.horizontalScrollbar.value && tempValue > factor / 4 && tempValue <= factor * 1.5 )
        {
            targetIndex = orgIndex - 1;
        }
        else if (lastValue > this.horizontalScrollbar.value && tempValue > factor * 1.5) {
            targetIndex = 0;
            if (targetIndex <= 0)
                targetIndex = 0;
            while (barIndex[targetIndex] < this.horizontalScrollbar.value) {
                targetIndex = targetIndex + 1;
                if (targetIndex > childCount - 1)
                {
                    targetIndex = childCount - 2;
                    break;
                }
            }
            if (barIndex[targetIndex] > this.horizontalScrollbar.value + factor / 2)
                targetIndex = targetIndex - 1;
        }


        if (lastValue < this.horizontalScrollbar.value && tempValue > factor / 4 && tempValue < factor * 1.5)
        {
                targetIndex = orgIndex + 1;
        }
        else if (lastValue < this.horizontalScrollbar.value && tempValue > factor * 1.5)
        {
            targetIndex = childCount - 1;
            while (barIndex[targetIndex] > this.horizontalScrollbar.value)
            {
                targetIndex = targetIndex - 1;
                if (targetIndex < 0)
                {
                    targetIndex = 1;
                    break;
                }
            }
            if(barIndex[targetIndex] < this.horizontalScrollbar.value - factor/2)
                targetIndex = targetIndex + 1;
        }


        if (targetIndex <= 0)
            targetIndex = 0;
        if(targetIndex >= childCount - 1)
            targetIndex = childCount - 1;

        //if (this.horizontalScrollbar.value >= 0 && this.horizontalScrollbar.value <= 0.25)
        //    targetIndex = 0;
        //if (this.horizontalScrollbar.value > 0.25 && this.horizontalScrollbar.value <= 0.75)
        //    targetIndex = 1;
        //if (this.horizontalScrollbar.value > 0.75)
        //    targetIndex = 2;

        NeedTween = true;
    }


    public void OnClickLeft() {
        if (needTween)
            return;
        if (orgIndex!=0&&targetIndex == orgIndex) {
            targetIndex--;
            NeedTween = true;
        }
    }

    public void OnClickRight()
    {
        if (needTween)
            return;
        if (orgIndex != childCount - 1 && targetIndex == orgIndex)
        {
            targetIndex++;
            NeedTween = true;
        }
    }

    public void SetCanDrag(bool bo) {
        canDrag = bo;
    }

}
