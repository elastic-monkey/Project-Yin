using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
public class DangerArea : MonoBehaviour
{
	public Color AreaColor = Color.red;
	public float Range = 10f;

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

	private void OnDrawGizmos()
	{
		GizmosHelper.DrawDangerArea(transform.position, Range, 10, AreaColor);
	}

	private void OnValidate()
	{
		BoundsCollider.radius = Range;
	}
}
