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
	public WarriorBehavior Target;
	public Eyesight Eye;
	public DangerArea DangerArea;

	public bool HasEnemiesInRange
	{
		get
		{
			return _enemiesInRange.Count > 0;
		}
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

		if (Eye.CanSee(Target.transform, transform))
		{
			if (Target.InsideDangerArea(DangerArea))
			{
				Movement.SetTarget(Target.transform.position);
			}
			else if (Target.InsideWarningArea(DangerArea))
			{
				Movement.SetTarget(DangerArea.GetBorderPosition(Target.transform));
			}
			else
			{
				Movement.ResetTarget();
			}
		}
		else
		{
			Movement.ResetTarget();
		}
	}

	private void OnDrawGizmos()
	{
		GizmosHelper.DrawAngleOfSight(transform.position, transform.forward * Eye.Range, Eye.Angle, 20, Color.yellow);
	}

	private IEnumerator AttackAndDefend()
	{
		while (true)
		{
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

[System.Serializable]
public class Eyesight
{
	public LayerMask Mask;
	public float Range = 10f;
	[Range(0, 360)]
	public int Angle = 50;

	public bool CanSee(Transform target, Transform from)
	{
		if (Vector3.Distance(target.position, from.position) > Range)
			return false;

		var direction = (target.position - from.position).normalized;
		var ray = new Ray(from.position, direction);

		if (Physics.Raycast(ray, Range, Mask) && Vector3.Angle(from.forward, direction) <= 0.5f * Angle)
		{
			Debug.DrawLine(from.position, target.position, Color.green);
			return true;
		}
		else
		{
			Debug.DrawLine(from.position, target.position, Color.red);
			return false;
		}
	}
}