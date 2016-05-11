using UnityEngine;
using System.Collections;

public class SaveMenuChangeMenuNavItem : SaveMenuActionNavItem
{
    public VerticalNavMenu Target;

    public override void OnSelect(MenuManager manager)
    {
        manager.OnAction(this, Action, Target);
    }
}
