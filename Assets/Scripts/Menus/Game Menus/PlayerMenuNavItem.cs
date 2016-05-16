using UnityEngine;
using System.Collections;

public class PlayerMenuNavItem : NavItem
{

    public PlayerMenuManager.Actions Action;
    public string[] Data;

    public override void OnSelect(MenuManager manager)
    {
        manager.OnNavItemSelected(this, Action, Data);
    }
}
