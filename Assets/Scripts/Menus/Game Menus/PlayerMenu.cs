using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMenu : GameMenu
{
    public Text CurrencyText;
    public UpgradeSubMenu Upgrades;
    public InventorySubMenu Inventory;

    private PlayerBehavior _player;
    private PlayerInventory _inventory;
    private ItemRepo _itemRepo;

    protected override void Awake()
    {
        base.Awake();

        _player = GameManager.Player;
        _itemRepo = GameManager.ItemRepo;
        _inventory = GameManager.Player.Inventory;
    }

    public override void Open()
    {
        base.Open();
    
        CurrencyText.text = _player.Currency.CurrentCredits.ToString();
        Upgrades.UpdateInfo(_player);
        Inventory.UpdateInfo(_itemRepo, _inventory);
    }

    public override void OnNavItemFocused(NavItem target)
    {
        if (CurrentNavMenu == Upgrades.NavMenu)
        {
            Upgrades.UpdateInfo(target, _player);
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
            item.UseItem();
            _inventory.DecreaseStock(type);

            Inventory.UpdateInfo(_itemRepo, _inventory);
        }
    }

    public void Upgrade(Upgradable.UpgradableTypes type)
    {
        Upgrade(_player.FindUpgradable(type));
        Upgrades.UpdateInfo(_player);
    }

    private void Upgrade(Upgradable upgradable)
    {
        if (upgradable == null)
            return;
        
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
        public Text SkillPoints, Cost, Effect, FlavorText;

        public void UpdateInfo(NavItem navItem, PlayerBehavior player)
        {
            var upgradableNavItem = navItem as UpgradableNavItem;
            if (upgradableNavItem == null)
                return;

            var upgradable = player.FindUpgradable(upgradableNavItem.Type);
            if (upgradable == null)
                return;

            var currentLevel = upgradable.CurrentLevel;
            var maxLevel = (currentLevel == Upgradable.MaxLevel);
            var upgradeCost = upgradable.UpgradeCost(currentLevel + 1);
            var canPurchase = maxLevel ? false : player.Experience.SkillPoints >= upgradeCost;
            Cost.text = maxLevel ? "-" : canPurchase ? upgradeCost.ToString() : string.Concat("<color=#f28f8aff>",upgradeCost,"</color>");
            Effect.text = upgradable.GetEffectText(currentLevel + 1);
            FlavorText.text = upgradable.GetFlavorText();
        }

        public void UpdateInfo(PlayerBehavior player)
        {
            foreach (var navItem in NavMenu.GetNavItems())
            {
                var upgradableNavItem = navItem as UpgradableNavItem;
                if (upgradableNavItem == null)
                    continue;

                var type = upgradableNavItem.Type;
                var upgradable = player.FindUpgradable(type);
                var currentLevel = upgradable.CurrentLevel;
                var level = upgradableNavItem.Level;

                var active = (level <= currentLevel) || (level == currentLevel + 1 && upgradable.UpgradeCost(level) <= player.Experience.SkillPoints);
                var purchased = (level <= currentLevel);

                upgradableNavItem.SetActive(active, upgradable, purchased);
            }

            SkillPoints.text = player.Experience.SkillPoints.ToString();
            UpdateInfo(NavMenu.GetCurrentNavItem(), player);
        }
    }

    [System.Serializable]
    public class InventorySubMenu
    {
        public MatrixNavMenu NavMenu;
        public Text Name, Effect, FlavorText;

        public void UpdateInfo(NavItem navItem, ItemRepo itemRepo, PlayerInventory inventory)
        {
            var inventoryNavItem = navItem as InventoryItemNavItem;
            if (inventoryNavItem == null)
                return;

            var item = itemRepo.Find(inventoryNavItem.Type);
            if (item == null)
            {
                Name.text = "No item selected";
                Effect.text = string.Empty;
                FlavorText.text = string.Empty;
            }
            else
            {
                Name.text = item.ItemName;
                Effect.text = item.Effect;
                FlavorText.text = item.FlavorText;
            }
        }

        public void UpdateInfo(ItemRepo itemRepo, PlayerInventory inventory)
        {
            var slotIndex = 0;

            foreach (var navItem in NavMenu.GetNavItems())
            {
                var inventoryNavItem = navItem as InventoryItemNavItem;
                if (inventoryNavItem == null)
                    continue;

                if (slotIndex < inventory.Slots.Count)
                {
                    var slot = inventory.Slots[slotIndex];
                    var item = itemRepo.Find(slot.Type);
                    var img = inventoryNavItem.TargetGraphic as Image;
                    img.sprite = item.Icon;
                    img.color = Color.white;
                    inventoryNavItem.Type = slot.Type;
                    inventoryNavItem.Stock.text = slot.Stock.ToString();

                    slotIndex++;
                }
                else
                {
                    inventoryNavItem.Type = Item.ItemType.Null;
                    var img = inventoryNavItem.TargetGraphic as Image;
                    img.color = Color.clear;
                    inventoryNavItem.Stock.text = "-";
                }
            }

            UpdateInfo(NavMenu.GetCurrentNavItem(), itemRepo, inventory);
        }
    }
}
