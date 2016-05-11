using UnityEngine;
using System.Collections;

public class QuitMenuActionNavItem : TextNavItem
{
    public QuitMenuManager.Actions Action;

    public override void OnSelect(MenuManager manager)
    {
        manager.OnAction(this, Action, null);
    }
}
