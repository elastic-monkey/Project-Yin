using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyBehavior : MonoBehaviour
{
    public const int AttackDefenseSliderStep = 5;
    public const int AttackDefenseSliderMin = 0, AttackDefenseSliderMax = 100;
    public const int CourageSliderStep = 5;
    public const int CourageSliderMin = 0, CourageSliderMax = 100;

    public Tags EnemyTag;
    public bool AutomaticAttack, AutomaticDefense;
    [HideInInspector]
    public float AttackDefense = 50;
    [HideInInspector]
    public float Courage = 50;

    private DefenseBehavior _defense;
    private AttackBehavior _attack;
    private int _enemyHash;
    private List<Collider> _enemiesInRange;

    public bool HasEnemiesToAttack
    {
        get
        {
            return _enemiesInRange.Count > 0;
        }
    }

    void Awake()
    {
        _defense = GetComponent<DefenseBehavior>();
        _attack = GetComponentInChildren<AttackBehavior>();
        _attack.Targets = _enemiesInRange;
        _enemiesInRange = new List<Collider>();
        _enemyHash = EnemyTag.ToHash();

        StartCoroutine(AttackAndDefend());
    }

    IEnumerator AttackAndDefend()
    {
        while (true)
        {
            if (HasEnemiesToAttack)
            {
                if (AutomaticAttack)
                {
                    if (AutomaticDefense)
                    {
                        // Of course there are other considerations:
                        //  - Are the enemies attacking me also?
                        //  - Am I sorrounded or not? (In which case, maybe running is not a bad idea)
                        var inclination = Random.Range(AttackDefenseSliderMin, AttackDefenseSliderMax);
                        if (inclination >= AttackDefense)
                        {
                            Debug.Log("Attack");
                        }
                        else
                        {
                            Debug.Log("Defense");
                        }
                    }
                    else
                    {
                        Debug.Log("Attack");
                    }
                }
                else if (AutomaticDefense)
                {
                    Debug.Log("Defense");
                }
            }

            yield return new WaitForSeconds(1);
        }
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
            if (_defense.Defending)
            {
                // This will only be effective if active defense is a no-duration defense
                _defense.CancelDefense();
            }
        }
    }
}
