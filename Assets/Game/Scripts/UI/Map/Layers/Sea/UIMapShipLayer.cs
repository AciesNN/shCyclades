using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Cyclades.Game;

public class UIMapShipLayer : UIMapGridLayer {

	public override void CreateGridElements() {
		elements = new UIMapShipElement[MapController.XSize, MapController.YSize];
	}

	public void GameContext_UpdateData(bool deserialize) {
		if (deserialize) {
			for(int x = 0; x < MapController.XSize; ++x) {
				for(int y = 0; y < MapController.YSize; ++y) {
					GridPosition cell = new GridPosition(x, y);
					if(MapController.IsCellPossible(cell) && Library.Map_GetIslandByPoint(Sh.In.GameContext, x, y) == -1) {
						long owner = Library.Map_GetPointOwner(Sh.In.GameContext, x, y);
						long count = Library.Map_GetShipCountByPoint(Sh.In.GameContext, x, y);
						AddShip(cell, owner, count);
					}
				}
			}
		}
	}

	public void AddShip(GridPosition cell, long owner, long count) {
		UIMapShipElement el = elements[cell.x, cell.y] as UIMapShipElement;
		if (elements[cell.x, cell.y] == null) {
			el = CreateElement<UIMapShipElement>(cell);
		}
		
		el.Count += count;
		if (count > 0) {
			el.Owner = owner;
		}
		
		//return el;
	}

	public void GameContext_ShowAnimation(Hashtable msg) {
		if (msg.ContainsKey("macros") && msg["macros"] is string) {//TODO надо придумать красивый фильтр
			string macros = (string)msg["macros"];
			if (macros == "MOVE_SHIP") {
				long x_from = (long)msg["x_from"]; 
				long x_to = (long)msg["x_to"]; 
				long y_from = (long)msg["y_from"]; 
				long y_to = (long)msg["y_to"];
				long count = (long)msg["count"];
				long owner = (long)msg["owner"]; 

				StartCoroutine( CreateAnimation_MoveShip(new GridPosition(x_from, y_from), new GridPosition(x_to, y_to), count, owner) );
			} else if (macros == "REMOVE_SHIP") {
				long x = (long)msg["x"]; 
				long y = (long)msg["y"]; 
				long count = (long)msg["count"]; 

				AddShip(new GridPosition(x, y), -1, -count);
			} else if (macros == "ADD_SHIP") {
				long x = (long)msg["x"]; 
				long y = (long)msg["y"]; 
				long count = (long)msg["count"]; 
				long owner = (long)msg["owner"]; 
				
				AddShip(new GridPosition(x, y), owner, count);
			}
		}
	}

	IEnumerator CreateAnimation_MoveShip(GridPosition from, GridPosition to, long count, long owner) {
		AddShip(from, owner, -count);

		UIMapShipElement anim = CreateSingleElement<UIMapShipElement>(from);
		anim.name += " (animation)";
		anim.Count = count;
		anim.Owner = owner;
		float time = 1;

		Vector3 _from = anim.transform.position;
		anim.transform.localPosition = MapController.CellToWorldPosition(to, anim.transform.localPosition.z);
		Vector3 _to = anim.transform.position;
		anim.transform.position = _from;

		iTween.MoveTo(anim.gameObject, _to, time);
		
		yield return new WaitForSeconds(time);

		Destroy(anim.gameObject);
		AddShip(to, owner, count);
	}
}