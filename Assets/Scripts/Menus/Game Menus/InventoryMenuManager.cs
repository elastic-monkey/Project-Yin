using UnityEngine;
using System.Collections.Generic;

public class InventoryMenuManager : GameMenuManager
{
    public List<InventorySlotNavItem> InventorySlots;

    public PlayerBehavior Player
    {
        get
        {
            return _gameManager.Player;
        }
    }

    public void AddItemToInventory(Item item)
    {
        Debug.Log(item.Type);
        for (var i = 0; i < InventorySlots.Count; i++)
        {
            InventorySlotNavItem SlotItem = InventorySlots[i];
            if (SlotItem.Item != null)
            {
                if (SlotItem.Item.Type == item.Type && SlotItem.Stock < item.MaxStock)
                {
                    SlotItem.IncreaseStock(1);
                    Player.Currency.RemoveCredits(item.BuyPrice);
                    return;
                }
            }
        }
        for (var i = 0; i < InventorySlots.Count; i++)
        {
            InventorySlotNavItem SlotItem = InventorySlots[i];
            if (SlotItem.Item == null)
            {
                SlotItem.Item = item;
                SlotItem.IncreaseStock(1);
                Player.Currency.RemoveCredits(item.BuyPrice);
                return;
            }
        }
    }

    public override void HandleInput(bool active)
    {
        base.HandleInput(active);

        if (active)
        {
            if (PlayerInput.IsButtonUp(BackKey) && active)
            {
                _gameManager.SetGamePaused(true);
            }
        }
    }

    public override void OnNavItemFocused(NavItem target)
    {
        base.OnNavItemFocused(target);

        NavMenu.UseHoverNavigation = true;
    }
}
