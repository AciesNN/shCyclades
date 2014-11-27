using UnityEngine;
using System.Collections;

public class UIMapShipElement : UIMapGridLayerElement {

	#region ViewWidgets
	public UISprite sprite;
	public UILabel countLabel;
	#endregion

	#region ViewWidgetsSet
	private long count;
	public long Count {
		set {
			count = value;
			if (count <= 0) {
				context.SetActive(false);
			} else {
				context.SetActive(true);
				countLabel.text = "" + count;
			}
		}

		get {
			return count;
		}
	}

	public long Owner {
		set {
			sprite.color = UIConsts.userColors[(int)value];
		}
	}
	#endregion

}
