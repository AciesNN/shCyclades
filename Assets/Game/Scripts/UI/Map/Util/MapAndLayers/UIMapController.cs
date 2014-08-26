using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class UIMapController : MonoBehaviour {

	public int XSize, YSize;
	public int CellXSize, CellYSize;
	public Transform ZerroPoint;

	Dictionary<GridLayerType, UIMapLayer> layers = new Dictionary<GridLayerType, UIMapLayer>();

	void Start() {
	}

	/*public virtual void LateInit() {
		foreach(UIMapLayer layer in layers.Values) {
			layer.LateInit();
		}
		
		//SetCenterToDefaultPoint();
	}*/

	#region Math, Geometry
	public virtual Vector2 GetSize() {
		return new Vector2(CellXSize * XSize, CellYSize * YSize);
	}

	protected virtual Vector2 GetZerroPoint() {
		return /*new Vector2(0,0);//*/new Vector2(ZerroPoint.localPosition.x, ZerroPoint.localPosition.y);
	}

	public virtual GridPosition WorldPositionToCell(Vector3 pos) {
		Vector2 pos2d = new Vector2(pos.x, pos.y);
		Vector2 nPos = pos2d - GetZerroPoint();
		GridPosition res = new GridPosition(nPos.x / CellXSize, nPos.y / CellYSize);
		return res;
	}

	public virtual Vector3 CellToWorldPosition(GridPosition cell) {
		Vector2 res = new Vector2((cell.x + 0.5f) * CellXSize, (cell.y + 0.5f) * CellYSize);
		//res += GetZerroPoint();
		return new Vector3(res.x, res.y, 0);
	}

	protected Vector2 GetCenter() {
		Vector2 res = GetSize();
		res.Scale(new Vector2(0.5f, 0.5f));
		return res;
	}

	public void SetCenterToDefaultPoint() {
		//1. calculate the center
		Vector2 center = GetCenter(); new Vector2();

		//2. set center to initial zerro point
		ZerroPoint.localPosition -= new Vector3(center.x, center.y, 0);
	}

	public virtual bool IsCellPossible(GridPosition cell) {
		return cell.x >= 0 && cell.x < XSize 
			&& cell.y >= 0 && cell.y < YSize;
	}
	#endregion

	#region Layers
	public void RegisterGridLayer(UIMapLayer layer) {
		layers[layer.type] = layer;
	}

	public UIMapLayer GetLayer(GridLayerType type) {
		return layers[type];
	}

	public T GetLayer<T>(GridLayerType type) where T: UIMapLayer {
		return layers[type] as T;
	}

	public void ActivateLayer(GridLayerType type, bool turnOn) {
		GetLayer(type).gameObject.SetActive(turnOn);
	}
	#endregion
}
