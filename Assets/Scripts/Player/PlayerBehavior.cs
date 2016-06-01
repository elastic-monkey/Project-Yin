using UnityEngine;

[RequireComponent(typeof(Animator), typeof(PlayerMovement), typeof(AbilitiesManager))]
[RequireComponent(typeof(Experience))]
public class PlayerBehavior : WarriorBehavior
{
    public Transform RaycastSpot;

    private PlayerMovement _playerMovement;
    private AbilitiesManager _abilities;
    private Experience _experience;
    private Currency _currency;

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

    public AbilitiesManager Abilities
    {
        get
        {
            if (_abilities == null)
                _abilities = GetComponent<AbilitiesManager>();

            return _abilities;
        }
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();

        if (!Health.Alive)
            return;

        if (Health.CurrentHealth <= 0)
        {
            _gameManager.OnPlayerDeath(this);

            PlayerDeath();

            return;
        }

        if (!PlayerInput.GameplayBlocked)
        {
            Abilities.ApplyAbilities();

            Attack.ApplyAttack();

            if (!Attack.Attacking)
                Defense.ApplyDefense();

            PlayerMovement.CanMove = !Attack.Attacking && !Defense.Defending;
        }
    }

    public override void OnAttacked(WarriorBehavior attacker)
    {
        base.OnAttacked(attacker);

        // TODO: sound or reaction
    }

    protected override void OnDeath()
    {
        throw new System.NotImplementedException();
    }

    private void FixedUpdate()
    {
        if (PlayerInput.GameplayBlocked)
            return;
        
        PlayerMovement.ApplyMovement();
    }
}
