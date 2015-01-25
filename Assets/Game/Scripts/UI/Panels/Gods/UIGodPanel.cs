using UnityEngine;
using System.Collections;

using Cyclades.Game.Client;

public class UIGodPanel : UIGamePanel {

	#region ViewWidgets
	public UIGodPanelAction[] actions;
	public UISprite godSprite;
	#endregion

	void Awake() {
	}

	public void SetGod(string god) {
		godSprite.spriteName = UIConsts.godSprites[god];

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
	}

	//TODO во всех функциях богов надо бы вычислять доступность кнопок, в том числе и "конца хода" и их цену

	public void SetApollo() {
		//todo доступность кнопки с рогом долдна зависеть от того, первый ли на апполоне
		actions[0].SetSprites(UIConsts.SPRITE_APOLLO_ACTION_HORN, "");
		actions[0].SetPrice(0);

		actions[1].SetSprites("", "");
		actions[2].SetSprites("", "");
		actions[3].SetSprites("", "");
	}

	public void SetMars() {
		actions[0].SetSprites(UIConsts.SPRITE_GOD_ACTION_BUILD, "");
		actions[0].SetPrice(Cyclades.Game.Constants.buildingCost);
		actions[0].click = OnBuildClick;

		actions[1].SetSprites(UIConsts.SPRITE_MARS_ACTION_BUY_UNIT, "");
		actions[1].SetPrice(2);
		actions[1].click = OnBuyUnitClick;

		actions[2].SetSprites(UIConsts.SPRITE_MARS_ACTION_MOVE_UNIT, UIConsts.SPRITE_MARS_ACTION_MOVE_UNIT_ADD);
		actions[2].SetPrice(Cyclades.Game.Constants.moveArmyCost);
		actions[2].click = OnMoveUnitClick;

		actions[3].SetSprites(UIConsts.SPRITE_GOD_ACTION_ENDTURN, "");
		actions[3].SetPrice(0);
	}

	public void SetPoseidon() {
		actions[0].SetSprites(UIConsts.SPRITE_GOD_ACTION_BUILD, "");
		actions[0].SetPrice(Cyclades.Game.Constants.buildingCost);
		actions[0].click = OnBuildClick;

		actions[1].SetSprites(UIConsts.SPRITE_MARS_ACTION_BUY_UNIT, "");
		actions[1].SetPrice(2);
		actions[1].click = OnBuyNavyClick;

		actions[2].SetSprites(UIConsts.SPRITE_POSEIDON_ACTION_MOVE_UNIT, UIConsts.SPRITE_POSEIDON_ACTION_MOVE_UNIT_ADD);
		actions[2].SetPrice(Cyclades.Game.Constants.moveNavyCost);
		actions[2].click = OnMoveNavyClick;

		actions[3].SetSprites(UIConsts.SPRITE_GOD_ACTION_ENDTURN, "");
		actions[3].SetPrice(0);
	}

	public void SetSophia() {
		actions[0].SetSprites(UIConsts.SPRITE_GOD_ACTION_BUILD, "");
		actions[0].SetPrice(Cyclades.Game.Constants.buildingCost);
		actions[0].click = OnBuildClick;

		actions[1].SetSprites(UIConsts.SPRITE_SOPHIA_ACTION_BUY_MAN, "");
		actions[1].SetPrice(Cyclades.Game.Constants.philosopherPrice[1]);
		actions[1].click = OnBuyPhilosotherClick;

		actions[2].SetSprites("", "");

		actions[3].SetSprites(UIConsts.SPRITE_GOD_ACTION_ENDTURN, "");
		actions[3].SetPrice(0);
	}

	public void SetZeus() {
		actions[0].SetSprites(UIConsts.SPRITE_GOD_ACTION_BUILD, "");
		actions[0].SetPrice(Cyclades.Game.Constants.buildingCost);
		actions[0].click = OnBuildClick;

		actions[1].SetSprites(UIConsts.SPRITE_ZEUS_ACTION_BUY_MAN, "");
		actions[1].SetPrice(Cyclades.Game.Constants.priestPrice[1]);
		actions[1].click = OnBuyPriestClick;

		actions[2].SetSprites(UIConsts.SPRITE_ZEUS_ACTION_CHANGE_CARD, "");
		actions[2].SetPrice(Cyclades.Game.Constants.changeCardCost);
		actions[2].click = OnCardChangeClick;

		actions[3].SetSprites(UIConsts.SPRITE_GOD_ACTION_ENDTURN, "");
		actions[3].SetPrice(0);
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

	public void OnEndTurn() {
		Sh.Out.Send(Messanges.EndPlayerTurn());
	}

	//////////////////////////////////
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

	public void OnMoveUnitClick() {
		Sh.GameState.mapStates.SetEventorType(MapEventerType.MOVEUNIT);
	}

	public void OnMoveNavyClick() {
		Sh.Out.Send(Messanges.StartMoveNavy());
	}

	public void OnBuyNavyClick() {
		Sh.GameState.mapStates.SetEventorType(MapEventerType.PLACESHIP);
	}

	public void OnBuyUnitClick() {
		Sh.GameState.mapStates.SetEventorType(MapEventerType.PLACEUNIT);
	}
	#endregion
}
