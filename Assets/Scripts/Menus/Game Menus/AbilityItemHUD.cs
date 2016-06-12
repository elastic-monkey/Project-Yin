using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AbilityItemHUD : MonoBehaviour
{

    public InventorySlotNavItem ActiveSlot;
    public Text ItemStock;
    public Image ItemIcon;
    public InventoryMenu Inventory;

    private Item.ItemType _lastType;

    public void Start()
    {
        ActiveSlot = null;
        UpdateItemSlot();
    }

    public void Update()
    {
        if (!PlayerInput.OnlyMenus)
        {
            if (PlayerInput.IsButtonDown(Axes.QuickInventoryChange))
            {
                GetNextItem();
            }
            else if (PlayerInput.IsButtonDown(Axes.QuickInventoryUse))
            {
                if (ActiveSlot != null)
                {
                    if (ActiveSlot.Item != null)
                    {
                        ItemQuickUse();
                    }
                }
            }
        }
    }

    private void ItemQuickUse()
    {
        _lastType = ActiveSlot.Item.Type;
        ActiveSlot.UseItem();
        UpdateItemSlot();
        if (ActiveSlot.Item == null) // This was the last use
        {
            GetNextItem();
        }
    }

    private void GetNextItem()
    {
        if (ActiveSlot == null)
        {
            ActiveSlot = Inventory.GetSlotItem(GetNextItemType(Item.ItemType.Null));
        }
        else
        {
            ActiveSlot = Inventory.GetSlotItem(GetNextItemType(_lastType));
        }

        if (ActiveSlot != null)
        {
            _lastType = ActiveSlot.Item.Type;
        }

        UpdateItemSlot();
    }

    private Item.ItemType GetNextItemType(Item.ItemType type)
    {
        if (type == Item.ItemType.Null)
        {
            return Item.ItemType.HealthRecovery;
        }
        else if (type == Item.ItemType.HealthRecovery)
        {
            return Item.ItemType.StaminaRegenRate;
        }
        else if (type == Item.ItemType.StaminaRegenRate)
        {
            return Item.ItemType.HealthRecovery;
        }

        return Item.ItemType.Null;
    }

    private void UpdateItemSlot()
    {
        Color temp = Color.white;
        temp.a = 1.0f;
        if (ActiveSlot == null || ActiveSlot.Item == null)
        {
            ItemStock.text = "";
            temp.a = 0f;
        }
        else
        {
            ItemStock.text = "[" + ActiveSlot.Stock.ToString() + "]";
            ItemIcon.sprite = ActiveSlot.Item.Icon;
        }

        ItemIcon.color = temp;
    }
}
