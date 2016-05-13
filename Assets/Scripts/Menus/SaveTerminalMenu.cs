using UnityEngine;

public class SaveTerminalMenu : GameMenuManager
{
	public bool ContinueGame;

    protected override void OnNavItemAction(object actionObj, NavItem item, NavMenu target, string[] data)
	{
        var action = (Actions)actionObj;

		switch (action)
		{
		case Actions.ConfirmSave:
			// TODO: save something before quitting?
			SaveLoad.Save (false);
			ContinueGame = true;
			break;

		case Actions.RefuseSave:
			ContinueGame = true;
			break;
		}
	}
}
