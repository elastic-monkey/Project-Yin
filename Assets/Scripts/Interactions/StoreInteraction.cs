using UnityEngine;
using System.Collections;

public class StoreInteraction : OpenMenuInteraction
{
    public Animator Animator;

    protected override void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);

        if (collider == _gameManager.Player.MainCollider)
        {
            Animator.ResetTrigger(AnimatorHashIDs.StoreCloseTrigger);
            Animator.SetTrigger(AnimatorHashIDs.StoreOpenTrigger);
        }
    }

    public override void StopInteraction()
    {
        base.StopInteraction();

        Animator.ResetTrigger(AnimatorHashIDs.StoreOpenTrigger);
        Animator.SetTrigger(AnimatorHashIDs.StoreCloseTrigger);
    }
}