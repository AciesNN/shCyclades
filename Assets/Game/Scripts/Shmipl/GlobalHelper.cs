using UnityEngine;
using System;

public static class GlobalHelper
{
	public static void SetImageButtonSprites(this UISprite sprite, string spriteName, string normalSuffix = "", string hoverSuffix = "") {
		UIImageButton spriteImageButton = sprite.GetComponent<UIImageButton>();
		if (spriteImageButton != null) {
			spriteImageButton.normalSprite = spriteName + normalSuffix;
			spriteImageButton.hoverSprite = spriteName + hoverSuffix;
			spriteImageButton.pressedSprite = spriteName + hoverSuffix;
			spriteImageButton.disabledSprite = spriteName + normalSuffix;
			
			sprite.spriteName = spriteImageButton.normalSprite;

			spriteImageButton.pixelSnap = false;
		} else {
			sprite.spriteName = spriteName + normalSuffix;
		}
	}

	public static UISprite Sprite(this GameObject go) {
		return go.GetComponent<UISprite>();
	}
}

