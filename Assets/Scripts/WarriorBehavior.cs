using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(AttackBehavior), typeof(DefenseBehavior))]
public class WarriorBehavior : MonoBehaviour
{
    public Tags EnemyTag;

    protected int _enemyHash;
    [SerializeField]
    protected List<Collider> _enemiesInRange;
    protected DefenseBehavior _defenseBehavior;
    protected AttackBehavior _attackBehavior;

    protected virtual void Awake()
    {
        _defenseBehavior = GetComponent<DefenseBehavior>();
        _attackBehavior = GetComponent<AttackBehavior>();
        _attackBehavior.Targets = _enemiesInRange;
        _enemiesInRange = new List<Collider>();
        _enemyHash = EnemyTag.ToHash();
    }

    protected virtual void Update()
    {
        _attackBehavior.Targets = _enemiesInRange;
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
