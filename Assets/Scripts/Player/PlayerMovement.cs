using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float TurnSmoothing = 10f;
    public float MoveSpeed = 6f;
    public float Acceleration = 8f;
    public float AngleThreshold = 80f;
    public float TurningSpeed = 50f;
    public float InputThreshold = 0.1f;
    public float MoveSpeedMulti = 1.0f;
	public float SpeedThreshold = 1.0f;
	public float SpeedDampTime = 0.1f;
    public bool CanMove = true;
    public bool Moving = false;

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

    public void ApplyMovement(PlayerInput playerInput)
    {
        if (!CanMove)
        {
            Moving = false;
            _rigidBody.velocity = Vector3.zero;
            return;
        }

        if (playerInput.HorizontalAxis > InputThreshold
            || playerInput.HorizontalAxis < -InputThreshold
            || playerInput.VerticalAxis > InputThreshold
            || playerInput.VerticalAxis < -InputThreshold)
        {
            Moving = true;
            Rotate(playerInput.HorizontalAxis, playerInput.VerticalAxis);

            //_rigidBody.velocity = Vector3.Lerp(_rigidBody.velocity, transform.forward * MoveSpeed * MoveSpeedMulti, Time.deltaTime * Acceleration);
			_rigidBody.velocity = transform.forward * MoveSpeed * MoveSpeedMulti;
        }
        else if (Moving)
        {
            Moving = false;
            _rigidBody.velocity = Vector3.zero;
        }
    }

    void Rotate(float hor, float vert)
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

    bool IsSuddenMovement(Vector3 current, Vector3 next)
    {
        return Vector3.Angle(current, next) >= AngleThreshold;
    }
}
