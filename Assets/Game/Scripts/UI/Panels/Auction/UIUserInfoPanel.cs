using UnityEngine;
using System.Collections;

public class UIUserInfoPanel: UIGamePanel
{
	#region ViewWidgets
	public UISprite PlayerSprite;
	public UISprite PlayerShieldSprite;
	#endregion

	#region ViewWidgetsSet
	private int currentBet;
	public int CurrentBet {
		get { return currentBet; }
		set { 

		}
	}
	private int player;
	public int Player {
		get { return player; }
		set {
			//PlayerSprite.spriteName = UIConsts.godBigSprites[godName];
		}
	}
	#endregion

	#region Events
	public void OnPressCancelButton() {
		Hide();
	}
	#endregion
}
