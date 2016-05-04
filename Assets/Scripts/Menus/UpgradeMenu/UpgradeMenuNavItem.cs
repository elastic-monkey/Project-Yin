using UnityEngine;
using System.Collections;

public class UpgradeMenuNavItem : ButtonNavItem {

	public UpgradeMenuManager.Actions Action;

	public override void OnSelect(MenuManager manager)
	{
		manager.OnAction (Action, null);
	}
}
