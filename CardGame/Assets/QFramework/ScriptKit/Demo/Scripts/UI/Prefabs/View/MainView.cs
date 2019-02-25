/****************************************************************************
 * 2019.2 Vin129
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.CardGame
{
	public class MainViewData : UIPanelData
	{
		// TODO: Query Mgr's Data
	}

	public partial class MainView : UILuaPanel
    {
		protected override void ProcessMsg (int eventId,QMsg msg)
		{
			throw new System.NotImplementedException ();
		}

		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as MainViewData ?? new MainViewData();
            //please add init code here

            BindLuaComponent();
        }

		protected override void OnOpen(IUIData uiData = null)
		{
		}

		protected override void OnShow()
		{
		}

		protected override void OnHide()
		{
		}

		protected override void OnClose()
		{
		}

		void ShowLog(string content)
		{
			Debug.Log("[ MainView:]" + content);
		}
	}
}