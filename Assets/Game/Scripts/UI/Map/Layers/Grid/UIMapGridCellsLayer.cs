using UnityEngine;
using System.Collections;

public class UIMapGridCellsLayer : UIMapGridLayer {

	//TODO по идее для всех этих хайлайтов надо засабачить скрипт, как в долбанной NGUI кнопке
	float normalAlfa = 0f;
	float highLightAlfa = 1.0f;

	public override void CreateGridElements() {
		elements = new UIMapGridLayerElement[MapController.XSize, MapController.YSize];

		for (int x = 0; x < MapController.XSize; ++x) {
			for (int y = 0; y < MapController.YSize; ++y) {
				GridPosition pos = new GridPosition(x, y);
				if (MapController.IsCellPossible(pos)) {
					UIMapGridLayerElement el = CreateElement<UIMapGridLayerElement>(pos);
					if (normalAlfa == 0f) {
						normalAlfa = el.gameObject.GetComponent<UISprite>().alpha;
					}
				}
			}
		}
	}

	public void HiglightCell(GridPosition cell, bool isHighLight) {
		if (cell.x == -1 && cell.y == -1) {//TODO спорный момент - не очень красиво передавать -1, если надо сбросить выделение со всех
			for(int x = 0; x < MapController.XSize; ++ x) {
				for(int y = 0; y < MapController.XSize; ++ y) {
					GridPosition c = new GridPosition(x, y);
					if (MapController.IsCellPossible(c))
						HiglightCell(c, isHighLight);
				}
			}
		} else {
			elements[cell.x, cell.y].GetComponent<UISprite>().alpha = (isHighLight ? highLightAlfa : normalAlfa);
		}
	}

}
