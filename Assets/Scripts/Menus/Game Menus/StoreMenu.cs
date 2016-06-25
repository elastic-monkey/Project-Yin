using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StoreMenu : GameMenu
{
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

        var playerCurrency = GameManager.Player.Currency;

        if (item.SellPrice > playerCurrency.CurrentCredits)
            return;

        playerCurrency.RemoveCredits(item.SellPrice);
        _inventory.IncreaseStock(type);

        Buy.UpdateInfo(item, _inventory);
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

            var item = _itemRepo.Find(buyItem.Type);
            if (item == null)
                return;

            Buy.UpdateInfo(item, _inventory);
        }
    }

    [System.Serializable]
    public class BuySubMenu
    {
        public NavMenu NavMenu;
        public Text Effect;
        public Text Name;
        public Text FlavorText;
        public Text StockText;
        public Text Price;

        public void UpdateInfo(Item item, PlayerInventory inventory = null)
        {
            var nullItem = (item == null);
            Effect.text = nullItem ? string.Empty : item.Effect;
            FlavorText.text = nullItem ? string.Empty : item.FlavorText;
            Name.text = nullItem ? "No Item"  : item.ItemName;
            StockText.text = nullItem ? string.Empty : string.Concat("Inventory: ", inventory.GetStock(item.Type), "/", item.MaxStock.ToString());
            Price.text = nullItem ? string.Empty : string.Concat("Cost: ", item.BuyPrice.ToString(), " Credits");
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