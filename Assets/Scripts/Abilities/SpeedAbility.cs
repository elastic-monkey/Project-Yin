﻿using UnityEngine;
using System.Collections;

public class SpeedAbility : Ability
{
    [Range(1, 2)]
    public float BaseMultiplier = 1.5f;
    [Range(0, 1)]
    public float Increment = 0.25f;

    private void Start()
    {
        Type = AbilityType.Speed;
    }

    public override void SetActive(PlayerBehavior player)
    {
        player.PlayerMovement.SpeedMulti = BaseMultiplier + CurrentLevel * Increment;
        Debug.Log("SPEED LEVEL: " + CurrentLevel);
    }

    public override void Deactivate(PlayerBehavior player)
    {
        player.PlayerMovement.SpeedMulti = 1f;
    }

    public override SerializableAbility Serialize()
    {
        var array = new string[2];
        array[0] = BaseMultiplier.ToString();
        array[1] = Increment.ToString();

        return new SerializableAbility(InputAxis, Type, CurrentLevel, array);
    }

    public static Ability Deserialize(SerializableAbility sAbility)
    {
        var obj = new GameObject().AddComponent<SpeedAbility>();
        obj.name = "Speed";

        obj.BaseMultiplier = float.Parse(sAbility.Data[0]);
        obj.Increment = float.Parse(sAbility.Data[1]);

        return obj;
    }
}
