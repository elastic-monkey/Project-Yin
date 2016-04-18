using UnityEngine;
using System.Collections;

public class IsometricCamera : MonoBehaviour
{
	public GameObject Target;
	public Camera Camera;
	public float Size = 10;
	public float ScrollSpeed = 30;
	public float Angle = 45;
	public float HorizontalDist = 5;
	public float VerticalDist = 5;

	void Start()
	{
		Camera = Camera.GetComponentInChildren<Camera>();
		Camera.orthographic = true;
		Camera.transform.localRotation = Quaternion.Euler(Angle, 0, 0);
		Camera.transform.localPosition = new Vector3(0, VerticalDist, -HorizontalDist);

		transform.position = Target.transform.position;
	}

	void FixedUpdate()
	{
		Size -= Input.GetAxis(Axis.MouseScroll.EditorName()) * 10;

		Camera.transform.localRotation = Quaternion.Euler(Angle, 0, 0);
		Camera.transform.localPosition = new Vector3(0, VerticalDist, -HorizontalDist);

		Camera.orthographicSize = Size;

		transform.position = Vector3.Lerp(transform.position, Target.transform.position, 5f * Time.fixedDeltaTime);
	}
}