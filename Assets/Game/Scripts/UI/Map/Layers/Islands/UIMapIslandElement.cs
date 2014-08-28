using UnityEngine;
using System.Collections;

public class UIMapIslandElement : UIMapGridLayerElement {

	#region ViewWidgets
	public UISprite sprite;
	public UISprite owner;
	#endregion

	#region ViewWidgetsSet
	public void SetHighlight(bool isHighlight) {
		sprite.color = (isHighlight ? Color.green : Color.white);
	}

	public void SetOwner(int user) {
		owner.color = UIConsts.userColors[user];
		owner.gameObject.SetActive(user != -1);
	}
	#endregion
}
