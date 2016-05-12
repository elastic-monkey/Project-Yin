using UnityEngine;

public abstract class MenuManager : MonoBehaviour
{
    public NavMenu NavMenu;
    public Axis BackKey;

    private void Start()
    {
        NavMenu.SetActive(NavMenu.IsActive);
    }

    private void Update()
    {
        if (!NavMenu.IsActive)
            return;

        if (PlayerInput.IsButtonUp(BackKey))
        {
            OnBackPressed();
        }
    }

    protected void TransitionTo(NavMenu other)
    {
        if (other != null)
        {
            other.SetActive(true);
            NavMenu.SetActive(false);
        }
    }

    public abstract void OnBackPressed();

    public abstract void OnNavItemSelected(NavItem item, object actionObj, object dataObj);

    public virtual void OnFocus(NavItem target)
    {
        // Do nothing
    }
}
