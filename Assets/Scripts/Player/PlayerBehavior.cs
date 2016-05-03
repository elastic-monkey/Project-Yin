using UnityEngine;

[RequireComponent(typeof(Animator), typeof(PlayerMovement), typeof(AbilitiesManager))]
[RequireComponent(typeof(Experience))]
public class PlayerBehavior : WarriorBehavior
{
	private PlayerMovement _playerMovement;
	private AbilitiesManager _abilities;
	private Experience _experience;
    private PlayerInventory _playerInventory;

	public Experience Experience
	{
		get
		{
			if (_experience == null)
				_experience = GetComponent<Experience>();

			return _experience;
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
			if(_abilities == null)
				_abilities = GetComponent<AbilitiesManager>();

			return _abilities;
		}
	}

    public PlayerInventory PlayerInventory
    {
        get
        {
            if (_playerInventory == null)
            {
                _playerInventory = GetComponent<PlayerInventory>();
            }

            return _playerInventory;
        }
    }

	protected override void Awake()
	{
		base.Awake();
	}

	protected override void Update()
	{
		base.Update();

		if (!PlayerInput.GameplayBlocked)
		{
			Abilities.ApplyAbilities();

			Attack.ApplyAttack();

			if (!Attack.Attacking)
				Defense.ApplyDefense();

			PlayerMovement.CanMove = !Attack.Attacking && !Defense.Defending;
		}
	}

	void FixedUpdate()
	{
		if (!PlayerInput.GameplayBlocked)
		{
			PlayerMovement.ApplyMovement();
		}
	}
}
