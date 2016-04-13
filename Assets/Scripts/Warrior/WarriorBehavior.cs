using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(AttackBehavior), typeof(DefenseBehavior))]
[RequireComponent(typeof(Health), typeof(Stamina))]
public class WarriorBehavior : MonoBehaviour
{
    public Tags EnemyTag;

    [SerializeField]
    protected List<Collider> _enemiesInRange;
    private DefenseBehavior _defenseBehavior;
    private AttackBehavior _attackBehavior;
    private Health _health;
    private Stamina _stamina;
    private GameManager _gameManager;

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
    }

    protected virtual void Start()
    {
        _gameManager = GameManager.Instance;
    }

    protected virtual void Update()
    {
        if (Health.IsDead)
        {
            _gameManager.OnWarriorDeath(this);
            return;
        }

        Attack.Targets = _enemiesInRange;
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
}
