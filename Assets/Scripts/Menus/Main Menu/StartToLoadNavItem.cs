using UnityEngine;
using System.Collections;

public class StartToLoadNavItem : ChangeNavMenuNavItem
{
    public bool AsNewGame;
    public LoadMenu LoadMenu;

    public override void OnSelect()
    {
        LoadMenu.IsNewGame = AsNewGame;

        base.OnSelect();
    }
}
