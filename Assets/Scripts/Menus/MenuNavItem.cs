using UnityEngine;

public class MenuNavItem : ButtonNavItem
{
    public NavMenu Target;

    public override void OnSelect(MainMenuManager manager)
    {
        manager.SelectMenu(Target);
    }
}
