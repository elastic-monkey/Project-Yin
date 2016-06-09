using UnityEngine;
using System.Collections;

public interface IMenu
{
    void OnNavItemFocused(NavItem target);

    void OnNavItemSelected(NavItem item, object actionObj, string[] dataObj);

    bool OnNavItemAction(NavItem navItem, object actionObj, string[] data);
}
