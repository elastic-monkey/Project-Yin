using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyNavigation : MonoBehaviour
{
    public float LineOfSight = 10f;
    [Range(0, 360)]
    public int AngleOfSight = 50;
    [Range(5, 60)]
    public int CheckForEnemyRate = 15;
    public Transform Target;
    public bool Asleep;
    public bool TargetInSight;

    private NavMeshAgent _navAgent;
    private Collider _targetCollider;
    private float _sqrEyesightRange;
    private Vector3 _lastKnownPosition;

    void Awake()
    {
        _targetCollider = Target.GetComponent<Collider>();
        _navAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        _sqrEyesightRange = LineOfSight * LineOfSight;
        _lastKnownPosition = transform.position;

        StartCoroutine(CheckForTargetInSight());
    }

    void Update()
    {
        if (TargetInSight)
        {
            _navAgent.SetDestination(Target.position);
        }
        else
        {
            _navAgent.SetDestination(_lastKnownPosition);
        }

        Debug.DrawLine(transform.position, Target.position, Color.green);
    }

    void OnDrawGizmos()
    {
        GizmosHelper.DrawAngleOfSight(transform.position, transform.forward * LineOfSight, AngleOfSight, 20, Color.red);
    }

    IEnumerator CheckForTargetInSight()
    {
        var oldCheckRate = CheckForEnemyRate;
        var refreshSec = 1f / CheckForEnemyRate;

        while (true)
        {
            if (!Asleep)
            {
                var delta = Target.position - transform.position;
                if (Vector3.SqrMagnitude(delta) <= _sqrEyesightRange)
                {
                    var direction = delta.normalized;
                    RaycastHit hitInfo;

                    var oldTargetInSight = TargetInSight;

                    TargetInSight = Physics.Raycast(transform.position, direction, out hitInfo, LineOfSight)
                        && (hitInfo.collider == _targetCollider)
                        && (Vector3.Angle(transform.forward, direction) <= 0.5f * AngleOfSight);

                    if (TargetInSight != oldTargetInSight)
                    {
                        if (TargetInSight)
                        {
                            transform.LookAt(Target);
                        }
                        else
                        {
                            _lastKnownPosition = Target.position;
                        }
                    }
                }
                else
                {
                    TargetInSight = false;
                }
            }

            if (oldCheckRate != CheckForEnemyRate)
            {
                oldCheckRate = CheckForEnemyRate;
                refreshSec = 1f / CheckForEnemyRate;
            }

            yield return new WaitForSeconds(refreshSec);
        }
    }
}
