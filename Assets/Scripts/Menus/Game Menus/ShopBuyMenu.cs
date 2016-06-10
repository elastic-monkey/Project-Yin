using UnityEngine.UI;

public class ShopBuyMenu : GameMenu
{
    public ItemRepo ItemRepo;
    public InventoryMenu PlayerInventory;
    public Text Effect;
    public Text Name;
    public Text FlavorText;
    public Text StockText;
    public Text Price;

    public PlayerBehavior Player
    {
        get
        {
            return GameManager.Player;
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
        StockText.text = nullItem ? string.Empty : string.Concat("Inventory: ", PlayerInventory.GetStockInInventory(item.Type), "/", item.MaxStock.ToString());
        Price.text = nullItem ? string.Empty : string.Concat("Cost: ", item.BuyPrice.ToString(), " Credits");
    }

    public override bool OnNavItemAction(NavItem navItem, object actionObj, string[] data)
    {
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
                return true;
        }

        return false;
    }
}