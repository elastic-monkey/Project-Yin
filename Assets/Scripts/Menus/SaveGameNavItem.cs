using UnityEngine;
using System.Collections;

public class SaveGameNavItem : ButtonNavItem
{
    public int TargetSlot;

    public override void OnSelect(MainMenuManager manager)
    {
        Debug.Log("Load save game: " + TargetSlot);
    }
}
