using UnityEngine;
using System.Collections;

public class OpenMenuInteraction : PlayerInteraction
{
    public GameMenuManager Target;

    public override void SetActive(bool value)
    {
        base.SetActive(value);

        if (value)
            Target.SetActive(value);
    }

    private void Update()
    {
        if (Active && !Target.NavMenu.IsActive)
        {
            SetActive(false);
            _interactionPrompt.Show(CanBeActivated, this);
        }
    }
}
