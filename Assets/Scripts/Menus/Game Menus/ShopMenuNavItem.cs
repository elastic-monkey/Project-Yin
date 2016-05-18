using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopMenuNavItem : GameNavItem
{
    public Item Item;
    public Image ItemIcon;
    public InventoryMenuManager PlayerInventory;

    public bool CanBuy
    {
        get
        {
            //return Item.Player.Credits.CurrentCredits > Item.BuyPrice && Item != null;
            return true;
        }
    }

    void Start()
    {
        UpdateSlot();
        PlayerInventory = GameObject.Find("InventorySubMenu").GetComponent<InventoryMenuManager>();
    }

    public override void OnSelect(MenuManager manager)
    {
        SellItemToPlayer();
    }

    public void SellItemToPlayer()
    {
        //Item.Player.Credits.CurrentCredits
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
