using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpgradeMenuNavItem : ImageNavItem
{
    public Color PurchasedColor;
    public UpgradeMenuManager.Actions Action;
    public int UpgradeLevel;
    public Text UpgradeCost;

    public override void OnSelect(MenuManager manager)
    {
        manager.OnAction(Action, UpgradeLevel);
    }

    protected override void OnFocus(bool value)
    {
        base.OnFocus(value);
        if (value)
        {
            int cost = (int)Mathf.Ceil(UpgradeLevel / 2.0f);
            UpgradeCost.text = cost.ToString();
        }
    }
}
