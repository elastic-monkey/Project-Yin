using UnityEngine;

public class ShieldAbility : Ability
{
    private PlayerBehavior _player;
    private bool _lastShieldOn;
    [SerializeField]
    private float _baseStaminaConsumed = 90f;
    [SerializeField]
    private float _baseComsumptionDecrement = 0.1f;

    public void Update()
    {
        if (_player == null)
            return;

        if (_player.Defense.ShieldOn != _lastShieldOn)
        {
            if (_player.Defense.ShieldOn)
            {
                Active = true;
            }
            else
            {
                Active = false;
            }
            UpdateHUDSlot();
        }

        _lastShieldOn = _player.Defense.ShieldOn;
    }

    public override void SetActive(PlayerBehavior player)
    {
        if (!player.Defense.ShieldOn)
        {
            _player = player;
            Active = true;
            player.Defense.ShieldOn = true;
            player.Stamina.ConsumeStamina(StaminaConsumed(CurrentLevel));
            player.Stamina.RegenerateIsOn = true;
            UpdateHUDSlot();
        }
    }

    public override void Deactivate(PlayerBehavior player)
    {
        //Active = false;
        //UpdateHUDSlot();
    }

    private float StaminaConsumed(int level)
    {
        return _baseStaminaConsumed - ((_baseStaminaConsumed * _baseComsumptionDecrement) * level);
    }

    public override SerializableAbility Serialize()
    {
        var array = new string[1];
        array[0] = Active.ToString();

        return new SerializableAbility(InputAxis, Type(), CurrentLevel, array);
    }

    public static void Deserialize(ShieldAbility recipient, SerializableAbility sAbility)
    {
        if (recipient == null)
            return;
        
        recipient.name = "Shield";
        recipient.Active = bool.Parse(sAbility.Data[0]);
    }

    public override AbilityType Type()
    {
        return AbilityType.Shield;
    }

    public override string GetFlavorText()
    {
        return "Creates a shield capable of deflecting one single blow.";
    }

    public override string GetEffectText(int level)
    {
        return "Consumes less " + (_baseComsumptionDecrement * level * 100.0f).ToString() + "% Energy Points";
    }
}
