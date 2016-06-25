using UnityEngine;
using System.Collections;

public class MenuNavItem : NavItem
{
    public MainMenu.Actions Action;
    public string[] Data;

    public override void OnSelect()
    {
        throw new System.NotImplementedException();
    }
}
