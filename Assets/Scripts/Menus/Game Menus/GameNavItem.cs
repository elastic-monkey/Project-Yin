using UnityEngine;
using System.Collections;

public class GameNavItem : NavItem
{
    public GameMenuManager.Actions Action;
    public string[] Data;

    public override void OnSelect(MenuManager manager)
    {
        manager.OnNavItemSelected(this, Action, Data);
    }
}
