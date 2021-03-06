﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class StaminaSyringe : Item
{
    public float StaminaRegenMulti;
    public float Duration;

    public void Start()
    {
        Type = ItemType.StaminaRegenRate;
        Effect = "Restores " + (StaminaRegenMulti*Duration).ToString() + " Energy Points over " + Duration.ToString() + " seconds";
        FlavorText = "A small syringe containing a blue liquid";
    }

    public override void UseItem()
    {
        StartCoroutine(UseStaminaSyringe());
    }

    private IEnumerator UseStaminaSyringe()
    {
        _player.Stamina.RegenerationRate *= StaminaRegenMulti;

        yield return new WaitForSeconds(Duration);

        _player.Stamina.RegenerationRate /= StaminaRegenMulti;
    }

    public override bool CanUse()
    {
        return true;
    }

    private void OnValidate()
    {
        Type = ItemType.StaminaRegenRate;
        Effect = "Restores " + (StaminaRegenMulti * Duration).ToString() + " Energy Points over " + Duration.ToString() + " seconds";
        FlavorText = "A small syringe containing a blue liquid";
    }
}