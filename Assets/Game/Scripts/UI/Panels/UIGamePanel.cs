using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIGamePanel : MonoBehaviour, IUpdateble {

	[HideInInspector]public UIGamePanelTabs TabPanel;

	protected Vector3 showPosition;
	protected static UIGamePanel activePanel;
	protected static UIGamePanel parentPanel;

	public GameObject content;
	public PanelType panelType;

	public bool IsModal;

	static Dictionary <PanelType, UIGamePanel> panels;

	public static T GetPanel<T>(PanelType type) where T: UIGamePanel {
		return panels[type] as T;
	}

	public static UIGamePanel GetPanel(PanelType type) {
		return panels[type];
	}

	void Awake() {
		if (panels == null) panels = new Dictionary <PanelType, UIGamePanel> ();
		if (panelType != PanelType.DEFAULT)
			panels.Add (panelType, this);
		else {
			if (transform.parent.GetComponent<UIGamePanelTabs>()) {
				Debug.LogError("У панели (" + name + ") входящей в закладки не установлен тип - переключение будет невозможно");
			}
		}

		showPosition = content.transform.position;

		Init ();
	}

	virtual protected void Init() {
	}

	/*поздняя инициализация - когда уже есть исходные данные, не вызывается автоматически*/
	virtual public void LateInit() {
	}

	virtual public void Show() {
		content.SetActive(true);
		if (IsModal) //???
			activePanel = this;
	}

	public void Show(bool show) {
		if (show)
			Show();
		else
			Hide();
	}

	virtual public void Hide() {
		content.SetActive(false);
	}

	virtual public bool IsActive() {
		return content.activeSelf;
	}

	public static void ShowPanel(PanelType panelType) {
		if (activePanel && activePanel.IsModal)
			return;
		if (panels.ContainsKey (panelType)) 
			panels [panelType].Show (); 
	}

	public static void ShowPanel(PanelType panelType, UIGamePanel parentPanel) {
		if (activePanel && activePanel.IsModal)
			return;
		UIGamePanel.parentPanel = parentPanel;
		if (panels.ContainsKey (panelType)) 
			panels [panelType].Show (); 
	}

	public static void CloseActivePanel() {
		if (!activePanel)
			return;
		activePanel.Hide();
		activePanel = null;
	}

	public static void CloseActivePanel(string methodName, ModelPanelCloseResult result) {
		parentPanel.SendMessage(methodName, result);
		CloseActivePanel();
	}
}

public enum ModelPanelCloseResult {
	OK,
	CANCEL
}