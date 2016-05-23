using UnityEngine;
using System.Collections;

public class StrengthAbility : Ability
{
    [Range(1, 2)]
    public float BaseDamageMultiplier = 2;
    [Range(0, 1)]
    public float DamageIncrement = 0.25f;
    [Range(1, 2)]
    public float BaseStaminaMultiplier = 2;
    [Range(0, 1)]
    public float StaminaIncrement = 0.25f;

    public override void SetActive(PlayerBehavior player)
    {
        player.Attack.DamageMultiplier = BaseDamageMultiplier + DamageIncrement * CurrentLevel;
        player.Attack.StaminaMultiplier = BaseStaminaMultiplier + StaminaIncrement * CurrentLevel;

        Debug.Log("STRENGHT LEVEL: " + CurrentLevel);
    }

    public override void Deactivate(PlayerBehavior player)
    {
        player.Attack.DamageMultiplier = 1f;
        player.Attack.StaminaMultiplier = 1f;
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

    public static Ability Deserialize(SerializableAbility sAbility)
    {
        var obj = new GameObject().AddComponent<StrengthAbility>();
        obj.name = "Strength";

        obj.BaseDamageMultiplier = float.Parse(sAbility.Data[0]);
        obj.DamageIncrement = float.Parse(sAbility.Data[1]);
        obj.BaseStaminaMultiplier = float.Parse(sAbility.Data[2]);
        obj.StaminaIncrement = float.Parse(sAbility.Data[3]);

        return obj;
    }

    public override AbilityType Type()
    {
        return AbilityType.Strength;
    }
}
