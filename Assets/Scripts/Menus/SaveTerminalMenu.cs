using UnityEngine;

public class SaveTerminalMenu : GameMenuManager
{
	public bool ContinueGame;

	protected override void OnAction(Actions action, NavItem item, NavMenu target, string[] data)
	{
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
