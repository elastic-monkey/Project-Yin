using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryItemNavItem : NavItem
{
    public Item.ItemType Type;
    public Text Stock;

    private PlayerMenu _playerMenu;

    protected override void Awake()
    {
        base.Awake();

        _playerMenu = GetComponentInParent<PlayerMenu>();
    }

    protected override void OnFocus(bool value)
    {
        // Do stuff
    }

    public override void OnSelect()
    {
        if (Type != Item.ItemType.Component && Type != Item.ItemType.Null)
        {
            _playerMenu.UseItem(Type);
        }
    }
}
