using System;
using UnityEngine;

public class UIUserPanelBuildInfo: MonoBehaviour
{
	#region VewWidgets
	public UISprite build;
	public UISprite border;
	public UILabel count;
	#endregion

	public void SetInfo(int count, string build) {
		this.count.gameObject.SetActive(count > 1);
		this.count.text = "" + count;
		this.border.gameObject.SetActive(count > 0);
		this.build.spriteName = build;
	}
}
