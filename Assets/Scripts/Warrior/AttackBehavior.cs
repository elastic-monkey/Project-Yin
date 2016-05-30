using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Utilities;

[RequireComponent(typeof(WarriorBehavior), typeof(Animator), typeof(Stamina))]
public class AttackBehavior : MonoBehaviour
{
    public float Range = 2f;
    public Attack[] Attacks;
    public bool CanAttack = true;
    public bool Attacking = false;
    public List<WarriorBehavior> Targets;
    public float DamageMultiplier = 1.0f;
    public float StaminaMultiplier = 1.0f;

    private Stamina _stamina;
    private WarriorBehavior _warrior;
    private GameManager _gameManager;
    private float _sqrRange;

    private bool CanPerformNewAttack
    {
        get
        {
            return CanAttack && !Attacking;
        }
    }

    private void Awake()
    {
        _stamina = GetComponent<Stamina>();
        _warrior = GetComponent<WarriorBehavior>();
        _sqrRange = Range * Range;
    }

    private void Start()
    {
        _gameManager = GameManager.Instance;
        }
    
    private void Update()
    {
        Targets.Clear();
        var enemies = _gameManager.GetWarriors(_warrior.EnemyTag);
        foreach (var enemy in enemies)
        {
            if (_sqrRange >= Vector3Helper.SqrDistanceXZ(_warrior.transform.position, enemy.transform.position))
            {
                Targets.Add(enemy);
            }
        }
    }

    private void OnDrawGizmos()
    {
        GizmosHelper.DrawAngleOfSight(transform.position + Vector3.up, transform.forward * Range, 360, 20, Color.red);
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

    public void ApplyAttack()
    {
        var chosenAttackIndex = -1;

        if (PlayerInput.IsButtonPressed(Axis.Fire2))
            chosenAttackIndex = 0;

        ApplyAttack(chosenAttackIndex);
    }

    public void ApplyAttack(int attackIndex)
    {
        if (!CanPerformNewAttack)
            return;

        if (attackIndex < 0 || attackIndex >= Attacks.Length)
            return;

        var chosenAttack = Attacks[attackIndex];

        if (!_stamina.CanConsume(chosenAttack.StaminaCost))
            return;

        StartCoroutine(AttackCoroutine(chosenAttack));
    }

    private IEnumerator AttackCoroutine(Attack attack)
    {
        Attacking = true;

        _stamina.ConsumeStamina(attack.StaminaCost * StaminaMultiplier);

        yield return new WaitForSeconds(attack.HitTime);

        foreach (var target in Targets)
        {
            var warrior = target.GetComponentInParent<WarriorBehavior>();
            warrior.OnAttacked(_warrior);

            if (warrior.Defense != null)
            {
                warrior.Defense.TakeDamage(attack.Damage * DamageMultiplier);
            }
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
