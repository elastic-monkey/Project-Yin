using UnityEngine;
using System.Collections;

public class ChangeNavMenuNavItem : NavItem
{
    public NavMenu TargetNavMenu;
    public bool IsSubmenu = true;

    private Menu _parentMenu;

    protected override void Awake()
    {
        base.Awake();

        _parentMenu = GetComponentInParent<Menu>();
    }

    public override void OnSelect()
    {
        _parentMenu.ChangeTo(TargetNavMenu, IsSubmenu);
    }

}
