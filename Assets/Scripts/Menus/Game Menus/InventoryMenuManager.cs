using UnityEngine;
using System.Collections.Generic;

public class InventoryMenuManager : GameMenuManager
{
    public List<InventorySlotNavItem> InventorySlots;

    public PlayerBehavior Player
    {
        get
        {
            return GameManager.Player;
        }
    }

    public void AddItemToInventory(Item item)
    {
        if (Player.Currency.CurrentCredits < item.BuyPrice)
            return;

        var stock = GetStockInInventory(item.Type);
        if (stock == 0)
        {
            // TODO: add new entry
        }
        else if (stock < item.MaxStock)
        {
            var slotItem = GetSlotItem(item);
            slotItem.IncreaseStock(1);
            Player.Currency.RemoveCredits(item.BuyPrice);
        }
    }

    public override void HandleInput(bool active)
    {
        base.HandleInput(active);

        if (active)
        {
            if (PlayerInput.IsButtonUp(BackKey) && active)
            {
                GameManager.SetGamePaused(true);
            }
        }
    }

    public override void OnNavItemFocused(NavItem target)
    {
        base.OnNavItemFocused(target);

        NavMenu.UseHoverNavigation = true;
    }

    public int GetStockInInventory(Item.ItemType Type)
    {
        foreach (var slotItem in InventorySlots)
        {
            if (slotItem.Item.Type == Type)
            {
                return slotItem.Stock;
            }
        }

        return 0;
    }

    public InventorySlotNavItem GetSlotItem(Item item)
    {
        foreach (var slotItem in InventorySlots)
        {
            if (slotItem.Item.Type == item.Type)
                return slotItem;
        }

        return null;
    }

    public int GetTotalComponentsValue()
    {
        for (var i = 0; i < InventorySlots.Count; i++)
        {
            InventorySlotNavItem Slot = InventorySlots[i];
            if (Slot.Item != null)
            {
                if (Slot.Item.Type == Item.ItemType.Component)
                {
                    return Slot.Stock * Slot.Item.SellPrice;
                }
            }
        }

        return 0;
    }

    public void SellAllComponents()
    {
        for (var i = 0; i < InventorySlots.Count; i++)
        {
            InventorySlotNavItem Slot = InventorySlots[i];
            if (Slot.Item != null)
            {
                if (Slot.Item.Type == Item.ItemType.Component)
                {
                    Debug.Log("Selling " + Slot.Stock + " components");
                    Player.Currency.AddCredits(Slot.Stock * Slot.Item.SellPrice);
                    Slot.RemoveItem();
                }
            }
        }
    }
}
