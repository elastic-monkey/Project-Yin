using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpgradeMenuNavItem : GameNavItem
{
    public int UpgradeLevel;

    public override void OnSelect(MenuManager manager)
    {
        Data = new string[1];
        Data[0] = UpgradeLevel.ToString();

        manager.OnNavItemSelected(this, Action, Data);
    }

    protected override void OnFocus(bool value)
    {
    }

    public void Purchase(bool value)
    {
        Focus(value);
    }
}
