using UnityEngine;

public class ShieldAbility : Ability
{
    public override void SetActive(PlayerBehavior player)
    {
        if (!player.Defense.ShieldOn)
        {
            Active = true;
            player.Defense.ShieldOn = true;
            player.Stamina.ConsumeStamina(90f);
            player.Stamina.Regenerating = true;
            UpdateHUDSlot();
        }

        Debug.Log("SHIELD LEVEL: " + CurrentLevel);
    }

    public override void Deactivate(PlayerBehavior player)
    {
        Active = false;
        UpdateHUDSlot();
    }

    public override SerializableAbility Serialize()
    {
        var array = new string[1];
        array[0] = Active.ToString();

        return new SerializableAbility(InputAxis, Type(), CurrentLevel, array);
    }

    public static Ability Deserialize(SerializableAbility sAbility)
    {
        var obj = new GameObject().AddComponent<ShieldAbility>();
        obj.name = "Shield";

        obj.Active = bool.Parse(sAbility.Data[0]);

        return obj;
    }

    public override AbilityType Type()
    {
        return AbilityType.Shield;
    }
}
