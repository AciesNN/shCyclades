using UnityEngine;
using System.Collections;

using Cyclades.Game;
using Cyclades.Game.Client;

public class UIGodPanel : UIGamePanel {

	public static UIGodPanel inst;

	#region ViewWidgets
	public UIGodPanelAction[] actions;
	public UISprite godSprite;
	public UISprite playerSpriteRing;

	public GameObject AdditionalInfoObject;
	public UILabel AdditionalInfoText;
	#endregion

	void Awake() {
		inst = this;
	}

	public void SetPlayer(int player) {
		int cur_player = (int)Library.GetCurrentPlayer(Sh.In.GameContext);
		playerSpriteRing.spriteName = UIConsts.userColorsString[cur_player] + "-ring1";

		for (int i = 0; i < actions.Length; ++i) {
			actions[i].SetPlayer(player);
		}
	}

	void SetGod(string god) {
		godSprite.spriteName = UIConsts.godSpritesString[god];

		switch (god) {
			case Cyclades.Game.Constants.godAppolon:
				SetApollo();
				break;
			case Cyclades.Game.Constants.godMars:
				SetMars();
				break;
			case Cyclades.Game.Constants.godPoseidon:
				SetPoseidon();
				break;
			case Cyclades.Game.Constants.godSophia:
				SetSophia();
				break;
			case Cyclades.Game.Constants.godZeus:
				SetZeus();
				break;
			default:
				Debug.DebugBreak();
				break;
		}

		SetAdditionalText("");
	}

	public void Reset() {
		string currentGod = Sh.In.GameContext.GetStr("/turn/current_god");
		SetGod(currentGod);
	}

	public void SetAdditionalText(string additionalText) {
		AdditionalInfoObject.SetActive(additionalText != "");
		AdditionalInfoText.text = additionalText;
	}

	//TODO во всех функциях богов надо бы вычислять доступность кнопок, в том числе и "конца хода" и их цену

	public void SetApollo() {
		//todo доступность кнопки с рогом должна зависеть от того, первый ли на апполоне
		actions[0].SetActionSprite(UIConsts.SPRITE_APOLLO_ACTION_HORN);
		actions[0].SetPrice(0);
		actions[0].click = OnPlaceHorn;

		actions[1].SetActionSprite("");
		actions[2].SetActionSprite("");

		Sh.GameState.mapStates.SetEventorType(MapEventerType.DEFAULT);
	}

	public void SetMars() {
		TabloidPanel.inst.SetText("Боги огня ждут указаний!");

		actions[0].SetActionSprite("icon-fort");
		actions[0].SetPrice(Cyclades.Game.Constants.buildingCost);
		actions[0].click = OnBuildClick;

		actions[1].SetActionSprite("icon-unit");
		actions[1].SetPrice(2);
		actions[1].click = OnBuyUnitClick;

		actions[2].SetActionSprite("exit");
		actions[2].SetPrice(0);
		actions[2].click = OnEndTurn;

		Sh.GameState.mapStates.SetEventorType(MapEventerType.MOVEUNIT);
	}

	public void SetPoseidon() {
		TabloidPanel.inst.SetText("Великая пучина внемлет тебе!");

		actions[0].SetActionSprite("port");
		actions[0].SetPrice(Cyclades.Game.Constants.buildingCost);
		actions[0].click = OnBuildClick;

		actions[1].SetActionSprite("ship");
		actions[1].SetPrice(2);
		actions[1].click = OnBuyNavyClick;

		actions[2].SetActionSprite("exit");
		actions[2].SetPrice(0);
		actions[2].click = OnEndTurn;

		UIMapStates.inst.SetEventorType(MapEventerType.MOVESHIP); 
	}

	public void SetSophia() {

		actions[0].SetActionSprite(UIConsts.SPRITE_GOD_ACTION_BUILD);
		actions[0].SetPrice(Cyclades.Game.Constants.buildingCost);
		actions[0].click = OnBuildClick;

		actions[1].SetActionSprite(UIConsts.SPRITE_SOPHIA_ACTION_BUY_MAN);
		actions[1].SetPrice(Cyclades.Game.Constants.philosopherPrice[1]);
		actions[1].click = OnBuyPhilosotherClick;

		actions[2].SetActionSprite("exit");
		actions[2].SetPrice(0);
		actions[2].click = OnEndTurn;

		Sh.GameState.mapStates.SetEventorType(MapEventerType.DEFAULT);
	}

	public void SetZeus() {

		actions[0].SetActionSprite(UIConsts.SPRITE_GOD_ACTION_BUILD);
		actions[0].SetPrice(Cyclades.Game.Constants.buildingCost);
		actions[0].click = OnBuildClick;

		actions[1].SetActionSprite(UIConsts.SPRITE_ZEUS_ACTION_BUY_MAN);
		actions[1].SetPrice(Cyclades.Game.Constants.priestPrice[1]);
		actions[1].click = OnBuyPriestClick;

		actions[2].SetActionSprite("exit");
		actions[2].SetPrice(0);
		actions[2].click = OnEndTurn;

		Sh.GameState.mapStates.SetEventorType(MapEventerType.DEFAULT);
	}


	#region Events
	public void OnAction1Click() {
		actions[0].click();
	}

	public void OnAction2Click() {
		actions[1].click();
	}

	public void OnAction3Click() {
		actions[2].click();
	}

	//////////////////////////////////
	public void OnEndTurn() {
		Sh.Out.Send(Messanges.EndPlayerTurn());
	}

	public void OnBuildClick() {
		Sh.GameState.mapStates.SetEventorType(MapEventerType.PLACEBUILD);
	}

	public void OnBuyPriestClick() {
		Sh.Out.Send(Messanges.BuyPriest());
	}

	public void OnBuyPhilosotherClick() {
		Sh.Out.Send(Messanges.BuyPhilosopher());
	}

	public void OnCardChangeClick() {
		Sh.Out.Send(Messanges.ChangeCard());
	}

	public void OnBuyNavyClick() {
		Sh.GameState.mapStates.SetEventorType(MapEventerType.PLACESHIP);
	}

	public void OnBuyUnitClick() {
		Sh.GameState.mapStates.SetEventorType(MapEventerType.PLACEUNIT);
	}

	public void OnPlaceHorn() {
		Sh.GameState.mapStates.SetEventorType(MapEventerType.PLACEHORN);
	}
	#endregion
}
