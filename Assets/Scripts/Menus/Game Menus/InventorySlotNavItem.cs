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
            Item.UseItem(null);
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
            StockDisplay.text = "[" + Stock + "]";
        }
        else
        {
            StockDisplay.text = "";
        }
    }
}
