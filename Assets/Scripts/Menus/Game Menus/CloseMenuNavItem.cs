using UnityEngine;
using System.Collections;

public class CloseMenuNavItem : NavItem
{
    private GameMenu _target; 

    protected override void Awake()
    {
        base.Awake();

        _target = GetComponentInParent<GameMenu>();
    }

    public override void OnSelect()
    {
        _target.Close();
    }
}
