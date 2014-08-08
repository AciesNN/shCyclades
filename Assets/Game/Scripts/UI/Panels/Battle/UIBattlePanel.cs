using UnityEngine;
using System.Collections;

public class UIBattlePanel : UIGamePanel {

	#region ViewWidgets
	public GameObject UIBattleSidePrefab;
	public UITable Table;

	UIBattleSideWidget attackSideWidget;
	UIBattleSideWidget deffenceSideWidget;
	#endregion

	#region UpdateData
	void Update() {
		UpdateData();
	}
	
	public void UpdateData() {
		attackSideWidget.SetUser(1);
		attackSideWidget.SetUnitsCount(3);
		attackSideWidget.SetUnitsCount(1);

		deffenceSideWidget.SetUser(3);
		deffenceSideWidget.SetUnitsCount(1);
		deffenceSideWidget.SetUnitsCount(2);
	}
	#endregion

	protected override void Init ()	{

		attackSideWidget = AddSide(true);
		deffenceSideWidget = AddSide(false);

		Table.Reposition();

	}

	UIBattleSideWidget AddSide(bool isAttack) {
		GameObject w_go = (GameObject) NGUITools.AddChild(Table.gameObject, UIBattleSidePrefab);
		w_go.name = (isAttack ? "attack side" : "deffence side");
		UIBattleSideWidget w = w_go.GetComponent<UIBattleSideWidget>();
		
		w.parentPanel = this;
		w.isAttack = isAttack;

		return w;
	}

	#region Events
	#endregion

}
