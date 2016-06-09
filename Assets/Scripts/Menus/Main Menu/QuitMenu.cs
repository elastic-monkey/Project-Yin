using UnityEngine;

public class QuitMenu : MainMenuManager
{
    protected override bool OnNavItemAction(NavItem item, object actionObj, string[] data)
    {
        var action = (Actions)actionObj;

        switch (action)
        {
            case Actions.Quit:
                Application.Quit();
                return true;
        }

        return false;
    }
}
