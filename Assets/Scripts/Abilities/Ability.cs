using UnityEngine;
using UnityEngine.UI;

public abstract class Ability : Upgradable
{
    public enum AbilityType
    {
        Speed,
        Shield,
        Strength,
        None
    }

    public bool Active;

    public Axes InputAxis;

    public Sprite ActiveIcon;
    public Sprite DeactivatedIcon;
    public Image HUDIcon;

    private void Awake()
    {
        CurrentLevel = 0;
    }

    public abstract void SetActive(PlayerBehavior player);

    public abstract void Deactivate(PlayerBehavior player);

    public void UpdateHUDSlot()
    {
		if (HUDIcon == null)
			return;

        if (Active)
        {
            HUDIcon.sprite = ActiveIcon;
        }
        else
        {
            HUDIcon.sprite = DeactivatedIcon;
        }
    }

    public abstract SerializableAbility Serialize();

    public static void Deserialize(Ability recipient, SerializableAbility sAbility)
    {
        if (recipient == null)
            return;
        
        switch (sAbility.Type)
        {
            case Ability.AbilityType.None:
                return;

            case Ability.AbilityType.Shield:
                ShieldAbility.Deserialize(recipient as ShieldAbility, sAbility);
                break;
        
            case Ability.AbilityType.Speed:
                SpeedAbility.Deserialize(recipient as SpeedAbility, sAbility);
                break;

            case Ability.AbilityType.Strength:
                StrengthAbility.Deserialize(recipient as StrengthAbility, sAbility);
                break;
        }
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