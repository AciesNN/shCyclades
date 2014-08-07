using UnityEngine;
using System.Collections;

public class UIBuyCardPanel : UIGamePanel {

	public void OnPressOKButton() {
		UIGamePanel.CloseActivePanel();
	}

	public void OnPressCancelButton() {
		UIGamePanel.CloseActivePanel();
	}
}
