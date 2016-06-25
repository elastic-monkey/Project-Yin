using UnityEngine;
using System.Collections;

public class ChangeNavMenuNavItem : NavItem
{
    public NavMenu TargetNavMenu;

    private GameMenu _parentMenu;

    protected override void Awake()
    {
        base.Awake();

        _parentMenu = GetComponentInParent<GameMenu>();
    }

    public override void OnSelect()
    {
        _parentMenu.ChangeTo(TargetNavMenu);
    }

}
