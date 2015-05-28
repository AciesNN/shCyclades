using UnityEngine;
using System;
using System.Collections;

public class TabloidPanel : UIGamePanel {

	public UISprite bannerSprite;
	public UILabel textLabel;

	public static TabloidPanel inst;
	protected String text;

	void Awake() {
		inst = this;
	}

	public void SetText(string text) {
		if (text != "" && !content.activeSelf) {
			textLabel.text = "";
			bannerSprite.gameObject.transform.localScale = new Vector3(0f, 1f, 1f);
			content.SetActive(true);
		}

		this.text = text;
		if (textLabel.text != text) {
			if (textLabel.text != "") {
				AnimateShrink().setOnComplete(() => AnimateGrow());
			} else {
				AnimateGrow();
			}
		}
	}

	LTDescr AnimateShrink() {
		textLabel.text = "";
		LeanTween.alphaWidget(textLabel, 0.0f, 0.1f);
		return LeanTween.scaleX(bannerSprite.gameObject, 0.0f, 0.3f).setEase(LeanTweenType.easeInCubic);
	}

	void AnimateGrow() {
		textLabel.text = text;
		if (text == "") {
			content.SetActive(false);
			return;
		}
		LeanTween.alphaWidget(textLabel, 1.0f, 0.1f).setDelay(0.2f);
		LeanTween.scaleX(bannerSprite.gameObject, 1.0f, 0.3f).setEase(LeanTweenType.easeOutCubic);
	}
}
