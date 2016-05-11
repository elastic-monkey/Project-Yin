using UnityEngine;
using System.Collections;

public class QuitMenuManager : MenuManager
{
    public enum Actions
    {
        ConfirmQuit,
        GoBack
    }

    public QuitMenuChangeMenuNavItem OnBackPressed;

    private void Start()
    {
        NavMenu.SetActive(NavMenu.IsActive);
    }

    private void Update()
    {
        if (!NavMenu.IsActive)
            return;

        if (PlayerInput.IsButtonUp(Axis.Escape))
        {
            OnAction(OnBackPressed, OnBackPressed.Action, OnBackPressed.Target);
        }
    }

    public override void OnFocus(NavItem target)
    {
        // TODO
    }

    public override void OnAction(NavItem item, object action, object data)
    {
        var actionEnum = (Actions)action;

        switch (actionEnum)
        {
            case Actions.ConfirmQuit:
                Debug.Log("Quiting application... [Will not work in the Editor]");
                Application.Quit();
                break;

            case Actions.GoBack:
                var target = data as NavMenu;
                if (target != null)
                {
                    target.SetActive(true);
                    NavMenu.SetActive(false);    
                }
                break;
        }
    }

}
