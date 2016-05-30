using UnityEngine;
using Utilities;

[RequireComponent(typeof(NavMeshAgent), typeof(EnemyBehavior))]
public class EnemyMovement : Movement
{
    public float StoppingRange;
    public bool ChasingTarget;
    public bool StandingGuard;
    public bool GoingBack;
    public bool OnInitialPos;
    public float DistanceToInitial;

    private EnemyBehavior _enemyBehavior;
    private NavMeshAgent _navAgent;
    private Quaternion _initialRotation;
    private Vector3 _initialPosition;
    [SerializeField]
    private Transform _targetTransform;

    private void Awake()
    {
        _enemyBehavior = GetComponent<EnemyBehavior>();
        _navAgent = GetComponent<NavMeshAgent>();
        _navAgent.stoppingDistance = 0;

        StoppingRange = _enemyBehavior.Attack.Range;
    }

    private void Start()
    {
        _initialPosition = transform.position;
        _initialRotation = transform.rotation;

        ChasingTarget = false;
        OnInitialPos = true;
    }

    private void Update()
    {
        if (!CanMove)
        {
            StopAndLookAtTarget();
            return;
        }

        if (_targetTransform == null)
        {
            if (_enemyBehavior.Target != null)
            {
                SetTarget(_enemyBehavior.Target.transform);
            }
        }
        else
        {
            if (ChasingTarget)
            {
                if (StandingGuard)
                {
                    // No movement except rotation 
                    StopAndLookAtTarget();
                }
                else if (Vector3Helper.DistanceXZ(_targetTransform.position, transform.position) <= StoppingRange)
                {
                    // If too close, stops movement.
                    StopAndLookAtTarget();
                    Moving = false;
                }
                else if(CanMove)
                {
                    // Else moves along path to catch player
                    _navAgent.SetDestination(_targetTransform.position);
                    _navAgent.Resume();
                    Moving = true;
                }
            }
        }

        if (GoingBack)
        {
            if (Moving)
            {
                DistanceToInitial = Vector3Helper.DistanceXZ(_initialPosition, transform.position);
                if (Mathf.Approximately(DistanceToInitial, 0))
                {
                    _navAgent.Stop();
                    Moving = false;
                }
                else
                {
                    _navAgent.Resume();
                }
            }
            else if (Quaternion.Angle(transform.rotation, _initialRotation) > 5)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, _initialRotation, Time.deltaTime * TurnSpeed);
            }
            else if (!OnInitialPos)
            {
                GoingBack = false;
                OnInitialPos = true;
            }
        }
    }

    private void StopAndLookAtTarget()
    {
        _navAgent.Stop();
        LookAtTarget();
    }

    private void LookAtTarget()
    {
        var lookRotation = Quaternion.LookRotation(_targetTransform.position - transform.position);
        if (Quaternion.Angle(transform.rotation, lookRotation) > 5)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * TurnSpeed);
        }
    }

    private void SetTarget(Transform target)
    {
        _targetTransform = target;
    }

    public void ChaseTarget()
    {
        if (!CanMove)
            return;

        if (ChasingTarget && !StandingGuard)
            return;

        Moving = true;
        ChasingTarget = true;
        StandingGuard = false;
        GoingBack = false;
        OnInitialPos = false;
    }

    public void StandGuard()
    {
        if (StandingGuard)
            return;
        
        Moving = false;
        ChasingTarget = true;
        StandingGuard = true;
        GoingBack = false;
    }

    public void GoBack()
    {
        if (GoingBack)
            return;
        
        Moving = true;
        ChasingTarget = false;
        StandingGuard = false;
        GoingBack = true;

        SetTarget(null);
        _navAgent.SetDestination(_initialPosition);
    }
}
