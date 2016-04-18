using UnityEngine;

[RequireComponent(typeof(Animator), typeof(PlayerMovement), typeof(AbilitiesManager))]
[RequireComponent(typeof(Experience))]
public class PlayerBehavior : WarriorBehavior
{
	private PlayerMovement _playerMovement;
	private AbilitiesManager _abilities;
	private Experience _experience;

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

	protected override void Awake()
	{
		base.Awake();

		_abilities = GetComponent<AbilitiesManager>();
	}

	protected override void Update()
	{
		base.Update();

		if (!PlayerInput.Blocked)
		{
			_abilities.ApplyAbilities();

			Attack.ApplyAttack();

			if (!Attack.Attacking)
				Defense.ApplyDefense();

			PlayerMovement.CanMove = !Attack.Attacking && !Defense.Defending;
		}
	}

	void FixedUpdate()
	{
		if (!PlayerInput.Blocked)
		{
			PlayerMovement.ApplyMovement();
		}
	}
}
