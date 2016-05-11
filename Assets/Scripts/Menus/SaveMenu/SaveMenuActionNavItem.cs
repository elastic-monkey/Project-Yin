using UnityEngine;

public class SaveMenuActionNavItem : TextNavItem
{
    public SaveMenuManager.Actions Action;

    public override void OnSelect(MenuManager manager)
    {
        manager.OnAction(this, Action, null);
    }
}
