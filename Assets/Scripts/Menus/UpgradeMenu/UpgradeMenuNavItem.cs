using UnityEngine;
using System.Collections;

public class UpgradeMenuNavItem : ImageNavItem {

	public UpgradeMenuManager.Actions Action;
    public int UpgradeLevel;

	public override void OnSelect(MenuManager manager)
	{
        manager.OnAction (Action, UpgradeLevel);
	}
}
