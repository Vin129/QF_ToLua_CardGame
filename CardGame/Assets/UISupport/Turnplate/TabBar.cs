using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabBar : MonoBehaviour {

    public Toggle[] togs;
	// Use this for initialization
	void Awake () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void resetTabBar(int count)
    {
        for (int i = 0; i < togs.Length;i++)
        {
            Toggle tog = (Toggle)togs[i];
            tog.gameObject.SetActive(i<count && count>1);
        }
    }

    public void setTabIndex(int index)
    {
        Toggle tog = (Toggle)togs[index];
        if (tog)
        {
            tog.isOn = true;
        }
    }
   
}
