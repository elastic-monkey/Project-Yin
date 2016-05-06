using UnityEngine;
using System.Collections;

public class MainMenuActionNavItem : TextNavItem
{
    public MainMenuManager.Actions Action;

    public override void OnSelect(MenuManager manager)
    {
        manager.OnAction(Action, null);
    }
}
