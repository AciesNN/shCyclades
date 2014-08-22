using UnityEngine;
using System.Collections;

public class UIMapSlotBuildPanel: UIGamePanel {

	#region ViewWidgets
	public UISprite[] slots = new UISprite[4];
	public UISprite metro;
	#endregion
	
	int activeIsland;

	public void SetActiveIsland(int island) {
		activeIsland = island;
		//TODO - тут отрисовать текущие слоты
		//типа: 
		SetSlotsCount(3);
		SetBuildInSlot(1, Cyclades.Game.Constants.buildUniver);
		SetMetro(false);
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
		Sh.Out.Send("build on island " + activeIsland + " on slot " + slot);
		Sh.GameState.mapStates.SetType(MapEventerType.DEFAULT);
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
