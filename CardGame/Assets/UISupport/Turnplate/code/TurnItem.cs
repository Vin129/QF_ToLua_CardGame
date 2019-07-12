using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnItem : MonoBehaviour {
    // Start index
    public int type = 0;
    public bool clickToCenter = true;
    private Turnplate plate;
    private int curveOffSetIndex = 0;
    public int CurveOffSetIndex
    {
        get { return this.curveOffSetIndex; }
        set { this.curveOffSetIndex = value; }
    }

    // Runtime real index(Be calculated in runtime)
    private int curRealIndex = 0;
    public int RealIndex
    {
        get { return this.curRealIndex; }
        set { this.curRealIndex = value; }
    }

    // Curve center offset 
    private float dCurveCenterOffset = 0.0f;
    public float CenterOffSet
    {
        get { return this.dCurveCenterOffset; }
        set { dCurveCenterOffset = value; }
    }
    private float depth = 1;
    public float Depth
    {
        get { return this.depth; }
        set { depth = value; }
    }

    private bool isCenter = false;
    
    public bool IsCenter
    {
        get { return this.isCenter; }
        set { this.isCenter = value;
            if (this.isCenter)
                OnCenter();
            else
                NotCenter();
        }
    }


    private Transform mTrs;

    void Awake()
    {
        mTrs = this.transform;
        OnAwake();
    }
    private void Start()
    {
        OnStar();
    }

    public void SetTurnplate(Turnplate t)
    {
        plate = t;
    }
    protected virtual void OnAwake()
    {

    }

    protected virtual void OnStar()
    {

    }

    protected virtual void OnCenter() {

    }
    protected virtual void NotCenter() {

    }
    public virtual void OnClickTurnItem()
    {
        if (clickToCenter)
        {
            plate.SetHorizontalTargetItemIndex(this);
        }
    }

    public void UpdateScrollViewItems(float xValue,float yValue,float scaleValue,float depth)
    {
        Vector3 targetPos = Vector3.one;
        Vector3 scale = new Vector3(scaleValue,scaleValue,1);
        targetPos.x = xValue;
        targetPos.y = yValue;
        this.depth = depth;
        mTrs.GetComponent<RectTransform>().anchoredPosition = targetPos;
        mTrs.localScale = scale;
    }

   
}
