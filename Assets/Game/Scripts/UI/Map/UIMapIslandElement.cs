using UnityEngine;
using System.Collections;

public class UIMapIslandElement : UIMapGridLayerElement {

	public UISprite sprite;

	public void SetHighlight(bool isHighlight) {
		sprite.color = (isHighlight ? Color.green : Color.white);
	}

}
