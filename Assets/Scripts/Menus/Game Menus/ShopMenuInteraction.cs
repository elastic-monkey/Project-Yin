using UnityEngine;
using System.Collections;

public class ShopMenuInteraction : GameMenuManager
{
    protected override void OnNavItemAction(object actionObj, NavItem item, NavMenu target, string[] data)
    {
        base.OnNavItemAction(actionObj, item, target, data);

        var action = (Actions)actionObj;

        switch (action)
        {
            case Actions.LeaveShop:
                SetActive(false);
                break;
        }
    }
}
