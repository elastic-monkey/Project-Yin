using UnityEngine;

public abstract class MenuManager : MonoBehaviour
{
    public NavMenu NavMenu;
    public Axis BackKey;

    public abstract void HandleInput(bool active);

    public virtual void SetActive(bool value)
    {
        NavMenu.SetActive(value);
    }

    protected void TransitionTo(NavMenu other)
    {
        if (other == null)
            return;
        
        other.SetActive(true);
        other.InputBlocked = false;

        if (other.IsSubMenu)
        {
            NavMenu.InputBlocked = true;
            NavMenu.UnfocusAll();
        }
        else
        {
            SetActive(false);
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
