using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpgradeMenuNavItem : GameNavItem
{
    public Color PurchasedColor, DisabledColor;
    public int UpgradeLevel;

    public override void OnSelect(MenuManager manager)
    {
        Data = new string[1];
        Data[0] = UpgradeLevel.ToString();

        manager.OnNavItemSelected(this, Action, Data);
    }

    protected override void OnFocus(bool value)
    {
        //base.OnFocus(value);
    }

    public void SetPurchased(bool value)
    {
        TargetGraphic.color = value ? PurchasedColor : _initialColor;
    }

    public void SetDisabled(bool value)
    {
        TargetGraphic.color = value ? DisabledColor : _initialColor;
    }
}
