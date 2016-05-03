using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
public class CircleDangerArea : DangerArea
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

    public override bool ContainsInDangerRadius(Transform t)
    {
        return DistanceToCenter(t) <= DangerRadius;
    }

    public override bool ContainsInWarningRadius(Transform t)
    {
        return DistanceToCenter(t) <= WarningRadius;
    }

    private float DistanceToCenter(Transform obj)
    {
        return Vector3.Distance(obj.position, _center);
    }
}
