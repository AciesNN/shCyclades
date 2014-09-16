using UnityEngine;
using System.Collections;

public class UIMapBuildInfoLayerElement : UIMapGridLayerElement {

	#region ViewWidgets
	public UISprite[] slots = new UISprite[4];
	public UISprite metro;
	#endregion

	#region ViewWidgetsSet
	public void SetBuildInSlot(int slot, string build) {
		slots[slot].spriteName = UIConsts.buildSprites[build];
	}

	public void SetSlotsCount(int count) {
		for(int i = 0; i < slots.Length; ++i) {
			slots[i].enabled = (i < count);
		}
	}

	public void SetMetro(bool isMetro, int metroSize) {
		
		metro.enabled = isMetro;
		if (isMetro) {
			for(int i = 0; i < metroSize; ++i) {
				slots[i].enabled = false;
			}
		}

	}
	#endregion

}
