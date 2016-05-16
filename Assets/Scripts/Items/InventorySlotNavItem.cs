using UnityEngine;
using System.Collections;

public class InventorySlotNavItem : NavItem
{
	private int _stock;
	public Item Item;

	public InventorySlotNavItem(Item item){
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

	public void UseItem()
	{
		if (CanUse)
		{
			Stock -= 1;
			Item.UseItem();
		}
	}

	public void IncreaseStock(int number)
	{
		Stock += number;
	}

	public override void OnSelect(MenuManager manager)
	{
		UseItem();
	}
}
