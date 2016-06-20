using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Utilities;

[RequireComponent(typeof(WarriorBehavior), typeof(Animator), typeof(Stamina))]
public class AttackBehavior : MonoBehaviour
{
	public float StunDuration = 0.5f;
	public float Range = 2f;
	public Attack[] Attacks;
	public bool CanAttack = true;
	public bool Stunned = false;
	public bool Attacking = false;
	public List<WarriorBehavior> Targets;
	public float DamageMultiplier = 1.0f;
	public float StaminaMultiplier = 1.0f;

	private Stamina _stamina;
	private WarriorBehavior _warrior;
	private GameManager _gameManager;
	private float _sqrRange;
	private Attack _currentAttack;
	private Coroutine _lastAttack;
	private float _timeSinceAttackBegin;

	public Attack CurrentAttack
	{
		get
		{
			return _currentAttack;
		}
	}

	private bool CanPerformNewAttack
	{
		get
		{
			return CanAttack && !Attacking && !Stunned;
		}
	}

	private void Awake()
	{
		_stamina = GetComponent<Stamina>();
		_warrior = GetComponent<WarriorBehavior>();
		_sqrRange = Range * Range;
	}

	private void Start()
	{
		_gameManager = GameManager.Instance;
	}

	private void Update()
	{
		Targets.Clear();
		var enemies = _gameManager.GetWarriors(_warrior.EnemyTag);
		foreach (var enemy in enemies)
		{
			if (_sqrRange >= Vector3Helper.SqrDistanceXZ(_warrior.transform.position, enemy.transform.position))
			{
				Targets.Add(enemy);
			}
		}
	}

	private void OnDrawGizmos()
	{
		GizmosHelper.DrawAngleOfSight(transform.position + Vector3.up, transform.forward * Range, 360, 20, Color.red);
	}

	public void ChooseAndApplyAttack()
	{
		var bestDamage = float.MinValue;
		var best = -1;

		for (int i = 0; i < Attacks.Length; i++)
		{
			var attack = Attacks[i];
			if (_stamina.CanConsume(attack.StaminaCost))
			{
				if (attack.Damage > bestDamage)
				{
					bestDamage = attack.Damage;
					best = i;
				}
			}
		}

		if (best < 0)
			return;

		ApplyAttack(best);
	}

	public void ApplyAttack()
	{
		var chosenAttackIndex = -1;

		if (PlayerInput.IsButtonPressed(Axes.Attack))
			chosenAttackIndex = 0;

		ApplyAttack(chosenAttackIndex);
	}

	public void ApplyAttack(int attackIndex)
	{
		if (!CanPerformNewAttack)
			return;

		if (attackIndex < 0 || attackIndex >= Attacks.Length)
			return;

		var chosenAttack = Attacks[attackIndex];

		if (!_stamina.CanConsume(chosenAttack.StaminaCost))
			return;

		_lastAttack = StartCoroutine(AttackCoroutine(chosenAttack));
	}

	public void StopAttack()
	{
		if (_timeSinceAttackBegin > CurrentAttack.Duration * 0.33f)
			return;

		StopCoroutine(_lastAttack);
		Attacking = false;

		if (!Stunned)
			StartCoroutine(Stun(StunDuration));
	}

	private IEnumerator AttackCoroutine(Attack attack)
	{
		Attacking = true;
		_currentAttack = attack;
		_stamina.ConsumeStamina(attack.StaminaCost * StaminaMultiplier);
		_stamina.RegenerateIsOn = false;
		_timeSinceAttackBegin = 0f;

		var swingTime = 0.6f * attack.HitTime;

		while (_timeSinceAttackBegin < swingTime)
		{
			_timeSinceAttackBegin += Time.deltaTime;
			yield return null;
		}

		_warrior.SoundManager.PlayClip(WarriorSoundManager.ClipActions.Swing);

		while (_timeSinceAttackBegin < attack.HitTime)
		{
			_timeSinceAttackBegin += Time.deltaTime;
			yield return null;
		}

		foreach (var target in Targets)
		{
			var warrior = target.GetComponentInParent<WarriorBehavior>();
			if (warrior == null)
			{
				Debug.LogWarning("Attack: target does not have WarriorBehaviour.");
				continue;
			}

			var defense = target.GetComponentInParent<WarriorBehavior>().Defense;
			if (defense == null)
			{
				Debug.LogWarning("Attack: target defense is NULL.");
				continue;
			}

			warrior.OnAttacked(_warrior, defense.TakeDamage(attack.Damage * DamageMultiplier));
		}

		if (Targets != null && Targets.Count > 0)
			_warrior.SoundManager.PlayClip(WarriorSoundManager.ClipActions.WeaponStrike);

		yield return new WaitForSeconds(Mathf.Max(0, attack.Duration - attack.HitTime));

		Attacking = false;
		_currentAttack = null;
		_stamina.RegenerateIsOn = true;
		_timeSinceAttackBegin = 0f;
	}

	private IEnumerator Stun(float duration)
	{
		Stunned = true;

		yield return new WaitForSeconds(duration);

		Stunned = false;
	}
}

[System.Serializable]
public class Attack
{
	public int Damage = 10;
	[Range(1, 10)]
	public float Radius = 1;
	[Tooltip("This is the full opening angle between the object's forward direction and the enemies.")]
	[Range(10, 360)]
	public float Angle = 10f;
	public int StaminaCost = 10;
	public float Duration = 0f, HitTime = 0f;
	public float AnimClipDuration = 1.0f;
	public float AnimDurationMulti
	{
		get
		{
			return (AnimClipDuration / Duration);
		}
	}
}
