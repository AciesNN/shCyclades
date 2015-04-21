using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Cyclades.Game.Client;
using Cyclades.Game;

public class UIMapSlotBuildPanel: UIGamePanel {

	#region ViewWidgets
	public UISprite[] slots = new UISprite[4];
	public UISprite metro;
	#endregion
	
	int activeIsland;

	public void SetActiveIsland(int island) {
		activeIsland = island;

		List<object> slots = Sh.In.GameContext.GetList ("/map/islands/buildings/[{0}]", island);
		SetSlotsCount(slots.Count);
		for(int i = 0; i < slots.Count; ++i)
			SetBuildInSlot(i, (string)slots[i]);
		SetMetro(Sh.In.GameContext.GetBool ("/map/islands/is_metro/[{0}]", island), Library.Map_IslandMetroSize(Sh.In.GameContext, island));
	}

	#region ViewWidgetsSet
	public void SetBuildInSlot(int slot, string build) {
		slots[slot].spriteName = UIConsts.buildSprites[build];
	}

	public void SetSlotsCount(int count) {
		for (int i = 0; i < slots.Length; ++i) {
			slots[i].gameObject.SetActive(i < count);
		}
	}

	public void SetMetro(bool isMetro, int metroSize) {

		metro.enabled = isMetro;
		if (isMetro) {
			for (int i = 0; i < metroSize; ++i) {
				slots[i].gameObject.SetActive(false);
			}
		}

	}
	#endregion

	#region Events
	void OnSlotClick(int slot) {
		Sh.Out.Send(Messanges.BuyBuild(activeIsland, slot));
		(UIMapStates.inst.eventer as BuildMapEventer).OnPanelSlotBuildOK();
	}

	public void OnSlot0Click() {
		OnSlotClick(0);
	}

	public void OnSlot1Click() {
		OnSlotClick(1);
	}

	public void OnSlot2Click() {
		OnSlotClick(2);
	}

	public void OnSlot3Click() {
		OnSlotClick(3);
	}

	public void OnCancelButtonClick() {
		UIMapStates.inst.Panel.HideAll();
		(UIMapStates.inst.eventer as BuildMapEventer).OnPanelSlotBuildCancel();
	}
	#endregion

	public override void Show() {
		base.Show();
		TabloidPanel.inst.SetText("Выберите слот, на котором будет распологаться здание");
	}
}
