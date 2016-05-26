using UnityEngine;

public abstract class MenuManager : MonoBehaviour
{
    public Axis BackKey;

    private NavMenu _navMenu;

    public NavMenu NavMenu
    {
        get
        {
            if (_navMenu == null)
                _navMenu = GetComponent<NavMenu>();

            return _navMenu;
        }
    }

    public virtual void SetActive(bool value) { NavMenu.SetActive(value); }

    public virtual void OnNavItemFocused(NavItem target) { }

    public abstract void OnNavItemSelected(NavItem item, object actionObj, object dataObj);

    public abstract void HandleInput(bool active);

    protected virtual void OnNavItemAction(object actionObj, NavItem navItem, NavMenu targetNavMenu, string[] data) { TransitionTo(targetNavMenu); }

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
}
