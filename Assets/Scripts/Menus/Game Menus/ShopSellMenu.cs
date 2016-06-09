using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopSellMenu : GameMenu
{
    public InventoryMenu Inventory;
    public Text Title;

    private void Start()
    {
        UpdateTitle();
    }

    protected override bool OnNavItemAction(NavItem item, object actionObj, string[] data)
    {
        var action = (Actions)actionObj;

        switch (action)
        {
            case Actions.SellComponents:
                Inventory.SellAllComponents();
                UpdateTitle();
                return true;
        }

        return false;
    }

    private void UpdateTitle()
    {
        Title.text = "Sell all Components for " + Inventory.GetTotalComponentsValue() + " Credits?";
    }
}
