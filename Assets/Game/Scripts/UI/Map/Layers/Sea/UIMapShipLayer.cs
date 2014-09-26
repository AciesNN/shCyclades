using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Cyclades.Game;

public class UIMapShipLayer : UIMapGridLayer {

	public override void CreateGridElements() {
		elements = new UIMapShipElement[MapController.XSize, MapController.YSize];
	}

	public void GameContext_UpdateData() {
		for(int x = 0; x < MapController.XSize; ++x) {
			for(int y = 0; y < MapController.YSize; ++y) {
				GridPosition cell = new GridPosition(x, y);
				if(MapController.IsCellPossible(cell) && Library.Map_GetIslandByPoint(Sh.In.GameContext, x, y) == -1) {
					CreateShip(cell);
				}
			}
		}
	}

	public UIMapShipElement CreateShip(GridPosition cell) {
		UIMapShipElement el = elements[cell.x, cell.y] as UIMapShipElement;
		if (elements[cell.x, cell.y] == null) {
			el = CreateElement<UIMapShipElement>(cell);
		}

		int owner = (int)Library.Map_GetPointOwner(Sh.In.GameContext, cell.x, cell.y);
		int count = Library.Map_GetShipCountByPoint(Sh.In.GameContext, cell.x, cell.y);

		el.SetCount(count);
		el.SetOwner(owner);

		return el;
	}

	public void GameContext_ShowAnimation(Hashtable msg) {
		if (msg.ContainsKey("Animation") && msg["Animation"] is string) {//TODO надо придумать красивый фильтр
			string animation = (string)msg["Animation"];
			if (animation == "MoveShip") {
				long x_from = (long)msg["x_from"]; long x_to = (long)msg["x_to"]; long y_from = (long)msg["y_from"]; long y_to = (long)msg["y_to"];
				long count = (long)msg["count"];
				CreateAnimation_MoveShip(new GridPosition(x_from, y_from), new GridPosition(x_to, y_to), count);
			}
		}
	}

	void CreateAnimation_MoveShip(GridPosition from, GridPosition to, long count) {
		UIMapShipElement anim = CreateSingleElement<UIMapShipElement>(from);
		anim.SetCount((int)count);
		float time = 1;

		Vector3 _from = anim.transform.position;
		anim.transform.localPosition = MapController.CellToWorldPosition(to, anim.transform.localPosition.z);
		Vector3 _to = anim.transform.position;
		anim.transform.position = _from;

		iTween.MoveTo(anim.gameObject, _to, time);
		Destroy(anim, time);
	}
}