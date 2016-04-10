using UnityEngine;
using System.Collections.Generic;

public class AbilityManagement : MonoBehaviour {

	private enum Modes{
		Vision = 0,
		Speed = 1,
		Shield = 2,
		Strength = 3
	}

	private Animator _animator;
	private List<Ability> _abilites;

	void Awake(){
		_animator = gameObject.GetComponent<Animator> ();
		LoadAbilities ();
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			print ("SPEED MODE");
			ActivateSpeedMode (true);
			ActivateStrengthMode (false);
		} else if(Input.GetKeyDown(KeyCode.Alpha3)) {
			print ("STRENGTH MODE");
			ActivateStrengthMode (true);
			ActivateSpeedMode (false);
		}
	}

	private void ActivateSpeedMode(bool active){
		float speedMulti = 1.0f;
		if (active) {
			speedMulti = 1.0f + 0.05f * _abilites [1].Level;
		}
		PlayerController.MoveSpeedMulti = speedMulti;
		_animator.SetFloat (AnimatorHashIDs.SpeedMultiFloat, speedMulti);
	}

	private void ActivateStrengthMode(bool active){
		float multi = 1.0f;
		if (active) {
			multi = 2f;
		} 
		PlayerAttack.DamageMulti = multi;
		PlayerAttack.StaminaMulti = multi;
	}

	private void LoadAbilities(){
		var numAbilities = 4;
		_abilites = new List<Ability> ();

		for (int i = 0; i < numAbilities; i++) {
			Ability abil = new Ability ();
			abil.Id = i;
			//abil.Level = AbilityReader.GetAbilityLevel (i);
			abil.Level = 10;
			_abilites.Add(abil);
		}
	}
}
