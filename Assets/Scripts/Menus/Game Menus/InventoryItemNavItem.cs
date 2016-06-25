﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryItemNavItem : NavItem
{
    public Item.ItemType Type;
    public Image IconImage;
    public Text Stock;

    private PlayerMenu _playerMenu;

    protected override void Awake()
    {
        base.Awake();

        _playerMenu = GetComponentInParent<PlayerMenu>();
    }

    public override void OnSelect()
    {
        _playerMenu.UseItem(Type);
    }
}