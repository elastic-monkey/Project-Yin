using UnityEngine;

public class SpeedAbility : Ability
{
    [Range(1, 2)]
    public float BaseMultiplier = 1.0f;
    [Range(0, 1)]
    public float Increment = 0.05f;

    public override void SetActive(PlayerBehavior player)
    {
        Active = true;
        player.PlayerMovement.SpeedMulti = GetSpeed(CurrentLevel);
        UpdateHUDSlot();
    }

    public override void Deactivate(PlayerBehavior player)
    {
        Active = false;
        player.PlayerMovement.SpeedMulti = 1f;
        UpdateHUDSlot();
    }

    private float GetSpeed(int level)
    {
        return BaseMultiplier + (level * Increment);
    }

    public override SerializableAbility Serialize()
    {
        var array = new string[2];
        array[0] = BaseMultiplier.ToString();
        array[1] = Increment.ToString();

        return new SerializableAbility(InputAxis, Type(), CurrentLevel, array);
    }

    public static void Deserialize(SpeedAbility recipient, SerializableAbility sAbility)
    {
        if (recipient == null)
            return;
        
        recipient.name = "Speed";
        recipient.BaseMultiplier = float.Parse(sAbility.Data[0]);
        recipient.Increment = float.Parse(sAbility.Data[1]);
    }

    public override AbilityType Type()
    {
        return AbilityType.Speed;
    }

    public override string GetFlavorText()
    {
        return "Control over artificial muscles allows Yin to move at greater speed";
    }

    public override string GetEffectText(int level)
    {
        return "Increases movement speed by " + GetSpeed(level).ToString();
    }
}
