using UnityEngine;

public class SaveTerminalMenu : GameMenu
{
    public override bool OnNavItemAction(NavItem item, object actionObj, string[] data)
    {
        var action = (Actions)actionObj;

        switch (action)
        {
            case Actions.ConfirmSave:
                // TODO: save something before quitting?
                SaveLoad.Save(false);
                break;
        }

        Close();
        return true;
    }
}
