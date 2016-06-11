using UnityEngine;

public class InputTester : MonoBehaviour
{
	private bool h = false, v = false;

	private void Awake()
	{
		foreach (var js in Input.GetJoystickNames())
		{
			Debug.Log("JS: " + js);
		}
	}

	private void Update()
	{
		if (PlayerInput.IsButtonDown(Axes.HorizontalDpad))
			Debug.Log("H D-pad");

		if (PlayerInput.IsButtonDown(Axes.VerticalDpad))
			Debug.Log("V D-pad");
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(Vector3.zero, Vector3.right * (h ? 1 : 0) * 2);
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(Vector3.zero, Vector3.forward * (v ? 1 : 0) * -2);
	}

	private void DebugAxis(Axes axis)
	{
		var value = PlayerInput.IsButtonDown(axis);
		if (value)
			Debug.Log(axis.InputName() + " --> " + value);
	}
}
