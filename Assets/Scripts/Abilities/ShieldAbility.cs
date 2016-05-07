using UnityEngine;
using System.Collections;

public class ShieldAbility : Ability
{
    public bool IsActive;

    private void Start()
    {
        Type = AbilityType.Shield;
    }

    public override void SetActive(PlayerBehavior player)
    {
        if (!player.Defense.ShieldOn)
        {
            IsActive = true;
            player.Defense.ShieldOn = true;
            player.Stamina.ConsumeStamina(90f);
        }

        Debug.Log("SHIELD LEVEL: " + CurrentLevel);
    }

    public override void Deactivate(PlayerBehavior player)
    {
        IsActive = false;
    }

    public override SerializableAbility Serialize()
    {
        var array = new string[1];
        array[0] = IsActive.ToString();

        return new SerializableAbility(InputAxis, Type, CurrentLevel, array);
    }

    public static Ability Deserialize(SerializableAbility sAbility)
    {
        var obj = new GameObject().AddComponent<ShieldAbility>();
        obj.name = "Shield";

        obj.IsActive = bool.Parse(sAbility.Data[0]);

        return obj;
    }
}
