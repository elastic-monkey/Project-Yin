using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Collider))]
public class AttackBehavior : MonoBehaviour
{
    public Tags EnemyTag;
    public Attack[] Attacks;
    public bool AutomaticAttack = false;
    public bool AttackIsOn = true;
    public bool Attacking = false;

    private float _currentDamage;
    private int _hashToAttack;
    private Stamina _stamina;
    private List<Collider> _collidersInRange;

    public bool CanAttack
    {
        get
        {
            return AttackIsOn && !Attacking;
        }
    }

    public bool HasEnemiesInRange
    {
        get
        {
            return _collidersInRange.Count > 0;
        }
    }

    void Awake()
    {
        _stamina = GetComponentInParent<Stamina>();
        _hashToAttack = EnemyTag.ToHash();
        _collidersInRange = new List<Collider>();
    }

    void Update()
    {
        if (!AutomaticAttack || !CanAttack)
            return;

        if (_collidersInRange.Count > 0)
        {
            ChooseAndApplyAttack();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        var otherHash = other.tag.GetHashCode();
        if (otherHash == _hashToAttack && !_collidersInRange.Contains(other))
        {
            _collidersInRange.Add(other);
        }
    }

    void OnTriggerExit(Collider other)
    {
        _collidersInRange.Remove(other);
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

        foreach (var target in _collidersInRange)
        {
            var defense = target.GetComponent<DefenseBehavior>();

            if (defense != null)
                defense.TakeDamage(attack.Damage);
        }

        yield return new WaitForSeconds(Mathf.Max(0, attack.Duration - attack.HitTime));
        Attacking = false;
    }

    [System.Serializable]
    public class Attack
    {
        public int Damage;
        public int StaminaCost;
        public float Duration, HitTime;
    }
}
