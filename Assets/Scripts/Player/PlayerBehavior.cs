﻿using UnityEngine;

[RequireComponent(typeof(Animator), typeof(PlayerMovement), typeof(AbilitiesManager))]
[RequireComponent(typeof(Experience))]
public class PlayerBehavior : WarriorBehavior
{
    private PlayerMovement _playerMovement;
    private Animator _animator;
    private AbilitiesManager _abilities;
	private Experience _experience;

	public Experience Experience
	{
		get
		{
			if (_experience == null)
				_experience = GetComponent<Experience> ();
			
			return _experience;
		}
	}

    public PlayerMovement Movement
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

        _animator = GetComponent<Animator>();
        _abilities = GetComponent<AbilitiesManager>();
		_experience = GetComponent<Experience> ();
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

            Movement.CanMove = !Attack.Attacking && !Defense.Defending;
        }

        SetAnimatorParameters();
    }

    private void SetAnimatorParameters()
    {
        _animator.SetBool(AnimatorHashIDs.CanMoveBool, Movement.CanMove);
		if (Movement.Moving && !PlayerInput.Blocked)
        {
            _animator.SetFloat(AnimatorHashIDs.SpeedFloat, Movement.SpeedThreshold, Movement.SpeedDampTime, Time.deltaTime);
        }
        else {
            _animator.SetFloat(AnimatorHashIDs.SpeedFloat, 0f);
        }
        _animator.SetBool(AnimatorHashIDs.AttackingBool, Attack.Attacking);
        _animator.SetBool(AnimatorHashIDs.DefendingBool, Defense.Defending);
        _animator.SetFloat(AnimatorHashIDs.SpeedMultiFloat, Movement.MoveSpeedMulti);
    }

    void FixedUpdate()
    {
		if (!PlayerInput.Blocked) {
			Movement.ApplyMovement ();
		}
    }
}
