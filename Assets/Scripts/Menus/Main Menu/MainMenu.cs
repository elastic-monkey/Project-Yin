using UnityEngine;

public class MainMenu : MainMenuManager
{
	public LoadMenu LoadMenu;

    protected override bool OnNavItemAction(NavItem item, object actionObj, string[] data)
    {
        var action = (Actions)actionObj;

        switch (action)
        {
			case Actions.New:
				LoadMenu.NewGameMode = true;
                break;

			case Actions.Load:
				LoadMenu.NewGameMode = false;
                break;
        }

        return false;
    }
}
