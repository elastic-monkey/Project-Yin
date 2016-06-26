using UnityEngine;
using System.Collections;

public class QuitGameNavItem : NavItem
{
    private Menu _parentMenu;

    protected override void Awake()
    {
        base.Awake();
    
        _parentMenu = GetComponentInParent<Menu>();
    }

    public override void OnSelect()
    {
        _parentMenu.QuitGame();
    }
}
