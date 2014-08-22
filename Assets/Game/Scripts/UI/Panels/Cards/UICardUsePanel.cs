using UnityEngine;
using System.Collections;

public class UICardUsePanel : UIGamePanel {

	public void OnCardCancelClick() {
		Sh.GameState.cardState = false;
	}
}
