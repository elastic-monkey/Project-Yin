using UnityEngine;
using System.Collections;

public class SettingsSubMenu : MainMenuManager
{
    public MenuManager MainMenu;

    protected override void OnAction(Actions action, NavItem item, NavMenu target, string[] data)
    {
        switch (action)
        {
            case Actions.Audio:
                Debug.Log("You clicked. So what?");
                break;

            case Actions.Back:
                NavMenu.OnSetActive(false);
                MainMenu.NavMenu.InputBlocked = false;
                MainMenu.NavMenu.FocusCurrent();
                break;
        }
    }
}
