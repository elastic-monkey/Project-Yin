using UnityEngine;
using System.Collections;

public class StrengthAbility : Ability
{
    [Range(1, 2)]
    public float BaseDamageMultiplier = 1.0f;
    [Range(0, 1)]
    public float DamageIncrement = 0.25f;
    [Range(1, 2)]
    public float BaseStaminaMultiplier = 1.0f;
    [Range(0, 1)]
    public float StaminaIncrement = 0.25f;

    public override void SetActive(PlayerBehavior player)
    {
        Active = true;
        player.Attack.DamageMultiplier = GetDamageMultiplier(CurrentLevel);
        player.Attack.StaminaMultiplier = GetStaminaMultiplier(CurrentLevel);

        UpdateHUDSlot();
    }

    public override void Deactivate(PlayerBehavior player)
    {
        Active = false;
        player.Attack.DamageMultiplier = 1f;
        player.Attack.StaminaMultiplier = 1f;
        UpdateHUDSlot();
    }

    private float GetDamageMultiplier(int level)
    {
        return BaseDamageMultiplier + (DamageIncrement * level);
    }

    private float GetStaminaMultiplier(int level)
    {
        return BaseStaminaMultiplier + (StaminaIncrement * level);
    }

    public override SerializableAbility Serialize()
    {
        var array = new string[4];
        array[0] = BaseDamageMultiplier.ToString();
        array[1] = DamageIncrement.ToString();
        array[2] = BaseStaminaMultiplier.ToString();
        array[3] = StaminaIncrement.ToString();

        return new SerializableAbility(InputAxis, Type(), CurrentLevel, array);
    }

    public static void Deserialize(StrengthAbility recipient, SerializableAbility sAbility)
    {
        if (recipient == null)
            return;

        recipient.CurrentLevel = sAbility.CurrentLevel;
        recipient.name = "Strength";
        recipient.BaseDamageMultiplier = float.Parse(sAbility.Data[0]);
        recipient.DamageIncrement = float.Parse(sAbility.Data[1]);
        recipient.BaseStaminaMultiplier = float.Parse(sAbility.Data[2]);
        recipient.StaminaIncrement = float.Parse(sAbility.Data[3]);
    }

    public override AbilityType Type()
    {
        return AbilityType.Strength;
    }

    public override string GetFlavorText()
    {
        return "Control over artificial muscles turns Yin's blade deadlier";
    }

    public override string GetEffectText(int level)
    {
        return "Increases attack power by " + GetDamageMultiplier(level).ToString() + "\n"
             + "Increases energy consumption by " + GetStaminaMultiplier(level).ToString();
    }
}
