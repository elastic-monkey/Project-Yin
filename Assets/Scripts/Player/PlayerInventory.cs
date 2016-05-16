using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
	public List<InventorySlotNavItem> Items;

	public void Start()
	{
		Items = new List<InventorySlotNavItem>();
	}

	public void AddItemToInventory(Item item)
	{
		for (var i = 0; i < Items.Count; i++)
		{
			if (Items[i].Item.Type == item.Type)
			{
				Items[i].IncreaseStock(Items[i].Stock + 1);
			}
		}
		Debug.Log("Adding a new Item");
	}
}

