using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(AttackBehavior), typeof(DefenseBehavior), typeof(Movement))]
[RequireComponent(typeof(Health), typeof(Stamina))]
public abstract class WarriorBehavior : MonoBehaviour
{
    public Tags EnemyTag;
    public Collider MainCollider;
    public float DeathDuration;

    private Collider[] _colliders;
    private DefenseBehavior _defenseBehavior;
    private AttackBehavior _attackBehavior;
    private DodgeBehaviour _dodgeBehaviour;
    private Health _health;
    private Stamina _stamina;
    private Animator _animator;
    private Movement _movement;
    private GameManager _gameManager;
	protected WarriorSoundManager _soundManager;
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

    public DodgeBehaviour Dodge
    {
        get
        {
            if (_dodgeBehaviour == null)
                _dodgeBehaviour = GetComponent<DodgeBehaviour>();

            return _dodgeBehaviour;
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

	public WarriorSoundManager SoundManager
	{
		get
		{
			if (_soundManager == null)
				_soundManager = GetComponent<WarriorSoundManager>();

			return _soundManager;
		}
	}

	public GameManager GameManager
	{
		get
		{
			if (_gameManager == null)
				_gameManager = GameManager.Instance;

			return _gameManager;
		}
	}

    protected virtual void Awake()
    {
        Live();
	}

    protected virtual void Start()
    {
		SoundManager.SoundManager = GameManager.EnemiesSoundManager;
	}

    protected virtual void Update()
    {
        Animator.SetBool(AnimatorHashIDs.CanMoveBool, Movement.CanMove);
        Animator.SetBool(AnimatorHashIDs.MovingBool, Movement.Moving);
        Animator.SetFloat(AnimatorHashIDs.SpeedMultiplierFloat, Movement.AnimSpeedMulti);
        Animator.SetBool(AnimatorHashIDs.AttackingBool, Attack.Attacking);
        Animator.SetBool(AnimatorHashIDs.DefendingBool, Defense.Defending);
        Animator.SetBool(AnimatorHashIDs.DodgingBool, Dodge.Dodging);
        Animator.SetBool(AnimatorHashIDs.DeadBool, !Health.Alive);

        if (Movement.Moving && !PlayerInput.OnlyMenus)
            Animator.SetFloat(AnimatorHashIDs.SpeedFloat, Movement.SpeedThreshold, Movement.SpeedDampTime, Time.deltaTime);
        else
            Animator.SetFloat(AnimatorHashIDs.SpeedFloat, 0f);

        if (Attack.Attacking)
            Animator.SetFloat(AnimatorHashIDs.AttackMultiplierFloat, Attack.CurrentAttack.AnimDurationMulti);
    }

    public virtual void OnAttacked(WarriorBehavior attacker, float normalizedDamage)
    {
		// Do stuff
    }

    public void Live()
    {
        Health.RegenerateFull();

        ToggleColliders(true);
    }

    protected void Die()
    {
        Health.Alive = false;
        ToggleColliders(false);
        OnDeath();

        StartCoroutine(DieCoroutine(false));
    }

    protected void PlayerDeath()
    {
        Health.Alive = false;
        StartCoroutine(DieCoroutine(true));
    }

    private IEnumerator DieCoroutine(bool playerDeath)
    {
        Debug.Log("Warrior is dying...");
        Animator.SetBool(AnimatorHashIDs.DeadBool, true);

        if (!playerDeath)
        {
            _gameManager.Player.Experience.GiveExp(30.0f);
        }

        yield return new WaitForSeconds(DeathDuration);

        Debug.Log("Warrior is dead.");
        if (!playerDeath)
        {
            gameObject.SetActive(false);
        }


    }

    protected abstract void OnDeath();

    private void ToggleColliders(bool value)
    {
        foreach (var collider in Colliders)
        {
            collider.enabled = value;
        }
    }
}