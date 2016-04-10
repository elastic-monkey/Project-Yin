using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Animator))]
public class AttackBehavior : MonoBehaviour
{
    public Tags EnemyTag;
    public Attack[] Attacks;
    public bool CanAttack = true;
    public bool Attacking = false;
    [HideInInspector]
    public List<Collider> Targets;

    private Stamina _stamina;

    private bool CanPerformNewAttack
    {
        get
        {
            return CanAttack && !Attacking;
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
        if (!CanPerformNewAttack)
            return;

        var chosenAttack = Attacks[attackIndex];

        if (!_stamina.CanConsume(chosenAttack.StaminaCost))
            return;

        StartCoroutine(AttackCoroutine(chosenAttack));
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
    public int Damage = 10;
    [Range(1, 10)]
    public float Radius = 1;
    [Tooltip("This is the full opening angle between the object's forward direction and the enemies.")]
    [Range(10, 360)]
    public float Angle = 10f;
    public int StaminaCost = 10;
    public float Duration = 0f, HitTime = 0f;
}
