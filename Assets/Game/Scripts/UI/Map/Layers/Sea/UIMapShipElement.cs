using UnityEngine;
using System.Collections;

public class UIMapShipElement : UIMapGridLayerElement {

	#region ViewWidgets
	public UISprite sprite;
	public UILabel countLabel;
	#endregion

	#region ViewWidgetsSet
	private long count;
	public long Count {
		set {
			count = value;
			if (count <= 0) {
				context.SetActive(false);
			} else {
				context.SetActive(true);
				countLabel.text = "" + count;
				AnimateShip1();
			}
		}

		get {
			return count;
		}
	}

	public long Owner {
		set {
			sprite.color = UIConsts.userColors[(int)value];
		}
	}
	#endregion

	void StartAnimation() {
		LeanTween.cancel(sprite.gameObject);
		sprite.transform.localRotation = Quaternion.identity;

		float pause = Random.Range(0.0f, 20.0f);
		LeanTween.delayedCall(pause, AnimateShip1);
	}

	void AnimateShip1() {
		LeanTween.rotateAround(sprite.gameObject, Vector3.forward, 8.0f, 1.5f).setEase(LeanTweenType.easeInOutCubic).setOnComplete(AnimateShip2);
	}

	void AnimateShip2() {
		LeanTween.rotateAround(sprite.gameObject, Vector3.forward, -16.0f, 3.0f).setEase(LeanTweenType.easeInOutCubic).setLoopPingPong().setRepeat(2).setOnComplete(AnimateShip3);
	}

	void AnimateShip3() {
		LeanTween.rotateAround(sprite.gameObject, Vector3.forward, -8.0f, 1.5f).setEase(LeanTweenType.easeInOutCubic).setOnComplete(StartAnimation);
	}
}
