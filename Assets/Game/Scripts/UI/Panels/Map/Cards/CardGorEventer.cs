using System.Collections;
using System.Collections.Generic;

using Cyclades.Game;
using Cyclades.Game.Client;

class CardGorEventer: IslandClickMapEventer {
	
	#region Events
	override public void Activate() {
		base.Activate();
		mapStates.Panel.SetTab(PanelType.MAP_TAB_ACTION_AND_CANCEL);

		int c = Sh.In.GameContext.GetList ("/map/islands/owners").Count;
		allowedIslands = new List<long>();
		for (long i = 0; i < c; ++i)
			allowedIslands.Add(i);

		HighlightIslands(true);
	}
	#endregion

	#region Abstract
	override protected void OnClickIsland(int island) {
		Sh.Out.Send(Messanges.UseCardGor(island));
	}
	#endregion
}
