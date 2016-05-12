using UnityEngine;

public class MainMenu : MainMenuManager
{
	public LoadMenu LoadM;

    protected override void OnAction(Actions action, NavItem item, NavMenu target, string[] data)
    {
        switch (action)
        {
			case Actions.New:
				LoadM.NewGameMode = true;
	            TransitionTo(target);
                break;

			case Actions.Load:
				LoadM.NewGameMode = false;
                TransitionTo(target);
                break;

            case Actions.Settings:
                // TODO
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
