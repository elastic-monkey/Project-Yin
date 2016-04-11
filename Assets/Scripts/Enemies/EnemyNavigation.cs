﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyNavigation : MonoBehaviour
{
    public Tags PlayerTag;
    public LayerMask RaycastMask;
    public float LineOfSight = 10f;
    [Range(0, 360)]
    public int AngleOfSight = 50;
    [Range(5, 60)]
    public int CheckForEnemyRate = 15;
    public Transform Target;
    public bool Asleep;
    public bool TargetInSight;

    private NavMeshAgent _navAgent;
    [SerializeField]
    private Collider _targetCollider;
    private float _sqrEyesightRange;
    private Vector3 _lastKnownPosition;

    void Awake()
    {
        var playerHash = PlayerTag.ToHash();
        var colliders = Target.GetComponentsInChildren<Collider>();
        for (var i = 0; i < colliders.Length; i++)
        {
            var hash = colliders[i].tag.GetHashCode();
            if (playerHash == hash)
            {
                _targetCollider = colliders[i];
                break;
            }
        }

        Target = _targetCollider.transform;

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
            transform.LookAt(Target);
            _navAgent.SetDestination(Target.position);
        }
        else
        {
            _navAgent.SetDestination(_lastKnownPosition);
        }

        Debug.DrawLine(transform.position, Target.position, TargetInSight ? Color.green : Color.red);
    }

    void OnDrawGizmos()
    {
        GizmosHelper.DrawAngleOfSight(transform.position, transform.forward * LineOfSight, AngleOfSight, 20, Color.yellow);
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
                    var ray = new Ray(transform.position, direction);
                    RaycastHit hitInfo;
                    
                    var newTargetInSight = Physics.Raycast(ray, out hitInfo, LineOfSight, RaycastMask)
                        && (hitInfo.collider == _targetCollider)
                        && (Vector3.Angle(transform.forward, direction) <= 0.5f * AngleOfSight);

                    if (TargetInSight != newTargetInSight)
                    {
                        TargetInSight = newTargetInSight;
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
                else if(TargetInSight)
                {
                    TargetInSight = false;
                    _lastKnownPosition = Target.position;
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