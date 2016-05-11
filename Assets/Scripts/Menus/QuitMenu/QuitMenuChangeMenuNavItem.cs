using UnityEngine;
using System.Collections;

public class QuitMenuChangeMenuNavItem : QuitMenuActionNavItem
{
    public NavMenu Target;

    public override void OnSelect(MenuManager manager)
    {
        manager.OnAction(this, Action, Target);
    }
}
