using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public abstract class NavItem : MonoBehaviour
{
    public Color SelectedColor;
    public Graphic TargetGraphic;

    protected Color _initialColor;

    protected void Awake()
    {
        _initialColor = TargetGraphic.color;
    }

    public virtual void OnFocus(bool value)
    {
        TargetGraphic.color = value ? SelectedColor : _initialColor;
    }

    public abstract void OnSelect(MenuManager manager);

    public virtual void OnHorizontalInput(float value)
    {
        // Not implemented here
    }

    public virtual void OnVerticalInput(float value)
    {
        // Not implemented here
    }
}
