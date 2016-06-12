using UnityEngine;

public class OpenMenuInteraction : PlayerInteraction
{
    public GameMenu Target;

    public override void StartInteraction()
    {
        base.StartInteraction();

        Target.Open();
    }

    public override bool ShouldStop()
    {
        return !(Target.NavMenu.IsActive);
    }
}
