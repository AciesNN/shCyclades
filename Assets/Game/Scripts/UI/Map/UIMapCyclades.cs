using UnityEngine;
using System.Collections;

public class UIMapCyclades : UIMapHexController {

	public int CycladGridY {
		get { return (YSize + 1) / 2; }
	}

	void Awake() {
		HexGridType = HexGridOrientation.HORIZONTAL;
	}

	public override GridPosition WorldPositionToCell(Vector3 pos) {
		
		GridPosition res = base.WorldPositionToCell(pos);
		
		return CellToCycladesCoord(res);
		
	}
	
	public override Vector3 CellToWorldPosition(GridPosition pos) {

		GridPosition cell = pos; //CycladesCoordToCell(pos);
		Vector3 res = base.CellToWorldPosition(cell);

		return res;

	}

	public override bool IsCellPossible(GridPosition cell) {

		GridPosition pos = CellToCycladesCoord(cell);

		return true;

	}


	GridPosition CycladesCoordToCell(GridPosition cell) {
		GridPosition res;
		
		//1. учтем перевернутый ыгрик 
		res = new GridPosition(cell.x, (CycladGridY - 1) - cell.y); //-1 потому что: например, высота 13, координата 0, надо получить 12, т.к. индексация с нуля
		
		//2. учтем бордюры
		res = new GridPosition(res.x + 1, res.y - 1); //+ и - потому, что в одном случае мы отступаем от начала координат, а потом - от конца (координаты Киклад идут от верхнего угла, а сетки - от нижнего)
		
		//3. учтем разную длину разных линий
		res = new GridPosition(res.x + System.Math.Abs(cell.y - 5)/2, res.y);
		
		return res;
	}
	
	GridPosition CellToCycladesCoord(GridPosition cell) {
		GridPosition res = cell;
		
		//2. учтем перевернутый ыгрик 
		res = new GridPosition(res.x, (CycladGridY - 1) - res.y);
		
		//2. учтем бордюры
		res = new GridPosition(res.x - 1, res.y - 1); 
		
		//3. учтем разную длину разных линий
		res = new GridPosition(res.x - System.Math.Abs(res.y - 5)/2, res.y);
		
		return res;
	}
}
