using UnityEngine;
using System.Collections;

public class UIAuctionGod : MonoBehaviour {

	#region VewWidgets
	public UILabel UserNumberLabel;
	public UISprite UserColorSprite;
	public UISprite GodSprite;
	public UILabel BetLabel;
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
		UserNumberLabel.text = "";
		if (userNumber >= 0)
			UserNumberLabel.text = "" + userNumber;
		UserColorSprite.color = UIConsts.userColors[userNumber];
	}	

	public void SetGod(string godName) {
		GodSprite.spriteName = UIConsts.godSprites[godName];
	}

	public void SetBet(int bet) {
		BetLabel.text = "";
		if (bet > 0)
			BetLabel.text = "" + bet;
	}
	#endregion
}
