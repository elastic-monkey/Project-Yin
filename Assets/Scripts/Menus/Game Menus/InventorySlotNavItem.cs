using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventorySlotNavItem : GameNavItem
{
    [SerializeField]
    private int _stock;
    public Item Item;
    public Text StockDisplay;
    public Image ItemIcon;

    public int Stock
    {
        get
        {
            return _stock;
        }
        set
        {
            _stock = value;
        }
    }

    public bool CanUse
    {
        get
        {
            return _stock > 0 && Item != null;
        }
    }

    public void Start()
    {
        UpdateSlot();
    }

    public void AddItem(Item item)
    {
        Item = item;
        IncreaseStock(1);
    }

    public void IncreaseStock(int number)
    {
        if (Stock + number <= Item.MaxStock)
            Stock += number;
    }

    public override void OnSelect(MenuManager manager)
    {
        UseItem();
    }

    protected override void OnFocus(bool value)
    {
        //base.OnFocus(value);
    }

    public void UseItem()
    {
        if (CanUse && Item.CanUse())
        {
            Stock -= 1;
            Item.UseItem();
            UpdateSlot();
        }
        if (Stock == 0)
        {
            RemoveItem();
        }
    }

    public void RemoveItem()
    {
        Item = null;
    }

    public void UpdateSlot()
    {
        if (Item != null && Stock > 0)
        {
            ItemIcon.sprite = Item.Icon.sprite;
            StockDisplay.text = "[" + Stock + "]";
        }
        else
        {
            StockDisplay.text = "";
            Color temp = Color.white;
            temp.a = 0f;
            ItemIcon.color = temp;
        }
    }
}
