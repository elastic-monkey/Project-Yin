using UnityEngine;

public class QuitMenu : MainMenuManager
{
    protected override void OnNavItemAction(object actionObj, NavItem item, NavMenu target, string[] data)
    {
        var action = (Actions)actionObj;

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
