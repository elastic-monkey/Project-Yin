using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent), typeof(EnemyBehavior))]
public class EnemyMovement : Movement
{
	public Tags PlayerTag;
	public LayerMask RaycastMask;
	[Range(5, 60)]
	public int CheckForEnemyRate = 15;
	public float LineOfSight = 10f;
	[Range(0, 360)]
	public int AngleOfSight = 50;
	[Range(0, 360)]
	public int EyePatrolRotation;
	public float PatrolSpeed;
	public bool TargetInRange, TargetInSight;

	private EnemyBehavior _behavior;
	private NavMeshAgent _navAgent;
	private float _sqrEyesightRange;
	private Vector3 _startingPosition, _startingDirection, _lastKnownPosition, _lastKnownDirection;
	private float RefreshRate;

	private void Awake()
	{
		_behavior = GetComponent<EnemyBehavior>();
		_navAgent = GetComponent<NavMeshAgent>();
	}

	private void Start()
	{
		_sqrEyesightRange = LineOfSight * LineOfSight;
		_lastKnownPosition = transform.position;
		_lastKnownDirection = transform.forward + transform.position;
		_startingPosition = _lastKnownPosition;
		_startingDirection = _lastKnownDirection;

		UpdateRefreshRate();

		StartCoroutine(CheckForTargetInSight());
	}

	private void Update()
	{
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_lastKnownDirection, Vector3.up), Time.deltaTime * TurnSpeed);
		_navAgent.SetDestination(_lastKnownPosition);

		Debug.DrawLine(transform.position, _lastKnownPosition, TargetInSight ? Color.green : Color.red);
	}

	private void OnDrawGizmos()
	{
		GizmosHelper.DrawAngleOfSight(transform.position, transform.forward * LineOfSight, AngleOfSight, 20, Color.yellow);
	}

	private IEnumerator CheckForTargetInSight()
	{
		while (true)
		{
			yield return new WaitForSeconds(RefreshRate);

			if (!CanMove || _behavior.Target == null)
				continue;

			var delta = (_behavior.Target.transform.position - transform.position);
			TargetInRange = Vector3.SqrMagnitude(delta) <= _sqrEyesightRange;

			if (TargetInRange)
			{
				var direction = delta.normalized;
				var ray = new Ray(transform.position, direction);
				RaycastHit hitInfo;

				TargetInSight = Physics.Raycast(ray, out hitInfo, LineOfSight, RaycastMask)
					&& (Vector3.Angle(transform.forward, direction) <= 0.5f * AngleOfSight);

				if (TargetInSight)
				{
					_lastKnownPosition = hitInfo.transform.position;
					_lastKnownDirection = _lastKnownPosition + hitInfo.transform.forward;
				}
			}
			else
			{
				TargetInSight = false;
			}
		}
	}

	private void UpdateRefreshRate()
	{
		RefreshRate = 1f / CheckForEnemyRate;
	}

	private void OnValidate()
	{
		UpdateRefreshRate();
	}
}
