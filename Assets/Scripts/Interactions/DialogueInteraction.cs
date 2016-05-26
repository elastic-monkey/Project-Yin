using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogueInteraction : PlayerInteraction
{
    public DialogueWindow Target;
    public DialogueType Type;
    public string DialogueID;

    public override bool ShouldStop()
    {
        return Target.HasFinished;
    }

    public override void StartInteraction()
    {
        base.StartInteraction();

        Target.LoadDialogue(DialogueID, Type);
        Target.SetActive(true);
    }

    public override void StopInteraction()
    {
        Target.SetActive(false);
    }
}

