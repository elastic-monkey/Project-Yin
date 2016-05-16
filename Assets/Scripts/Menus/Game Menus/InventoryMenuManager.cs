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

    public void Start()
    {
        //TODO Get the components
        InventorySlots = new List<InventorySlotNavItem>();
    }

    public void AddItemToInventory(Item item)
    {
        for (var i = 0; i < InventorySlots.Count; i++)
        {
            if (InventorySlots[i].Item.Type == item.Type)
            {
                InventorySlots[i].IncreaseStock(InventorySlots[i].Stock + 1);
            }
            else
            {
                if (InventorySlots[i].Item == null) // Empty inventory slot
                {
                    InventorySlots[i].AddItem(item);
                }
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
                TransitionTo(Transitions[0].TargetMenu);
                _gameManager.SetGamePaused(true);
            }
        }
    }

    public override void OnNavItemFocused(NavItem target)
    {
        base.OnNavItemFocused(target);

    }

    private void OnItemFocused(UpgradeMenuNavItem navItem, Upgradable upgradable)
    {
      
    }

    protected override void OnNavItemAction(object actionObj, NavItem item, NavMenu target, string[] data)
    {

    }
}
