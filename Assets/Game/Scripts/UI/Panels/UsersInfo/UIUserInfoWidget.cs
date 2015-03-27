using UnityEngine;
using System.Collections;

public class UIUserInfoWidget : MonoBehaviour {

	#region VewWidgets
	public UISprite UserColorSprite;
	#endregion

	#region ViewWidgetsSet
	void UpdateUser() {
		UserColorSprite.spriteName = UIConsts.userColorsShields[player] + (isCurrent ? "2" : "1");
		iTween.ScaleTo(UserColorSprite.gameObject, Vector3.one * (isCurrent ? 1 : 0.8f), 0.5f);
	}

	int player;
	public void SetUser(int userNumber) {
		player = userNumber;
		UpdateUser();
	}

	bool isCurrent;
	public void SetIsCurrentUser(bool isCurrent) {
		this.isCurrent = isCurrent;
		UpdateUser();
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