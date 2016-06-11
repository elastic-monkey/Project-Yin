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

    protected override void OnTriggerExit(Collider collider)
    {
        base.OnTriggerExit(collider);

        if (collider == _gameManager.Player.MainCollider)
        {
            Animator.ResetTrigger(AnimatorHashIDs.StoreOpenTrigger);
            Animator.SetTrigger(AnimatorHashIDs.StoreCloseTrigger);
        }
    }

    public override void StopInteraction()
    {
        base.StopInteraction();

		if (CanBeTriggered)
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