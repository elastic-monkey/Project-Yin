using UnityEngine;
using System.Collections;

public class StoreInteraction : OpenMenuInteraction
{
    public Animator Animator;
    public StoreSoundManager SoundManager;

    protected override void Awake()
    {
        base.Awake();
    
        Target = GameManager.Instance.StoreMenu;
    }

    protected override void OnRadiusEnter()
    {
        base.OnRadiusEnter();

        SoundManager.PlayOpen();

        Animator.ResetTrigger(AnimatorHashIDs.StoreCloseTrigger);
        Animator.SetTrigger(AnimatorHashIDs.StoreOpenTrigger);
    }

    protected override void OnRadiusExit()
    {
        base.OnRadiusExit();

        Animator.ResetTrigger(AnimatorHashIDs.StoreOpenTrigger);
        Animator.SetTrigger(AnimatorHashIDs.StoreCloseTrigger);

        SoundManager.PlayClose();
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