using UnityEngine;

public class StartMenu : MainMenu
{
    public LoadMenu LoadMenu;

    public override bool OnNavItemAction(NavItem item, object actionObj, object dataObj)
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
