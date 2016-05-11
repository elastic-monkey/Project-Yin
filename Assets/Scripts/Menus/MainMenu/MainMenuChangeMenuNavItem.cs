using UnityEngine;

public class MainMenuChangeMenuNavItem : MainMenuActionNavItem
{
    public VerticalNavMenu Target;

    public override void OnSelect(MenuManager manager)
    {
        manager.OnAction(this, Action, Target);
    }
}
