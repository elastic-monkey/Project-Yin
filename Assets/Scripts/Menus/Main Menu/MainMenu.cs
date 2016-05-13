using UnityEngine;

public class MainMenu : MainMenuManager
{
	public LoadMenu LoadMenu;

    protected override void OnNavItemAction(object actionObj, NavItem item, NavMenu target, string[] data)
    {
        base.OnNavItemAction(actionObj, item, target, data);

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
    }
}
