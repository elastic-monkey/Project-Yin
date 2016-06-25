using UnityEngine;
using System.Collections;

public class SaveGameNavItem : CloseMenuNavItem
{
    public override void OnSelect()
    {
        SaveLoad.Save(false);

        base.OnSelect();
    }
}
