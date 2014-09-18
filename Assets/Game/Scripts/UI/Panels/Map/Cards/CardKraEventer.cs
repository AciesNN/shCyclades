using System.Collections;
using System.Collections.Generic;

using Cyclades.Game;
using Cyclades.Game.Client;

class CardKraEventer: SeaClickMapEventer {
	
	#region Events
	override public void Activate() {
		base.Activate();
		mapStates.Panel.SetTab(PanelType.MAP_TAB_ACTION_AND_CANCEL);

		if (Sh.GameState.currentUser != -1) { //todo совершенно лишнее в реальной игре условие
			for(int x = 0; x < mapStates.MapController.XSize; ++x) {
				for(int y = 0; y < mapStates.MapController.YSize; ++y) {
					if(Library.Map_IsPointOnMap(Sh.In.GameContext, x, y)  && Library.Map_GetIslandByPoint(Sh.In.GameContext, x, y) == -1)
						allowedCells.Add(new GridPosition(x, y));
				}
			}
		}

		HighlightSeaCells(true);
	}
	#endregion

	#region Abstract
	override protected void OnClickSeaCell(GridPosition cell) {
		Sh.Out.Send( Messanges.UseCardKra(cell.x, cell.y) );
	}
	#endregion
}
