using UnityEngine;
using System.Collections;

[System.Serializable]
public class StaminaSyringe : Item
{

    public float StaminaRegenMulti;
    public float Duration;

    public StaminaSyringe()
    {
        Type = ItemType.StaminaRegenRate;
    }

    public override void UseItem(PlayerBehavior player)
    {
        StartCoroutine(UseStaminaSyringe(player));
    }

    private IEnumerator UseStaminaSyringe(PlayerBehavior player)
    {
        player.Stamina.RegenerationRate *= StaminaRegenMulti;

        yield return new WaitForSeconds(Duration);

        player.Stamina.RegenerationRate /= StaminaRegenMulti;
    }
}
