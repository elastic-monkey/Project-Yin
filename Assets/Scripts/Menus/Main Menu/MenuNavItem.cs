﻿using UnityEngine;
using System.Collections;

public class MenuNavItem : NavItem
{
    public MainMenuManager.Actions Action;
    public string[] Data;

    public override void OnSelect(MenuManager manager)
    {
        manager.OnNavItemSelected(this, Action, Data);
    }
}