using System.Collections;
using System.Collections.Generic;

using Cyclades.Game;
using Cyclades.Game.Client;

class PlaceShipMapEventer: SeaClickMapEventer {

	int count;

	#region Events
	override public void Activate() {
		base.Activate();

		count = 1;

		UIInit();

		if (Sh.GameState.currentUser != -1) { //todo совершенно лишнее в реальной игре условие
			List<long> islands = Library.Map_GetIslandsByOwner(Sh.In.GameContext, Sh.GameState.currentUser);
			foreach(long island in islands) {

				List<Shmipl.FrmWrk.Library.Coords> seaCoords = Library.Map_GetSeasNearIsland(Sh.In.GameContext, island);

				foreach(Shmipl.FrmWrk.Library.Coords seaCoord in seaCoords) {
					if (Library.Map_IsPointAccessibleForShip(Sh.In.GameContext, seaCoord.x, seaCoord.y, Sh.GameState.currentUser, false)) {
						GridPosition cell = new GridPosition(seaCoord.x, seaCoord.y);
						if (!allowedCells.Contains(cell))
						    allowedCells.Add(cell);
					}
				}

			}
		}
		
		HighlightSeaCells(true);
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

	void UIInit() {
		TabloidPanel.inst.SetText("Выберите место для размещения корабля");

		UIGodPanel.inst.godSprite.spriteName = "pic-ship";

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

	#region Abstract
	override protected void OnClickSeaCell(GridPosition cell) {
		for ( int i = 0; i < count; ++i )
			Sh.Out.Send(Messanges.BuyNavy(cell.x, cell.y));
		CloseEventer();
	}
	#endregion

	void CloseEventer() {
		Sh.GameState.mapStates.SetEventorType(MapEventerType.DEFAULT);
		UIGodPanel.inst.Reset();
	}
}
