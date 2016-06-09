using UnityEngine;
using System.Collections;

public class PlayerMenu : GameMenu
{
    protected override bool OnNavItemAction(NavItem navItem, object actionObj, string[] data)
    {
        var action = (Actions)actionObj;

        switch (action)
        {
            case Actions.Close:
                Close();
                return true;
        }

        return false;
    }
}
