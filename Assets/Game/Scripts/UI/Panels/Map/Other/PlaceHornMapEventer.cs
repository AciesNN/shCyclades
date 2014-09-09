using System.Collections;
using System.Collections.Generic;

using Cyclades.Game;
using Cyclades.Game.Client;

class PlaceHornMapEventer: IslandClickMapEventer {

	UIMapActionAndCancelPanel panel;

	#region Events
	override public void Activate() {
		base.Activate();

		panel = UIGamePanel.GetPanel<UIMapActionAndCancelPanel>(PanelType.MAP_TAB_ACTION_AND_CANCEL);
		//panel.SetDescription(fromIsland); //todo
		panel.SetCancelButtonEnabled(true); //todo

		mapStates.Panel.SetTab(PanelType.MAP_TAB_ACTION_AND_CANCEL);

		if (Sh.GameState.currentUser != -1) { //todo совершенно лишнее в реальной игре условие
			allowedIslands = Library.Map_GetIslandsByOwner(Sh.In.GameContext, Sh.GameState.currentUser);
		}

		HighlightIslands(true);
	}
	#endregion

	#region Abstract
	override protected void OnClickIsland(int island) {
		Sh.Out.Send(Messanges.PlaceHorn(island));
		Sh.Out.Send(Messanges.EndPlayerTurn()); //?
		Sh.GameState.mapStates.SetEventorType(MapEventerType.DEFAULT);
	}
	#endregion

}