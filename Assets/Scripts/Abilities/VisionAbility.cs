using UnityEngine;
using System.Collections;

public class VisionAbility : Ability
{
    public override void SetActive(PlayerBehavior player)
    {
        Debug.LogWarning("Vision is not yet conceived");
    }

    public override void Deactivate(PlayerBehavior player)
    {
        Debug.LogWarning("Vision is not yet conceived");
    }

    public override SerializableAbility Serialize()
    {
        return new SerializableAbility(InputAxis, Type(), CurrentLevel, null);
    }

    public static Ability Deserialize(SerializableAbility sAbility)
    {
        var obj = new GameObject().AddComponent<VisionAbility>();
        obj.name = "Vision";

        // No additional data
        return obj;
    }

    public override AbilityType Type()
    {
        return AbilityType.Vision;
    }

    public override string GetFlavorText()
    {
        return "";
    }

    public override string GetEffectText(int level)
    {
        return "";
    }
}
