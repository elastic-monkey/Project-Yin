using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ShopMenuManager : GameMenuManager
{
    public ItemRepo ItemRepo;
    public InventoryMenuManager PlayerInventory;
    public Text Effect;
    public Text Name;
    public Text FlavorText;
    public Text StockText;
    public Text Price;

    public PlayerBehavior Player
    {
        get
        {
            return _gameManager.Player;
        }
    }

    public override void OnNavItemFocused(NavItem target)
    {
        base.OnNavItemFocused(target);

        NavMenu.UseHoverNavigation = true;

        UpdateInfo(target);
    }

    private void UpdateInfo(NavItem navItem)
    {
        var shopMenuNavItem = navItem as ShopMenuNavItem;

        if (shopMenuNavItem == null)
            return;
        
        var item = shopMenuNavItem.Item;
        var nullItem = item == null;

        Effect.text = nullItem ? string.Empty : item.Effect;
        FlavorText.text = nullItem ? string.Empty : item.FlavorText;
        Name.text = nullItem ? "No Item"  : item.ItemName;
        StockText.text = nullItem ? string.Empty : string.Concat("Inventory: ", PlayerInventory.GetStockInInventory(item), "/", item.MaxStock.ToString());
        Price.text = nullItem ? string.Empty : string.Concat("Cost: ", item.BuyPrice.ToString(), " Credits");
    }

    protected override void OnNavItemAction(object actionObj, NavItem navItem, NavMenu targetNavMenu, string[] data)
    {
        base.OnNavItemAction(actionObj, navItem, targetNavMenu, data);

        var action = (Actions)actionObj;

        switch (action)
        {
            case Actions.BuyItem:
                var item = ItemRepo.Find((Item.ItemType)int.Parse(data[0]));
                if (item != null)
                {
                    PlayerInventory.AddItemToInventory(item);
                    UpdateInfo(navItem);
                }
                break;
        }
    }
}