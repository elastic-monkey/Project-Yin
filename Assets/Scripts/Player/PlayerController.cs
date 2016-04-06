using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public static bool CanMove;

	public float TurnSmoothing = 10f;
	public float SpeedDampTime = 0.1f;
	public float MoveSpeed = 6f;
	public float AngleThreshold = 80f;
	public float SpeedThreshold = 1f;

	private Animator _animator;
	private Rigidbody _rigidBody;
	private HashIDs _hash;

	void Awake(){
		_animator = gameObject.GetComponent<Animator> ();
		_rigidBody = gameObject.GetComponent<Rigidbody> ();
		_hash = gameObject.GetComponent<HashIDs> ();
		CanMove = true;
	}

	void FixedUpdate(){
		if (CanMove) {
			float h = Input.GetAxis ("Horizontal");
			float v = Input.GetAxis ("Vertical");
			MovementManagement (h, v);
		}
	}

	void MovementManagement(float hor, float vert){
		if (hor > 0.1f || hor < -0.1f || vert > 0.1f || vert < -0.1f) {
			Rotating (hor, vert);
			_animator.SetFloat(_hash.SpeedFloat, SpeedThreshold, SpeedDampTime, Time.deltaTime);
			_rigidBody.velocity = gameObject.transform.forward.normalized * MoveSpeed;
		} else {
			_animator.SetFloat(_hash.SpeedFloat, 0);
			_rigidBody.velocity = gameObject.transform.forward.normalized * 0f;
		}
	}

	void Rotating(float hor, float vert){
		Vector3 targetDirection = new Vector3 (hor, 0f, vert);
		Quaternion targetRotation = Quaternion.LookRotation (targetDirection, Vector3.up);

		if (!SuddenMovement (gameObject.transform.forward, targetDirection)) {
			Quaternion newRotation = Quaternion.Lerp (_rigidBody.rotation, targetRotation, TurnSmoothing * Time.deltaTime);
			_rigidBody.MoveRotation (newRotation);
		} else {
			_rigidBody.MoveRotation (targetRotation);
		}
	}

	bool SuddenMovement(Vector3 current, Vector3 next){
		float angle = Vector3.Angle (current, next);
		return angle >= AngleThreshold;
	}
}
