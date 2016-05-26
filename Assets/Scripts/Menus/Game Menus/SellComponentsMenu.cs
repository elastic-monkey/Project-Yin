using UnityEngine;
using System.Collections;

public class SellComponentsMenu : GameMenuManager {
    
    protected override void OnNavItemAction(object actionObj, NavItem item, NavMenu target, string[] data)
    {
        base.OnNavItemAction(actionObj, item, target, data);

        var action = (Actions)actionObj;

        switch (action)
        {
            case Actions.SellComponents:
                SetActive(false);
                break;
        }
    }
}
