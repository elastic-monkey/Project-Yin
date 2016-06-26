using UnityEngine;
using System.Collections;

public class CloseMenuNavItem : NavItem
{
    private GameMenu _parentMenu; 

    protected override void Awake()
    {
        base.Awake();

        _parentMenu = GetComponentInParent<GameMenu>();
    }

    public override void OnSelect()
    {
        _parentMenu.Close();
    }
}
