using System;
using System.Collections;
using System.Collections.Generic;

using Cyclades.Game;

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
		UIMapSlotBuildPanel p = UIGamePanel.GetPanel<UIMapSlotBuildPanel>(PanelType.MAP_TAB_SLOT_BUILD);
		p.SetActiveIsland(island);
		mapStates.Panel.SetTab(PanelType.MAP_TAB_SLOT_BUILD);
	}
	#endregion

	void CloseEventer() {
		Sh.GameState.mapStates.SetEventorType(MapEventerType.DEFAULT);
		UIGodPanel.inst.Reset();
	}
}