//в целом скопированно из NGUI

using UnityEngine;

public class UIMapDraggerAndPresser : MonoBehaviour
{
	public UIDraggableCamera draggableCamera;

	void OnPress (bool isPressed)	{
		/*if (enabled && NGUITools.GetActive(gameObject) && draggableCamera != null)
		{
			draggableCamera.Press(isPressed);
		}*/

		//NGUIDebug.Log(draggableCamera.camera.ScreenToWorldPoint(Input.mousePosition) / 0.003125f);
	}

	void OnDrag (Vector2 delta)	{
		if (enabled && NGUITools.GetActive(gameObject) && draggableCamera != null)	{
			draggableCamera.Drag(delta);
		}
	}
}

