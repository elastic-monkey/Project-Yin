using UnityEngine;
using System.Collections;
using Utilities;

[RequireComponent(typeof(AttackBehavior), typeof(DefenseBehavior))]
public class EnemyBehavior : WarriorBehavior
{
    public const int AttackDefenseSliderStep = 5;
    public const int CourageSliderStep = 5;
    public const int MinAttackDefense = 0, MaxAttackDefense = 100;
    public const int MinCourage = 0, MaxCourage = 100;

    public bool AutomaticAttack, AutomaticDefense;
    [Tooltip("Experience gained by the player when this enemy is killed.")]
    public int ExperienceValue;
    [HideInInspector]
    public float AttackDefense = 50;
    [HideInInspector]
    public float Courage = 50;
    public Eyesight Eye;
    public EnemyArea MyArea;

    private EnemyMovement _myMovement;
    [SerializeField]
    private WarriorBehavior _target;

    public bool HasEnemiesInRange
    {
        get
        {
            return Attack.Targets.Count > 0;
        }
    }

    public WarriorBehavior Target
    {
        get
        {
            return _target;
        }
    }

    protected override void Awake()
    {
        base.Awake();

        _myMovement = Movement as EnemyMovement;
    }

    protected override void Update()
    {
        base.Update();

        if (!Health.Alive)
            return;

        if (Health.CurrentHealth <= 0)
        {
            _gameManager.OnEnemyDeath(this);

            Die();

            return;
        }

        _myMovement.CanMove = !Attack.Attacking;

        if (Target == null)
        {
            if (Eye.CanSee(transform, _gameManager.Player.transform))
            {
                SetTarget(_gameManager.Player);
            }
        }
        else
        {
            if (MyArea.PlayerInDangerZone)
            {
                _myMovement.ChaseTarget();
                MyArea.NotifyEnemies(Target);
            }
            else if (MyArea.PlayerInWarningZone)
            {
                _myMovement.StandGuard();
            }
            else if (Eye.CanSee(transform, Target.transform))
            {
                _myMovement.StandGuard();
            }
            else
            {
                _myMovement.GoBack();
                SetTarget(null);
            }

            if (HasEnemiesInRange)
            {
                if (AutomaticAttack)
                {
                    if (AutomaticDefense)
                    {
                        // Of course there are other considerations:
                        //  - Are the enemies attacking me also?
                        //  - Am I sorrounded or not? (In which case, maybe running is not a bad idea)
                        var inclination = Random.Range(MinAttackDefense, MaxAttackDefense);
                        if (inclination >= AttackDefense)
                        {
                            Attack.ChooseAndApplyAttack();
                        }
                        else
                        {
                            Defense.ChooseAndApplyDefense();
                        }
                    }
                    else
                    {
                        Attack.ChooseAndApplyAttack();
                    }
                }
                else if (AutomaticDefense)
                {
                    Defense.ChooseAndApplyDefense();
                }
            }
        }
    }

    public override void OnAttacked(WarriorBehavior attacker)
    {
        SetTarget(attacker);

        MyArea.NotifyEnemies(attacker);
    }

    public void SetTarget(WarriorBehavior target)
    {
        _target = target;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.DangerArea.TagToString()))
        {
            var dangerArea = other.GetComponent<EnemyArea>();
            dangerArea.AddEnemy(this);
            MyArea = dangerArea;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tags.DangerArea.TagToString()))
        {
            MyArea = null;
        }
    }

    private void OnDrawGizmos()
    {
        GizmosHelper.DrawAngleOfSight(transform.position, transform.forward * Eye.Range, Eye.Angle, 20, Color.yellow);
    }
}

[System.Serializable]
public class Eyesight
{
    public LayerMask Mask;
    public float Range = 10f;
    [Range(0, 360)]
    public int Angle = 50;

    public bool CanSee(Transform from, Transform to)
    {
        if (Vector3.Distance(to.position, from.position) > Range)
            return false;

        var direction = (to.position - from.position).normalized;
        var ray = new Ray(from.position, direction);

        if (Physics.Raycast(ray, Range, Mask) && Vector3.Angle(from.forward, direction) <= 0.5f * Angle)
        {
            //Debug.DrawLine(from.position, target.position, Color.green);
            return true;
        }
        else
        {
            //Debug.DrawLine(from.position, target.position, Color.red);
            return false;
        }
    }
}