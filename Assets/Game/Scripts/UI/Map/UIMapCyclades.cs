using UnityEngine;
using System.Collections;

public class UIMapCyclades : UIMapHexController {

	public int CycladGridY;
	public int CycladGridX;

	void Awake() {
		YSize = CycladGridY * 2 - 1;
		XSize = CycladGridX + CycladGridY - 1;
		HexGridType = HexGridOrientation.HORIZONTAL;
	}

	public override GridPosition WorldPositionToCell(Vector3 pos) {
		
		GridPosition res = base.WorldPositionToCell(pos);
		
		return CellToCycladesCoord(res);
		
	}
	
	public override Vector3 CellToWorldPosition(GridPosition pos) {

		GridPosition cell = CycladesCoordToCell(pos);
		Vector3 res = base.CellToWorldPosition(cell);

		return res;

	}

	public override bool IsCellPossible(GridPosition cell) {

		return cell.x >= 0 && cell.y >= 0 && cell.y < YSize
			&& cell.x < CycladGridX + CycladGridY - 1 - System.Math.Abs(cell.y + 1 - CycladGridY); //	10 - (6 - 6)

	}


	GridPosition CycladesCoordToCell(GridPosition cell) {
		GridPosition res;
		
		// учтем перевернутый ыгрик 
		res = new GridPosition(cell.x, (CycladGridY - 1) - cell.y); //-1 потому что: например, высота 13, координата 0, надо получить 12, т.к. индексация с нуля

		// учтем разную длину разных линий
		res = new GridPosition(res.x + System.Math.Abs(cell.y - 5)/2, res.y);
		
		return res;
	}
	
	GridPosition CellToCycladesCoord(GridPosition cell) {
		GridPosition res = cell;
		
		// учтем перевернутый ыгрик 
		res = new GridPosition(res.x, (CycladGridY - 1) - res.y);
		
		// учтем разную длину разных линий
		res = new GridPosition(res.x - System.Math.Abs(res.y - 5)/2, res.y);
		
		return res;
	}
}
