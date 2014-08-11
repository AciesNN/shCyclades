//в целом скопированно из NGUI

using UnityEngine;

public class UIMapDraggerAndPresser : MonoBehaviour
{
	public UIDraggableCamera draggableCamera;
	public UIMapHexController MapController;

	void OnPress (bool isPressed)	{

		if (isPressed) {
			Vector3 pos = draggableCamera.camera.ScreenToWorldPoint(Input.mousePosition) / 0.003125f; //что за блядское волшебное число?! не знаю, поперто из SBSK
			GridPosition cell = MapController.WorldPositionToCell(pos);
			NGUIDebug.Log("press cell: " + cell); 
		}

	}

	void OnDrag (Vector2 delta)	{
		if (enabled && NGUITools.GetActive(gameObject) && draggableCamera != null)	{
			draggableCamera.Drag(delta);
		}
	}
}

