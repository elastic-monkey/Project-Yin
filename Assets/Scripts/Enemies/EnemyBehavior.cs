using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AttackBehavior), typeof(DefenseBehavior))]
public class EnemyBehavior : WarriorBehavior
{
    public const int AttackDefenseSliderStep = 5;
    public const int AttackDefenseSliderMin = 0, AttackDefenseSliderMax = 100;
    public const int CourageSliderStep = 5;
    public const int CourageSliderMin = 0, CourageSliderMax = 100;

    public bool AutomaticAttack, AutomaticDefense;
    [HideInInspector]
    public float AttackDefense = 50;
    [HideInInspector]
    public float Courage = 50;
    public float EyesightRange = 5f;
    public Transform Target;

    public bool HasEnemiesToAttack
    {
        get
        {
            return _enemiesInRange.Count > 0;
        }
    }

    void Start()
    {
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
                            //Debug.Log("Attack");
                        }
                        else
                        {
                            //Debug.Log("Defense");
                        }
                    }
                    else
                    {
                        //Debug.Log("Attack");
                    }
                }
                else if (AutomaticDefense)
                {
                   //Debug.Log("Defense");
                }
            }

            yield return new WaitForSeconds(1);
        }
    }
}
