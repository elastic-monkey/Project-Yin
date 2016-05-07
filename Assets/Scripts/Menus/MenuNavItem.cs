using UnityEngine;

public class MenuNavItem : TextNavItem
{
    public VerticalNavMenu Target;

    public override void OnSelect(MenuManager manager)
    {
        manager.OnAction(this, MainMenuManager.Actions.SelectMenu, Target);
    }
}
