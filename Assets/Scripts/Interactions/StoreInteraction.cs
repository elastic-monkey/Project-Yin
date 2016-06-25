using UnityEngine;
using System.Collections;

public class StoreInteraction : OpenMenuInteraction
{
    public Animator Animator;

    protected override void OnRadiusEnter()
    {
        Animator.ResetTrigger(AnimatorHashIDs.StoreCloseTrigger);
        Animator.SetTrigger(AnimatorHashIDs.StoreOpenTrigger);
    }

    protected override void OnRadiusExit()
    {
        Animator.ResetTrigger(AnimatorHashIDs.StoreOpenTrigger);
        Animator.SetTrigger(AnimatorHashIDs.StoreCloseTrigger);
    }

    public override void StopInteraction()
    {
        base.StopInteraction();

        if (IsInsideRadius)
        {
            Animator.SetTrigger(AnimatorHashIDs.StoreOpenTrigger);
            Animator.ResetTrigger(AnimatorHashIDs.StoreCloseTrigger);
        }
        else
        {
            Animator.ResetTrigger(AnimatorHashIDs.StoreOpenTrigger);
            Animator.SetTrigger(AnimatorHashIDs.StoreCloseTrigger);
        }
    }
}