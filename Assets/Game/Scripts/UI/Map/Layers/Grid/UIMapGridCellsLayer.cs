using UnityEngine;
using System.Collections;

public class UIMapGridCellsLayer : UIMapGridLayer {

	//TODO по идее для всех этих хайлайтов надо засабачить скрипт, как в долбанной NGUI кнопке
	float normalAlfa = 0f;
	float highLightAlfa = 1.0f;

	public override void CreateGridElements() {
		elements = new UIMapGridCellElement[MapController.XSize, MapController.YSize];

		for (int x = 0; x < MapController.XSize; ++x) {
			for (int y = 0; y < MapController.YSize; ++y) {
				GridPosition pos = new GridPosition(x, y);
				if (MapController.IsCellPossible(pos)) {
					UIMapGridCellElement el = CreateElement<UIMapGridCellElement>(pos);
					el.SetAlpha(normalAlfa);
				}
			}
		}
	}

	public void HiglightCell(GridPosition cell, bool isHighLight) {
		if (cell.IsLessThanZero()) {//TODO спорный момент - не очень красиво передавать -1, если надо сбросить выделение со всех
			for(int x = 0; x < MapController.XSize; ++ x) {
				for(int y = 0; y < MapController.XSize; ++ y) {
					GridPosition c = new GridPosition(x, y);
					if (MapController.IsCellPossible(c))
						HiglightCell(c, isHighLight);
				}
			}
		} else {
			(elements[cell.x, cell.y] as UIMapGridCellElement).SetAlpha(isHighLight ? highLightAlfa : normalAlfa);
		}
	}

	public void OnHoverCell(GridPosition cell, bool onHover) {
		(elements[cell.x, cell.y] as UIMapGridCellElement).OnHover(onHover);
	}
}
