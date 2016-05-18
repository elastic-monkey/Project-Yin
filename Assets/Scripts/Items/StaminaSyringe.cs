using UnityEngine;
using System.Collections;

[System.Serializable]
public class StaminaSyringe : Item
{

    public float StaminaRegenMulti;
    public float Duration;
    public PlayerBehavior Player;

    public StaminaSyringe()
    {
        Type = ItemType.StaminaRegenRate;
    }

    public override void UseItem()
    {
        StartCoroutine(UseStaminaSyringe());
    }

    private IEnumerator UseStaminaSyringe()
    {
        Player.Stamina.RegenerationRate *= StaminaRegenMulti;

        yield return new WaitForSeconds(Duration);

        Player.Stamina.RegenerationRate /= StaminaRegenMulti;
    }

    public override bool CanUse()
    {
        return true;
    }
}