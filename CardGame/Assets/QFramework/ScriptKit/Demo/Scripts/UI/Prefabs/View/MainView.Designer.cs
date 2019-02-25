/****************************************************************************
 * 2019.2 Vin129
 ****************************************************************************/

namespace QFramework.CardGame
{
	using UnityEngine;
	using UnityEngine.UI;

	public partial class MainView
	{
		public const string NAME = "MainView";

		[SerializeField] public Button StartBtn;

		protected override void ClearUIComponents()
		{
			StartBtn = null;
			mData = null;
		}

		private MainViewData mPrivateData = null;

		public MainViewData mData
		{
			get { return mPrivateData ?? (mPrivateData = new MainViewData()); }
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
