using UnityEngine;
using System.Collections;

public class IsometricCamera : MonoBehaviour
{
	public GameObject Target;
    public Transform CameraHanger;
	public float Size = 10;
	public float ScrollSpeed = 30;
    public float Angle = 45;
    public float HorizontalDist = 5;
    public float VerticalDist = 5;

	Vector3 _cameraGoTo;
    private Camera _cam;

	void Start()
	{
        _cam = CameraHanger.GetComponentInChildren<Camera>();
        _cam.orthographic = true;
        CameraHanger.localRotation = Quaternion.Euler(Angle, 0, 0);
        CameraHanger.localPosition = new Vector3(0, VerticalDist, -HorizontalDist);

        transform.position = Target.transform.position;

        _cameraGoTo = Target.transform.position;
	}

	void FixedUpdate()
	{
        CameraHanger.localRotation = Quaternion.Euler(Angle, 0, 0);
        CameraHanger.localPosition = new Vector3(0, VerticalDist, -HorizontalDist);

        transform.position = Vector3.Lerp(transform.position, Target.transform.position, 5f * Time.fixedDeltaTime);
    }
}