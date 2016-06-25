using UnityEngine;

public class QuitMenu : MainMenu
{
    public override bool OnNavItemAction(NavItem item, object actionObj, object dataObj)
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
