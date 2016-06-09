using UnityEngine;

public class UpgradeMenuNavItem : GameNavItem
{
    public bool Purchased;
    public Color PurchasedColor;
    public int UpgradeLevel;

    public override void OnSelect(IMenu manager)
    {
        Data = new string[1];
        Data[0] = UpgradeLevel.ToString();

        manager.OnNavItemSelected(this, Action, Data);
    }

    protected override void OnFocus(bool value)
    {
        //base.OnFocus(value);
    }

    public void Purchase(bool value)
    {
        Purchased = true;
        UpdateColor();
    }

    public override void UpdateColor()
    {
        TargetGraphic.color = Disabled ? DisabledColor : Purchased ? PurchasedColor : _initialColor;
    }
}
