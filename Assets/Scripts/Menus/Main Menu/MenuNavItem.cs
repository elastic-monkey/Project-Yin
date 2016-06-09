using UnityEngine;
using System.Collections;

public class MenuNavItem : NavItem
{
    public MainMenu.Actions Action;
    public string[] Data;

    public override void OnSelect(IMenu manager)
    {
        manager.OnNavItemSelected(this, Action, Data);
    }
}
