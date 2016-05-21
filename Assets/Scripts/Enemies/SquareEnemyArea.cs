using UnityEngine;
using System.Collections;
using Utilities;

[RequireComponent(typeof(BoxCollider))]
public class SquareEnemyArea : EnemyArea
{
    public float DangerWidth = 10f;
    public float DangerDepth = 10f;
    public float WarningWidth = 15f;
    public float WarningDepth = 15f;
    public float Height = 15f;

    private BoxCollider _collider;
    private float _halfDangerWidth, _halfWarningWidth;
    private float _halfDangerDepth, _halfWarningDepth;

    public BoxCollider BoundsCollider
    {
        get
        {
            if (_collider == null)
                _collider = GetComponent<BoxCollider>();

            return _collider;
        }
    }

    private void Awake()
    {
        _halfDangerWidth = DangerWidth / 2;
        _halfWarningWidth = WarningWidth / 2;
        _halfDangerDepth = DangerDepth / 2;
        _halfWarningDepth = WarningDepth / 2;
    }

    private void OnDrawGizmos()
    {
        GizmosHelper.DrawSquareArena(transform.position, transform.rotation, DangerWidth, DangerDepth, DangerColor);
        GizmosHelper.DrawSquareArena(transform.position, transform.rotation, WarningWidth, WarningDepth, WarningColor);
    }

    protected override void OnValidate()
    {
        BoundsCollider.size = new Vector3(WarningWidth, Height, WarningDepth);
    }

    public override Vector3 GetBorder(Transform source, Transform target)
    {
        throw new System.NotImplementedException();
    }

    public override bool ContainsInDangerZone(Transform t)
    {
        var localPosition = transform.InverseTransformPoint(t.position);

        if (Mathf.Abs(localPosition.x) > _halfDangerWidth)
            return false;

        if (Mathf.Abs(localPosition.z) > _halfDangerDepth)
            return false;

        return true;
    }

    public override bool ContainsInWarningZone(Transform t)
    {
        var localPosition = transform.InverseTransformPoint(t.position);

        if (Mathf.Abs(localPosition.x) > _halfWarningWidth)
            return false;

        if (Mathf.Abs(localPosition.z) > _halfWarningDepth)
            return false;

        return true;
    }

    public override bool ContainsInZone(Transform t)
    {
        return ContainsInWarningZone(t);
    }
}
