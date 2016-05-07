using UnityEngine;
using System.Collections;

public class PauseActionNavItem : TextNavItem
{
    public PauseMenuManager.Actions Action;

    public override void OnSelect(MenuManager manager)
    {
        manager.OnAction(this, Action, null);
    }
}
