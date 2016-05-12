using UnityEngine;

public abstract class MenuManager : MonoBehaviour
{
    public NavMenu NavMenu;
    public Axis BackKey;

    public abstract void HandleInput(bool active);

    protected void TransitionTo(NavMenu other)
    {
        if (other != null)
        {
            other.OnSetActive(true);
            NavMenu.OnSetActive(false);
        }
    }

    public abstract void OnNavItemSelected(NavItem item, object actionObj, object dataObj);

    public virtual void OnFocus(NavItem target)
    {
        // Do nothing
    }
}
