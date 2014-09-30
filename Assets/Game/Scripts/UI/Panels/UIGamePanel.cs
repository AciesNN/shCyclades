using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIGamePanel : MonoBehaviour {

	const float UI_DELAY = 10.75f;

	[HideInInspector]public UIGamePanelTabs TabPanel;

	protected Vector3 showPosition;
	protected static UIGamePanel activePanel;
	protected static UIGamePanel parentPanel;

	public GameObject content;
	public PanelType panelType;

	public bool IsModal;

	static Dictionary <PanelType, UIGamePanel> panels;

	public static T GetPanel<T>(PanelType type) where T: UIGamePanel {
		T p = panels[type] as T;
		if (!p)
			Debug.LogError("Менеджер панелей не смог найти панель с типом: " + type);
		return p;
	}

	public static UIGamePanel GetPanel(PanelType type) {
		UIGamePanel p = panels[type];
		if (!p)
			Debug.LogError("Менеджер панелей не смог найти панель с типом: " + type);
		return p;
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
	virtual public void GameContext_LateInit() {
	}

	public void GameContext_UpdateData(bool deserialize) {
		if(IsActive())
			GameContext_UpdateData_Panel(deserialize);
	}

	virtual protected void GameContext_UpdateData_Panel(bool deserialize) {
	}

	virtual public void Show() {
		StartCoroutine(DoShow());
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
		if(IsModal && activePanel == this)
			activePanel = null;
		StartCoroutine(DoHide());
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
		if (parentPanel)
			parentPanel.SendMessage(methodName, result);
		CloseActivePanel();
	}

	virtual protected IEnumerator DoHide() {
		if (!content.activeSelf)
			yield break;
		OnPanelClose();
		//iTween.FadeTo(content, 0, UI_DELAY);
		//yield return new WaitForSeconds(UI_DELAY / 3.0f);
		content.SetActive (false);
	}

	virtual protected IEnumerator DoShow() {
		if (content.activeSelf)
			yield break;
		OnPanelOpen();
		//yield return new WaitForSeconds(UI_DELAY / 3.0f);
		content.SetActive (true);
		//iTween.FadeFrom(content, 1, UI_DELAY);
	}

	virtual protected void OnPanelOpen() {
	}

	virtual protected void OnPanelClose() {
	}
}

public enum ModelPanelCloseResult {
	OK,
	CANCEL
}