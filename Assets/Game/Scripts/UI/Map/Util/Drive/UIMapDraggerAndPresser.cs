using UnityEngine;

public class UIMapDraggerAndPresser : MonoBehaviour
{
	public UIDraggableCamera draggableCamera;
	public UIMapController MapController;

	void OnClick ()	{

		Vector3 pos = draggableCamera.camera.ScreenToWorldPoint(Input.mousePosition) / 0.003125f; //волшебное число - scale UIRoot NGUI по-умолчанию
		GridPosition cell = MapController.WorldPositionToCell(pos);
		if (MapController.IsCellPossible(cell))
			NGUIDebug.Log("press cell: " + cell); 

	}

	void OnDrag (Vector2 delta)	{
		if (enabled && NGUITools.GetActive(gameObject) && draggableCamera != null)	{
			draggableCamera.Drag(delta);
		}
	}
	
	void OnPress (bool isPressed)	{
		if (enabled && NGUITools.GetActive(gameObject) && draggableCamera != null)	{
			draggableCamera.Press(isPressed);
		}
	}
}

