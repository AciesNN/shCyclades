using UnityEngine;
using System.Collections;

using Cyclades.Game;

public class UIUserInfoPanel: UIGamePanel
{
	#region VewWidgets
	public UISprite PlayerSprite;
	public UISprite PlayerShieldSprite;
	public UISprite CancelButtonSprite;

	public UILabel IncomeLabel;
	public UILabel GoldLabel;

	public UILabel PhilosophersLabel;
	public UISprite PhilosophersSprite;
	public UILabel PriestsLabel;
	public UISprite PriestsSprite;
	#endregion

	public int player;	
	
	#region ViewWidgetsSet
	override protected void OnPanelOpen() {
		PlayerSprite.spriteName = "Player" + player + "Big";
		PlayerShieldSprite.spriteName = UIConsts.userColorsShields[player] + "1";
		CancelButtonSprite.spriteName = UIConsts.userColorsCancelButton[player] + "1";

		int philosopherNumber = Sh.In.GameContext.GetInt("/markers/philosopher/[{0}]", player);
		PhilosophersLabel.text = "" + philosopherNumber;
		PhilosophersSprite.spriteName = "alchemists" + (philosopherNumber > 0 ? "1" : "0");
		int priestsNumber = Sh.In.GameContext.GetInt("/markers/priest/[{0}]", player);
		PriestsLabel.text = "" + priestsNumber;
		PriestsSprite.spriteName = "wisperess" + (priestsNumber > 0 ? "1" : "0");

		bool is_current_user = (player == Library.GetCurrentPlayer(Sh.In.GameContext));
		IncomeLabel.text = "" + Sh.In.GameContext.GetInt("/markers/income/[{0}]", player);
		GoldLabel.text = (is_current_user ? "/ " + Sh.In.GameContext.GetInt("/markers/gold/[{0}]", player) : "");
	}
	#endregion

	#region Events
	public void OnPressCancelButton() {
		Hide();
	}
	#endregion
}
