using UnityEngine;

class DefaultMapEventer: MapEventer {

	override public void Activate() {
		mapStates.Panel.HideAll();
	}

}