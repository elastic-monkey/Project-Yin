using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopMenuNavItem : GameNavItem
{
    public Item Item;
    public Image ItemIcon;
    public InventoryMenuManager PlayerInventory;

    public Text Effect;
    public Text Name;
    public Text FlavorText;
    public Text StockText;
    public Text Price;

    public bool CanSell
    {
        get
        {
            return Item.Player.Currency.CanBuy(Item.BuyPrice) && Item != null;
        }
    }

    void Start()
    {
        UpdateSlot();
        PlayerInventory = GameObject.Find("InventorySubMenu").GetComponent<InventoryMenuManager>();
    }

    public override void OnSelect(MenuManager manager)
    {
        if (CanSell)
            SellItemToPlayer();
    }

    protected override void OnFocus(bool value)
    {
        base.OnFocus(value);
        if (Focused)
        {
            UpdateDescription();
        }
    }

    public void SellItemToPlayer()
    {
        PlayerInventory.AddItemToInventory(Item);
        UpdateDescription();
    }

    public void UpdateSlot()
    {
        if (Item != null)
        {
            ItemIcon.sprite = Item.Icon.sprite;
        }
        else
        {
            Color temp = Color.white;
            temp.a = 0f;
            ItemIcon.color = temp;
        }
    }

    public void UpdateDescription()
    {
        if (Item != null)
        {
            Effect.text = Item.Effect;
            FlavorText.text = Item.FlavorText;
            Name.text = Item.ItemName;
            int stock = PlayerInventory.GetStockInInventory(Item);
            StockText.text = "Inventory: " + stock.ToString() + "/" + Item.MaxStock.ToString();
            Price.text = Item.BuyPrice.ToString() + " Credits";
        }
        else
        {
            Effect.text = "";
            FlavorText.text = "";
            Name.text = "";
            StockText.text = ""; 
            Price.text = "";
        }
    }
}
