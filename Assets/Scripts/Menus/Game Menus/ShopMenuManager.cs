using UnityEngine;
using System.Collections.Generic;

public class ShopMenuManager : GameMenuManager
{
    public List<ShopMenuNavItem> ShopSlots;

    public PlayerBehavior Player
    {
        get
        {
            return _gameManager.Player;
        }
    }

    public void Start()
    {
        //TODO Get the components
        ShopSlots = new List<ShopMenuNavItem>();
    }

    public override void HandleInput(bool active)
    {
        base.HandleInput(active);

        if (active)
        {
            if (PlayerInput.IsButtonUp(BackKey) && active)
            {
                _gameManager.SetGamePaused(true);
            }
        }
    }

    public override void OnNavItemFocused(NavItem target)
    {
        base.OnNavItemFocused(target);

        NavMenu.UseHoverNavigation = true;
    }
}