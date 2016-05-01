using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Button))]
public class ButtonNavItem : NavItem
{
    public Color SelectedColor;

    private Color _targetGraphicInitialColor;
    private Button _btn;
    private Text _btnText;

    private void Awake()
    {
        _btn = GetComponent<Button>();
        _btnText = GetComponentInChildren<Text>();
        _targetGraphicInitialColor = _btn.targetGraphic.color;
    }
        
    protected override void OnSelect(bool value)
    {
        _btn.targetGraphic.color = value ? SelectedColor : _targetGraphicInitialColor;
    }
}
