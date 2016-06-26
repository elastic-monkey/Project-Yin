using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMenu : GameMenu
{
    public UpgradeSubMenu Upgrades;
    public InventorySubMenu Inventory;

    private PlayerInventory _inventory;
    private ItemRepo _itemRepo;

    protected override void Awake()
    {
        base.Awake();

        _itemRepo = GameManager.ItemRepo;
        _inventory = GameManager.Player.Inventory;
    }

    public override void Open()
    {
        base.Open();
    
        Upgrades.UpdateInfo(_itemRepo, _inventory);
        Inventory.UpdateInfo(_itemRepo, _inventory);
    }

    public override void OnNavItemFocused(NavItem target)
    {
        if (CurrentNavMenu == Upgrades.NavMenu)
        {
            Upgrades.UpdateInfo(_itemRepo, _inventory);
        }
        else if (CurrentNavMenu == Inventory.NavMenu)
        {
            Inventory.UpdateInfo(_itemRepo, _inventory);
        }
    }

    public void UseItem(Item.ItemType type)
    {
        var item = _itemRepo.Find(type);
        if (item.CanUse() && _inventory.GetStock(type) > 0)
        {
            _inventory.DecreaseStock(type);
            item.UseItem();

            Inventory.UpdateInfo(_itemRepo, _inventory);
        }
    }

    public void Upgrade(Upgradable.UpgradableTypes type)
    {
        switch (type)
        {
            case Upgradable.UpgradableTypes.Health:
                Upgrade(GameManager.Player.Health);
                break;

            case Upgradable.UpgradableTypes.Shield:
                Upgrade(GameManager.Player.Abilities.Find(Ability.AbilityType.Shield));
                break;

            case Upgradable.UpgradableTypes.Speed:
                Upgrade(GameManager.Player.Abilities.Find(Ability.AbilityType.Speed));
                break;

            case Upgradable.UpgradableTypes.Stamina:
                Upgrade(GameManager.Player.Stamina);
                break;

            case Upgradable.UpgradableTypes.Strength:
                Upgrade(GameManager.Player.Abilities.Find(Ability.AbilityType.Strength));
                break;
        }

        Upgrades.UpdateInfo(_itemRepo, _inventory);
    }

    private void Upgrade(Upgradable upgradable)
    {
        var currentLevel = upgradable.CurrentLevel;
        if (upgradable.CanBeUpgradedTo(currentLevel + 1, GameManager.Player))
        {
            upgradable.UpgradeTo(currentLevel + 1, GameManager.Player);
        }
    }

    [System.Serializable]
    public class UpgradeSubMenu
    {
        public MatrixNavMenu NavMenu;
        public Sprite DefaultSprite;

        public void UpdateInfo(ItemRepo itemRepo, PlayerInventory inventory)
        {
//            var slotIndex = 0;
//
//            foreach (var navItems in NavMenu.Items)
//            {
//                foreach (var navItem in navItems.Items)
//                {
//                    var upgradableNavItem = navItem as UpgradableNavItem;
//                    if (upgradableNavItem == null)
//                        continue;
//
//                    upgradableNavItem.IconImage.sprite = DefaultSprite;
//
//                    slotIndex++;
//                }
//            }
        }
    }

    [System.Serializable]
    public class InventorySubMenu
    {
        public MatrixNavMenu NavMenu;

        public void UpdateInfo(ItemRepo itemRepo, PlayerInventory inventory)
        {
            var slotIndex = 0;

            foreach (var navItems in NavMenu.Items)
            {
                foreach (var navItem in navItems.Items)
                {
                    var inventoryNavItem = navItem as InventoryItemNavItem;
                    if (inventoryNavItem == null)
                        continue;

                    if (slotIndex < inventory.Slots.Count)
                    {
                        var slot = inventory.Slots[slotIndex];
                        var item = itemRepo.Find(slot.Type);
                        inventoryNavItem.IconImage.sprite = item.Icon;

                        slotIndex++;
                    }
                    else
                    {
                        inventoryNavItem.Type = Item.ItemType.Null;
                        inventoryNavItem.IconImage.color = Color.clear;
                        inventoryNavItem.Stock.text = 0.ToString();
                    }
                }
            }
        }
    }
}
