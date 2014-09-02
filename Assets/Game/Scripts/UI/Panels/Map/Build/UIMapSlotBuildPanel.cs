﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Cyclades.Game.Client;

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
		SetMetro(Sh.In.GameContext.GetBool ("/map/islands/is_metro/[{0}]", island));
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

	public void SetMetro(bool isMetro) {

		metro.enabled = isMetro;
		if (isMetro) {
			for (int i = 0; i <= 2; ++i) {
				slots[i].gameObject.SetActive(false);
			}
		}

	}
	#endregion

	#region Events
	void OnSlotClick(int slot) {
		Sh.Out.Send(Messanges.BuyBuild(activeIsland, slot));
		Sh.GameState.mapStates.SetEventorType(MapEventerType.DEFAULT);
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
		TabPanel.SetTab(PanelType.MAP_TAB_PLACE_BUILD);
	}
	#endregion
}
