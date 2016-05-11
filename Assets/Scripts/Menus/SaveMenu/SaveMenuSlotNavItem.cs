using UnityEngine;
using System.Collections;

public class SaveMenuSlotNavItem : SaveMenuActionNavItem
{
    public int TargetSlot;

    void Start()
    {
        if (TargetSlot < SaveMenuManager.saves.Count)
        {
            var save = SaveMenuManager.saves[TargetSlot];
            _text.text = save.CurrentScene;
        }
        else
        {
            _text.text = "Empty Save Slot";
        }
    }

    public override void OnSelect(MenuManager manager)
    {
        manager.OnAction(this, Action, TargetSlot);
    }
}
