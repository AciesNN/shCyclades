﻿using UnityEngine;

public class UIMapDraggerAndPresser : MonoBehaviour {
	public UIDraggableCamera draggableCamera;
	public UIMapController MapController;
	public UIMapPanel MapPanel;

	Vector3 InputPosition {
		get { return draggableCamera.camera.ScreenToWorldPoint(Input.mousePosition) / 0.003125f; } //волшебное число - scale UIRoot NGUI по-умолчанию
	}

	void OnClick ()	{

		GridPosition cell = MapController.WorldPositionToCell(InputPosition);
		if (MapController.IsCellPossible(cell))
			MapPanel.OnClickCell(cell); 

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
	
	void LateUpdate() {

		GridPosition cell = MapController.WorldPositionToCell(InputPosition);
		if (MapController.IsCellPossible(cell))
			MapPanel.OnHoverCell(cell);

	}
}
