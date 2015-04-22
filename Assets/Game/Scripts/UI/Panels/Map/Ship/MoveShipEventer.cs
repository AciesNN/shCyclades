using System.Collections;
using System.Collections.Generic;

using Cyclades.Game;
using Cyclades.Game.Client;

class MoveShipEventer: SeaClickMapEventer {

	bool isFirstCall;
	GridPosition firstCell;
	int count;
	int max_units_count;
	int countOfMovement;

	GridPosition _lastSeaCell {
		get {
			if (isFirstCall)
				return firstCell;
			GridPosition cell = new GridPosition( Sh.In.GameContext.GetList("/fight/navy/last_coords") );
			if (cell.IsLessThanZero()) {
				return firstCell;
			} else {
				return cell;
			}
		}
	}

	#region Events
	override public void Activate() {
		base.Activate();
		isFirstCall = true;
		firstCell = GridPosition.LessThanZero();
		ReInit();
	}

	override public void ReActivate() {
		ReInit ();
	}

	void OnCountUp() {
		if (count >= max_units_count)
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

	public void OnClickCancel() {
		Sh.Out.Send(Messanges.CancelMoveNavy());
		CloseEventer();
	}
	#endregion

	#region Abstract
	override protected void OnClickSeaCell(GridPosition cell) {
		HighlightSeaCells(false);
		if (isFirstCall) {
			isFirstCall = false;
			firstCell = cell;
			Sh.Out.Send(Messanges.StartMoveNavy());
			ReInit();
		} else {
			GridPosition lastSeaCell = _lastSeaCell;
			Sh.Out.Send(Messanges.MoveNavy(lastSeaCell.x, lastSeaCell.y, cell.x, cell.y, count));
		}
	}
	#endregion

	void UIInit() {
		if (!isFirstCall) {
			TabloidPanel.inst.SetText("Укажите, куда переместить корабли и сколько.");

			UIGodPanel.inst.godSprite.spriteName = "pic-ship_move";

			UIGodPanel.inst.actions[0].SetActionSprite("arrow-up");
			UIGodPanel.inst.actions[0].SetPrice(0);
			UIGodPanel.inst.actions[0].click = OnCountUp;

			UIGodPanel.inst.actions[1].SetActionSprite("arrow-down");
			UIGodPanel.inst.actions[1].SetPrice(0);
			UIGodPanel.inst.actions[1].click = OnCountDown;

			UIGodPanel.inst.actions[2].SetActionSprite(countOfMovement  == 3 ? "exit" : "OK");
			UIGodPanel.inst.actions[2].SetPrice(0);
			UIGodPanel.inst.actions[2].click = OnClickCancel;

			UIGodPanel.inst.SetAdditionalText("" + count);
		}
	}

	void ReInit() {

		countOfMovement = Sh.In.GameContext.GetInt("/fight/navy/move");
		GridPosition lastSeaCell = _lastSeaCell;

		if (!lastSeaCell.IsLessThanZero()) {
			max_units_count = Library.Map_GetShipCountByPoint(Sh.In.GameContext, lastSeaCell.x, lastSeaCell.y);
			count = max_units_count;
		} else {
			count = 1; //useless code
		}
		
		if (lastSeaCell.IsLessThanZero()) {
			CalculateAllowedCellsFrom();
		} else {
			CalculateAllowedCellsTo();
		}
		HighlightSeaCells(!isFirstCall);		

		UIInit();
	}

	void CalculateAllowedCellsFrom() {
		List<object> owners = Sh.In.GameContext.GetList ("/map/seas/ships/owners");

		for(int i = 0; i < owners.Count; ++i) {
			if ((long)owners[i] == Sh.GameState.currentUser) {
				GridPosition cell = new GridPosition( Sh.In.GameContext.GetList ("/map/seas/ships/coords/[{0}]", i) );
				if(!allowedCells.Contains(cell))
					allowedCells.Add(cell);
			}
		}
	}
	
	void CalculateAllowedCellsTo() {
		GridPosition lastSeaCell = _lastSeaCell;

		allowedCells = new List<GridPosition>();

		List<Shmipl.FrmWrk.Library.Coords> seaCoords = Library.Map_GetPointNeighbors(Sh.In.GameContext, lastSeaCell.x, lastSeaCell.y);
		foreach(Shmipl.FrmWrk.Library.Coords seaCoord in seaCoords) {
			if (Library.Map_IsPointAccessibleForShip(Sh.In.GameContext, seaCoord.x, seaCoord.y, Sh.GameState.currentUser, true)) {
				allowedCells.Add(new GridPosition(seaCoord.x, seaCoord.y));
			}
		}
	}

	void CloseEventer() {
		Sh.GameState.mapStates.SetEventorType(MapEventerType.DEFAULT);
		UIGodPanel.inst.Reset();
	}
}
