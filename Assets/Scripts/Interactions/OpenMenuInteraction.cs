using UnityEngine;

public class OpenMenuInteraction : PlayerInteraction
{
    public GameMenu Target;

    protected override void Update()
    {
        base.Update();
    
        if (Ongoing && !Target.IsOpen)
        {
            StopInteraction();
        }
    }

    public override void StartInteraction()
    {
        base.StartInteraction();

        Target.Open();
    }
}
