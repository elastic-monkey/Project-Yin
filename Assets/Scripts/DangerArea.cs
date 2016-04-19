using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
public class DangerArea : MonoBehaviour
{
	public Color AreaColor = Color.red;
	public float Radius = 10f;

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
		GizmosHelper.DrawArena(transform.position, Radius, 20, AreaColor);
	}

	public bool IsInside(Transform obj)
	{
		return Vector3.Distance(obj.position, _center) <= Radius;
	}

	private void OnValidate()
	{
		BoundsCollider.radius = Radius;
	}
}
