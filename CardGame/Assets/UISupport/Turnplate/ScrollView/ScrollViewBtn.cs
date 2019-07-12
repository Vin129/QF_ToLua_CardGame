using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewBtn : MonoBehaviour {
    [Range(0f, 1f)]
    public float moveSpeed = 0.2f;
    [Range(0f, 1f)]
    public float movePre = 0.3f;
    public Button rightBtn;
    public Button leftBtn;
    // Use this for initialization
    private Scrollbar sbar;
    private bool needTween = false;
    private float targeValue = 0f;
    private float changeValue = 0f;
	void Start () {
        sbar = GetComponent<Scrollbar>();
        sbar.onValueChanged.AddListener(setChangValue);
        if (rightBtn != null) {
            rightBtn.onClick.AddListener(onClickRight);
        }
        if (leftBtn != null)
        {
            leftBtn.onClick.AddListener(onClickLeft);
        }
    }

    private void Update()
    {
        if (needTween) {
            MoveTo();
        }
    }
    private void MoveTo() {
        if (targeValue > sbar.value) {
            sbar.value += Time.deltaTime * moveSpeed;
            if (sbar.value >= targeValue) {
                sbar.value = targeValue;
                needTween = false;
                return;
            }
        }
        if (targeValue < sbar.value)
        {
            sbar.value -= Time.deltaTime * moveSpeed;
            if (sbar.value <= targeValue)
            {
                sbar.value = targeValue;
                needTween = false;
                return;
            }
        }
    }

    private void onClickRight() {
        if (needTween)
            return;
        if (targeValue >= 1)
        {
            targeValue = 1;
            return;
        }
        targeValue += movePre;
        if (targeValue >= 1)
        {
            targeValue = 1;
        }
        needTween = true;
    }
    private void onClickLeft()
    {
        if (needTween)
            return;
        if (targeValue <= 0)
        {
            targeValue = 0;
            return;
        }
        targeValue -= movePre;
        if (targeValue <= 0)
        {
            targeValue = 0;
        }
        needTween = true;
    }
    private void setChangValue(float value) {
        if (!needTween) {
            targeValue = value;
        }
    }
}
