using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadSlotNavItem : MenuNavItem
{
    public int Slot;

    public void Start()
    {
        Data = new string[1];
        Data[0] = Slot.ToString();

        var text = TargetGraphic as Text;
        if (text != null && Slot <LoadMenu.SavedGames.Count)
        {
            text.text = LoadMenu.SavedGames[Slot].CurrentScene;
        }
    }
}
