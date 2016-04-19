using UnityEngine;

[RequireComponent(typeof(NavMeshAgent), typeof(EnemyBehavior))]
public class EnemyMovement : Movement
{
	private NavMeshAgent _navAgent;
	private Quaternion _initialRot;
	[SerializeField]
	private Vector3 _initialPos, _targetPos;

	public Vector3 CurrentTarget
	{
		get
		{
			return _targetPos;
		}

		private set
		{
			_targetPos = value;
		}
	}

	public bool GoingBack
	{
		get
		{
			return CurrentTarget == _initialPos;
		}
	}

	private void Awake()
	{
		_navAgent = GetComponent<NavMeshAgent>();
	}

	private void Start()
	{
		_initialPos = transform.position;
		_initialRot = transform.rotation;
	}

	private void Update()
	{
		// TODO: Transform movement into owr own personal movement, using navMeshAgent paths.

		if (Moving && Vector3.Distance(CurrentTarget, transform.position) < 0.1f)
		{
			_navAgent.ResetPath();
			Moving = false;
		}
		else if (!Moving && GoingBack && Quaternion.Angle(transform.rotation, _initialRot) > 4)
		{
			transform.rotation = Quaternion.Lerp(transform.rotation, _initialRot, Time.deltaTime * TurnSpeed);
		}
	}

	public override void SetTarget(Vector3 position)
	{
		Moving = true;
		CurrentTarget = position;
		_navAgent.SetDestination(CurrentTarget);
	}

	public override void ResetTarget()
	{
		if (GoingBack)
			return;

		Moving = true;
		CurrentTarget = _initialPos;
		_navAgent.SetDestination(CurrentTarget);
	}
}
