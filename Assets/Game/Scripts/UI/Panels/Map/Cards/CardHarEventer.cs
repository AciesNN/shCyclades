using System.Collections;
using System.Collections.Generic;

using Cyclades.Game;
using Cyclades.Game.Client;

class CardHarEventer: IslandClickMapEventer {
	
	#region Events
	override public void Activate() {
		base.Activate();
		mapStates.Panel.SetTab(PanelType.MAP_TAB_ACTION_AND_CANCEL);

		List<object> armies = Sh.In.GameContext.GetList ("/map/islands/army");
		allowedIslands = new List<long>();
		for (int i = 0; i < armies.Count; ++i)
			if ((long)armies[i] > 0 && Sh.In.GameContext.GetLong ("/map/island/owners/[{0}]", i) != Sh.GameState.currentUser)
				allowedIslands.Add(i);

		HighlightIslands(true);
	}
	#endregion

	#region Abstract
	override protected void OnClickIsland(int island) {
		Sh.Out.Send(Messanges.UseCardHar(island));
	}
	#endregion
}
