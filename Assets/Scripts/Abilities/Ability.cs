using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public const int MaxLevel = 4;

    public enum AbilityType
    {
        Vision,
        Speed,
        Shield,
        Strength
    }

    public Axis InputAxis;
    public int CurrentLevel;

    public AbilityType Type { get; protected set; }

    public int UpgradeCost(int level)
    {
        var sum = 0;
        for (var i = CurrentLevel + 1; i < level; i++)
        {
            sum += (int)Mathf.Ceil(i * 0.5f);
        }

        return sum;
    }

    private void Awake()
    {
        CurrentLevel = 0;
    }

    public abstract void SetActive(PlayerBehavior player);

    public abstract void Deactivate(PlayerBehavior player);

    public void UpgradeTo(int level)
    {
        CurrentLevel = level;
        Debug.Log("Upgrading Ability to Level: " + level);
    }

    public bool CanBeUpgradedTo(int level)
    {
        return (level >= 0 && level < MaxLevel);
    }

    public abstract SerializableAbility Serialize();

    public static Ability DeserializeAbility(SerializableAbility sAbility)
    {
        Ability obj = null;

        switch (sAbility.Type)
        {
            case AbilityType.Shield:
                obj = ShieldAbility.Deserialize(sAbility);
                break;

            case AbilityType.Speed:
                obj = SpeedAbility.Deserialize(sAbility);
                break;

            case AbilityType.Strength:
                obj = StrengthAbility.Deserialize(sAbility);
                break;

            case AbilityType.Vision:
                obj = VisionAbility.Deserialize(sAbility);
                break;
        }

        if (obj.Exists())
        {
            obj.InputAxis = sAbility.InputAxis;
            obj.Type = sAbility.Type;
            obj.CurrentLevel = sAbility.CurrentLevel;
        }

        return obj;
    }
}

[System.Serializable]
public class SerializableAbility
{
    public Axis InputAxis;
    public Ability.AbilityType Type;
    public int CurrentLevel;
    public string[] Data;

    public SerializableAbility(Axis axis, Ability.AbilityType type, int level, string[] data)
    {
        InputAxis = axis;
        Type = type;
        CurrentLevel = level;
        Data = data.IsNull() ? new string[0] : data;
    }
}