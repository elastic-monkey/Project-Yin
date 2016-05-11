using UnityEngine;
using System.Collections;

public class GameOverMenuNavItem : TextNavItem
{
	public GameOverMenuManager.Actions Action;

	public override void OnSelect(MenuManager manager)
	{
		manager.OnAction(this, Action, null);
	}
}
