using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    public int MaxSlots;
    public List<ItemSlot> Slots;

    private ItemRepo _itemRepo;

    private void Awake()
    {
        _itemRepo = GameManager.Instance.ItemRepo;
        Slots = new List<ItemSlot>();
    }

    public void IncreaseStock(Item.ItemType type, int value = 1)
    {
        foreach(var stockItem in Slots)
        {
            if (stockItem.Type == type)
            {
                stockItem.Stock += value;
                return;
            }
        }

        var itemSlot = new ItemSlot();
        itemSlot.Type = type;
        itemSlot.Stock = value;
        Slots.Add(itemSlot);
    }

    public void DecreaseStock(Item.ItemType type, int value = 1)
    {
        for(int i = 0; i < Slots.Count; i++)
        {
            if (Slots[i].Type == type)
            {
                Slots[i].Stock -= value;
                if (Slots[i].Stock <= 0)
                {
                    Slots.RemoveAt(i);
                }
                return;
            }
        }
    }

    public void UseItem(Item.ItemType type)
    {
        var item = _itemRepo.Find(type);
        if (item.CanUse())
        {
            item.UseItem();
            DecreaseStock(type);
        }
    }

    public int GetStock(Item.ItemType type)
    {
        foreach (var slot in Slots)
        {
            if (slot.Type == type)
            {
                return slot.Stock;
            }
        }

        return 0;
    }

    public int GetComponentsSellPrice()
    {
        var sellPrice = _itemRepo.Find(Item.ItemType.Component).SellPrice;

        foreach (var slot in Slots)
        {
            if (slot.Type == Item.ItemType.Component)
            {
                return (slot.Stock * sellPrice);
            }
        }

        return 0;
    }

    public void SellComponents()
    {
        var price = GetComponentsSellPrice();

        GameManager.Instance.Player.Currency.AddCredits(price);

        for (var i = 0; i < Slots.Count; i++)
        {
            var slot = Slots[i];
            if (slot.Type == Item.ItemType.Component)
            {
                Slots.RemoveAt(i);
                break;
            }
        }
    }

    [System.Serializable]
    public class ItemSlot
    {
        public Item.ItemType Type;
        public int Stock;
    }
}
