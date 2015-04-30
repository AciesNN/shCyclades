using System;
using System.Collections;
using System.Collections.Generic;

using Cyclades.Game;
using Cyclades.Game.Client;

class BuildMapEventer: IslandClickMapEventer {

	#region Events
	override public void Activate() {
		base.Activate();

		UIInit();

		if (Sh.GameState.currentUser != -1) { //todo совершенно лишнее в реальной игре условие
			allowedIslands = Library.Map_GetIslandsByOwner(Sh.In.GameContext, Sh.GameState.currentUser);
		}

		HighlightIslands(true);
	}

	public void OnPanelSlotBuildCancel() {
		UIInit();
	}

	public void OnPanelSlotBuildOK() {
		CloseEventer();
	}

	void OnClickCancel() {
		CloseEventer();
	}
	#endregion

	void UIInit() {
		String currentGod = Sh.In.GameContext.GetStr("/turn/current_god");

		String buildName = "";
		String spriteName = "";

		switch (currentGod) {
			case Cyclades.Game.Constants.godPoseidon:
				buildName = "Гавани";
				spriteName = "pic-port";
				break;
			case Cyclades.Game.Constants.godMars:
				buildName = "Форта";
				spriteName = "fort-icon";
				break;
			case Cyclades.Game.Constants.godSophia:
				buildName = "Университета";
				spriteName = "pic-port";
				break;
			case Cyclades.Game.Constants.godZeus:
				buildName = "Храма";
				spriteName = "fort-icon";
				break;
		}
		TabloidPanel.inst.SetText("Выберите место для постройки " + buildName  + ".");
		UIGodPanel.inst.godSprite.spriteName = spriteName;

		UIGodPanel.inst.actions[0].SetActionSprite("");
		UIGodPanel.inst.actions[0].SetPrice(0);
		UIGodPanel.inst.actions[0].click = null;

		UIGodPanel.inst.actions[1].SetActionSprite("");
		UIGodPanel.inst.actions[1].SetPrice(0);
		UIGodPanel.inst.actions[1].click = null;

		UIGodPanel.inst.actions[2].SetActionSprite("exit");
		UIGodPanel.inst.actions[2].SetPrice(0);
		UIGodPanel.inst.actions[2].click = OnClickCancel;

		UIGodPanel.inst.SetAdditionalText("");
	}

	#region Abstract
	override protected void OnClickIsland(int island) {
		List<string> buildings = Sh.In.GameContext.GetList<string>("/map/islands/buildings/[{0}]", island);

		bool is_metro = Sh.In.GameContext.GetBool("/map/islands/is_metro/[{0}]", island);
		int min_slot = (is_metro ? Library.Map_IslandMetroSizeByIslandSize(buildings.Count) : 0);
		int slot = -1;
		for (int slotN = buildings.Count - 1; slotN >= min_slot; --slotN) {
			if (buildings[slotN] == Constants.buildNone) {
				slot = slotN;
				break;
			}
		}

		if (slot >= 0)
			Sh.Out.Send(Messanges.BuyBuild(island, slot));

		//TODO а учитывается ли при создании списка доступных островов наличие свободного места?
	}
	#endregion

	void CloseEventer() {
		Sh.GameState.mapStates.SetEventorType(MapEventerType.DEFAULT);
		UIGodPanel.inst.Reset();
	}
}