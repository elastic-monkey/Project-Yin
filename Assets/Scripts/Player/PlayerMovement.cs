using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : Movement
{
	private Rigidbody _rigidBody;

	public float CurrentSpeed
	{
		get
		{
			if (Moving)
				return _rigidBody.velocity.magnitude;
			else
				return 0;
		}
	}

	void Awake()
	{
		_rigidBody = gameObject.GetComponent<Rigidbody>();
	}

	public void ApplyMovement()
	{
		if (!CanMove)
		{
			Moving = false;
			_rigidBody.velocity = Vector3.zero;
			return;
		}

        var h = PlayerInput.GetAxis(Axes.Horizontal);
        var v = PlayerInput.GetAxis(Axes.Vertical);

		if (Mathf.Abs(h) > MinInputValue || Mathf.Abs(v) > MinInputValue)
		{
			Moving = true;
			Rotate(h, v);
			
			_rigidBody.velocity = transform.forward * Speed * SpeedMulti;
		}
		else if (Moving)
		{
			Moving = false;
			_rigidBody.velocity = Vector3.zero;
		}
	}

	private void Rotate(float hor, float vert)
	{
		var targetDirection = new Vector3(hor, 0f, vert);
		var targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);

		if (!IsSuddenMovement(gameObject.transform.forward, targetDirection))
		{
			var newRotation = Quaternion.Lerp(_rigidBody.rotation, targetRotation, TurnSmoothing * Time.deltaTime);
			_rigidBody.MoveRotation(newRotation);
		}
		else
		{
			_rigidBody.MoveRotation(targetRotation);
		}
	}

	private bool IsSuddenMovement(Vector3 current, Vector3 next)
	{
		return Vector3.Angle(current, next) >= TurnAngleThreshold;
	}
}
