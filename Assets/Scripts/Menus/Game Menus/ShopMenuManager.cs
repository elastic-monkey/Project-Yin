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
        
    public override void HandleInput(bool active)
    {
        base.HandleInput(active);

        if (active)
        {
            if (PlayerInput.IsButtonUp(BackKey) && active)
            {
                _gameManager.SetGamePaused(false);
            }
        }
    }

    public override void OnNavItemFocused(NavItem target)
    {
        base.OnNavItemFocused(target);

        NavMenu.UseHoverNavigation = true;
    }
}