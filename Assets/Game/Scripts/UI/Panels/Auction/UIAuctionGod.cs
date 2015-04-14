using UnityEngine;
using System.Collections;

public class UIAuctionGod : MonoBehaviour {

	#region VewWidgets
	//public UILabel UserNumberLabel;
	public UIImageButton UserColorSprite;
	public UISprite GodSprite;
	public UILabel BetLabel;
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
		/*UserNumberLabel.text = "";
		if (userNumber >= 0)
			UserNumberLabel.text = "" + userNumber;*/
		UserColorSprite.normalSprite = UIConsts.userColorsString[userNumber] + "-ring1";
		UserColorSprite.hoverSprite = UIConsts.userColorsString[userNumber] + "-ring2";
		UserColorSprite.pressedSprite = UIConsts.userColorsString[userNumber] + "-ring2";
		UserColorSprite.disabledSprite = UIConsts.userColorsString[userNumber] + "-ring1";

		UserColorSprite.target.spriteName = UserColorSprite.normalSprite;
	}	

	public void SetGod(string godName) {
		GodSprite.spriteName = UIConsts.godSpritesString[godName];
		betObject.SetActive( godName != Cyclades.Game.Constants.godAppolon );		
	}

	public void SetBet(int bet) {
		if (BetLabel) {
			BetLabel.text = "";
			if (bet > 0)
				BetLabel.text = "" + bet;
		}
	}
	#endregion
}
