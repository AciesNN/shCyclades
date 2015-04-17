using UnityEngine;
using System.Collections;

public class TabloidPanel : UIGamePanel {

	public UISprite bannerSprite;
	public UILabel textLabel;

	public static TabloidPanel inst;

	void Awake() {
		inst = this;
	}

	public void SetText(string text) {
		if (text != "" && !content.activeSelf)
			content.SetActive(true);
			
		if (textLabel.text != text) {
			if (textLabel.text != "") {
				AnimateShrink().setOnComplete(() => AnimateGrow(text));
			} else {
				AnimateGrow(text);
			}
		}
	}

	LTDescr AnimateShrink() {
		textLabel.text = "";
		return LeanTween.scaleX(bannerSprite.gameObject, 0.0f, 0.5f);
	}

	void AnimateGrow(string text) {
		textLabel.text = text;
		if (text == "") {
			content.SetActive(false);
			return;
		}
		LeanTween.scaleX(bannerSprite.gameObject, 1.0f, 0.5f);
	}
}
