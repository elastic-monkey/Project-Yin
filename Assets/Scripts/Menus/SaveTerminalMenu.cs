using UnityEngine;

public class SaveTerminalMenu : GameMenuManager
{
    protected override void OnNavItemAction(object actionObj, NavItem item, NavMenu target, string[] data)
    {
        var action = (Actions)actionObj;

        switch (action)
        {
            case Actions.ConfirmSave:
                // TODO: save something before quitting?
                SaveLoad.Save(false);
                break;
        }

        SetActive(false);

    }
}
