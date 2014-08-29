using System.Collections;
using System.Collections.Generic;

using Cyclades.Game;

class BuildMapEventer: IslandClickMapEventer {

	#region Events
	override public void Activate() {
		base.Activate();

		mapStates.Panel.SetTab(PanelType.MAP_TAB_PLACE_BUILD);

		if (Sh.GameState.currentUser != -1) { //todo совершенно лишнее в реальной игре условие
			allowedIslands = Library.Map_GetIslandsByOwner(Sh.In.GameContext, Sh.GameState.currentUser);
		}

		HighlightIslands(true);
	}
	#endregion

	#region Abstract
	override protected void OnClickIsland(int island) {
		UIMapSlotBuildPanel p = UIGamePanel.GetPanel<UIMapSlotBuildPanel>(PanelType.MAP_TAB_SLOT_BUILD);
		p.SetActiveIsland(island);
		mapStates.Panel.SetTab(PanelType.MAP_TAB_SLOT_BUILD);
	}
	#endregion

}