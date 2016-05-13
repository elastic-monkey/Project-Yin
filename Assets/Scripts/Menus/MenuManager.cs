using UnityEngine;

public abstract class MenuManager : MonoBehaviour
{
    public NavMenu NavMenu;
    public Axis BackKey;

    public abstract void HandleInput(bool active);

    protected void TransitionTo(NavMenu other)
    {
        if (other == null)
            return;
        
        other.OnSetActive(true);
        other.InputBlocked = false;

        if (other.IsSubMenu)
        {
            NavMenu.InputBlocked = true;
            NavMenu.UnfocusAll();
        }
        else
        {
            NavMenu.OnSetActive(false);
        }
    }

    public abstract void OnNavItemSelected(NavItem item, object actionObj, object dataObj);

    protected virtual void OnNavItemAction(object actionObj, NavItem navItem, NavMenu targetNavMenu, string[] data)
    {
        TransitionTo(targetNavMenu);
    }

    public virtual void OnNavItemFocused(NavItem target)
    {
        // Do nothing
    }
}
