using UnityEngine;

[RequireComponent(typeof(Animator), typeof(PlayerMovement), typeof(AbilitiesManager))]
[RequireComponent(typeof(Experience))]
public class PlayerBehavior : WarriorBehavior
{
    public Transform RaycastSpot;
	public EnemyArea FightArea;

    private PlayerMovement _playerMovement;
    private AbilitiesManager _abilities;
    private Experience _experience;
    private Currency _currency;
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
