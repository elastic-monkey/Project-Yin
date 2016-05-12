using UnityEngine;

public class MainMenu : MainMenuManager
{
	public LoadMenu LoadMenu;
    public SettingsSubMenu AudioMenu;

    public override void OnFocus(NavItem target)
    {
        base.OnFocus(target);

        var menuItem = target as MenuNavItem;
        if (menuItem == null)
            return;

        var action = menuItem.Action;

        switch(action)
        {
            case Actions.Settings:
                
                break;

            default:
                AudioMenu.NavMenu.OnSetActive(false);
                break;
        }
    }

    protected override void OnAction(Actions action, NavItem item, NavMenu target, string[] data)
    {
        switch (action)
        {
			case Actions.New:
				LoadMenu.NewGameMode = true;
	            TransitionTo(target);
                break;

			case Actions.Load:
				LoadMenu.NewGameMode = false;
                TransitionTo(target);
                break;

            case Actions.Settings:
                AudioMenu.NavMenu.OnSetActive(true);
                NavMenu.InputBlocked = true;
                NavMenu.UnfocusAll();
                break;

            case Actions.Back:
                TransitionTo(target);
                break;

            case Actions.Quit:
                TransitionTo(target);
                break;
        }
    }
}
