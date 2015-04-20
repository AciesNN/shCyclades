using UnityEngine;
using System.Collections;

public class UIMapGridCellElement: UIMapGridLayerElement {

	public GridPosition position;
	public string onHoverSpriteName;
	[HideInInspector]
	public string normalSpriteName;

	UISprite sprite;
	void Awake() {
		sprite = gameObject.GetComponent<UISprite>();
		normalSpriteName = sprite.spriteName;
	}

	public void SetAlpha(float alpha) {
		sprite.alpha = alpha;
	}

	public void OnHover(bool onHover) {
		sprite.spriteName = (onHover ? onHoverSpriteName : normalSpriteName);
	}
}