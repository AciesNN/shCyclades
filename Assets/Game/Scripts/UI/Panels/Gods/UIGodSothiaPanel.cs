﻿using UnityEngine;
using System.Collections;

public class UIGodSothiaPanel : UIGamePanel {
	
	public void OnManClick() {
	}

	public void OnBuildClick() {
		Sh.GameState.mapStates.SetType(MapEventerType.BUILD);
	}

	public void OnEndTurn() {
		Sh.Out.Send("end turn");
	}
}
