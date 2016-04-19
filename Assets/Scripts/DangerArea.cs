using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(SphereCollider))]
public class DangerArea : MonoBehaviour
{
	public Color DangerColor = Color.red, WarningColor = Color.yellow;
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
		GizmosHelper.DrawArena(transform.position, DangerRadius, 36, DangerColor);
		GizmosHelper.DrawArena(transform.position, WarningRadius, 36, WarningColor);
	}

	public float DistanceToCenter(Transform obj)
	{
		return Vector3.Distance(obj.position, _center);
	}

	private void OnValidate()
	{
		BoundsCollider.radius = DangerRadius;
	}

	public Vector3 GetBorderPosition(Transform transform)
	{
		return Vector3.ClampMagnitude((transform.position - _center), DangerRadius);
	}
}
