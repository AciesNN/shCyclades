using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	public List<UISprite> GoldSprites = new List<UISprite>();
	public UILabel IncomeLabel;
	public UILabel GoldLabel;

	public UILabel PhilosophersLabel;
	public UISprite PhilosophersSprite;
	public UILabel PriestsLabel;
	public UISprite PriestsSprite;

	public UIUserPanelBuildInfo BuildFortress;
	public UIUserPanelBuildInfo BuildPort;
	public UIUserPanelBuildInfo BuildUniver;
	public UIUserPanelBuildInfo BuildTemple;
	#endregion

	[HideInInspector]
	public int player;	
	
	#region ViewWidgetsSet
	override protected void OnPanelOpen() {
		PlayerSprite.spriteName = "Player" + player + "Big";
		PlayerColorName.text = "Локрум (" + player + ")";
		PlayerShieldSprite.spriteName = "coat-of-arms" + player;

		CancelButtonSprite.SetImageButtonSprites(UIConsts.userColorsCancelButton[player], "1", "2");

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

		SetGoldCount(Sh.In.GameContext.GetInt("/markers/gold/[{0}]", player), Sh.In.GameContext.GetInt("/markers/income/[{0}]", player), (player == Library.GetCurrentPlayer(Sh.In.GameContext)));

		SetBuildSprite(BuildFortress, "build-fortress", Cyclades.Game.Constants.buildFortres);
		SetBuildSprite(BuildPort, "build-fortress", Cyclades.Game.Constants.buildMarina);
		SetBuildSprite(BuildUniver, "build-laboratory", Cyclades.Game.Constants.buildUniver);
		SetBuildSprite(BuildTemple, "build-haven", Cyclades.Game.Constants.buildTemple);
	}

	void SetBuildSprite(UIUserPanelBuildInfo build, string buildSpriteName, string buildType) {
		long buildsCount = Cyclades.Game.Library.Map_GetBuildCountAtPlayersIslands(Sh.In.GameContext, player, buildType);
		build.SetInfo((int)buildsCount, buildsCount > 0 ? buildSpriteName : "build-placeholder");
	}

	void SetGoldCount(int goldCount, int incomeCount, bool is_current_user) {
		IncomeLabel.text = "" + incomeCount;
		GoldLabel.text = (is_current_user ? "/ " + goldCount : "");

		GameObject prototipe = GoldSprites[0].gameObject;
		//TODO на одну меньше надо создавать
		for (int i = GoldSprites.Count; i <= incomeCount; ++i) {
			GameObject go = GameObject.Instantiate(prototipe) as GameObject;
			go.transform.parent = prototipe.transform.parent;
			UISprite sprite = go.GetComponent<UISprite>();
			sprite.MakePixelPerfect();
			sprite.depth = prototipe.GetComponent<UISprite>().depth + GoldSprites.Count;
			GoldSprites.Add(sprite);
		}
		prototipe.transform.parent.GetComponent<UIGrid>().Reposition();

		for (int i = 0; i < GoldSprites.Count; ++i) {
			GoldSprites[i].gameObject.SetActive(i < incomeCount);
		}
	}
	#endregion

	#region Events
	public void OnPressCancelButton() {
		Hide();
	}
	#endregion
}
