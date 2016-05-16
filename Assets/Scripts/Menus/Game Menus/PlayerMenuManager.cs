using UnityEngine;
using System.Collections;

public class PlayerMenuManager : GameMenuManager {

    protected override void OnNavItemAction(object actionObj, NavItem item, NavMenu target, string[] data)
    {
        base.OnNavItemAction(actionObj, item, target, data);
    }
}