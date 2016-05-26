using UnityEngine;
using System.Collections;

public class OpenMenuInteraction : PlayerInteraction
{
    public GameMenuManager Target;

    public override void StartInteraction()
    {
        base.StartInteraction();

        Target.SetActive(true);
    }

    public override bool ShouldStop()
    {
        return !Target.NavMenu.IsActive;
    }
}
