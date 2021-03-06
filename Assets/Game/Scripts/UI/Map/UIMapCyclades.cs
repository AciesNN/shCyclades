﻿using UnityEngine;
using System.Collections;

//using Cyclades.Game;

public class UIMapCyclades : UIMapHexController {

	public int CycladGridY;
	public int CycladGridX;

	public void GameContext_Init() {

		CycladGridX = (int)Sh.In.GameContext.GetLong("/map/size_x");
		CycladGridY = (int)Sh.In.GameContext.GetLong("/map/size_y");

		YSize = CycladGridY * 2 - 1;
		XSize = CycladGridX + CycladGridY - 1;
		HexGridType = HexGridOrientation.HORIZONTAL;
		odd = (CycladGridY % 2 != 0); //Y потому, что она - горизонтал

	}

	void Awake() {
	}

	public override GridPosition WorldPositionToCell(Vector3 pos) {
		
		GridPosition res = base.WorldPositionToCell(pos);
		
		return CellToCycladesCoord(res);
		
	}

	public override Vector3 CellToWorldPosition(GridPosition pos, float z) {

		GridPosition cell = CycladesCoordToCell(pos);
		Vector3 res = base.CellToWorldPosition(cell, z);

		return res;

	}

	public override bool IsCellPossible(GridPosition cell) {

		return cell.x >= 0 && cell.y >= 0 && cell.y < YSize
			&& cell.x < CycladGridX + CycladGridY - 1 - System.Math.Abs(cell.y + 1 - CycladGridY); //	10 - (6 - 6)

	}


	GridPosition CycladesCoordToCell(GridPosition cell) {
		GridPosition res;
		
		// учтем перевернутый ыгрык 
		res = new GridPosition(cell.x, (YSize - 1) - cell.y); //-1 потому что: например, высота 13, координата 0, надо получить 12, т.к. индексация с нуля

		// учтем разную длину разных линий
		res = new GridPosition(res.x + System.Math.Abs(cell.y - CycladGridY + 1) / 2, res.y);
		
		return res;
	}
	
	GridPosition CellToCycladesCoord(GridPosition cell) {
		GridPosition res = cell;
		
		// учтем перевернутый ыгрык 
		res = new GridPosition(res.x, (YSize - 1) - res.y);
		
		// учтем разную длину разных линий
		res = new GridPosition(res.x - System.Math.Abs(res.y - CycladGridY + 1) / 2, res.y);
		
		return res;
	}
}
