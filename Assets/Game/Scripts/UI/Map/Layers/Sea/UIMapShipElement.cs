using UnityEngine;
using System.Collections;

public class UIMapShipElement : UIMapGridLayerElement {

	#region ViewWidgets
	public UISprite sprite;
	public UILabel countLabel;
	#endregion

	#region ViewWidgetsSet
	public void SetCount(int count) {
		if (count <= 0) {
			context.SetActive(false);
		} else {
			context.SetActive(true);
			countLabel.text = "" + count;
		}
	}

	public void SetOwner(int user) {
		sprite.color = UIConsts.userColors[user];
	}
	#endregion

}
