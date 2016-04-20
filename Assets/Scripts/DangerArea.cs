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

	public Vector3 GetBorderPosition(Transform source, Transform target)
	{
		return Vector3.ClampMagnitude((target.position - _center), DangerRadius);
	}

	//public Vector3 GetBorderPosition(Transform source, Transform target)
	//{
	//	var d = target.position - source.position;
	//	var f = source.position - _center;
	//	float a = Vector3.Dot(d, d);
	//	float b = 2 * Vector3.Dot(f, d);
	//	float c = Vector3.Dot(f, f) - DangerRadius * DangerRadius;

	//	float discriminant = b * b - 4 * a * c;
	//	if (discriminant < 0)
	//		// no intersection
	//		return source.position;

	//	Debug.DrawLine(source.position, source.position + d, Color.yellow);

	//	// ray didn't totally miss circle,
	//	// so there is a solution to
	//	// the equation.
	//	discriminant = Mathf.Sqrt(discriminant);

	//	// either solution may be on or off the ray so need to test both.
	//	// t1 is always the smaller value, because BOTH discriminant and
	//	// a are nonnegative.
	//	float t1 = (-b - discriminant) / (2 * a);
	//	float t2 = (-b + discriminant) / (2 * a);

	//	// 3x HIT cases:
	//	//          -o->             --|-->  |            |  --|->
	//	// Impale(t1 hit,t2 hit), Poke(t1 hit,t2>1), ExitWound(t1<0, t2 hit), 

	//	// 3x MISS cases:
	//	//       ->  o                     o ->              | -> |
	//	// FallShort (t1>1,t2>1), Past (t1<0,t2<0), CompletelyInside(t1<0, t2>1)

	//	if (t1 >= 0 && t1 <= 1)
	//	{
	//		Debug.DrawLine(source.position + new Vector3(0, 0.2f, 0), source.position + t1 * d, Color.cyan);

	//		// t1 is the intersection, and it's closer than t2
	//		// (since t1 uses -b - discriminant)
	//		// Impale, Poke
	//		return source.position + t1 * d;
	//	}

	//	// here t1 didn't intersect so we are either started
	//	// inside the sphere or completely past it
	//	if (t2 >= 0 && t2 <= 1)
	//	{
	//		Debug.DrawLine(source.position + new Vector3(0, 0.4f, 0), source.position + t2 * d, Color.magenta);

	//		// ExitWound
	//		return source.position + t2 * d;
	//	}

	//	// no intn: FallShort, Past, CompletelyInside
	//	return source.position;
	//}
}
