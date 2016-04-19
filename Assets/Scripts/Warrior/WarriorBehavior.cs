using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(AttackBehavior), typeof(DefenseBehavior), typeof(Movement))]
[RequireComponent(typeof(Health), typeof(Stamina))]
public class WarriorBehavior : MonoBehaviour
{
	public Tags EnemyTag;
	public float DeathDuration;

	private Collider[] _colliders;
	private DefenseBehavior _defenseBehavior;
	private AttackBehavior _attackBehavior;
	private Health _health;
	private Stamina _stamina;
	private Animator _animator;
	private Movement _movement;
	protected List<Collider> _enemiesInRange;
	protected GameManager _gameManager;
	protected bool _dying = false;

	public DefenseBehavior Defense
	{
		get
		{
			if (_defenseBehavior == null)
				_defenseBehavior = GetComponent<DefenseBehavior>();

			return _defenseBehavior;
		}
	}

	public AttackBehavior Attack
	{
		get
		{
			if (_attackBehavior == null)
				_attackBehavior = GetComponent<AttackBehavior>();

			return _attackBehavior;
		}
	}

	public Health Health
	{
		get
		{
			if (_health == null)
				_health = GetComponent<Health>();

			return _health;
		}
	}

	public Stamina Stamina
	{
		get
		{
			if (_stamina == null)
				_stamina = GetComponent<Stamina>();

			return _stamina;
		}
	}

	public Animator Animator
	{
		get
		{
			if (_animator == null)
				_animator = GetComponent<Animator>();

			return _animator;
		}
	}

	public Movement Movement
	{
		get
		{
			if (_movement == null)
				_movement = GetComponent<Movement>();

			return _movement;
		}
	}

	public Collider[] Colliders
	{
		get
		{
			if (_colliders == null)
				_colliders = GetComponentsInChildren<Collider>();

			return _colliders;
		}
	}

	protected virtual void Awake()
	{
		Live();
	}

	protected virtual void Start()
	{
		_gameManager = GameManager.Instance;
	}

	protected virtual void Update()
	{
		SetAnimatorParameters();

		Attack.Targets = _enemiesInRange;
	}

	private void SetAnimatorParameters()
	{
		Animator.SetBool(AnimatorHashIDs.CanMoveBool, Movement.CanMove);
		if (Movement.Moving && !PlayerInput.Blocked)
		{
			Animator.SetFloat(AnimatorHashIDs.SpeedFloat, Movement.SpeedThreshold, Movement.SpeedDampTime, Time.deltaTime);
		}
		else {
			Animator.SetFloat(AnimatorHashIDs.SpeedFloat, 0f);
		}
		Animator.SetBool(AnimatorHashIDs.AttackingBool, Attack.Attacking);
		Animator.SetBool(AnimatorHashIDs.DefendingBool, Defense.Defending);
		Animator.SetFloat(AnimatorHashIDs.SpeedMultiFloat, Movement.SpeedMulti);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag(EnemyTag.ToString()) && !_enemiesInRange.Contains(other))
		{
			_enemiesInRange.Add(other);
		}
	}

	void OnTriggerExit(Collider other)
	{
		_enemiesInRange.Remove(other);
		if (_enemiesInRange.Count == 0)
		{
			if (Defense.Defending)
			{
				// This will only be effective if active defense is not a oneTime defense
				Defense.CancelDefense();
			}
		}
	}

	public void Live()
	{
		Health.RegenerateFull();

		_enemiesInRange = new List<Collider>();
		Attack.Targets = _enemiesInRange;

		ToggleColliders(true);
	}

	protected void Die()
	{
		Health.Alive = false;
		ToggleColliders(false);

		StartCoroutine(DieCoroutine());
	}

	private IEnumerator DieCoroutine()
	{
		Debug.Log("Warrior is dying...");
		Animator.SetBool(AnimatorHashIDs.DeadBool, true);

		yield return new WaitForSeconds(DeathDuration);

		Debug.Log("Warrior is dead.");
		gameObject.SetActive(false);
	}

	private void ToggleColliders(bool value)
	{
		foreach (var collider in Colliders)
		{
			collider.enabled = value;
		}
	}
}

public static class WarriorBehaviorHelper
{
	public static bool InsideDangerArea(this WarriorBehavior warrior, DangerArea area)
	{
		return area.IsInside(warrior.transform);
	}
}