using UnityEngine;
using System.Collections;
using Utilities;

public class CircleEnemyArea : EnemyArea
{
    public float DangerRadius = 10f;
    public float WarningRadius = 15f;

    private Vector3 _center;

    protected override void Awake()
    {
        base.Awake();

        _center = transform.position;
    }

    private void OnDrawGizmos()
    {
        GizmosHelper.DrawCircleArena(transform.position, DangerRadius, 36, DangerColor);
        GizmosHelper.DrawCircleArena(transform.position, WarningRadius, 36, WarningColor);
    }

    public override bool ContainsInDangerZone(Transform t)
    {
        return DistanceToCenter(t) <= DangerRadius;
    }

    public override bool ContainsInWarningZone(Transform t)
    {
        return DistanceToCenter(t) <= WarningRadius;
    }

    public override bool ContainsInZone(Transform t)
    {
        return ContainsInWarningZone(t);
    }

    private float DistanceToCenter(Transform obj)
    {
        return Vector3Helper.DistanceXZ(obj.position, _center);
    }
}
