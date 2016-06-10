using UnityEngine;

public class InputTester : MonoBehaviour
{
	private float h, v;

	private void Awake()
	{
		foreach (var js in Input.GetJoystickNames())
		{
			Debug.Log("JS: " + js);
		}
	}

    private void Update()
    {
		h = PlayerInput.GetAxis(Axes.Horizontal);
		v = PlayerInput.GetAxis(Axes.Vertical);
    }

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(Vector3.zero, Vector3.right * h * 2);
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(Vector3.zero, Vector3.forward * v * -2);
	}

    private void DebugAxis(Axes axis)
    {
        var value = PlayerInput.IsButtonDown(axis);
        if (value)
            Debug.Log(axis.InputName() + " --> " + value);
    }
}
