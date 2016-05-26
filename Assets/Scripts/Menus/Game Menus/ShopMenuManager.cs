using UnityEngine;
using System.Collections.Generic;

public class ShopMenuManager : GameMenuManager
{
    public PlayerBehavior Player
    {
        get
        {
            return _gameManager.Player;
        }
    }

    public override void OnNavItemFocused(NavItem target)
    {
        base.OnNavItemFocused(target);

        NavMenu.UseHoverNavigation = true;
    }
}