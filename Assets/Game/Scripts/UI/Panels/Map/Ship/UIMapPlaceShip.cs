﻿using UnityEngine;
using System.Collections;

public class UIMapPlaceShip: UIGamePanel {

	public void OnCancelButtonClick() {
		Sh.GameState.mapStates.SetEventorType(MapEventerType.DEFAULT);
	}

}
