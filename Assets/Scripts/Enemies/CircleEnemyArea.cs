using UnityEngine;
using System.Collections;
using Utilities;

[RequireComponent(typeof(SphereCollider))]
public class CircleEnemyArea : EnemyArea
{
    public float DangerRadius = 10f;
    public float WarningRadius = 15f;

    private Vector3 _center;
    private SphereCollider _collider;

    public SphereCollider BoundsCollider
    {
        get
        {
            if (_collider == null)
                _collider = GetComponent<SphereCollider>();

            return _collider;
        }
    }

    private void Awake()
    {
        _center = transform.position;
    }

    private void OnDrawGizmos()
    {
        GizmosHelper.DrawCircleArena(transform.position, DangerRadius, 36, DangerColor);
        GizmosHelper.DrawCircleArena(transform.position, WarningRadius, 36, WarningColor);
    }

    protected override void OnValidate()
    {
        BoundsCollider.radius = DangerRadius;
    }

    public override Vector3 GetBorder(Transform source, Transform target)
    {
        return Vector3.ClampMagnitude((target.position - _center), DangerRadius);
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
