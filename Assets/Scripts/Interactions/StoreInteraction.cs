using UnityEngine;
using System.Collections;

public class StoreInteraction : OpenMenuInteraction
{
    public Animator Animator;

    protected override void Awake()
    {
        base.Awake();
    
        Target = GameManager.Instance.StoreMenu;
    }

    protected override void OnRadiusEnter()
    {
        base.OnRadiusEnter();

        Animator.ResetTrigger(AnimatorHashIDs.StoreCloseTrigger);
        Animator.SetTrigger(AnimatorHashIDs.StoreOpenTrigger);
    }

    protected override void OnRadiusExit()
    {
        base.OnRadiusExit();

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