using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AbilityItemHUD : MonoBehaviour
{
    public Text ItemStock;
    public Image ItemIcon;
    public Item.ItemType CurrentType;

    private PlayerInventory _inventory;
    private ItemRepo _itemRepo;

    public GameManager GameManager
    {
        get
        {
            return GameManager.Instance;
        }
    }

    private void Start()
    {
        _inventory = GameManager.Player.Inventory;
        _itemRepo = GameManager.ItemRepo;

        FindFirstValidType();

        UpdateItemSlot();
    }

    private void FindFirstValidType()
    {
        CurrentType = Item.ItemType.Null;
        foreach (var slot in _inventory.Slots)
        {
            if (IsValidQuickItemType(slot.Type))
            {
                CurrentType = slot.Type;
                return;
            }
        }
    }

    private void FindNextValidType()
    {
        var currentIndex = 0;

        // Find current type index
        for (int i = 0; i < _inventory.Slots.Count; i++)
        {
            var slot = _inventory.Slots[i];
            if (slot.Type == CurrentType)
            {
                currentIndex = i;
                break;
            }
        }

        // Then try items after current
        for (int i = currentIndex + 1; i < _inventory.Slots.Count; i++)
        {
            var slot = _inventory.Slots[i];
            if (IsValidQuickItemType(slot.Type))
            {
                CurrentType = slot.Type;
                return;
            }
        }

        // If not found, cycle
        for (int i = 0; i < currentIndex; i++)
        {
            var slot = _inventory.Slots[i];
            if (IsValidQuickItemType(slot.Type))
            {
                CurrentType = slot.Type;
                return;
            }
        }
    }

    private void Update()
    {
        if (GameManager.IsGamePaused)
            return;

        if (PlayerInput.OnlyMenus)
            return;

        if (!IsValidQuickItemType(CurrentType))
        {
            FindFirstValidType();
        }

        UpdateItemSlot();

        if (PlayerInput.IsButtonDown(Axes.QuickInventoryChange))
        {
            FindNextValidType();
            UpdateItemSlot();
        }
        else if (PlayerInput.IsButtonDown(Axes.QuickInventoryUse))
        {
            UseCurrentItem();
        }
    }

    private void UseCurrentItem()
    {
        _inventory.UseItem(CurrentType);

        var stock = _inventory.GetStock(CurrentType);
        if (stock == 0)
        {
            var last = CurrentType;
            FindNextValidType();
            if (last == CurrentType)
            {
                CurrentType = Item.ItemType.Null;
            }
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

    private bool IsValidQuickItemType(Item.ItemType type)
    {
        return (type != Item.ItemType.Component && type != Item.ItemType.Null);
    }

    private void UpdateItemSlot()
    {
        if (IsValidQuickItemType(CurrentType))
        {
            ItemStock.text = string.Concat("[", _inventory.GetStock(CurrentType), "]");
            ItemIcon.sprite = _itemRepo.Find(CurrentType).Icon;
            ItemIcon.color = Color.white;
        }
        else
        {
            ItemStock.text = "";
            ItemIcon.color = Color.clear;
        }
    }
}
