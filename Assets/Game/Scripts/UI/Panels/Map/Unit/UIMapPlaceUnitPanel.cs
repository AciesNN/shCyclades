using UnityEngine;
using System.Collections;

public class UIMapPlaceUnitPanel : UIGamePanel {

	public void OnCancelButtonClick() {
		Sh.GameState.mapStates.SetType(MapEventerType.DEFAULT);
	}

}
