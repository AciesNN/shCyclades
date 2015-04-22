using System.Collections;
using System.Collections.Generic;

using Cyclades.Game;
using Cyclades.Game.Client;

class PlaceUnitMapEventer : IslandClickMapEventer {

	int count;

	#region Events
	override public void Activate() {
		base.Activate();

		count = 1;

		UIInit();

		if (Sh.GameState.currentUser != -1) { //todo совершенно лишнее в реальной игре условие
			allowedIslands = Library.Map_GetIslandsByOwner(Sh.In.GameContext, Sh.GameState.currentUser);
		}
		
		HighlightIslands(true);
	}

	void OnClickCancel() {
		CloseEventer();
	}

	void OnCountUp() {
		if (count >= 8)
			return;
		count++;
		UIGodPanel.inst.SetAdditionalText("" + count);
	}

	void OnCountDown() {
		if (count <= 1)
			return;
		count--;
		UIGodPanel.inst.SetAdditionalText("" + count);
	}
	#endregion

	#region Abstract
	override protected void OnClickIsland(int island) {
		for (int i = 0; i < count; ++i )
			Sh.Out.Send(Messanges.BuyArmy(island));
		Sh.GameState.mapStates.SetEventorType(MapEventerType.DEFAULT);
	}
	#endregion

	void UIInit() {
		TabloidPanel.inst.SetText("Выберите остров для размещения армии");

		UIGodPanel.inst.godSprite.spriteName = "unit";

		UIGodPanel.inst.actions[0].SetActionSprite("arrow-up");
		UIGodPanel.inst.actions[0].SetPrice(0);
		UIGodPanel.inst.actions[0].click = OnCountUp;

		UIGodPanel.inst.actions[1].SetActionSprite("arrow-down");
		UIGodPanel.inst.actions[1].SetPrice(0);
		UIGodPanel.inst.actions[1].click = OnCountDown;

		UIGodPanel.inst.actions[2].SetActionSprite("exit");
		UIGodPanel.inst.actions[2].SetPrice(0);
		UIGodPanel.inst.actions[2].click = OnClickCancel;

		UIGodPanel.inst.SetAdditionalText("" + count);
	}

	void CloseEventer() {
		Sh.GameState.mapStates.SetEventorType(MapEventerType.DEFAULT);
		UIGodPanel.inst.Reset();
	}
}