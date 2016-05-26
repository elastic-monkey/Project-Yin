﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyDropItem : Item
{

    public int Value;
    public InventoryMenuManager Inventory;

    void Start()
    {
        Icon = GetComponent<Image>();
        Type = ItemType.Component;
        Effect = "Can be sold for " + Value.ToString() + " Credits";
        FlavorText = "A bunch of circuitry dropped by automatons";
    }

    public override bool CanUse()
    {
        return true;
    }

    public override void UseItem()
    {

    }
}