using UnityEngine;

[System.Serializable]
public abstract class Ability
{
    public const int MaxLevel = 4;

    public enum Type
    {
        Vision,
        Speed,
        Shield,
        Strength
    }

    public Axis InputAxis;
    public int CurrentLevel;
    
    [SerializeField]
    private string _identifier;

    public string Identifier
    {
        get
        {
            return _identifier;
        }

        protected set
        {
            _identifier = value;
        }
    }

    protected Ability(Axis inputAxis)
    {
        InputAxis = inputAxis;
        CurrentLevel = 0;
    }

    public bool CanBeUpgraded
    {
        get
        {
            return CurrentLevel < MaxLevel;
        }
    }

    public int GetUpgradeCost()
    {
        return (int)Mathf.Ceil((CurrentLevel + 1) / 2.0f);
    }

    public static Ability ParseAbility(string[] data)
    {
        return null;
    }

    public abstract Type GetAbilityType();

    public abstract void Activate(PlayerBehavior player);

    public abstract void Deactivate(PlayerBehavior player);

    public void Upgrade()
    {
        if (CanBeUpgraded)
        {
            CurrentLevel++;
            Debug.Log("Upgrading Ability to Level: " + CurrentLevel);
        }
        else
        {
            Debug.Log("Ability cannot be leveled any further");
        }
    }
}

[System.Serializable]
public class VisionAbility : Ability
{
    public VisionAbility(Axis axis)
        : base(axis)
    {
    }

    public override void Activate(PlayerBehavior player)
    {
        Debug.LogWarning("Vision is not yet conceived");
    }

    public override void Deactivate(PlayerBehavior player)
    {
        Debug.LogWarning("Vision is not yet conceived");
    }

    public override Type GetAbilityType()
    {
        return Type.Vision;
    }
}

[System.Serializable]
public class SpeedAbility : Ability
{
    public SpeedAbility(Axis axis)
        : base(axis)
    {
    }

    public override void Activate(PlayerBehavior player)
    {
        //player.Movement.MoveSpeedMulti = 1f + 0.05f * CurrentLevel;
        player.PlayerMovement.SpeedMulti = 1f + 0.05f * 10f;
        Debug.Log("SPEED LEVEL: " + CurrentLevel);
    }

    public override void Deactivate(PlayerBehavior player)
    {
        player.PlayerMovement.SpeedMulti = 1f;
    }

    public override Type GetAbilityType()
    {
        return Type.Speed;
    }
}

[System.Serializable]
public class ShieldAbility : Ability
{
    public ShieldAbility(Axis axis)
        : base(axis)
    {
    }

    public override void Activate(PlayerBehavior player)
    {
        if (!player.Defense.ShieldOn)
        {
            player.Defense.ShieldOn = true;
            player.Stamina.ConsumeStamina(90f);
        }
        Debug.Log("SHIELD LEVEL: " + CurrentLevel);
    }

    public override void Deactivate(PlayerBehavior player)
    {
        /*Debug.LogWarning("Deactivation of shield ability was called. Abilities should not be applied whilst shield is up.");
        player.Defense.ShieldOn = false;*/
    }

    public override Type GetAbilityType()
    {
        return Type.Shield;
    }
}

[System.Serializable]
public class StrengthAbility : Ability
{
    public StrengthAbility(Axis axis)
        : base(axis)
    {
    }

    public override void Activate(PlayerBehavior player)
    {
        player.Attack.DamageMultiplier = 2f;
        player.Attack.StaminaMultiplier = 2f;
        Debug.Log("STRENGHT LEVEL: " + CurrentLevel);
    }

    public override void Deactivate(PlayerBehavior player)
    {
        player.Attack.DamageMultiplier = 1f;
        player.Attack.StaminaMultiplier = 1f;
    }

    public override Type GetAbilityType()
    {
        return Type.Strength;
    }
}
