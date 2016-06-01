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
    private Health _health;
    private Stamina _stamina;
    private Animator _animator;
    private Movement _movement;
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
        Animator.SetBool(AnimatorHashIDs.CanMoveBool, Movement.CanMove);
        Animator.SetBool(AnimatorHashIDs.MovingBool, Movement.Moving);
        Animator.SetFloat(AnimatorHashIDs.SpeedMultiplierFloat, Movement.SpeedMulti);
        Animator.SetBool(AnimatorHashIDs.AttackingBool, Attack.Attacking);
        Animator.SetBool(AnimatorHashIDs.DefendingBool, Defense.Defending);
        Animator.SetBool(AnimatorHashIDs.DeadBool, !Health.Alive);

        if (Movement.Moving && !PlayerInput.GameplayBlocked)
            Animator.SetFloat(AnimatorHashIDs.SpeedFloat, Movement.SpeedThreshold, Movement.SpeedDampTime, Time.deltaTime);
        else
            Animator.SetFloat(AnimatorHashIDs.SpeedFloat, 0f);
    }

    public virtual void OnAttacked(WarriorBehavior attacker)
    {
        if (Defense.ShieldOn)
        {
            // Shield defense
        }
        else
        {
            // No defense. Hit
        }
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

        yield return new WaitForSeconds(DeathDuration);

        Debug.Log("Warrior is dead.");
        if (!playerDeath)
            gameObject.SetActive(false);
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