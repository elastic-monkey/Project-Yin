using UnityEngine;
using Utilities;

[RequireComponent(typeof(AttackBehavior), typeof(DefenseBehavior), typeof(HideByFading))]
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
    public EnemyArea Area;
	public GameObject CollectablePrefab;

    private EnemyMovement _myMovement;
    [SerializeField]
    private WarriorBehavior _target;
	private HideByFading _hide;

	public HideByFading HideByFading
	{
		get
		{
			if (_hide == null)
				_hide = GetComponent<HideByFading>();

			return _hide;
		}
	}

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
            if (Area.PlayerInDangerZone)
            {
                _myMovement.ChaseTarget();
                Area.NotifyEnemies(Target);
            }
            else if (Area.PlayerInWarningZone)
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
                            _myMovement.Stop();
                            Attack.ChooseAndApplyAttack();
                        }
                        else
                        {
                            _myMovement.Stop();
                            Defense.ChooseAndApplyDefense();
                        }
                    }
                    else
                    {
                        _myMovement.Stop();
                        Attack.ChooseAndApplyAttack();
                    }
                }
                else if (AutomaticDefense)
                {
                    _myMovement.Stop();
                    Defense.ChooseAndApplyDefense();
                }
            }
        }
    }

    public override void OnAttacked(WarriorBehavior attacker)
    {
        base.OnAttacked(attacker);

        SetTarget(attacker);
        Area.NotifyEnemies(attacker);
    }

    protected override void OnDeath()
    {
        Area.RemoveEnemy(this);

		HideByFading.Duration = DeathDuration * 0.5f;
		HideByFading.Hide();

		if (CollectablePrefab != null)
		{
			var obj = Instantiate(CollectablePrefab);
			obj.transform.position = transform.position + Vector3.up;
		}
	}

    public void SetTarget(WarriorBehavior target)
    {
        _target = target;
    }

    private void OnDrawGizmos()
    {
        GizmosHelper.DrawAngleOfSight(transform.position, transform.forward * Eye.Range, Eye.Angle, 20, Color.green);
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

        return (Physics.Raycast(ray, Range, Mask) && Vector3.Angle(from.forward, direction) <= 0.5f * Angle);
    }
}