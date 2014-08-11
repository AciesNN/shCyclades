using UnityEngine;
using System.Collections;

public class UIMapGridLayer : MonoBehaviour {

	public UIMapController MapController;

	public int Depth;

	public GameObject SpritePrefab;
	protected UISprite[,] sprites;

	void Awake() {
		CreateGridSprites();
	}

	public virtual void CreateGridSprites() {
		sprites = new UISprite[MapController.XSize, MapController.YSize];

		for (int x = 0; x < MapController.XSize; ++x) {
			for (int y = 0; y < MapController.YSize; ++y) {
				CreateSprite(new GridPosition(x, y));
			}
		}

		MapController.SetCenterToDefaultPoint();
	}

	void CreateSprite(GridPosition pos) {
		GameObject go = NGUITools.AddChild(gameObject, SpritePrefab);
		go.name = pos.ToString();

		UISprite sprite = go.GetComponent<UISprite>();
		sprites[pos.x, pos.y] = sprite;

		sprite.width = MapController.CellXSize;
		sprite.height = MapController.CellYSize;

		go.transform.localPosition = MapController.CellToWorldPosition(pos);
		sprite.depth = Depth;
	}
}
