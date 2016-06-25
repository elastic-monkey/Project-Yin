using UnityEngine;
using System.Collections;

public interface IMenu
{
    void Open();

    void Close();

    bool IsSubMenu();

    void OnNavItemFocused(NavItem target);

    void OnNavItemSelected(NavItem item, object actionObj, object dataObj);

    bool OnNavItemAction(NavItem navItem, object actionObj, object dataObj);

    void TransitionTo(IMenu other);
}
