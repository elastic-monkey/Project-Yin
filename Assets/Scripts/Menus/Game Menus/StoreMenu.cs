using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StoreMenu : GameMenu
{
    public Text CurrencyText;
    public BuySubMenu Buy;
    public SellSubMenu Sell;

    private ItemRepo _itemRepo;
    private PlayerInventory _inventory;

    protected override void Awake()
    {
        base.Awake();

        _itemRepo = GameManager.ItemRepo;
        _inventory = GameManager.Player.Inventory;
    }

    public void BuyItem(Item.ItemType type)
    {
        var item = _itemRepo.Find(type);
        if (item == null)
            return;

        var player = GameManager.Player;
        var playerCurrency = player.Currency;

        if (item.BuyPrice > playerCurrency.CurrentCredits)
            return;

        if (item.MaxStock <= player.Inventory.GetStock(type))
            return;

        playerCurrency.RemoveCredits(item.BuyPrice);
        _inventory.IncreaseStock(type);
        CurrencyText.text = playerCurrency.CurrentCredits.ToString();

        Buy.UpdateInfo(type, _itemRepo, _inventory);
    }

    public void SellComponents()
    {
        _inventory.SellComponents();
        Sell.UpdateInfo(_inventory);
    }

    public override void OnNavItemFocused(NavItem target)
    {
        base.OnNavItemFocused(target);

        if (CurrentNavMenu == Buy.NavMenu)
        {
            var buyItem = target as BuyItemNavItem;
            if (buyItem == null)
                return;

            Buy.UpdateInfo(buyItem, _itemRepo, _inventory);
        }
    }

    public override void Open()
    {
        base.Open();

        CurrencyText.text = GameManager.Player.Currency.CurrentCredits.ToString();
    }

    public override void ChangeTo(NavMenu target, bool submenu = true)
    {
        base.ChangeTo(target, submenu);
    
        if (CurrentNavMenu == Buy.NavMenu)
        {
            Buy.UpdateAll(_itemRepo, _inventory);
        }
        else if (CurrentNavMenu == Sell.NavMenu)
        {
            Sell.UpdateInfo(_inventory);
        }
    }

    [System.Serializable]
    public class BuySubMenu
    {
        public NavMenu NavMenu;
        public Text Effect, Name, FlavorText, StockText, Price;

        public void UpdateInfo(Item.ItemType type, ItemRepo itemRepo, PlayerInventory inventory)
        {
            foreach (var item in NavMenu.GetNavItems())
            {
                var buyItem = item as BuyItemNavItem;
                if (buyItem == null)
                    continue;

                if (buyItem.Type == type)
                {
                    UpdateInfo(buyItem, itemRepo, inventory);
                }
            }
        }

        public void UpdateInfo(BuyItemNavItem buyNavItem, ItemRepo itemRepo, PlayerInventory inventory)
        {
            var item = itemRepo.Find(buyNavItem.Type);
            if (item == null)
            {
                
                Effect.text = string.Empty;
                FlavorText.text = string.Empty;
                Name.text = "No Item";
                StockText.text = string.Empty;
                Price.text = string.Empty;
                buyNavItem.SetSprite(null);
            }
            else
            {
                Effect.text = item.Effect;
                FlavorText.text = item.FlavorText;
                Name.text = item.ItemName;
                var playerStock = inventory.GetStock(item.Type);
                var maxStock = item.MaxStock;
                var stockText = (maxStock == playerStock) ? string.Concat("<color=#f28f8aff>", playerStock, "/", maxStock, "</color>") :
                    string.Concat(playerStock, "/", maxStock);
                var cost = item.BuyPrice;
                var costText = GameManager.Instance.Player.Currency.CurrentCredits >= cost ? cost.ToString() : string.Concat("<color=#f28f8aff>",cost,"</color>"); 
                StockText.text = string.Concat("Inventory: ", stockText);
                Price.text = string.Concat("Cost: ", costText, " Credits");
                buyNavItem.SetSprite(item.Icon);
            }
        }

        public void UpdateAll(ItemRepo itemRepo, PlayerInventory inventory)
        {
            foreach (var item in NavMenu.GetNavItems())
            {
                var buyItem = item as BuyItemNavItem;
                if (buyItem == null)
                    continue;

                UpdateInfo(buyItem, itemRepo, inventory);
            }

            var currentItem = NavMenu.GetCurrentNavItem() as BuyItemNavItem;
            if (currentItem == null)
                return;

            UpdateInfo(currentItem, itemRepo, inventory);
        }
    }

    [System.Serializable]
    public class SellSubMenu
    {
        public NavMenu NavMenu;
        public Text Title;

        public void UpdateInfo(PlayerInventory inventory)
        {
            Title.text = "Sell all Components for " + inventory.GetComponentsSellPrice() + " Credits?";
        }
    }
}