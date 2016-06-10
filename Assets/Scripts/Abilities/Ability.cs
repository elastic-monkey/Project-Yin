using UnityEngine;
using UnityEngine.UI;

public abstract class Ability : Upgradable
{

    public enum AbilityType
    {
        Vision,
        Speed,
        Shield,
        Strength,
        None
    }

    public bool Active;

    public Axes InputAxis;

    public Image ActiveIcon;
    public Image DeactivatedIcon;
    public Image HUDIcon;

    private void Awake()
    {
        CurrentLevel = 0;
    }

    public abstract void SetActive(PlayerBehavior player);

    public abstract void Deactivate(PlayerBehavior player);

    public void UpdateHUDSlot()
    {
        if (Active)
        {
            HUDIcon.sprite = ActiveIcon.sprite;
        }
        else
        {
            HUDIcon.sprite = DeactivatedIcon.sprite;
        }
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

        if (obj != null)
        {
            obj.InputAxis = sAbility.InputAxis;
            obj.CurrentLevel = sAbility.CurrentLevel;
        }

        return obj;
    }

    protected override void OnUpgradeTo(int level)
    {
        // Nothing
    }

    protected override bool OnCanBeUpgradedTo(int level)
    {
        return true;
    }

    public abstract AbilityType Type();
}

[System.Serializable]
public class SerializableAbility
{
    public Axes InputAxis;
    public Ability.AbilityType Type;
    public int CurrentLevel;
    public string[] Data;

    public SerializableAbility(Axes axis, Ability.AbilityType type, int level, string[] data)
    {
        InputAxis = axis;
        Type = type;
        CurrentLevel = level;
        Data = (data == null) ? new string[0] : data;
    }
}