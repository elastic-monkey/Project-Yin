using UnityEngine;

[RequireComponent(typeof(Animator), typeof(PlayerMovement), typeof(AbilitiesManager))]
[RequireComponent(typeof(Experience), typeof(PlayerInventory))]
public class PlayerBehavior : WarriorBehavior
{
    public Transform RaycastSpot;
    public EnemyArea FightArea;

    private PlayerMovement _playerMovement;
    private AbilitiesManager _abilities;
    private Experience _experience;
    private Currency _currency;
    private PlayerInventory _inventory;
    private PlayerHit _splashHit;

    public Experience Experience
    {
        get
        {
            if (_experience == null)
                _experience = GetComponent<Experience>();

            return _experience;
        }
    }

    public Currency Currency
    {
        get
        {
            if (_currency == null)
                _currency = GetComponent<Currency>();

            return _currency;
        }
    }

    public PlayerMovement PlayerMovement
    {
        get
        {
            if (_playerMovement == null)
                _playerMovement = GetComponent<PlayerMovement>();

            return _playerMovement;
        }
    }

    public PlayerInventory Inventory
    {
        get
        {
            if (_inventory == null)
                _inventory = GetComponent<PlayerInventory>();

            return _inventory;
        }
    }

    public AbilitiesManager Abilities
    {
        get
        {
            if (_abilities == null)
                _abilities = GetComponent<AbilitiesManager>();

            return _abilities;
        }
    }

    protected override void Start()
    {
        base.Start();

        _splashHit = GameManager.SplashHit;
    }

    protected override void Update()
    {
        base.Update();

        if (!Health.Alive)
            return;

        if (Health.CurrentHealth <= 0)
        {
            GameManager.OnPlayerDeath(this);

            PlayerDeath();

            return;
        }

        if (!PlayerInput.OnlyMenus)
        {
            Abilities.ApplyAbilities();

            if (!Defense.Defending)
                Attack.ApplyAttack();

            if (!Attack.Attacking)
                Defense.ApplyDefense();

            PlayerMovement.CanMove = !Attack.Attacking && !Defense.Defending;
        }
    }

    public override void OnAttacked(WarriorBehavior attacker, float normalizedDamage)
    {
        base.OnAttacked(attacker, normalizedDamage);

        if (normalizedDamage > 0)
        {
            _splashHit.Show(normalizedDamage);
        }
        // TODO: sound or reaction
    }

    public Upgradable FindUpgradable(Upgradable.UpgradableTypes type)
    {
        switch (type)
        {
            case Upgradable.UpgradableTypes.Health:
                return Health;

            case Upgradable.UpgradableTypes.Stamina:
                return Stamina;
                    
            case Upgradable.UpgradableTypes.Shield:
                return Abilities.Find(Ability.AbilityType.Shield);

            case Upgradable.UpgradableTypes.Speed:
                return Abilities.Find(Ability.AbilityType.Speed);

            case Upgradable.UpgradableTypes.Strength:
                return Abilities.Find(Ability.AbilityType.Strength);
        }

        return null;
    }

    protected override void OnDeath()
    {
        throw new System.NotImplementedException();
    }

    private void FixedUpdate()
    {
        if (PlayerInput.OnlyMenus)
            return;
        
        PlayerMovement.ApplyMovement();
    }
}
