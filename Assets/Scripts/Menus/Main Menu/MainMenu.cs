using UnityEngine;

public class MainMenu : MainMenuManager
{
    protected override void OnAction(Actions action, NavItem item, NavMenu target, string[] data)
    {
        switch (action)
        {
            case Actions.New:
                // Customized usage of "data" array goes here
                TransitionTo(target);
                break;

            case Actions.Load:
                // Customized usage of "data" array goes here
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
