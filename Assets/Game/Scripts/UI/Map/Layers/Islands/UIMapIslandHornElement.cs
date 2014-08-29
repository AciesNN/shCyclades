using UnityEngine;
using System.Collections;

public class UIMapIslandHornElement : UIMapGridLayerElement {

	#region ViewWidgets
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
	#endregion

}
