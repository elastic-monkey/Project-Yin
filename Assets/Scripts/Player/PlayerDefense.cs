using UnityEngine;
using System.Collections;

public class PlayerDefense : MonoBehaviour {

	public static bool CanDefend;
    
	private Animator _animator;

	void Awake(){
		_animator = gameObject.GetComponent<Animator> ();
		CanDefend = true;
	}
		
	void Update(){
		if (CanDefend && Input.GetButton ("Fire3")) {
			_animator.SetBool (AnimatorHashIDs.DefendingBool, true);
			PlayerController.CanMove = false;
			PlayerAttack.CanAttack = false;
		} else if(CanDefend && Input.GetButtonUp("Fire3")){
			_animator.SetBool (AnimatorHashIDs.DefendingBool, false);
			PlayerController.CanMove = true;
			PlayerAttack.CanAttack = true;
		}
	}
}
