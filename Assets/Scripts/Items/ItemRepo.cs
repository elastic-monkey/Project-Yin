using UnityEngine;
using System.Collections.Generic;

public class ItemRepo : MonoBehaviour
{
    public List<Item> Items;

    private void Awake()
    {
        Items = new List<Item>(GetComponentsInChildren<Item>());
    }

    public Item Find(Item.ItemType type)
    {
        foreach (var item in Items)
        {
            if (item.Type == type)
                return item;
        }

        return null;
    }
}
