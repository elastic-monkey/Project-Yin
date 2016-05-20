using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopMenuNavItem : GameNavItem
{
    public Item Item;
    public Image ItemIcon;
    public InventoryMenuManager PlayerInventory;

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

    public void SellItemToPlayer()
    {
        PlayerInventory.AddItemToInventory(Item);
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
}
