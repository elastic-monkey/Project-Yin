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
		if (!(Target.NavMenu.IsActive))
		{
			Debug.Log("Should Stop: " + name);
		}
        return !(Target.NavMenu.IsActive);
    }
}
