using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class UIMapController : MonoBehaviour {

	public int XSize, YSize;
	public int CellXSize, CellYSize;
	public Transform ZerroPoint;

	protected virtual Vector2 GetZerroPoint() {
		return new Vector2(ZerroPoint.localPosition.x, ZerroPoint.localPosition.y);
	}

	public virtual GridPosition WorldPositionToCell(Vector3 pos) {
		Vector2 pos2d = new Vector2(pos.x, pos.y);
		Vector2 nPos = pos2d - GetZerroPoint();
		GridPosition res = new GridPosition(nPos.x / CellXSize, nPos.y / CellYSize);
		return res;
	}

	public virtual Vector3 CellToWorldPosition(GridPosition cell) {
		Vector2 res = new Vector2((cell.x + 0.5f) * CellXSize, (cell.y + 0.5f) * CellYSize);
		res += GetZerroPoint();
		return new Vector3(res.x, res.y, 0);
	}

	protected Vector2 GetCenter() {
		return new Vector2(CellXSize * XSize / 2.0f, CellYSize * YSize / 2.0f);
	}

	public void SetCenterToDefaultPoint() {
		//1. calculate the center
		Vector2 center = GetCenter(); new Vector2();

		//2. set center to initial zerro point
		ZerroPoint.localPosition -= new Vector3(center.x, center.y, 0);

	}

}
