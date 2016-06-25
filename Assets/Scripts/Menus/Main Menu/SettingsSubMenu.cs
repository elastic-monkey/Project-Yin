using UnityEngine;
using System.Collections;

public class SettingsSubMenu : MainMenu
{
    public override bool OnNavItemAction(NavItem navItem, object actionObj, object dataObj)
    {
        return false;
    }
}
