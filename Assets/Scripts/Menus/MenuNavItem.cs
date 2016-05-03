using UnityEngine;

public class MenuNavItem : ButtonNavItem
{
    public NavMenu Target;

    public override void OnSelect(MenuManager manager)
    {
        manager.OnAction(MainMenuManager.Actions.SelectMenu, Target);
    }
}
