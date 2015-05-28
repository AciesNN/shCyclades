using UnityEngine;
using System.Collections;

public class UIUserInfoWidget : MonoBehaviour {

	float time = 0.5f;

	#region VewWidgets
	public UISprite UserColorSprite;
	UIButton btn;
	#endregion

	void Start() {
		btn = UserColorSprite.GetComponent<UIButton>();
		btn.enabled = true;
	}

	#region ViewWidgetsSet
	void UpdateUser() {

	}

	int player;
	bool isCurrent;
	public void SetUser(int userNumber, bool isCurrent) {
		player = userNumber;
		gameObject.name = "player " + UIConsts.userColorsString[player];

		UserColorSprite.spriteName = "shield-" + UIConsts.userColorsString[player] + (isCurrent ? "2" : "1");

	}

	bool alreadyMovedInThisTurn = false;
	public void SetAlreadyMovedInThisTurn(bool alreadyMovedInThisTurn) {
		this.alreadyMovedInThisTurn = alreadyMovedInThisTurn;

		float alpha = (alreadyMovedInThisTurn ? 0.5f : 1.0f);
		LeanTween.alphaWidget(UserColorSprite, alpha, time);
		if (btn != null) {
			btn.defaultColor = new Color(btn.defaultColor.r, btn.defaultColor.g, btn.defaultColor.b, alpha);
		}
	}
	#endregion

	#region Events
	public void OnClick() {
		UIUserInfoPanel panel = UIGamePanel.GetPanel<UIUserInfoPanel>(PanelType.PLAYER_INFO_PANEL);

		panel.player = player;

		UIGamePanel.ShowPanel(PanelType.PLAYER_INFO_PANEL);
	}
	#endregion
}