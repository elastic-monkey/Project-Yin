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

    public Text Effect;
    public Text Name;
    public Text FlavorText;

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
        {
            Stock += number;
            UpdateSlot();
        }
    }

    public override void OnSelect(IMenu manager)
    {
        UseItem();
    }

    protected override void OnFocus(bool value)
    {
        if (Focused)
        {
            UpdateDescription();
        }
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
        Stock = 0;
        UpdateSlot();
    }

    public void UpdateSlot()
    {
        Color temp = Color.white;
        temp.a = 1.0f;
        if (Item != null && Stock > 0)
        {
            ItemIcon.sprite = Item.Icon;
            StockDisplay.text = "[" + Stock + "]";
        }
        else
        {
            StockDisplay.text = "";
            temp.a = 0f;
        }
        ItemIcon.color = temp;
    }

    public void UpdateDescription()
    {
        if (Item != null)
        {
            Name.text = Item.ItemName;
            Effect.text = Item.Effect;
            FlavorText.text = Item.FlavorText;
        }
        else
        {
            Name.text = "";
            Effect.text = "";
            FlavorText.text = "";
        }
    }
}

[System.Serializable]
public class InventorySlotSave
{
    public Item.ItemType Type;
    public int Stock;

    public InventorySlotSave(Item.ItemType type, int stock)
    {
        Type = type;
        Stock = stock;
    }
}
