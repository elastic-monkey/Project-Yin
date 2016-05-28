using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SellComponentsMenu : GameMenuManager
{

    public InventoryMenuManager Inventory;
    public Text Title;

    public void Start()
    {
        UpdateTitle();
    }

    protected override void OnNavItemAction(object actionObj, NavItem item, NavMenu target, string[] data)
    {
        base.OnNavItemAction(actionObj, item, target, data);

        var action = (Actions)actionObj;

        switch (action)
        {
            case Actions.SellComponents:
                Inventory.SellAllComponents();
                UpdateTitle();
                break;
        }
    }

    private void UpdateTitle()
    {
        Title.text = "Sell all Components for " + Inventory.GetTotalComponentsValue() + " Credits?";
    }
}
