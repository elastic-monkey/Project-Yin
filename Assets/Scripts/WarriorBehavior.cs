using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(AttackBehavior), typeof(DefenseBehavior))]
[RequireComponent(typeof(Health), typeof(Stamina))]
public class WarriorBehavior : MonoBehaviour
{
    public Tags EnemyTag;

    protected int _enemyHash;
    [SerializeField]
    protected List<Collider> _enemiesInRange;
    private DefenseBehavior _defenseBehavior;
    private AttackBehavior _attackBehavior;
    private Health _health;
    private Stamina _stamina;

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

    protected virtual void Awake()
    {
        Attack.Targets = _enemiesInRange;
        _enemiesInRange = new List<Collider>();
        _enemyHash = EnemyTag.ToHash();
    }

    protected virtual void Update()
    {
        Attack.Targets = _enemiesInRange;
    }

    void OnTriggerEnter(Collider other)
    {
        var otherHash = other.tag.GetHashCode();
        if (otherHash == _enemyHash && !_enemiesInRange.Contains(other))
        {
            _enemiesInRange.Add(other);
        }
    }

    void OnTriggerExit(Collider other)
    {
        _enemiesInRange.Remove(other);
        if (_enemiesInRange.Count == 0)
        {
            if (_defenseBehavior.Defending)
            {
                // This will only be effective if active defense is not a oneTime defense
                _defenseBehavior.CancelDefense();
            }
        }
    }
}
