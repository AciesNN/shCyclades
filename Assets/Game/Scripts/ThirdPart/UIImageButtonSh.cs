using UnityEngine;

//скопировано из NGUI
public class UIImageButtonSh : MonoBehaviour
{
	public UISprite target;
	public string normalSprite;
	public string hoverSprite;
	public string pressedSprite;
	public string disabledSprite;
	public bool pixelSnap = true;

	bool _isHighlight;
	public bool isHighlight {
		get { return _isHighlight; }
		set {
			_isHighlight = value;
			UpdateImage();
		}
	}


	public bool isEnabled
	{
		get
		{
			Collider col = collider;
			return col && col.enabled;
		}
		set
		{
			Collider col = collider;
			if (!col) return;

			if (col.enabled != value)
			{
				col.enabled = value;
				UpdateImage();
			}
		}
	}

	void OnEnable ()
	{
		if (target == null) target = GetComponentInChildren<UISprite>();
		UpdateImage();
	}

	void OnValidate ()
	{
		if (target != null)
		{
			if (string.IsNullOrEmpty(normalSprite)) normalSprite = target.spriteName;
			if (string.IsNullOrEmpty(hoverSprite)) hoverSprite = target.spriteName;
			if (string.IsNullOrEmpty(pressedSprite)) pressedSprite = target.spriteName;
			if (string.IsNullOrEmpty(disabledSprite)) disabledSprite = target.spriteName;
		}
	}

	void UpdateImage()
	{
		if (target != null)
		{
			if (isEnabled) SetSprite(UICamera.IsHighlighted(gameObject) || _isHighlight ? hoverSprite : normalSprite);
			else SetSprite(disabledSprite);
		}
	}

	void OnHover (bool isOver)
	{
		if (isEnabled && target != null)
			SetSprite(isOver || _isHighlight ? hoverSprite : normalSprite);
	}

	void OnPress (bool pressed)
	{
		if (pressed) SetSprite(pressedSprite);
		else UpdateImage();
	}

	void SetSprite (string sprite)
	{
		if (target.atlas == null || target.atlas.GetSprite(sprite) == null) return;
		target.spriteName = sprite;
		if (pixelSnap) target.MakePixelPerfect();
	}
}
