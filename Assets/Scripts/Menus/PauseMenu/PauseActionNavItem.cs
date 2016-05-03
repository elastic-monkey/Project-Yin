using UnityEngine;
using System.Collections;

public class PauseActionNavItem : ButtonNavItem
{
    public PauseMenuManager.Actions Action;

    public override void OnSelect(MenuManager manager)
    {
        manager.OnAction(Action, null);
    }
}
