using UnityEngine;
using System.Collections;

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
	public EnemyArea MyDangerArea;

    private EnemyMovement _myMovement;
    [SerializeField]
    private WarriorBehavior _target;

	public bool HasEnemiesInRange
	{
		get
		{
			return _enemiesInRange.Count > 0;
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

	protected override void Start()
	{
		base.Start();

		StartCoroutine(AttackAndDefend());
	}

	protected override void Update()
	{
		base.Update();

		if (!Health.Alive)
			return;

		if (Health.CurrentHealth < 0)
		{
			_gameManager.OnEnemyDeath(this);

			Die();

			return;
		}

        if (Target == null)
        {
            if (Eye.CanSee(transform, _gameManager.Player.transform))
            {
                SetTarget(_gameManager.Player);
            }
        }
        else
        {
            if (MyDangerArea.PlayerInDangerZone)
            {
                _myMovement.ChaseTarget();
            }
            else if (MyDangerArea.PlayerInWarningZone)
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
        }
	}

    public override void OnAttacked(WarriorBehavior attacker)
    {
        base.OnAttacked(attacker);

        SetTarget(attacker);
    }

    public void SetTarget(WarriorBehavior target)
    {
        _target = target;
    }

	private IEnumerator AttackAndDefend()
	{
		while (true)
		{
            if (HasEnemiesInRange && Eye.CanSee(_enemiesInRange[0].transform, transform))
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

			yield return new WaitForSeconds(1);
		}
	}

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (other.CompareTag(Tags.DangerArea.TagToString()))
        {
            var dangerArea = other.GetComponent<EnemyArea>();
            dangerArea.AddEnemy(this);
            MyDangerArea = dangerArea;
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);

        if (other.CompareTag(Tags.DangerArea.TagToString()))
        {
            MyDangerArea = null;
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