using System.Collections;
using System.Collections.Generic;

using Cyclades.Game;
using Cyclades.Game.Client;

class CardPegEventer: IslandClickMapEventer {
	
	int fromIsland;
	UIMapMoveUnitPanel panel;

	#region Events
	override public void Activate() {
		base.Activate();

		fromIsland = -1;

		panel = UIGamePanel.GetPanel<UIMapMoveUnitPanel>(PanelType.MAP_TAB_MOVE_UNIT);
		panel.SetDescription(fromIsland);
		panel.SetUnitsVisible(false);

		mapStates.Panel.SetTab(PanelType.MAP_TAB_MOVE_UNIT);

		CalculateAllowedIslandsFrom();
		HighlightIslands(true);
	}
	#endregion

	#region Abstract
	override protected void OnClickIsland(int island) {
		if (fromIsland == -1) {
			fromIsland = island;
			panel.SetDescription(fromIsland);
			panel.SetUnitsVisible(true);
			panel.SetUnitMaxCount( Sh.In.GameContext.GetInt ("/map/islands/army/[{0}]", island) );
			panel.SetUnitCount(1);

			HighlightIslands(false);
			CalculateAllowedIslandsTo();
			HighlightIslands(true);
		} else {
			Sh.Out.Send(Messanges.UseCardPeg(fromIsland, island, panel.activeUnitCount));
		}
	}
	#endregion

	void CalculateAllowedIslandsFrom() {
		if (Sh.GameState.currentUser != -1) { //todo совершенно лишнее в реальной игре условие
			List<long> islands = Library.Map_GetIslandsByOwner(Sh.In.GameContext, Sh.GameState.currentUser);
			foreach(long island in islands) {
				if(Sh.In.GameContext.GetInt ("/map/islands/army/[{0}]", island) > 0 && Library.Map_GetBridgetIslands(Sh.In.GameContext, island, Sh.GameState.currentUser).Count > 0)
					allowedIslands.Add(island);
			}
		}
	}

	void CalculateAllowedIslandsTo() {
		//все острова, кроме того, с которого перемещаемся
		int c = Sh.In.GameContext.GetList ("/map/islands/owner").Count;
		allowedIslands = new List<long>();
		for (long i = 0; i < c; ++i)
			if (i != fromIsland)
				allowedIslands.Add(i);		
	}
}
