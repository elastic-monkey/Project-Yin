using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackBehavior : MonoBehaviour
{
    public Tags EnemyTag;
    public Attack[] Attacks;
    public bool AttackIsOn = true;
    public bool Attacking = false;
    [HideInInspector]
    public List<Collider> Targets;

    private float _currentDamage;
    private Stamina _stamina;

    public bool CanAttack
    {
        get
        {
            return AttackIsOn && !Attacking;
        }
    }

    void Awake()
    {
        _stamina = GetComponent<Stamina>();
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

    // This class was created so that we can call it
    // on the editor, for example, or with input controls
    public void ApplyAttack(int attackIndex)
    {
        if (!CanAttack)
            return;

        var chosenAttack = Attacks[attackIndex];

        if (_stamina.CanConsume(chosenAttack.StaminaCost))
        {
            StartCoroutine(AttackCoroutine(chosenAttack));
        }
    }

    private IEnumerator AttackCoroutine(Attack attack)
    {
        Attacking = true;

        yield return new WaitForSeconds(attack.HitTime);

        _stamina.ConsumeStamina(attack.StaminaCost);

        foreach (var target in Targets)
        {
            var defense = target.GetComponent<DefenseBehavior>();

            if (defense != null)
                defense.TakeDamage(attack.Damage);
        }

        yield return new WaitForSeconds(Mathf.Max(0, attack.Duration - attack.HitTime));
        Attacking = false;
    }
}

[System.Serializable]
public class Attack
{
    public int Damage;
    public int StaminaCost;
    public float Duration, HitTime;
}
