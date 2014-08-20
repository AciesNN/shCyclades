using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIMapHexController : UIMapController {

	public HexGridOrientation HexGridType;
	protected bool odd; //какие же ряды сдвигать

	public override GridPosition WorldPositionToCell(Vector3 pos) {

		Vector2 pos2d = new Vector2(pos.x, pos.y);
		Vector2 nPos = pos2d - GetZerroPoint();
		GridPosition res = new GridPosition(nPos.x / CellXSize, nPos.y / CellYSize);

		if (HexGridType == HexGridOrientation.VERTICAL			&& (res.x % 2 != 0 && odd || res.x % 2 == 0 && !odd)) {
			res.y = (int)System.Math.Floor((nPos.y - CellYSize * 0.5f) / CellYSize);
		} else if (HexGridType == HexGridOrientation.HORIZONTAL && (res.y % 2 != 0 && odd || res.y % 2 == 0 && !odd)) {
			res.x = (int)System.Math.Floor((nPos.x - CellXSize * 0.5f) / CellXSize);
		}

		return res;

	}
	
	public override Vector3 CellToWorldPosition(GridPosition cell) {

		float x = (cell.x + 0.5f + (HexGridType == HexGridOrientation.HORIZONTAL	&& (cell.y % 2 != 0 && odd || cell.y % 2 == 0 && !odd) ? 0.5f : 0.0f)) * CellXSize;
		float y = (cell.y + 0.5f + (HexGridType == HexGridOrientation.VERTICAL		&& (cell.x % 2 != 0 && odd || cell.x % 2 == 0 && !odd) ? 0.5f : 0.0f)) * CellYSize;

		Vector2 res = new Vector2(x, y);
		//res += GetZerroPoint();
		return new Vector3(res.x, res.y, 0);
	}

}

public enum HexGridOrientation {
	HORIZONTAL,
	VERTICAL
}