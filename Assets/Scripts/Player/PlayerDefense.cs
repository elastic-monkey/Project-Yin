using UnityEngine;
using System.Collections;

public class PlayerDefense : MonoBehaviour {

	public static bool CanDefend;

	private HashIDs _hash;
	private Animator _animator;

	void Awake(){
		_animator = gameObject.GetComponent<Animator> ();
		_hash = gameObject.GetComponent<HashIDs> ();
		CanDefend = true;
	}
		
	void Update(){
		if (CanDefend && Input.GetButton ("Fire3")) {
			_animator.SetBool (_hash.DefendingBool, true);
			PlayerController.CanMove = false;
			PlayerAttack.CanAttack = false;
		} else if(CanDefend && Input.GetButtonUp("Fire3")){
			_animator.SetBool (_hash.DefendingBool, false);
			PlayerController.CanMove = true;
			PlayerAttack.CanAttack = true;
		}
	}
}
