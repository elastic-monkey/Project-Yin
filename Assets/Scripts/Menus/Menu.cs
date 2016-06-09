using UnityEngine;

public abstract class Menu : MonoBehaviour
{
    public Axes OpenKey;
    public Axes CloseKey;
    public bool IsOpen = false;

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

    protected virtual void Update()
    {
        if (PlayerInput.OnlyMenus && !IsOpen)
            return;

        if (!IsOpen && PlayerInput.IsButtonUp(OpenKey))
        {
            Open();
        }
        else if (IsOpen && PlayerInput.IsButtonUp(CloseKey))
        {
            Close();
        }
    }

    public virtual void Open()
    {
        IsOpen = true;
        NavMenu.SetActive(true);
    }

    public virtual void Close()
    {
        IsOpen = false;
        NavMenu.SetActive(false);
    }

    public virtual void OnNavItemFocused(NavItem target)
    {
        // Do stuff
    }

    public void OnNavItemSelected(NavItem item, object actionObj, string[] dataObj)
    {
        if (OnNavItemAction(item, actionObj, dataObj))
            return;

        var targetMenu = FindTransitionTarget(actionObj);
        TransitionTo(targetMenu);
    }

    protected abstract NavMenu FindTransitionTarget(object actionObj);

    protected virtual bool OnNavItemAction(NavItem navItem, object actionObj, string[] data)
    {
        return false;
    }

    protected void TransitionTo(NavMenu other)
    {
        if (other == null)
            return;
        
        other.MenuManager.Open();
        other.InputBlocked = false;

        if (other.IsSubMenu)
        {
            NavMenu.InputBlocked = true;
            NavMenu.UnfocusAll();
            IsOpen = false;
        }
        else if (IsOpen)
        {
            Close();
        }
    }
}
