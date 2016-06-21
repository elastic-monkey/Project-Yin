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
        if (text != null)
        {
			GameState save = LoadMenu.GetSaveInSlot (Slot);
			if (save != null)
			{
                text.text = save.CurrentScene + " " + save.SaveDate;
			}
			else
			{
				text.text = "Empty Save Slot";
			}
        }
    }
}
