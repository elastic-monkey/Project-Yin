using UnityEngine;
using UnityEngine.UI;

public abstract class NavItem : MonoBehaviour
{
	public bool Disabled, Focused;
	public Color FocusedColor = new Color32(113, 183, 188, 255);
	public Color DisabledColor = new Color32(255, 255, 255, 40);
	public Graphic TargetGraphic;

	protected Color _initialColor;

	protected virtual void Awake()
	{
		_initialColor = TargetGraphic.color;
	}

	public void Focus(bool value)
	{
		Focused = value;
		OnFocus(value);
	}

	protected virtual void OnFocus(bool value)
	{
		UpdateColor();
	}

	public void Disable(bool value)
	{
		Disabled = value;
        UpdateColor();
    }

	public virtual void UpdateColor()
	{
		TargetGraphic.color = Disabled ? DisabledColor : Focused ? FocusedColor : _initialColor;
	}

    public abstract void OnSelect();

	public virtual void OnHorizontalInput(float value)
	{
		// Not implemented here
	}

	public virtual void OnVerticalInput(float value)
	{
		// Not implemented here
	}
}
