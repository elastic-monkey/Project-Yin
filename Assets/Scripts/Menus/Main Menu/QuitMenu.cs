using UnityEngine;

public class QuitMenu : MainMenuManager
{
    protected override void OnAction(Actions action, NavItem item, NavMenu target, string[] data)
    {
        switch (action)
        {
            case Actions.Quit:
                // TODO: save something before quitting?
                Application.Quit();
                break;

            case Actions.Back:
                TransitionTo(target);
                break;
        }
    }
}
