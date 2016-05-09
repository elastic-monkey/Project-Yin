using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<Item> Items;

    public PlayerInventory()
    {
        Items = new List<Item>();
    }

    public void AddItemToInventory(Item item)
    {
        Items.Add(item);
    }

    public void RemoveItemFromInventory(Item item)
    {
        Items.Remove(item);
    }

    public int GetTotalInventoryValue()
    {
        var value = 0;

        foreach (var item in Items)
        {
            value += item.Value;
        }

        return value;
    }
}

