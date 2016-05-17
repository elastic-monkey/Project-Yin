using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventorySlotNavItem : GameNavItem
{
    private int _stock;
    public Item Item;
    public Text StockDisplay;

    public InventorySlotNavItem(Item item)
    {
        Item = item;
        Stock = 1;
    }

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
            return _stock > 0;
        }
    }

    public void AddItem(Item item)
    {
        Item = item;
        IncreaseStock(1);
    }

    public void IncreaseStock(int number)
    {
        Stock += number;
    }

    public override void OnSelect(MenuManager manager)
    {
        Debug.Log("asdasdasd");
        UseItem();
    }

    protected override void OnFocus(bool value)
    {
        //base.OnFocus(value);
    }

    public void UseItem()
    {
        if (CanUse)
        {
            Stock -= 1;
            Item.UseItem();
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
}
