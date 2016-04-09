using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Collider))]
public class AttackBehavior : MonoBehaviour
{
    public Attack[] Attacks;
    [Range(0, 1)]
    public float DamageMultiplier = 1f;
    public bool AutomaticAttack = false;
    public bool AttackIsOn = true;
    public bool Attacking = false;

    private float _currentDamage;
    private int _playerHash;
    private Stamina _stamina;
    private List<Collider> _playersInRange;

    public bool CanAttack
    {
        get
        {
            return AttackIsOn && !Attacking;
        }
    }

    void Awake()
    {
        _stamina = GetComponentInParent<Stamina>();
        _playerHash = Tags.Player.ToHash();
        _playersInRange = new List<Collider>();
    }

    void Update()
    {
        if (!AutomaticAttack || !CanAttack)
            return;

        if (_playersInRange.Count > 0)
        {
            ChooseAndApplyAttack();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        var timer = new System.Diagnostics.Stopwatch();
        timer.Start();

        var otherHash = other.tag.GetHashCode();
        if (otherHash == _playerHash && !_playersInRange.Contains(other))
        {
            _playersInRange.Add(other);
        }
    }

    void OnTriggerExit(Collider other)
    {
        _playersInRange.Remove(other);
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

        foreach (var target in _playersInRange)
        {
            var damageable = target.GetComponent<IDamageable>();

            if (damageable != null)
                damageable.TakeDamage(attack.Damage * DamageMultiplier);
        }

        yield return new WaitForSeconds(attack.Duration - attack.HitTime);
        Attacking = false;
    }

    [System.Serializable]
    public class Attack
    {
        public float Damage;
        public float StaminaCost;
        public float Duration, HitTime;
    }
}
