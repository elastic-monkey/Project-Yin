using UnityEngine;

[RequireComponent(typeof(NavMeshAgent), typeof(EnemyBehavior))]
public class EnemyMovement : Movement
{
    public bool GoingBack;
    public bool OnInitialPos;

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
		// TODO: Transform movement into our own personal movement, using navMeshAgent paths.

        if (Moving)
        {
            if (Vector3.Distance(CurrentTarget, transform.position) < 1)
            {
                _navAgent.ResetPath();
                Moving = false;
            }
        }
        else if(GoingBack)
        {
            if (Quaternion.Angle(transform.rotation, _initialRot) > 10)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, _initialRot, Time.deltaTime * TurnSpeed);
            }
            else
            {
                GoingBack = false;
                OnInitialPos = true;
            }
        }
	}

	public override void SetTarget(Vector3 position)
	{
		Moving = true;
        OnInitialPos = false;
        GoingBack = false;
		CurrentTarget = position;

		_navAgent.SetDestination(CurrentTarget);
	}

	public override void ResetTarget()
	{
		if (GoingBack)
			return;

        if (OnInitialPos)
            return;

		Moving = true;
        GoingBack = true;
        OnInitialPos = false;
		CurrentTarget = _initialPos;

		_navAgent.SetDestination(CurrentTarget);
	}
}
