using UnityEngine;
using System.Collections;

public class PlayerMenu : GameMenu
{
    public UpgradeMenu UpgradeMenu;

    public override bool OnNavItemAction(NavItem navItem, object actionObj, string[] data)
    {
        var action = (Actions)actionObj;

        switch (action)
        {
            case Actions.GoToUpgradeMenu:
                UpgradeMenu.UpdateAllItems();
                return false;
            case Actions.Close:
                Close();
                return true;
        }

        return false;
    }
}
