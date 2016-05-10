using UnityEngine;
using System.Collections;

public abstract class Movement : MonoBehaviour
{
	public bool CanMove = true;
	public bool Moving = false;

	public float Speed = 6f;
	public float SpeedMulti = 1.0f;
	public float SpeedThreshold = 1.0f;
	public float SpeedDampTime = 0.1f;

	public float TurnSmoothing = 10f;
	public float TurnAngleThreshold = 80f;
	public float TurnSpeed = 50f;

	public float MinInputValue = 0.1f;
}
