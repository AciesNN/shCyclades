using System.Collections;
using System.Collections.Generic;

using Cyclades.Game;
using Cyclades.Game.Client;

class RetreatUnitEventer: IslandClickMapEventer {
	
	public int fromIsland;
	public UIBattlePanel battlePanel;

	#region Events
	override public void Activate() {
		base.Activate();

		mapStates.Panel.SetTab(PanelType.MAP_TAB_RETREAT_UNIT);

		allowedIslands = Library.Map_GetBridgetIslands(Sh.In.GameContext, fromIsland, Sh.GameState.currentUser);
		HighlightIslands(true);

		battlePanel.SetShowContextVisible(false);
	}

	public override void Deactivate() {
		base.Deactivate();
	}
	#endregion

	#region Abstract
	override protected void OnClickIsland(int island) {
		Sh.Out.Send(Messanges.PassFightArmy(island));
		battlePanel.SetShowContextVisible(true);
		Sh.GameState.mapStates.SetEventorType(MapEventerType.DEFAULT);
	}
	#endregion
}
