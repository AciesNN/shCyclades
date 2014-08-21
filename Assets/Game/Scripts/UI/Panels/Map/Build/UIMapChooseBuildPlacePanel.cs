using UnityEngine;
using System.Collections;

public class UIMapChooseBuildPlacePanel : UIGamePanel {

	public void OnCancelButtonClick() {
		Sh.GameState.mapStates.SetType(MapEventerType.DEFAULT);
	}

}
