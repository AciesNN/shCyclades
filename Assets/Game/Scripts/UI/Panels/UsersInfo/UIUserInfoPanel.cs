using UnityEngine;
using System.Collections;

using Cyclades.Game;

public class UIUserInfoPanel: UIGamePanel
{
	#region VewWidgets
	public UISprite PlayerSprite;
	public UILabel PlayerColorName;
	public UISprite PlayerShieldSprite;
	public UISprite CancelButtonSprite;

	public UISprite Metro1;
	public UISprite Metro2;
	public UISprite Metro3;

	public UILabel IncomeLabel;
	public UILabel GoldLabel;

	public UILabel PhilosophersLabel;
	public UISprite PhilosophersSprite;
	public UILabel PriestsLabel;
	public UISprite PriestsSprite;

	public UISprite BuildFortress;
	public UISprite BuildPort;
	public UISprite BuildUniver;
	public UISprite BuildTemple;
	#endregion

	[HideInInspector]
	public int player;	
	
	#region ViewWidgetsSet
	override protected void OnPanelOpen() {
		PlayerSprite.spriteName = "Player" + player + "Big";
		PlayerColorName.text = "Локрум (" + player + ")";
		PlayerShieldSprite.spriteName = UIConsts.userColorsShields[player] + "1";

		UIImageButton CancelButtonSpriteImageButton = CancelButtonSprite.GetComponent<UIImageButton>();
		CancelButtonSpriteImageButton.normalSprite = UIConsts.userColorsCancelButton[player] + "1";
		CancelButtonSpriteImageButton.hoverSprite = UIConsts.userColorsCancelButton[player] + "2";
		CancelButtonSpriteImageButton.pressedSprite = UIConsts.userColorsCancelButton[player] + "3";
		CancelButtonSpriteImageButton.disabledSprite = UIConsts.userColorsCancelButton[player] + "4";
		CancelButtonSprite.spriteName = CancelButtonSpriteImageButton.normalSprite;

		long metroCount = Cyclades.Game.Library.Map_GetMetroCountAtPlayersIslands(Sh.In.GameContext, player);
		Metro1.gameObject.SetActive(metroCount > 0);
		Metro2.gameObject.SetActive(metroCount > 1);
		Metro3.gameObject.SetActive(metroCount > 2);

		int philosopherNumber = Sh.In.GameContext.GetInt("/markers/philosopher/[{0}]", player);
		PhilosophersLabel.text = "" + philosopherNumber;
		PhilosophersSprite.spriteName = "alchemists" + (philosopherNumber > 0 ? "1" : "0");
		int priestsNumber = Sh.In.GameContext.GetInt("/markers/priest/[{0}]", player);
		PriestsLabel.text = "" + priestsNumber;
		PriestsSprite.spriteName = "wisperess" + (priestsNumber > 0 ? "1" : "0");

		bool is_current_user = (player == Library.GetCurrentPlayer(Sh.In.GameContext));
		IncomeLabel.text = "" + Sh.In.GameContext.GetInt("/markers/income/[{0}]", player);
		GoldLabel.text = (is_current_user ? "/ " + Sh.In.GameContext.GetInt("/markers/gold/[{0}]", player) : "");

		SetBuildSprite(BuildFortress, "build-fortress", Cyclades.Game.Constants.buildFortres);
		SetBuildSprite(BuildPort, "build-fortress", Cyclades.Game.Constants.buildMarina);
		SetBuildSprite(BuildUniver, "build-laboratory", Cyclades.Game.Constants.buildUniver);
		SetBuildSprite(BuildTemple, "build-haven", Cyclades.Game.Constants.buildTemple);
	}

	void SetBuildSprite(UISprite buildSprite, string buildSpriteName, string buildType) {
		long buildsCount = Cyclades.Game.Library.Map_GetBuildCountAtPlayersIslands(Sh.In.GameContext, player, buildType);
		buildSprite.spriteName = (buildsCount > 0 ? buildSpriteName : "build-placeholder");
	}
	#endregion

	#region Events
	public void OnPressCancelButton() {
		Hide();
	}
	#endregion
}
