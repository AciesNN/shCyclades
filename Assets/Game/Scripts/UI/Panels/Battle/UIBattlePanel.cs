using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Cyclades.Game.Client;

public class UIBattlePanel : UIGamePanel {

	#region ViewWidgets
	public GameObject UIBattleSidePrefab;
	public UITable Table;
	public GameObject ShowContext;

	UIBattleSideWidget attackSideWidget;
	UIBattleSideWidget deffenceSideWidget;
	#endregion

	#region UpdateData
	protected override void Init() {

		attackSideWidget = AddSide(true);
		deffenceSideWidget = AddSide(false);

		Table.Reposition();

	}
	
	override protected void GameContext_UpdateData_Panel() {
		UpdateSide(attackSideWidget);
		UpdateSide(deffenceSideWidget);
	}
	#endregion

	UIBattleSideWidget AddSide(bool isAttack) {
		GameObject w_go = (GameObject) NGUITools.AddChild(Table.gameObject, UIBattleSidePrefab);
		w_go.name = (isAttack ? "attack side" : "deffence side");
		UIBattleSideWidget w = w_go.GetComponent<UIBattleSideWidget>();
		
		w.parentPanel = this;
		w.isAttack = isAttack;

		return w;
	}

	void UpdateSide(UIBattleSideWidget w) {
		bool isAttack = w.isAttack;

		int user = Sh.In.GameContext.GetInt(GetDataPath(isAttack) + "/player");
		int unitsCount = Sh.In.GameContext.GetInt(GetDataPath(isAttack) + "/units");
		int buildingCount = 0;
		if (!isAttack) {
			buildingCount = Sh.In.GameContext.GetInt(GetDataPath(isAttack) + (isArmyFight() ? "/fortress" : "/marines"));
		}

		//TODO тут много недочетов:
		//крепости есть только у защищающегося острова, а марины у обеих сторон
		//надо устанавливать минотавра
		//если юнит не может бежать, то и кнопка должны быть неактивна
		//наступающий может отступитть только туда, откуда пришел, по крайней мере в кораблях

		w.SetUser(user);
		w.SetUnitsCount(unitsCount);
		w.SetBuildingsCount(buildingCount);
	}

	public void SetShowContextVisible(bool on) {
		ShowContext.SetActive(on);
	}

	#region Events
	public void OnRetreatClick(bool isAttack) {
		if (isArmyFight()) {
			MapEventerType e_type = MapEventerType.RETREATUNIT;
			RetreatUnitEventer e = Sh.GameState.mapStates.GetEventorByType(e_type) as RetreatUnitEventer;
			e.fromIsland = Sh.In.GameContext.GetInt(GetDataPath(isAttack) + "/island");
			e.battlePanel = this;

			Sh.GameState.mapStates.SetEventorType(e_type);			
		} else {
			MapEventerType e_type = MapEventerType.RETREATSHIP;
			RetreatShipEventer e = Sh.GameState.mapStates.GetEventorByType(e_type) as RetreatShipEventer;
			List<object> coords = Sh.In.GameContext.GetList(GetDataPath(isAttack) + "/coords"); //TODO это не факт
			e.lastSeaCell = new GridPosition((long)coords[0], (long)coords[1]);
			e.battlePanel = this;

			Sh.GameState.mapStates.SetEventorType(e_type);	
		}
	}

	public void OnMoveButtleOnClick(bool isAttack) {
		Sh.Out.Send(Messanges.Fight());
	}

	string GetDataPath(bool isAttacker) {
		return string.Format("/fight/{0}/{1}", (isArmyFight() ? "army" : "navy"), (isAttacker ? "attacker" : "deffender"));
	}

	bool isArmyFight() {
		return Sh.In.GameContext.GetBool("/fight/army/fight");
	}
	#endregion

}
