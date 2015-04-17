using UnityEngine;
using System.Collections;

public class UIUserInfoWidget : MonoBehaviour {

	float time = 0.5f;

	#region VewWidgets
	public UISprite UserColorSprite;
	#endregion

	void Start() {
		UIButton btn = gameObject.GetComponent<UIButton>();
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

		LeanTween.scale(UserColorSprite.gameObject, Vector3.one * (isCurrent ? 1 : 0.8f), time);
	}

	bool alreadyMovedInThisTurn = false;
	public void SetAlreadyMovedInThisTurn(bool alreadyMovedInThisTurn) {
		this.alreadyMovedInThisTurn = alreadyMovedInThisTurn;

		UserColorSprite.color = new Color(UserColorSprite.color.r, UserColorSprite.color.g, UserColorSprite.color.b, (alreadyMovedInThisTurn ? 0.5f : 1f));
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