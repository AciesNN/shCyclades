using UnityEngine;
using System.Collections;

public class UIMapMoveUnitPanel: UIGamePanel {

	#region ViewWidgets	
	public UILabel DesriptionLabel;
	#endregion

	#region ViewWidgetsSet
	public void SetDescription(int island) {
		DesriptionLabel.text = (island == -1 ? "Выберите свой остров для перемещения": "Выберите остров, куда хотите переместиться");
	}
	#endregion

	#region Events
	public void OnCancelButtonClick() {
		Sh.GameState.mapStates.SetType(MapEventerType.DEFAULT);
	}
	#endregion
}

