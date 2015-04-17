using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIAuctionGod : MonoBehaviour {

	#region VewWidgets
	public UIImageButton UserColorSprite;
	public UISprite GodSprite;
	public UILabel BetLabel;
	public UISprite[] ApolloBetSprites;
	public GameObject betObject;
	#endregion

	public UIAuctionInfo AuctionInfoPanel;
	public int MyNumber;

	#region Events
	public void OnClick() {
		AuctionInfoPanel.OnConreteAuctionGodClick(MyNumber);
	}
	#endregion

	#region ViewWidgetsSet
	public void SetUser(int userNumber) {
		UserColorSprite.normalSprite = UIConsts.userColorsString[userNumber] + "-ring1";
		UserColorSprite.hoverSprite = UIConsts.userColorsString[userNumber] + "-ring2";
		UserColorSprite.pressedSprite = UIConsts.userColorsString[userNumber] + "-ring2";
		UserColorSprite.disabledSprite = UIConsts.userColorsString[userNumber] + "-ring1";

		UserColorSprite.target.spriteName = UserColorSprite.normalSprite;
	}	

	public void SetGod(string godName) {
		GodSprite.spriteName = UIConsts.godSpritesString[godName];
		if (godName != Cyclades.Game.Constants.godAppolon) {
			betObject.SetActive(true);
		}
		for (int i = 0; i < ApolloBetSprites.Length; ++i)
			ApolloBetSprites[i].gameObject.SetActive(false);
	}

	public void SetBet(int bet) {
		if (BetLabel) {
			BetLabel.text = "";
			if (bet > 0)
				BetLabel.text = "" + bet;
		}
	}

	public void SetApolloBets(List<int> bets) {
		if (bets.Count > 0)
			SetUser(bets[0]);

		for (int i = 0; i < ApolloBetSprites.Length; ++i) {
			ApolloBetSprites[i].gameObject.SetActive(bets.Count > i + 1);
			if (bets.Count > i + 1) {
				ApolloBetSprites[i].spriteName = "vinzurian--" + UIConsts.userColorsString[bets[i+1]];
			}
		}
	}
	#endregion
}
