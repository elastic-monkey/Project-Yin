using UnityEngine;
using UnityEngine.UI;

public abstract class TextNavItem : NavItem
{
    public Color SelectedColor;

    private Color _initialColor;
    protected Text _text;

    public Text Text
    {
        get
        {
            if (_text == null)
            {
                _text = GetComponent<Text>();

                if (_text == null)
                {
                    _text = GetComponentInChildren<Text>();
                }
            }

            return _text;
        }
    }

    protected void Awake()
    {
        _initialColor = Text.color;
    }

    protected override void OnFocus(bool value)
    {
        Text.color = value ? SelectedColor : _initialColor;
    }
}
