using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Button))]
public class ButtonNavItem : NavItem
{
    public Color SelectedColor;

    private Color _initialColor;
    private Button _btn;
    private Text _btnText;

    public Button Button
    {
        get
        {
            if (_btn == null)
            {
                _btn = GetComponent<Button>();
            }

            return _btn;
        }
    }

    public Text Text
    {
        get
        {
            if (_btnText == null)
                _btnText = GetComponentInChildren<Text>();

            return _btnText;
        }
    }

    protected void Awake()
    {
        _initialColor = Text.color;
    }
        
    protected override void OnSelect(bool value)
    {
        Text.color = value ? SelectedColor : _initialColor;
    }
}
